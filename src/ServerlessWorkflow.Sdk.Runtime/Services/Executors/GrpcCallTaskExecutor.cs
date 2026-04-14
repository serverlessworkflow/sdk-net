using DynamicGrpc;
using Google.Protobuf.Reflection;
using Grpc.Net.Client;
using ServerlessWorkflow.Sdk.Models.Calls;

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute gRPC <see cref="CallTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="externalResourceReader">The service used to read external resources</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class GrpcCallTaskExecutor(IServiceProvider serviceProvider, ILogger<GrpcCallTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IExternalResourceReader externalResourceReader, ITaskExecutionContext<CallTaskDefinition> task)
    : TaskExecutor<CallTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    GrpcCallDefinition? grpc;
    DynamicGrpcClient? grpcClient;

    /// <inheritdoc/>
    protected override async Task InitializeCoreAsync(CancellationToken cancellationToken)
    {
        try
        {
            grpc = JsonSerializer.Deserialize(Task.Definition.With!, Sdk.Serialization.Json.JsonSerializationContext.Default.GrpcCallDefinition) ?? throw new InvalidOperationException("Failed to deserialize gRPC call definition from 'with'");
            var fileDescriptor = await GetProtoFileDescriptorAsync(grpc.Proto, cancellationToken).ConfigureAwait(false);
            var address = grpc.Service.Port.HasValue
                ? $"http://{grpc.Service.Host}:{grpc.Service.Port.Value}"
                : $"http://{grpc.Service.Host}";
            var channel = GrpcChannel.ForAddress(address);
            var callInvoker = channel.CreateCallInvoker();
            grpcClient = DynamicGrpcClient.FromDescriptorProtos(callInvoker: callInvoker, [fileDescriptor]);
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while initializing the gRPC call task '{task}': {ex}", Task.Instance.Reference, ex);
            await SetErrorAsync(Error.Validation(new Uri(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute), $"Invalid/missing call parameters for function 'grpc': {ex.Message}"), cancellationToken).ConfigureAwait(false);
        }
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (grpc == null || grpcClient == null) throw new InvalidOperationException("The executor must be initialized before execution");
        if (!grpcClient.TryFindMethod(grpc.Service.Name, grpc.Method, out _))
        {
            await SetErrorAsync(Error.Configuration(new Uri(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute), $"Failed to find a method with name '{grpc.Method}' in GRPC service with name '{grpc.Service.Name}'"), cancellationToken).ConfigureAwait(false);
            return;
        }
        var arguments = GetExpressionEvaluationArguments();
        var requestArgs = grpc.Arguments != null
            ? await Task.Workflow.Expressions.EvaluateAsync(grpc.Arguments, Task.Instance.Input, arguments, cancellationToken).ConfigureAwait(false)
            : null;
        var requestDictionary = requestArgs is JsonObject jsonObj
            ? jsonObj.Deserialize<Dictionary<string, object>>() ?? []
            : new Dictionary<string, object>();
        IDictionary<string, object> response;
        try
        {
            response = await grpcClient.AsyncUnaryCall(grpc.Service.Name, grpc.Method, requestDictionary).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to call the gRPC method '{method}' on '{service}' service at '{host}:{port}': {ex}", grpc.Method, grpc.Service.Name, grpc.Service.Host, grpc.Service.Port, ex.Message);
            await SetErrorAsync(Error.Communication(new Uri(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute), ErrorStatus.Communication, ex.Message), cancellationToken).ConfigureAwait(false);
            return;
        }
        var result = JsonSerializer.SerializeToNode(response);
        await SetResultAsync(result, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

    async Task<FileDescriptorProto> GetProtoFileDescriptorAsync(ExternalResourceDefinition resource, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(resource);
        var protoFile = new FileInfo(System.IO.Path.GetTempFileName());
        var protoDescriptorFileName = System.IO.Path.Combine(protoFile.Directory!.FullName, $"{System.IO.Path.GetFileNameWithoutExtension(protoFile.Name)}.desc");
        using var stream = await externalResourceReader.ReadAsync(resource, Task.Workflow.Definition, cancellationToken).ConfigureAwait(false);
        {
            using var protoFileStream = new FileStream(protoFile.FullName, FileMode.Create);
            {
                await stream.CopyToAsync(protoFileStream, cancellationToken).ConfigureAwait(false);
                await protoFileStream.FlushAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        var processInfo = new ProcessStartInfo("protoc", $"{protoFile.FullName} --proto_path={protoFile.Directory!.FullName} --descriptor_set_out={protoDescriptorFileName}")
        {
            RedirectStandardError = true,
            RedirectStandardOutput = true
        };
        using var process = Process.Start(processInfo)!;
        await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
        using var protoDescriptorFileStream = new FileStream(protoDescriptorFileName, FileMode.Open);
        var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(protoDescriptorFileStream);
        return fileDescriptorSet.File.First();
    }

}

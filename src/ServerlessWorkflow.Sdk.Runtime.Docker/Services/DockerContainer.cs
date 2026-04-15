// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents a Docker <see cref="IContainer"/>
/// </summary>
/// <param name="id">The container's ID</param>
/// <param name="docker">The service used to interact with the Docker API</param>
public sealed class DockerContainer(string id, IDockerClient docker)
    : IContainer
{

    Pipe? standardOutputPipe;
    Pipe? standardErrorPipe;
    MultiplexedStream? multiplexedStream;
    Task? copyTask;

    /// <inheritdoc/>
    public StreamReader? StandardOutput { get; private set; }

    /// <inheritdoc/>
    public StreamReader? StandardError { get; private set; }

    /// <inheritdoc/>
    public long? ExitCode { get; private set; }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await docker.Containers.StartContainerAsync(id, new() { }, cancellationToken).ConfigureAwait(false);
        multiplexedStream = await docker.Containers.GetContainerLogsAsync(id, false, new()
        {
            Follow = true,
            ShowStdout = true,
            ShowStderr = true,
            Timestamps = false
        }, cancellationToken).ConfigureAwait(false);
        standardOutputPipe = new();
        standardErrorPipe = new();
        copyTask = multiplexedStream.CopyOutputToAsync(Stream.Null, standardOutputPipe.Writer.AsStream(), standardErrorPipe.Writer.AsStream(), cancellationToken);
        StandardOutput = new(standardOutputPipe.Reader.AsStream());
        StandardError = new(standardErrorPipe.Reader.AsStream());
    }

    /// <inheritdoc/>
    public async Task WaitForExitAsync(CancellationToken cancellationToken = default)
    {
        var response = await docker.Containers.WaitContainerAsync(id, cancellationToken).ConfigureAwait(false);
        ExitCode = response.StatusCode;
        if (copyTask != null) await copyTask.ConfigureAwait(false);
        if (standardOutputPipe != null) await standardOutputPipe.Writer.CompleteAsync().ConfigureAwait(false);
        if (standardErrorPipe != null) await standardErrorPipe.Writer.CompleteAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken = default) => docker.Containers.StopContainerAsync(id, new() { }, cancellationToken);

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        multiplexedStream?.Dispose();
        if (standardOutputPipe != null)
        {
            await standardOutputPipe.Writer.CompleteAsync().ConfigureAwait(false);
            await standardOutputPipe.Reader.CompleteAsync().ConfigureAwait(false);
        }
        if (standardErrorPipe != null)
        {
            await standardErrorPipe.Writer.CompleteAsync().ConfigureAwait(false);
            await standardErrorPipe.Reader.CompleteAsync().ConfigureAwait(false);
        }
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        multiplexedStream?.Dispose();
        GC.SuppressFinalize(this);
    }

}

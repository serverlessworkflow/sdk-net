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

namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents a JSON converter for <see cref="TaskDefinition"/> objects, responsible for serializing and deserializing instances of <see cref="TaskDefinition"/> and its derived types to and from JSON format.
/// </summary>
public sealed class TaskDefinitionJsonConverter
    : JsonConverter<TaskDefinition>
{

    /// <inheritdoc/>
    public override TaskDefinition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var namingPolicy = options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase;
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;
        if (root.ValueKind != JsonValueKind.Object) throw new JsonException($"Expected a JSON object to deserialize a {nameof(TaskDefinition)}.");
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(CallTaskDefinition.Call)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.CallTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(EmitTaskDefinition.Emit)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.EmitTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(ForkTaskDefinition.Fork)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.ForkTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(ForTaskDefinition.For)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.ForTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(DoTaskDefinition.Do)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.DoTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(ListenTaskDefinition.Listen)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.ListenTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(RaiseTaskDefinition.Raise)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.RaiseTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(RunTaskDefinition.Run)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.RunTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(SetTaskDefinition.Set)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.SetTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(SwitchTaskDefinition.Switch)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.SwitchTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(TryTaskDefinition.Try)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.TryTaskDefinition);
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(WaitTaskDefinition.Wait)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.WaitTaskDefinition);
        return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.ExtensionTaskDefinition);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TaskDefinition value, JsonSerializerOptions options)
    {
        var json = value switch
        {
            CallTaskDefinition callTaskDefinition => JsonSerializer.Serialize(callTaskDefinition, JsonSerializationContext.Default.CallTaskDefinition),
            DoTaskDefinition doTaskDefinition => JsonSerializer.Serialize(doTaskDefinition, JsonSerializationContext.Default.DoTaskDefinition),
            EmitTaskDefinition emitTaskDefinition => JsonSerializer.Serialize(emitTaskDefinition, JsonSerializationContext.Default.EmitTaskDefinition),
            ExtensionTaskDefinition extensionTaskDefinition => JsonSerializer.Serialize(extensionTaskDefinition, JsonSerializationContext.Default.ExtensionTaskDefinition),
            ForkTaskDefinition forkTaskDefinition => JsonSerializer.Serialize(forkTaskDefinition, JsonSerializationContext.Default.ForkTaskDefinition),
            ForTaskDefinition forTaskDefinition => JsonSerializer.Serialize(forTaskDefinition, JsonSerializationContext.Default.ForTaskDefinition),
            ListenTaskDefinition listenTaskDefinition => JsonSerializer.Serialize(listenTaskDefinition, JsonSerializationContext.Default.ListenTaskDefinition),
            RaiseTaskDefinition raiseTaskDefinition => JsonSerializer.Serialize(raiseTaskDefinition, JsonSerializationContext.Default.RaiseTaskDefinition),
            RunTaskDefinition runTaskDefinition => JsonSerializer.Serialize(runTaskDefinition, JsonSerializationContext.Default.RunTaskDefinition),
            SetTaskDefinition setTaskDefinition => JsonSerializer.Serialize(setTaskDefinition, JsonSerializationContext.Default.SetTaskDefinition),
            SwitchTaskDefinition switchTaskDefinition => JsonSerializer.Serialize(switchTaskDefinition, JsonSerializationContext.Default.SwitchTaskDefinition),
            TryTaskDefinition tryTaskDefinition => JsonSerializer.Serialize(tryTaskDefinition, JsonSerializationContext.Default.TryTaskDefinition),
            WaitTaskDefinition waitTaskDefinition => JsonSerializer.Serialize(waitTaskDefinition, JsonSerializationContext.Default.WaitTaskDefinition),
            _ => throw new NotSupportedException($"The type {value.GetType().FullName} is not supported for JSON serialization.")
        };
        writer.WriteRawValue(json);
    }

}

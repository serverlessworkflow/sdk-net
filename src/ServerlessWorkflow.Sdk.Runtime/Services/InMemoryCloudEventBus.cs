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
/// Represents an in-memory implementation of the <see cref="ICloudEventBus"/> interface
/// </summary>
public sealed class InMemoryCloudEventBus 
    : ICloudEventBus, IDisposable
{

    readonly Subject<ICloudEvent> subject = new();

    /// <inheritdoc/>
    public Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default)
    {
        subject.OnNext(e);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<IObservable<ICloudEvent>> SubscribeAsync(CancellationToken cancellationToken = default) => Task.FromResult(subject.AsObservable());

    /// <inheritdoc/>
    public void Dispose() => subject.Dispose();

}

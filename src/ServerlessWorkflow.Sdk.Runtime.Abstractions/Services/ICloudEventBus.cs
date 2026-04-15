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
/// Defines the fundamentals of a service used to publish and subscribe to <see cref="ICloudEvent"/>s
/// </summary>
public interface ICloudEventBus
{

    /// <summary>
    /// Publishes the specified <see cref="ICloudEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="ICloudEvent"/> to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribes to streamed <see cref="ICloudEvent"/>s
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IObservable{T}"/> used to observe streamed <see cref="ICloudEvent"/>s</returns>
    Task<IObservable<ICloudEvent>> SubscribeAsync(CancellationToken cancellationToken = default);

}
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

using System.Reactive.Linq;
using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class InMemoryCloudEventBusTests
{

    [Fact]
    public async Task PublishAsync_Should_Emit_Event_To_Subscribers()
    {
        //arrange
        using var bus = new InMemoryCloudEventBus();
        var eventSource = new Uri("https://example.com");
        var eventType = "com.test";
        var cloudEvent = new CloudEvent { Source = eventSource, Type = eventType };
        var observable = await bus.SubscribeAsync(TestContext.Current.CancellationToken);
        ICloudEvent? received = null;
        var tcs = new TaskCompletionSource();
        observable.Subscribe(e => { received = e; tcs.TrySetResult(); });

        //act
        await bus.PublishAsync(cloudEvent, TestContext.Current.CancellationToken);
        await tcs.Task.WaitAsync(TimeSpan.FromSeconds(2), TestContext.Current.CancellationToken);

        //assert
        received.Should().NotBeNull();
        received!.Type.Should().Be(eventType);
        received!.Source.Should().Be(eventSource);
    }

    [Fact]
    public async Task SubscribeAsync_Should_Return_Observable()
    {
        //arrange
        using var bus = new InMemoryCloudEventBus();

        //act
        var observable = await bus.SubscribeAsync(TestContext.Current.CancellationToken);

        //assert
        observable.Should().NotBeNull();
    }

    [Fact]
    public async Task PublishAsync_Should_Deliver_To_Multiple_Subscribers()
    {
        //arrange
        using var bus = new InMemoryCloudEventBus();
        var eventType = "com.multi";
        var cloudEvent = new CloudEvent { Source = new Uri("https://example.com"), Type = eventType };
        var obs1 = await bus.SubscribeAsync(TestContext.Current.CancellationToken);
        var obs2 = await bus.SubscribeAsync(TestContext.Current.CancellationToken);
        var count1 = 0;
        var count2 = 0;
        var tcs1 = new TaskCompletionSource();
        var tcs2 = new TaskCompletionSource();
        obs1.Subscribe(_ => { count1++; tcs1.TrySetResult(); });
        obs2.Subscribe(_ => { count2++; tcs2.TrySetResult(); });

        //act
        await bus.PublishAsync(cloudEvent, TestContext.Current.CancellationToken);
        await Task.WhenAll(tcs1.Task, tcs2.Task).WaitAsync(TimeSpan.FromSeconds(2), TestContext.Current.CancellationToken);

        //assert
        count1.Should().Be(1);
        count2.Should().Be(1);
    }

}

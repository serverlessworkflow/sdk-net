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

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IObservable{T}"/> that support async callbacks
/// </summary>
public static class AsyncObservableExtensions
{

    /// <summary>
    /// Subscribes to the specified <see cref="IObservable{T}"/> with async callbacks
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence</typeparam>
    /// <param name="source">The source sequence</param>
    /// <param name="onNextAsync">Async action to invoke for each element in the observable sequence</param>
    /// <param name="onErrorAsync">Async action to invoke upon exceptional termination of the observable sequence</param>
    /// <param name="onCompletedAsync">Async action to invoke upon graceful termination of the observable sequence</param>
    /// <returns>An <see cref="IDisposable"/> used to unsubscribe from the observable sequence</returns>
    public static IDisposable SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNextAsync, Func<Exception, Task>? onErrorAsync = null, Func<Task>? onCompletedAsync = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(onNextAsync);
        onErrorAsync ??= _ => Task.CompletedTask;
        onCompletedAsync ??= () => Task.CompletedTask;
        return source
            .Select(item => Observable.FromAsync(() => onNextAsync(item)))
            .Concat()
            .Subscribe(
                onNext: _ => { },
                onError: ex =>
                {
                    try { onErrorAsync(ex).ConfigureAwait(false).GetAwaiter().GetResult(); }
                    catch (Exception handlerEx) { Debug.Fail($"Unhandled exception in SubscribeAsync onError handler: {handlerEx}"); }
                },
                onCompleted: () =>
                {
                    try { onCompletedAsync().ConfigureAwait(false).GetAwaiter().GetResult(); }
                    catch (Exception handlerEx) { Debug.Fail($"Unhandled exception in SubscribeAsync onCompleted handler: {handlerEx}"); }
                }
            );
    }

    /// <summary>
    /// Subscribes to the specified <see cref="IObservable{T}"/> with async callbacks and a <see cref="CancellationToken"/>
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence</typeparam>
    /// <param name="source">The source sequence</param>
    /// <param name="onNextAsync">Async action to invoke for each element in the observable sequence</param>
    /// <param name="onErrorAsync">Async action to invoke upon exceptional termination of the observable sequence</param>
    /// <param name="onCompletedAsync">Async action to invoke upon graceful termination of the observable sequence</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    public static void SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNextAsync, Func<Exception, Task>? onErrorAsync, Func<Task>? onCompletedAsync, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(onNextAsync);
        onErrorAsync ??= _ => Task.CompletedTask;
        onCompletedAsync ??= () => Task.CompletedTask;
        source
            .Select(item => Observable.FromAsync(() => onNextAsync(item)))
            .Concat()
            .Subscribe(
                onNext: _ => { },
                onError: ex =>
                {
                    try { onErrorAsync(ex).ConfigureAwait(false).GetAwaiter().GetResult(); }
                    catch (Exception handlerEx) { Debug.Fail($"Unhandled exception in SubscribeAsync onError handler: {handlerEx}"); }
                },
                onCompleted: () =>
                {
                    try { onCompletedAsync().ConfigureAwait(false).GetAwaiter().GetResult(); }
                    catch (Exception handlerEx) { Debug.Fail($"Unhandled exception in SubscribeAsync onCompleted handler: {handlerEx}"); }
                },
                token: cancellationToken
            );
    }

    /// <summary>
    /// Subscribes to the specified <see cref="IObservable{T}"/> with an async onNext callback and a <see cref="CancellationToken"/>
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence</typeparam>
    /// <param name="source">The source sequence</param>
    /// <param name="onNextAsync">Async action to invoke for each element in the observable sequence</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    public static void SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNextAsync, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(onNextAsync);
        source
            .Select(item => Observable.FromAsync(() => onNextAsync(item)))
            .Concat()
            .Subscribe(cancellationToken);
    }

}

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
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="DateTimeOffset"/>s
/// </summary>
public static class DateTimeOffsetExtensions
{

    /// <summary>
    /// Gets an object describing the specified <see cref="DateTimeOffset"/>
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTimeOffset"/> to get the descriptor of</param>
    /// <returns>A new <see cref="DateTimeDescriptor"/> describing the specified <see cref="DateTimeOffset"/></returns>
    public static DateTimeDescriptor GetDescriptor(this DateTimeOffset dateTime) => new()
    {
        Iso8601 = dateTime.ToString("o"),
        Epoch = new()
        {
            Milliseconds = (ulong)dateTime.ToUnixTimeSeconds() * 1000
        }
    };

}
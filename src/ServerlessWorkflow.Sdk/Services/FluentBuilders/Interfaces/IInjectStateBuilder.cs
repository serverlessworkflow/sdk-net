// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="InjectStateDefinition"/>s
/// </summary>
public interface IInjectStateBuilder
    : IStateBuilder<InjectStateDefinition>
{

    /// <summary>
    /// Injects the specified data into the workflow
    /// </summary>
    /// <param name="data">The data to inject</param>
    /// <returns>A new <see cref="IInjectStateBuilder"/></returns>
    IInjectStateBuilder Data(object data);

}

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
/// Defines extensions for <see cref="IWorkflowInstance"/>s
/// </summary>
public static class IWorkflowStateExtensions
{

    /// <summary>
    /// Gets the qualified name of the <see cref="IWorkflowInstance"/>'s definition, in the format {namespace}.{name}:{version}
    /// </summary>
    /// <param name="state">The <see cref="IWorkflowInstance"/> to get the qualified name of</param>
    /// <returns></returns>
    public static string GetQualifiedName(this IWorkflowInstance state) => $"{state.Definition}-{state.Id}";

}
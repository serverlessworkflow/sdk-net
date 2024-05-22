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

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes constants representing different transition options for a workflow
/// </summary>
public static class FlowDirective
{

    /// <summary>
    /// Indicates that the workflow should continue its execution, possibly exiting the current branch and/or completing execution if transitionning from the last task
    /// </summary>
    public const string Continue = "continue";
    /// <summary>
    /// Indicates that the workflow should end its execution, possibly ignoring other defined tasks in the flow
    /// </summary>
    public const string End = "end";
    /// <summary>
    /// Indicates that the workflow should exit the current branch, iteration or loop, possibly completing execution if transitionning from the main branch
    /// </summary>
    public const string Exit = "exit";

}

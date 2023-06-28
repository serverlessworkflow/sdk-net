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

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates the ways a subflow should behave when its parent completes before it 
/// </summary>
public static class SubflowParentCompletionBehavior
{

    /// <summary>
    /// Indicates that the subflow is terminated upon completion of its parent
    /// </summary>
    public const string Terminate = "terminate";
    /// <summary>
    /// Indicates that the subflow should continue to run even if its parent has completed
    /// </summary>
    public const string Continue = "continue";

}

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
/// Exposes statics and constants about ServerlessWorkflow runtime expressions
/// </summary>
public static class RuntimeExpressions
{

    /// <summary>
    /// Exposes the runtime expression language supported by default
    /// </summary>
    public static class Languages
    {

        /// <summary>
        /// Gets the 'jq' runtime expression language
        /// </summary>
        public const string JQ = "jq";
        /// <summary>
        /// Gets the 'js' runtime expression language
        /// </summary>
        public const string JavaScript = "js";

    }

    /// <summary>
    /// Exposes default ServerlessWorkflow runtime expression arguments
    /// </summary>
    public static class Arguments
    {

        /// <summary>
        /// Gets the name of the 'workflow' argument, used to access the current workflow resource
        /// </summary>
        public const string Workflow = "workflow";
        /// <summary>
        /// Gets the name of the 'context' argument, used to access the current context data
        /// </summary>
        public const string Context = "context";
        /// <summary>
        /// Gets the name of the 'item' argument, used to access the current item of the collection being enumerated
        /// </summary>
        public const string Each = "item";
        /// <summary>
        /// Gets the name of the 'index' argument, used to access the index of the current item of the collection being enumerated
        /// </summary>
        public const string Index = "index";
        /// <summary>
        /// Gets the name of the 'output' argument, used to access the task's output
        /// </summary>
        public const string Output = "output";
        /// <summary>
        /// Gets the name of the 'secret' argument
        /// </summary>
        public const string Secret = "secret";
        /// <summary>
        /// Gets the name of the 'task' argument
        /// </summary>
        public const string Task = "task";
        /// <summary>
        /// Gets the name of the 'input' argument
        /// </summary>
        public const string Input = "input";
        /// <summary>
        /// Gets the name of the 'error' argument, used to access the current error, if any
        /// </summary>
        public const string Error = "error";

    }

}
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

using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.IO;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Defines extensions for <see cref="IWorkflowReader"/>s
    /// </summary>
    public static class IWorkflowReaderExtensions
    {

        /// <summary>
        /// Reads a <see cref="WorkflowDefinition"/> from the specified <see cref="Stream"/>
        /// </summary>
        /// <param name="reader">The extended <see cref="IWorkflowReader"/></param>
        /// <param name="stream">The <see cref="Stream"/> to read the <see cref="WorkflowDefinition"/> from</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        public static async Task<WorkflowDefinition> ReadAsync(this IWorkflowReader reader, Stream stream, CancellationToken cancellationToken = default)
        {
            return await reader.ReadAsync(stream, new(), cancellationToken);
        }

        /// <summary>
        /// Parses the specified input into a new <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="reader">The extended <see cref="IWorkflowReader"/></param>
        /// <param name="input">The input to parse</param>
        /// <param name="options"><see cref="WorkflowReaderOptions"/> to use</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        public static async Task<WorkflowDefinition> ParseAsync(this IWorkflowReader reader, string input, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            return await reader.ReadAsync(stream, options, cancellationToken);
        }

        /// <summary>
        /// Parses the specified input into a new <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="reader">The extended <see cref="IWorkflowReader"/></param>
        /// <param name="input">The input to parse</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        public static async Task<WorkflowDefinition> ParseAsync(this IWorkflowReader reader, string input,CancellationToken cancellationToken = default)
        {
            return await reader.ParseAsync(input, new(), cancellationToken);
        }

    }

}

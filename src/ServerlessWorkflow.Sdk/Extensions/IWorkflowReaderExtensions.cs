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
        /// Parses the specified input into a new <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="reader">The extended <see cref="IWorkflowReader"/></param>
        /// <param name="input">The input to parse</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        public static async Task<WorkflowDefinition> ParseAsync(this IWorkflowReader reader, string input, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            return await reader.ReadAsync(stream, cancellationToken);
        }

    }

}

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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a reference to a sub workflow definition
/// </summary>
[DataContract]
public class SubflowReference
    : IExtensible
{

    /// <summary>
    /// Initializes a new <see cref="SubflowReference"/>
    /// </summary>
    public SubflowReference() { }

    /// <summary>
    /// Initializes a new <see cref="SubflowReference"/>
    /// </summary>
    /// <param name="workflowId">The id of the workflow definition to run</param>
    /// <param name="version">The version of the workflow definition to run. Defaults to 'latest'</param>
    /// <param name="invocationMode">The subflow's <see cref="Sdk.InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/>.</param>
    public SubflowReference(string workflowId, string? version = null, string invocationMode = Sdk.InvocationMode.Synchronous)
        : this()
    {
        if (string.IsNullOrWhiteSpace(workflowId)) throw new ArgumentNullException(nameof(workflowId));
        this.WorkflowId = workflowId;
        this.Version = version;
        this.InvocationMode = invocationMode;
    }

    /// <summary>
    /// Initializes a new <see cref="SubflowReference"/>
    /// </summary>
    /// <param name="workflowId">The id of the workflow definition to run</param>
    /// <param name="invocationMode">The subflow's <see cref="Sdk.InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/>.</param>
    public SubflowReference(string workflowId, string invocationMode = Sdk.InvocationMode.Synchronous) : this(workflowId, null, invocationMode) { }

    /// <summary>
    /// Gets/sets the id of the workflow definition to run
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "workflowId", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("workflowId"), YamlMember(Alias = "workflowId", Order = 1)]
    public virtual string WorkflowId { get; set; } = null!;

    /// <summary>
    /// Gets/sets the version of the workflow definition to run. Defaults to 'latest'
    /// </summary>
    [DataMember(Order = 2, Name = "version"), JsonPropertyOrder(2), JsonPropertyName("version"), YamlMember(Alias = "version", Order = 2)]
    public virtual string? Version { get; set; } = "latest";

    /// <summary>
    /// Gets/sets the subflow's <see cref="Sdk.InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/>.
    /// </summary>
    /// <remarks>
    /// Default value of this property is sync, meaning that workflow execution should wait until the subflow completes.<para></para>
    /// If set to async, workflow execution should just invoke the subflow and not wait for its results. Note that in this case the action does not produce any results, and the associated actions actionDataFilter as well as its retry definition, if defined, should be ignored.<para></para>
    /// Subflows that are invoked async do not propagate their errors to the associated action definition and the workflow state, meaning that any errors that happen during their execution cannot be handled in the workflow states onErrors definition.<para></para>
    /// Note that errors raised during subflows that are invoked async should not fail workflow execution.
    /// </remarks>
    [DataMember(Order = 3, Name = "invoke"), JsonPropertyOrder(3), JsonPropertyName("invoke"), YamlMember(Alias = "invoke", Order = 3)]
    public virtual string InvocationMode { get; set; } = Sdk.InvocationMode.Synchronous;

    /// <summary>
    /// Gets/sets a value that defines how subflow execution that is invoked async should behave if the parent workflow completes execution before the subflow completes its own execution
    /// </summary>
    [DataMember(Order = 4, Name = "onParentComplete"), JsonPropertyOrder(4), JsonPropertyName("onParentComplete"), YamlMember(Alias = "onParentComplete", Order = 4)]
    public virtual bool OnParentComplete { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="FunctionReference"/>'s extension properties
    /// </summary>
    [DataMember(Order = 5, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => string.IsNullOrWhiteSpace(this.Version) ? $"{this.WorkflowId}:latest" : $"{this.WorkflowId}:{this.Version}";

    /// <summary>
    /// Parses the specified input into a new <see cref="SubflowReference"/>
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <returns>A new <see cref="SubflowReference"/></returns>
    public static SubflowReference Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(nameof(input));
        var components = input.Split(":", StringSplitOptions.RemoveEmptyEntries);
        var workflowId = components.First();
        var version = null as string;
        if (components.Length > 1)
            version = components.Last();
        return new SubflowReference(workflowId, version!);
    }

}
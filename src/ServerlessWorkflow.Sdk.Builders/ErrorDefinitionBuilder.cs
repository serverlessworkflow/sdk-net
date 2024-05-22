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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IErrorDefinitionBuilder"/> interface
/// </summary>
public class ErrorDefinitionBuilder
    : IErrorDefinitionBuilder
{

    /// <summary>
    /// Gets the type of the error to build
    /// </summary>
    protected string? Type { get; set; }

    /// <summary>
    /// Gets the status of the error to build
    /// </summary>
    protected string? Status { get; set; }

    /// <summary>
    /// Gets the title of the error to build
    /// </summary>
    protected string? Title { get; set; }

    /// <summary>
    /// Gets the detail of the error to build
    /// </summary>
    protected string? Detail { get; set; }

    /// <summary>
    /// Gets the instance of the error to build
    /// </summary>
    protected string? Instance { get; set; }

    /// <inheritdoc/>
    public virtual IErrorDefinitionBuilder WithType(string type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(type);
        this.Type = type;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorDefinitionBuilder WithStatus(string status)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(status);
        this.Status = status;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorDefinitionBuilder WithTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        this.Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorDefinitionBuilder WithDetail(string detail)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(detail);
        this.Detail = detail;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorDefinitionBuilder WithInstance(string instance)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(instance);
        this.Instance = instance;
        return this;
    }

    /// <inheritdoc/>
    public virtual ErrorDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Type)) throw new NullReferenceException("The error type must be set");
        if (string.IsNullOrWhiteSpace(this.Title)) throw new NullReferenceException("The error title must be set");
        if (string.IsNullOrWhiteSpace(this.Status)) throw new NullReferenceException("The error status must be set");
        return new()
        {
            Type = this.Type,
            Status = this.Status,
            Title = this.Title,
            Detail = this.Detail,
            Instance = this.Instance
        };
    }

}

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
public sealed class ErrorDefinitionBuilder
    : IErrorDefinitionBuilder
{

    string? type;
    string? status;
    string? title;
    string? detail;
    string? instance;

    /// <inheritdoc/>
    public IErrorDefinitionBuilder WithType(string type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(type);
        this.type = type;
        return this;
    }

    /// <inheritdoc/>
    public IErrorDefinitionBuilder WithStatus(string status)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(status);
        this.status = status;
        return this;
    }

    /// <inheritdoc/>
    public IErrorDefinitionBuilder WithTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        this.title = title;
        return this;
    }

    /// <inheritdoc/>
    public IErrorDefinitionBuilder WithDetail(string detail)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(detail);
        this.detail = detail;
        return this;
    }

    /// <inheritdoc/>
    public IErrorDefinitionBuilder WithInstance(string instance)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(instance);
        this.instance = instance;
        return this;
    }

    /// <inheritdoc/>
    public ErrorDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(type)) throw new NullReferenceException("The error type must be set");
        if (string.IsNullOrWhiteSpace(title)) throw new NullReferenceException("The error title must be set");
        if (string.IsNullOrWhiteSpace(status)) throw new NullReferenceException("The error status must be set");
        return new()
        {
            Type = type,
            Status = status,
            Title = title,
            Detail = detail,
            Instance = instance
        };
    }

}

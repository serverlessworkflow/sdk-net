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
/// Represents the default implementation of the <see cref="IFunctionBuilder"/> interface
/// </summary>
public class FunctionBuilder
    : MetadataContainerBuilder<IFunctionBuilder>, IFunctionBuilder
{

    /// <summary>
    /// Initializes a new <see cref="FunctionBuilder"/>
    /// </summary>
    /// <param name="workflow">The service used to build the workflow definition the <see cref="FunctionDefinition"/> to build belongs to</param>
    public FunctionBuilder(IWorkflowBuilder workflow)
    {
        this.Workflow = workflow;
    }

    /// <summary>
    /// Gets the service used to build the workflow definition the <see cref="FunctionDefinition"/> to build belongs to
    /// </summary>
    protected IWorkflowBuilder Workflow { get; }

    /// <summary>
    /// Gets the <see cref="FunctionDefinition"/> to configure
    /// </summary>
    protected FunctionDefinition Function { get; } = new FunctionDefinition();

    /// <inheritdoc/>
    public override DynamicMapping? Metadata
    {
        get
        {
            return this.Function.Metadata;
        }
        protected set
        {
            this.Function.Metadata = value;
        }
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Function.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder WithExtensionProperty(string name, object value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Function.ExtensionData ??= new Dictionary<string, object>();
        this.Function.ExtensionData[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder WithExtensionProperties(IDictionary<string, object> properties)
    {
        this.Function.ExtensionData = properties ?? throw new ArgumentNullException(nameof(properties));
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder OfType(string type)
    {
        this.Function.Type = type;
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder ForOperation(string operation)
    {
        if (string.IsNullOrWhiteSpace(operation)) throw new ArgumentNullException(nameof(operation));
        this.Function.Operation = operation;
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder ForOperation(Uri operation)
    {
        if (operation == null) throw new ArgumentNullException(nameof(operation));
        this.Function.Operation = operation.ToString();
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder UseAuthentication(string authentication)
    {
        if (string.IsNullOrWhiteSpace(authentication)) throw new ArgumentNullException(nameof(authentication));
        this.Function.AuthRef = authentication;
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder UseAuthentication(AuthenticationDefinition authenticationDefinition)
    {
        if (authenticationDefinition == null) throw new ArgumentNullException(nameof(authenticationDefinition));
        this.Function.AuthRef = authenticationDefinition.Name;
        this.Workflow.AddAuthentication(authenticationDefinition);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder UseBasicAuthentication(string name, Action<IBasicAuthenticationBuilder> configurationAction)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (configurationAction == null) throw new ArgumentNullException(nameof(configurationAction));
        this.Function.AuthRef = name;
        this.Workflow.AddBasicAuthentication(name, configurationAction);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder UseBearerAuthentication(string name, Action<IBearerAuthenticationBuilder> configurationAction)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (configurationAction == null) throw new ArgumentNullException(nameof(configurationAction));
        this.Function.AuthRef = name;
        this.Workflow.AddBearerAuthentication(name, configurationAction);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionBuilder UseOAuth2Authentication(string name, Action<IOAuth2AuthenticationBuilder> configurationAction)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (configurationAction == null) throw new ArgumentNullException(nameof(configurationAction));
        this.Function.AuthRef = name;
        this.Workflow.AddOAuth2Authentication(name, configurationAction);
        return this;
    }

    /// <inheritdoc/>
    public virtual FunctionDefinition Build() => this.Function;

}

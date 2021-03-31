/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IFunctionBuilder"/> interface
    /// </summary>
    public class FunctionBuilder
        : MetadataContainerBuilder<IFunctionBuilder>, IFunctionBuilder
    {

        /// <summary>
        /// Gets the <see cref="FunctionDefinition"/> to configure
        /// </summary>
        protected FunctionDefinition Function { get; } = new FunctionDefinition();

        /// <inheritdoc/>
        public override JObject Metadata
        {
            get
            {
                return this.Function.Metadata;
            }
        }

        /// <inheritdoc/>
        public virtual IFunctionBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Function.Name = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IFunctionBuilder SetOperationExpression(string operation)
        {
            if (string.IsNullOrWhiteSpace(operation))
                throw new ArgumentNullException(nameof(operation));
            this.Function.Operation = operation;
            return this;
        }

        /// <inheritdoc/>
        public virtual IFunctionBuilder SetOperationUri(Uri operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));
            this.Function.Operation = operation.ToString();
            return this;
        }

        /// <inheritdoc/>
        public virtual FunctionDefinition Build()
        {
            return this.Function;
        }

    }
}

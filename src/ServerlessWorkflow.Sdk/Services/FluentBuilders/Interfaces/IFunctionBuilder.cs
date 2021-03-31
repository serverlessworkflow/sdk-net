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
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="FunctionDefinition"/>s
    /// </summary>
    public interface IFunctionBuilder
        : IMetadataContainerBuilder<IFunctionBuilder>
    {

        /// <summary>
        /// Sets the name of the <see cref="FunctionDefinition"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="FunctionDefinition"/> to build</param>
        /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
        IFunctionBuilder WithName(string name);

        /// <summary>
        /// Sets the <see cref="FunctionDefinition"/>'s operation expression. Sets the <see cref="FunctionDefinition"/>'s <see cref="FunctionDefinition.Type"/> to <see cref="FunctionType.Expression"/>
        /// </summary>
        /// <param name="operation">The <see cref="FunctionDefinition"/>'s operation expression</param>
        /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
        IFunctionBuilder SetOperationExpression(string operation);

        /// <summary>
        /// Sets the <see cref="FunctionDefinition"/>'s operation <see cref="Uri"/>. Sets the <see cref="FunctionDefinition"/>'s <see cref="FunctionDefinition.Type"/> to <see cref="FunctionType.Rest"/>
        /// </summary>
        /// <param name="operation">The <see cref="FunctionDefinition"/>'s operation <see cref="Uri"/></param>
        /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
        IFunctionBuilder SetOperationUri(Uri operation);

        /// <summary>
        /// Builds the <see cref="FunctionDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="FunctionDefinition"/></returns>
        FunctionDefinition Build();

    }

}

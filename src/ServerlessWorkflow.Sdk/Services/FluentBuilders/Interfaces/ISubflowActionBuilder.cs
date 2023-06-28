﻿// Copyright © 2023-Present The Serverless Workflow Specification Authors
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
/// Defines the fundamentals of a service used to build <see cref="ActionDefinition"/>s of type <see cref="ActionType.Subflow"/>
/// </summary>
public interface ISubflowActionBuilder
    : IActionBuilder, IExtensibleBuilder<ISubflowActionBuilder>
{

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the latest version of the specified workflow definition
    /// </summary>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder LatestVersion();

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the workflow definition with the specified version
    /// </summary>
    /// <param name="version">The version of the workflow definition to run</param>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder Version(string version);

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the referenced workflow definition synchronously, which is the default.
    /// </summary>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder Synchronously();

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the referenced workflow definition asynchronously
    /// </summary>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder Asynchronously();

}

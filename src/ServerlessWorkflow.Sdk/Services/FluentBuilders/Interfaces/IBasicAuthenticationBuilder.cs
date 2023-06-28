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
/// Defines the fundamentals of a service used to build a authentication definition with scheme <see cref="AuthenticationScheme.Basic"/>
/// </summary>
public interface IBasicAuthenticationBuilder
    : IAuthenticationDefinitionBuilder
{

    /// <summary>
    /// Configures the authentication definition to use the specified username to authenticate
    /// </summary>
    /// <param name="username">The username to use</param>
    /// <returns>The configured <see cref="IBasicAuthenticationBuilder"/></returns>
    IBasicAuthenticationBuilder WithUserName(string username);

    /// <summary>
    /// Configures the authentication definition to use the specified password to authenticate
    /// </summary>
    /// <param name="password">The password to use</param>
    /// <returns>The configured <see cref="IBasicAuthenticationBuilder"/></returns>
    IBasicAuthenticationBuilder WithPassword(string password);

}

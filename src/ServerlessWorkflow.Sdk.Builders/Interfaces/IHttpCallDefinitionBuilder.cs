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

using ServerlessWorkflow.Sdk.Models.Calls;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="HttpCallDefinition"/>s
/// </summary>
public interface IHttpCallDefinitionBuilder
{

    /// <summary>
    /// Sets the HTTP method of the request to perform
    /// </summary>
    /// <param name="method">The HTTP method of the request to perform</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithMethod(string method);

    /// <summary>
    /// Sets the <see cref="Uri"/> to request
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to request</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithUri(Uri uri);

    /// <summary>
    /// Sets the endpoint to request
    /// </summary>
    /// <param name="endpoint">An <see cref="Action{T}"/> used to setup the endpoint to request</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithEndpoint(EndpointDefinition endpoint);

    /// <summary>
    /// Sets the endpoint to request
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the endpoint to request</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithEndpoint(Action<IEndpointDefinitionBuilder> setup);

    /// <summary>
    /// Sets the value of the header with the specified name
    /// </summary>
    /// <param name="name">The name of the header to set</param>
    /// <param name="value">The value of the header to set</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithHeader(string name, string value);

    /// <summary>
    /// Sets the headers of the HTTP request to perform
    /// </summary>
    /// <param name="headers">The headers of the HTTP request to perform</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithHeaders(IDictionary<string, string> headers);

    /// <summary>
    /// Sets the value of the cookie with the specified name
    /// </summary>
    /// <param name="name">The name of the cookie to set</param>
    /// <param name="value">The value of the cookie to set</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithCookie(string name, string value);

    /// <summary>
    /// Sets the cookies of the HTTP request to perform
    /// </summary>
    /// <param name="cookies">The cookies of the HTTP request to perform</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithCookies(IDictionary<string, string> cookies);

    /// <summary>
    /// Sets the HTTP request body
    /// </summary>
    /// <param name="body">The request body</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithBody(object body);

    /// <summary>
    /// Uses the specified output format
    /// </summary>
    /// <param name="format">The format of the call's output</param>
    /// <returns>The configured <see cref="IHttpCallDefinitionBuilder"/></returns>
    IHttpCallDefinitionBuilder WithOutputFormat(string format);

    /// <summary>
    /// Builds the configured <see cref="HttpCallDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="HttpCallDefinition"/></returns>
    HttpCallDefinition Build();

}
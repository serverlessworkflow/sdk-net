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

using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents an object used to configure a 'Bearer' authentication scheme
    /// </summary>
    public class BearerAuthenticationProperties
        : AuthenticationProperties
    {

        /// <summary>
        /// Gets/sets the token used to authenticate
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "token")]
        [System.Text.Json.Serialization.JsonPropertyName("token")]
        [YamlMember(Alias = "token")]
        public virtual string Token { get; set; }

    }

}

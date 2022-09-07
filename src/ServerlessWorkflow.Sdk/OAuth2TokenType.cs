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

namespace ServerlessWorkflow.Sdk
{
    /// <summary>
    /// Enumerates all supported token types
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.StringEnumConverterFactory))]
    public enum OAuth2TokenType
    {
        /// <summary>
        /// Indicates an access token
        /// </summary>
        [EnumMember(Value = "urn:ietf:params:oauth:token-type:access_token")]
        AccessToken = 1,
        /// <summary>
        /// Indicates an identity token
        /// </summary>
        [EnumMember(Value = "urn:ietf:params:oauth:token-type:id_token")]
        IdentityToken = 2
    }

}

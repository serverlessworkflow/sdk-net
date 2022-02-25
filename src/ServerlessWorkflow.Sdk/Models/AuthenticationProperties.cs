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

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to configure an authentication mechanism
    /// </summary>
    [DataContract]
    [ProtoContract]
    [ProtoInclude(100, typeof(BasicAuthenticationProperties))]
    [ProtoInclude(200, typeof(BearerAuthenticationProperties))]
    [ProtoInclude(300, typeof(OAuth2AuthenticationProperties))]
    [ProtoInclude(400, typeof(SecretBasedAuthenticationProperties))]
    public abstract class AuthenticationProperties
    {

        /// <summary>
        /// Gets/sets the <see cref="AuthenticationProperties"/>'s metadata
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual DynamicObject? Metadata { get; set; }

    }

}

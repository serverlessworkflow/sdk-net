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
    /// Represents the options used to configure an OData command
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class ODataCommandOptions
    {

        /// <summary>
        /// Gets the unique identifier of the single entry to query
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets the options used to configure the OData query
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual ODataQueryOptions QueryOptions { get; set; }

    }

}

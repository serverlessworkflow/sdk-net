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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the options used to configure an OData operation call
    /// </summary>
    public class ODataOperationOptions
    {

        /// <summary>
        /// Gets the unique identifier of the single entry to query
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets the options used to configure the OData query
        /// </summary>
        public virtual ODataQueryOptions QueryOptions { get; set; }

    }

}

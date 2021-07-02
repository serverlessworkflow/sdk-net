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
    /// Represents the options used to configure an OData query
    /// </summary>
    public class ODataQueryOptions
    {

        /// <summary>
        /// Gets the $filter system query option, which allows clients to filter the set of resources that are addressed by a request URL. $filter specifies conditions that MUST be met by a resource for it to be returned in the set of matching resources
        /// </summary>
        public virtual string Filter { get; set; }

        /// <summary>
        /// Gets the $expand system query option, which allows clients to request related resources when a resource that satifies a particular request is retrieved
        /// </summary>
        public virtual string Expand { get; set; }

        /// <summary>
        /// Gets the $select system query option, which allows clients to requests a limited set of information for each entity or complex type identified by the ResourcePath and other System Query Options like $filter, $top, $skip etc. The $select query option is often used in conjunction with the $expand query option, to first increase the scope of the resource graph returned ($expand) and then selectively prune that resource graph ($select)
        /// </summary>
        public virtual string Select { get; set; }

        /// <summary>
        /// Gets the $orderby system query option, which allows clients to request resource in a particular order
        /// </summary>
        public virtual string OrderBy { get; set; }

        /// <summary>
        /// Gets the $top system query option, which allows clients a required number of resources. Usually used in conjunction with the $skip query options
        /// </summary>
        public virtual int? Top { get; set; }

        /// <summary>
        /// Gets the $skip system query option, which allows clients to skip a given number of resources. Usually used in conjunction with the $top query options
        /// </summary>
        public virtual int? Skip { get; set; }

        /// <summary>
        /// Gets the $count system query option, which allows clients to request a count of the matching resources included with the resources in the response
        /// </summary>
        public virtual bool Count { get; set; }

        /// <summary>
        /// Gets the $search system query option, which allows clients to request items within a collection matching a free-text search expression
        /// </summary>
        public virtual string Search { get; set; }

        /// <summary>
        /// Gets the $format system query option, if supported, which allows clients to request a response in a particular format
        /// </summary>
        public virtual string Format { get; set; }

        /// <summary>
        /// Gets the $compute system query option, which allows clients to define computed properties that can be used in a $select or within a $filter or $orderby expression
        /// </summary>
        public virtual string Compute { get; set; }

        /// <summary>
        /// Gets the $index system query option, which allows clients to do a positional insert into a collection annotated with using the Core.PositionalInsert term (see http://docs.oasis-open.org/odata/odata/v4.01/odata-v4.01-part2-url-conventions.html#VocCore)
        /// </summary>
        public virtual string Index { get; set; }

        /// <summary>
        /// Gets the $schemaversion system query option, which allows clients to specify the version of the schema against which the request is made. The semantics of $schemaversion is covered in the OData-Protocol (http://docs.oasis-open.org/odata/odata/v4.01/odata-v4.01-part2-url-conventions.html#odata) document
        /// </summary>
        public virtual string SchemaVersion { get; set; }

    }

}

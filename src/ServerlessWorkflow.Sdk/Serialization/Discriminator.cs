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

namespace ServerlessWorkflow.Sdk.Serialization
{

    /// <summary>
    /// Represents the <see cref="Attribute"/> used to indicate the property used to discriminate derived types of the marked class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DiscriminatorAttribute
        : Attribute
    {

        /// <summary>
        /// Initializes a new <see cref="DiscriminatorAttribute"/>
        /// </summary>
        /// <param name="property">The name of the property used to discriminate derived types of the class marked by the <see cref="DiscriminatorAttribute"/></param>
        public DiscriminatorAttribute(string property)
        {
            this.Property = property;
        }

        /// <summary>
        /// Gets the name of the property used to discriminate derived types of the class marked by the <see cref="DiscriminatorAttribute"/>
        /// </summary>
        public string Property { get; }

    }

}

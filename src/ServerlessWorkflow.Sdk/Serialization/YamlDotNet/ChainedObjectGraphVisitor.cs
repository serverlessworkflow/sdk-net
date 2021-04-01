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
using ServerlessWorkflow.Sdk;
using System.Collections;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{

    /// <summary>
    /// Represents a <see cref="ObjectGraphVisitors.ChainedObjectGraphVisitor"/> used to ignore the serialization of empty <see cref="IEnumerable"/>s
    /// </summary>
    /// <remarks>Based on <see href="https://stackoverflow.com/a/64737155/3637555">Kees C. Bakker's answer on StackOverflow</see></remarks>
    public class ChainedObjectGraphVisitor 
        : ObjectGraphVisitors.ChainedObjectGraphVisitor
    {

        /// <summary>
        /// Initializes a new <see cref="ChainedObjectGraphVisitor"/>
        /// </summary>
        /// <param name="nextVisitor"></param>
        public ChainedObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor) 
            : base(nextVisitor)
        {

        }

        /// <inheritdoc/>
        public override bool Enter(IObjectDescriptor value, IEmitter context)
        {
            if (this.IsEmptyEnumerable(value))
                return false;
            return base.Enter(value, context);
        }

        /// <inheritdoc/>
        public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
        {
            if (this.IsEmptyEnumerable(value))
                return false;
            return base.EnterMapping(key, value, context);
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="IObjectDescriptor"/> is an empty <see cref="IEnumerable"/>
        /// </summary>
        /// <param name="descriptor">The <see cref="IObjectDescriptor"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="IObjectDescriptor"/> is an empty <see cref="IEnumerable"/></returns>
        protected virtual bool IsEmptyEnumerable(IObjectDescriptor descriptor)
        {
            if (descriptor.Value == null)
                return true;
            if (typeof(IEnumerable).IsAssignableFrom(descriptor.Value.GetType()))
                return !((IEnumerable)descriptor.Value).GetEnumerator().MoveNext();
            return false;
        }

    }

}

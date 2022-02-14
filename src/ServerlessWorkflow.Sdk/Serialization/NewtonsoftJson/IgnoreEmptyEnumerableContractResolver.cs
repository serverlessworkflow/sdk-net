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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json
{

    /// <summary>
    /// Represents a <see cref="DefaultContractResolver"/> used to ignore empty <see cref="IEnumerable"/>s when serializing
    /// </summary>
    public class IgnoreEmptyEnumerableContractResolver 
        : DefaultContractResolver
    {

        /// <inheritdoc/>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty result = base.CreateProperty(member, memberSerialization);
            switch (member)
            {
                case PropertyInfo property:
                    result.Writable |= property.CanWrite;
                    result.Ignored |= !property.CanRead;
                    break;
            }
            if (result.PropertyType != typeof(string)
                && result.PropertyType != typeof(JToken))
            {
                if (result.PropertyType!.GetInterface(nameof(IEnumerable)) != null)
                    result.ShouldSerialize =
                        instance => (instance?.GetType().GetProperties(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                            .First(p => string.Equals(p.Name, result.PropertyName, StringComparison.InvariantCultureIgnoreCase) 
                                || string.Equals(p.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName, result.PropertyName, StringComparison.InvariantCultureIgnoreCase)).GetValue(instance) 
                                as IEnumerable<object>)?
                            .Count() > 0;
            }
            return result;
        }
    }

}

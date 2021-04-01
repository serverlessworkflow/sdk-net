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
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{

    /// <summary>
    /// Defines extensions for <see cref="ParsingEventStream"/>
    /// </summary>
    public static class IParserExtensions
    {

        /// <summary>
        /// Attempts to find the specified mapping entry
        /// </summary>
        /// <param name="parser">The <see cref="ParsingEventStream"/> to search</param>
        /// <param name="selector">A predicate <see cref="Func{T, TResult}"/> used to search the <see cref="ParsingEventStream"/> for a specific <see cref="ParsingEvent"/></param>
        /// <param name="key">The key of the matching <see cref="Scalar"/></param>
        /// <param name="value">The matching <see cref="Scalar"/>'s <see cref="ParsingEvent"/> </param>
        /// <returns>A boolean indicating whether or not the specified mapping entry could be found</returns>
        public static bool TryFindMappingEntry(this ParsingEventStream parser, Func<Scalar, bool> selector, out Scalar key, out ParsingEvent value)
        {
            parser.Consume<MappingStart>();
            do
            {
                switch (parser.Current)
                {
                    case Scalar scalar:
                        var keyMatched = selector(scalar);
                        parser.MoveNext();
                        if (keyMatched)
                        {
                            value = parser.Current;
                            key = scalar;
                            return true;
                        }
                        parser.SkipThisAndNestedEvents();
                        break;
                    case MappingStart or SequenceStart:
                        parser.SkipThisAndNestedEvents();
                        break;
                    default:
                        parser.MoveNext();
                        break;
                }
            } 
            while (parser.Current is not null);
            key = null;
            value = null;
            return false;
        }

    }

}

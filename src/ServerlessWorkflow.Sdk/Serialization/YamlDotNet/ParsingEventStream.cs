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
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
    /// <summary>
    /// Represents a replayable stream of <see cref="ParsingEvent"/>s
    /// </summary>
    public class ParsingEventStream
       : IParser
    {

        /// <summary>
        /// Initializes a new <see cref="ParsingEventStream"/>
        /// </summary>
        /// <param name="events">A <see cref="LinkedList{T}"/> containing the <see cref="ParsingEvent"/>s to stream</param>
        public ParsingEventStream(LinkedList<ParsingEvent> events)
        {
            this.Events = events;
            this._Current = this.Events.First;
        }

        /// <summary>
        /// Gets a <see cref="LinkedList{T}"/> containing the <see cref="ParsingEvent"/>s to stream
        /// </summary>
        protected LinkedList<ParsingEvent> Events { get; } = new LinkedList<ParsingEvent>();

        private LinkedListNode<ParsingEvent> _Current;
        /// <inheritdoc/>
        public ParsingEvent Current
        {
            get
            {
                return this._Current?.Value;
            }
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            this._Current = this._Current.Next;
            return this.Current is not null;
        }

        /// <summary>
        /// Resets the <see cref="ParsingEventStream"/>
        /// </summary>
        public void Reset()
        {
            this._Current = this.Events.First;
        }

        /// <summary>
        /// Creates a new <see cref="ParsingEventStream"/> based on the specified <see cref="IParser"/>
        /// </summary>
        /// <param name="parser">The <see cref="IParser"/> based on which to create a new <see cref="ParsingEventStream"/></param>
        /// <returns>A new <see cref="ParsingEventStream"/></returns>
        public static ParsingEventStream Create(IParser parser)
        {
            LinkedList<ParsingEvent> events = new LinkedList<ParsingEvent>();
            events.AddLast(parser.Consume<MappingStart>());
            var depth = 0;
            do
            {
                var next = parser.Consume<ParsingEvent>();
                depth += next.NestingIncrease;
                events.AddLast(next);
            } while (depth >= 0);
            return  new ParsingEventStream(events);
        }

    }

}

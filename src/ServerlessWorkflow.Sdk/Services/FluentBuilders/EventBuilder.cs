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
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IEventBuilder"/> interface
    /// </summary>
    public class EventBuilder
        : MetadataContainerBuilder<IEventBuilder>, IEventBuilder
    {

        /// <summary>
        /// Gets the <see cref="Models.EventDefinition"/> to configure
        /// </summary>
        protected EventDefinition Event { get; } = new EventDefinition();

        /// <inheritdoc/>
        public override Any Metadata
        {
            get
            {
                return this.Event.Metadata;
            }
        }

        /// <inheritdoc/>
        public virtual IEventBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Event.Name = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventBuilder WithSource(Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            this.Event.Source = source.ToString();
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventBuilder WithType(string type)
        {
            this.Event.Type = type;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventBuilder CorrelateUsing(string contextAttributeName)
        {
            if (string.IsNullOrWhiteSpace(contextAttributeName))
                throw new ArgumentNullException(nameof(contextAttributeName));
            EventCorrelationDefinition correlation = this.Event.Correlations.FirstOrDefault(c => c.ContextAttributeName == contextAttributeName);
            if(correlation != null)
                this.Event.Correlations.Remove(correlation);
            this.Event.Correlations.Add(new EventCorrelationDefinition() { ContextAttributeName = contextAttributeName });
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventBuilder CorrelateUsing(string contextAttributeName, string contextAttributeValue)
        {
            if (string.IsNullOrWhiteSpace(contextAttributeName))
                throw new ArgumentNullException(nameof(contextAttributeName));
            EventCorrelationDefinition correlation = this.Event.Correlations.FirstOrDefault(c => c.ContextAttributeName == contextAttributeName);
            if (correlation != null)
            {
                if (correlation.ContextAttributeValue == contextAttributeValue)
                    return this;
                this.Event.Correlations.Remove(correlation);
            } 
            this.Event.Correlations.Add(new EventCorrelationDefinition() { ContextAttributeName = contextAttributeName, ContextAttributeValue = contextAttributeValue });
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventBuilder CorrelateUsing(IDictionary<string, string> correlations)
        {
            if (correlations == null)
                throw new ArgumentNullException(nameof(correlations));
            this.Event.Correlations = correlations.Select(kvp => new EventCorrelationDefinition() { ContextAttributeName = kvp.Key, ContextAttributeValue = kvp.Value }).ToList();
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventBuilder IsConsumed()
        {
            this.Event.Kind = EventKind.Consumed;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventBuilder IsProduced()
        {
            this.Event.Kind = EventKind.Produced;
            return this;
        }

        /// <inheritdoc/>
        public virtual EventDefinition Build()
        {
            return this.Event;
        }

    }

}

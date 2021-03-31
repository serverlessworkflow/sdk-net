using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Represents the base class for all <see cref="IMetadataContainerBuilder{TContainer}"/>
    /// </summary>
    /// <typeparam name="TContainer">The type of the <see cref="IMetadataContainerBuilder{TContainer}"/></typeparam>
    public abstract class MetadataContainerBuilder<TContainer>
        : IMetadataContainerBuilder<TContainer>
        where TContainer : class, IMetadataContainerBuilder<TContainer>
    {

        /// <inheritdoc/>
        public abstract JObject Metadata { get; }

        /// <inheritdoc/>
        public virtual TContainer WithMetadata(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            JToken token = null;
            if (value != null)
                token = JToken.FromObject(value);
            this.Metadata.Add(key, token);
            return (TContainer)(object)this;
        }

        /// <inheritdoc/>
        public virtual TContainer WithMetadata(IDictionary<string, object> metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            foreach (KeyValuePair<string, object> kvp in metadata)
            {
                this.WithMetadata(kvp.Key, kvp.Value);
            }
            return (TContainer)(object)this;
        }

    }

}

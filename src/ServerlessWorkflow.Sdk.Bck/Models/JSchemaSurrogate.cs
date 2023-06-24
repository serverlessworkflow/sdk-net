using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a ProtoBuf surrogate used to serialize and deserialize <see cref="JSchema"/>s
    /// </summary>
    [ProtoContract]
    public class JSchemaSurrogate
        : DynamicObject
    {

        /// <summary>
        /// Initializes a new <see cref="JSchemaSurrogate"/>
        /// </summary>
        public JSchemaSurrogate()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="JSchemaSurrogate"/>
        /// </summary>
        /// <param name="properties"></param>
        public JSchemaSurrogate(IDictionary<string, object> properties)
            : base(properties)
        {

        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the <see cref="DynamicObject"/>'s properties
        /// </summary>
        [DataMember(Order = 1)]
        [ProtoMember(1)]
        protected new List<DynamicProperty> Properties
        {
            get => base.Properties;
            set => base.Properties = value;
        }

        /// <summary>
        /// Implicitly converts the specified <see cref="JSchemaSurrogate"/> into a new <see cref="JSchema"/>
        /// </summary>
        /// <param name="surrogate">The <see cref="JSchemaSurrogate"/> to convert</param>
        public static implicit operator JSchema(JSchemaSurrogate surrogate)
        {
            var settings = JsonConvert.DefaultSettings!();
            settings.ContractResolver = new DefaultContractResolver();
            var json = JsonConvert.SerializeObject(surrogate, settings);
            return JSchema.Parse(json);
        }

        /// <summary>
        /// Implicitly converts the specified <see cref="JSchema"/> into a new <see cref="JSchemaSurrogate"/>
        /// </summary>
        /// <param name="schema">The <see cref="JSchema"/> to convert</param>
        public static implicit operator JSchemaSurrogate(JSchema schema)
        {
            if (schema == null)
                return null!;
            var json = schema.ToString();
            var expando = JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(json);
            var res = FromObject(expando);
            return res!;
        }

        /// <inheritdoc/>
        public new static JSchemaSurrogate? FromObject(object? value)
        {
            if (value == null)
                return null;
            if (value is JSchemaSurrogate surrogate)
                return surrogate;
            if (value is IDictionary<string, object> mappings)
                return new(mappings);
            var ignoreIfNotDecorated = false;
            if (value.GetType().TryGetCustomAttribute<DataContractAttribute>(out _)
                || value.GetType().TryGetCustomAttribute<ProtoContractAttribute>(out _))
                ignoreIfNotDecorated = true;
            surrogate = new();
            foreach (var property in value.GetType()
                .GetProperties()
                .Where(p => p.CanRead && p.GetGetMethod(true) != null)
                .Where(p => !ignoreIfNotDecorated || p.TryGetCustomAttribute<DataMemberAttribute>(out _) || p.TryGetCustomAttribute<ProtoMemberAttribute>(out _)))
            {
                surrogate.Set(property.Name, property.GetValue(value));
            }
            return surrogate;
        }

    }

}

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
using Neuroglia.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Gets an object that is one of the specified types
    /// </summary>
    /// <typeparam name="T1">A first type alternative</typeparam>
    /// <typeparam name="T2">A second type alternative</typeparam>
    [ProtoContract]
    [DataContract]
    public class OneOf<T1, T2>
        : IExtensible
    {

        private IExtension _Extension;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing) => Extensible.GetExtensionObject(ref this._Extension, createIfMissing);

        private DiscriminatedUnionObject _DicriminatorUnionObject;

        /// <summary>
        /// Initializes a new <see cref="OneOf{T1, T2}"/>
        /// </summary>
        protected OneOf()
        {
            
        }

        /// <summary>
        /// Initializes a new <see cref="OneOf{T1, T2}"/>
        /// </summary>
        /// <param name="value">The value of the <see cref="OneOf{T1, T2}"/></param>
        public OneOf(T1 value)
        {
            this.Value1 = value;
        }

        /// <summary>
        /// Initializes a new <see cref="OneOf{T1, T2}"/>
        /// </summary>
        /// <param name="value">The value of the <see cref="OneOf{T1, T2}"/></param>
        public OneOf(T2 value)
        {
            this.Value2 = value;
        }

        /// <summary>
        /// Gets the first possible value
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlDotNet.Serialization.YamlIgnore]
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public T1 Value1
        {
            get => _DicriminatorUnionObject.Is(1) ? ((T1)_DicriminatorUnionObject.Object) : default;
            set => _DicriminatorUnionObject = new(1, value);
        }

        bool ShouldSerializeT1() => this._DicriminatorUnionObject.Is(1);

        void ResetT1() => DiscriminatedUnionObject.Reset(ref this._DicriminatorUnionObject, 1);

        /// <summary>
        /// Gets the second possible value
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlDotNet.Serialization.YamlIgnore]
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public T2 Value2
        {
            get => _DicriminatorUnionObject.Is(2) ? ((T2)_DicriminatorUnionObject.Object) : default;
            set => _DicriminatorUnionObject = new(2, value);
        }

        bool ShouldSerializeT2() => this._DicriminatorUnionObject.Is(2);

        void ResetT2() => DiscriminatedUnionObject.Reset(ref this._DicriminatorUnionObject, 2);

        /// <summary>
        /// Implicitly convert the specified value into a new <see cref="OneOf{T1, T2}"/>
        /// </summary>
        /// <param name="value">The value to convert</param>
        public static implicit operator OneOf<T1, T2>(T1 value)
        {
            return new(value);
        }

        /// <summary>
        /// Implicitly convert the specified value into a new <see cref="OneOf{T1, T2}"/>
        /// </summary>
        /// <param name="value">The value to convert</param>
        public static implicit operator OneOf<T1, T2>(T2 value)
        {
            return new(value);
        }

        /// <summary>
        /// Implicitly convert the specified <see cref="OneOf{T1, T2}"/> into a new value
        /// </summary>
        /// <param name="value">The <see cref="OneOf{T1, T2}"/> to convert</param>
        public static implicit operator T1(OneOf<T1, T2> value)
        {
            return value.Value1;
        }

        /// <summary>
        /// Implicitly convert the specified <see cref="OneOf{T1, T2}"/> into a new value
        /// </summary>
        /// <param name="value">The <see cref="OneOf{T1, T2}"/> to convert</param>
        public static implicit operator T2(OneOf<T1, T2> value)
        {
            return value.Value2;
        }

    }

    /// <summary>
    /// Represents an object of any type
    /// </summary>
    [ProtoContract]
    [DataContract]
    [Newtonsoft.Json.JsonConverter(typeof(Any))]
    public class Any
        : ProtoObject
    {

        /// <inheritdoc/>
        [ProtoMember(1)]
        protected new List<ProtoField> Fields
        {
            get => base.Fields;
            set => base.Fields = value;
        }

    }

    /// <summary>
    /// Represents the <see cref="Newtonsoft.Json.JsonConverter"/> used to convert from and to <see cref="Any"/> instances
    /// </summary>
    public class AnyConverter
        : Newtonsoft.Json.JsonConverter<Any>
    {

        /// <inheritdoc/>
        public override Any ReadJson(JsonReader reader, Type objectType, Any existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jobject = JObject.ReadFrom(reader);
            var any = new Any();
            foreach(var property in jobject)
            {
                any.Set(property.Name, property.Value<object>());
            }
            return any;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, Any value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToObject());
        }

    }

}

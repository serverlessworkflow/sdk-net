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
    /// Gets an object that is one of the specified types
    /// </summary>
    /// <typeparam name="T1">A first type alternative</typeparam>
    /// <typeparam name="T2">A second type alternative</typeparam>
    [ProtoContract]
    [DataContract]
    public class OneOf<T1, T2>
        : IExtensible, IOneOf
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
            this.T1Value = value;
        }

        /// <summary>
        /// Initializes a new <see cref="OneOf{T1, T2}"/>
        /// </summary>
        /// <param name="value">The value of the <see cref="OneOf{T1, T2}"/></param>
        public OneOf(T2 value)
        {
            this.T2Value = value;
        }

        /// <summary>
        /// Gets the first possible value
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlDotNet.Serialization.YamlIgnore]
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public T1 T1Value
        {
            get => _DicriminatorUnionObject.Is(1) ? ((T1)_DicriminatorUnionObject.Object) : default;
            set => _DicriminatorUnionObject = new(1, value);
        }

        bool ShouldSerializeT1Value() => this._DicriminatorUnionObject.Is(1);

        void ResetT1Value() => DiscriminatedUnionObject.Reset(ref this._DicriminatorUnionObject, 1);

        /// <summary>
        /// Gets the second possible value
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlDotNet.Serialization.YamlIgnore]
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public T2 T2Value
        {
            get => _DicriminatorUnionObject.Is(2) ? ((T2)_DicriminatorUnionObject.Object) : default;
            set => _DicriminatorUnionObject = new(2, value);
        }

        bool ShouldSerializeT2Value() => this._DicriminatorUnionObject.Is(2);

        void ResetT2Value() => DiscriminatedUnionObject.Reset(ref this._DicriminatorUnionObject, 2);

        object IOneOf.GetValue()
        {
            if (this.T1Value == null)
                return this.T2Value;
            else
                return this.T1Value;
        }

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
            return value.T1Value;
        }

        /// <summary>
        /// Implicitly convert the specified <see cref="OneOf{T1, T2}"/> into a new value
        /// </summary>
        /// <param name="value">The <see cref="OneOf{T1, T2}"/> to convert</param>
        public static implicit operator T2(OneOf<T1, T2> value)
        {
            return value.T2Value;
        }

    }

}

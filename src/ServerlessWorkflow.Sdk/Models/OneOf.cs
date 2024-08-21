// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ServerlessWorkflow.Sdk.Serialization.Json;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Gets an object that is one of the specified types
/// </summary>
/// <typeparam name="T1">A first type alternative</typeparam>
/// <typeparam name="T2">A second type alternative</typeparam>
[DataContract, JsonConverter(typeof(OneOfConverter))]
public class OneOf<T1, T2>
    : IOneOf
{

    /// <summary>
    /// Initializes a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value of the <see cref="OneOf{T1, T2}"/></param>
    public OneOf(T1 value)
    {
        this.TypeIndex = 1;
        this.T1Value = value!;
    }

    /// <summary>
    /// Initializes a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value of the <see cref="OneOf{T1, T2}"/></param>
    public OneOf(T2 value)
    {
        this.TypeIndex = 2;
        this.T2Value = value!;
    }

    /// <summary>
    /// Gets the index of the discriminated type
    /// </summary>
    public int TypeIndex { get; }

    /// <summary>
    /// Gets the first possible value
    /// </summary>
    [DataMember(Order = 1), JsonIgnore, YamlIgnore]
    public T1? T1Value { get; }

    /// <summary>
    /// Gets the second possible value
    /// </summary>
    [DataMember(Order = 2), JsonIgnore, YamlIgnore]
    public T2? T2Value { get; }

    object? IOneOf.GetValue() => this.TypeIndex switch
    {
        1 => this.T1Value,
        2 => this.T2Value,
        _ => null
    };

    /// <summary>
    /// Implicitly convert the specified value into a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value to convert</param>
    public static implicit operator OneOf<T1, T2>(T1 value) => new(value);

    /// <summary>
    /// Implicitly convert the specified value into a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value to convert</param>
    public static implicit operator OneOf<T1, T2>(T2 value) => new(value);

    /// <summary>
    /// Implicitly convert the specified <see cref="OneOf{T1, T2}"/> into a new value
    /// </summary>
    /// <param name="value">The <see cref="OneOf{T1, T2}"/> to convert</param>
    public static implicit operator T1?(OneOf<T1, T2> value) => value.T1Value;

    /// <summary>
    /// Implicitly convert the specified <see cref="OneOf{T1, T2}"/> into a new value
    /// </summary>
    /// <param name="value">The <see cref="OneOf{T1, T2}"/> to convert</param>
    public static implicit operator T2?(OneOf<T1, T2> value) => value.T2Value;

}
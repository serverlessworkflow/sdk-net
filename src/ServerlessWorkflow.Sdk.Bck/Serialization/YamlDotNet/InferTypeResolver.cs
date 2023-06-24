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
using YamlDotNet.Core.Events;
/// <summary>
/// Represents an <see cref="INodeTypeResolver"/> used to infer node types from deserialized values
/// </summary>
public class InferTypeResolver
    : INodeTypeResolver
{

    /// <inheritdoc/>
    public bool Resolve(NodeEvent? nodeEvent, ref Type currentType)
    {
        var scalar = nodeEvent as Scalar;
        if (scalar != null)
        {
            if (bool.TryParse(scalar.Value, out _))
            {
                currentType = typeof(bool);
                return true;
            }
            if (byte.TryParse(scalar.Value, out _))
            {
                currentType = typeof(byte);
                return true;
            }
            if (short.TryParse(scalar.Value, out _))
            {
                currentType = typeof(short);
                return true;
            }
            if (int.TryParse(scalar.Value, out _))
            {
                currentType = typeof(int);
                return true;
            }
            if (long.TryParse(scalar.Value, out _))
            {
                currentType = typeof(long);
                return true;
            }
            if (decimal.TryParse(scalar.Value, out _))
            {
                currentType = typeof(decimal);
                return true;
            }
        }
        return false;
    }

}
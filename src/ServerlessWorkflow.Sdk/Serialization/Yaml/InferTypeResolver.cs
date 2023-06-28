// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

using YamlDotNet.Core.Events;

namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents an <see cref="INodeTypeResolver"/> used to infer node types from deserialized values
/// </summary>
public class InferTypeResolver
    : INodeTypeResolver
{

    /// <inheritdoc/>
    public bool Resolve(NodeEvent? nodeEvent, ref Type currentType)
    {
        if (nodeEvent is Scalar scalar)
        {
            if (scalar.Style == ScalarStyle.SingleQuoted || scalar.Style == ScalarStyle.DoubleQuoted)
            {
                currentType = typeof(string);
                return true;
            }
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

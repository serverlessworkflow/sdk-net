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

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes constants about the Serverless Workflow DSL naming convention
/// </summary>
public static class NamingConvention
{

    static readonly int _nameMaxLength = 63;

    /// <summary>
    /// Determines whether or not the specified value is a valid name, following <see href="https://datatracker.ietf.org/doc/html/rfc1123">RFC 1123</see> DNS label name
    /// </summary>
    /// <param name="name">The name to check</param>
    /// <returns>A boolean indicating whether or not the specified name follows specification for <see href="https://datatracker.ietf.org/doc/html/rfc1123">RFC 1123</see> DNS label name</returns>
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        return name.Length <= _nameMaxLength
            && name.IsLowercased()
            && name.IsAlphanumeric('-')
            && char.IsLetterOrDigit(name.First())
            && char.IsLetterOrDigit(name.Last());
    }

}

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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Exposes all schema formats supported by default by ServerlessWorkflow
/// </summary>
public static class SchemaFormat
{

    /// <summary>
    /// Gets the Avro schema format
    /// </summary>
    public const string Avro = "avro";
    /// <summary>
    /// Gets the JSON schema format
    /// </summary>
    public const string Json = "json";
    /// <summary>
    /// Gets the XML schema format
    /// </summary>
    public const string Xml = "xml";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all default schema format
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all default schema format</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Avro;
        yield return Json;
        yield return Xml;
    }

}
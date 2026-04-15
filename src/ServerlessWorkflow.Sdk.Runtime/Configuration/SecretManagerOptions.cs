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

namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents the options used to configure secret management
/// </summary>
[DataContract]
public sealed class SecretManagerOptions
{

    /// <summary>
    /// Gets the default directory where to locate secrets
    /// </summary>
    public static readonly string DefaultDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "secrets");

    /// <summary>
    /// Gets/sets the directory where secrets are located
    /// </summary>
    [Description("The directory where the runner's secrets are located")]
    [DataMember(Order = 1, Name = "directory"), JsonPropertyOrder(1), JsonPropertyName("directory")]
    public string? Directory { get; set; }

}

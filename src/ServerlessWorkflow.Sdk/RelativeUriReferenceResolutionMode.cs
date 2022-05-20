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

namespace ServerlessWorkflow.Sdk
{
    /// <summary>
    /// Enumerates all types of reference resolution modes for relative <see cref="Uri"/>s
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.StringEnumConverterFactory))]
    public enum RelativeUriReferenceResolutionMode
    {
        /// <summary>
        /// Indicates that relative uris should be converted to an absolute one by combining them to a specified base uri
        /// </summary>
        [EnumMember(Value = "convertToAbsolute")]
        ConvertToAbsolute,
        /// <summary>
        /// Indicates that relative uris should be converted to a file path relative to a specified base directory
        /// </summary>
        [EnumMember(Value = "convertToRelativeFilePath")]
        ConvertToRelativeFilePath,
        /// <summary>
        /// Indicates that relative uris should not be resolved
        /// </summary>
        [EnumMember(Value = "none")]
        None
    }

}

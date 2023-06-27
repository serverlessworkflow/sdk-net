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
using System;

namespace ServerlessWorkflow.Sdk.Services.IO
{
    /// <summary>
    /// Represents the options used to configure an <see cref="IWorkflowReader"/>
    /// </summary>
    public class WorkflowReaderOptions
    {

        /// <summary>
        /// Gets/sets the base <see cref="Uri"/> to use to combine to relative <see cref="Uri"/>s when the <see cref="RelativeUriResolutionMode"/> property is set to <see cref="RelativeUriReferenceResolutionMode.ConvertToAbsolute"/>
        /// </summary>
        public virtual Uri? BaseUri { get; set; }

        /// <summary>
        /// Gets/sets the base directory to use when resolving relative <see cref="Uri"/> when the <see cref="RelativeUriResolutionMode"/> property is set to <see cref="RelativeUriReferenceResolutionMode.ConvertToRelativeFilePath"/>. Defaults to <see cref="AppContext.BaseDirectory"/>
        /// </summary>
        public virtual string BaseDirectory { get; set; } = AppContext.BaseDirectory;

        /// <summary>
        /// Gets/sets the <see cref="RelativeUriReferenceResolutionMode"/> to use. Defaults to <see cref="RelativeUriReferenceResolutionMode.ConvertToRelativeFilePath"/>
        /// </summary>
        public virtual string RelativeUriResolutionMode { get; set; } = RelativeUriReferenceResolutionMode.ConvertToRelativeFilePath;

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to load external definitions
        /// </summary>
        public virtual bool LoadExternalDefinitions { get; set; }

    }   

}

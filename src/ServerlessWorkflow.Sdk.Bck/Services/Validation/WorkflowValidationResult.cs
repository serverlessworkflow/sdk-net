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
using FluentValidation.Results;
using Newtonsoft.Json.Schema;
using ServerlessWorkflow.Sdk.Models;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IWorkflowValidationResult"/>
    /// </summary>
    public class WorkflowValidationResult
        : IWorkflowValidationResult
    {

        /// <summary>
        /// Inherits a new <see cref="WorkflowValidationResult"/>
        /// </summary>
        protected WorkflowValidationResult()
        {

        }

        /// <summary>
        /// Inherits a new <see cref="WorkflowValidationResult"/>
        /// </summary>
        /// <param name="schemaValidationErrors">An <see cref="IEnumerable{T}"/> containing the schema-related validation errors  that have occured while validating the read <see cref="WorkflowDefinition"/></param>
        /// <param name="dslValidationErrors">An <see cref="IEnumerable{T}"/> containing the Serverless Workflow DSL-related validation errors that have occured while validating the read <see cref="WorkflowDefinition"/></param>
        public WorkflowValidationResult(IEnumerable<ValidationError> schemaValidationErrors, IEnumerable<ValidationFailure> dslValidationErrors)
        {
            this.SchemaValidationErrors = schemaValidationErrors;
            this.DslValidationErrors = dslValidationErrors;
        }

        /// <inheritdoc/>
        public virtual IEnumerable<ValidationError> SchemaValidationErrors { get; } = new List<ValidationError>();

        /// <inheritdoc/>
        public virtual IEnumerable<ValidationFailure> DslValidationErrors { get; } = new List<ValidationFailure>();

        /// <inheritdoc/>
        public virtual bool IsValid => !this.SchemaValidationErrors.Any() && !this.DslValidationErrors.Any();

    }
}

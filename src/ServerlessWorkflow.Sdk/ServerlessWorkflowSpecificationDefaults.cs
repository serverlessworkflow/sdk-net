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
/// Exposes the default values for the Serverless Workflow Specification
/// </summary>
public static class ServerlessWorkflowSpecificationDefaults
{

    /// <summary>
    /// Gets the current version of the Serverless Workflow Specification
    /// </summary>
    public const string Version = "1.1.0";

    /// <summary>
    /// Exposes constants about the cloud events defined by the Serverless Workflow Specification
    /// </summary>
    public static class CloudEvents
    {

        /// <summary>
        /// Gets the type prefix for all cloud events defined by the Serverless Workflow Specification
        /// </summary>
        public const string TypePrefix = "io.serverlessworkflow.";

        /// <summary>
        /// Exposes constants about workflow-related cloud events
        /// </summary>
        public static class Workflow
        {

            /// <summary>
            /// Gets the type prefix for all workflow-related cloud events produced by the Serverless Workflow Specification
            /// </summary>
            public const string TypePrefix = CloudEvents.TypePrefix + "workflow.";

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a workflow started
            /// </summary>
            public static class Started
            {

                const string TypeName = "started";
                /// <summary>
                /// Gets the type of the workflow started cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a workflow has been suspended
            /// </summary>
            public static class Suspended
            {

                const string TypeName = "suspended";
                /// <summary>
                /// Gets the type of the workflow suspended cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a workflow has been resumed
            /// </summary>
            public static class Resumed
            {

                const string TypeName = "resumed";
                /// <summary>
                /// Gets the type of the workflow resumed cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a workflow has started correlating events
            /// </summary>
            public static class CorrelationStarted
            {

                const string TypeName = "correlation-started";
                /// <summary>
                /// Gets the type of the workflow correlation started cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a workflow has finished correlating events
            /// </summary>
            public static class CorrelationCompleted
            {

                const string TypeName = "correlation-completed";
                /// <summary>
                /// Gets the type of the workflow correlation completed cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a workflow has faulted
            /// </summary>
            public static class Faulted
            {

                const string TypeName = "faulted";
                /// <summary>
                /// Gets the type of the workflow faulted cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a workflow ran to completion
            /// </summary>
            public static class Completed
            {

                const string TypeName = "completed";
                /// <summary>
                /// Gets the type of the workflow completed cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that the execution of a workflow has been cancelled
            /// </summary>
            public static class Cancelled
            {

                const string TypeName = "cancelled";
                /// <summary>
                /// Gets the type of the workflow cancelled cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that the execution of a workflow has ended
            /// </summary>
            public static class Ended
            {

                const string TypeName = "ended";
                /// <summary>
                /// Gets the type of the workflow ended cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

        }

        /// <summary>
        /// Exposes constants about task-related cloud events
        /// </summary>
        public static class Task
        {

            /// <summary>
            /// Gets the type prefix for all task-related cloud events produced by the Serverless Workflow Specification
            /// </summary>
            public const string TypePrefix = CloudEvents.TypePrefix + "task.";

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task has been created
            /// </summary>
            public static class Created
            {

                const string TypeName = "created";
                /// <summary>
                /// Gets the type of the task created cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task started
            /// </summary>
            public static class Started
            {

                const string TypeName = "started";
                /// <summary>
                /// Gets the type of the task started cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task has been suspended
            /// </summary>
            public static class Suspended
            {

                const string TypeName = "suspended";
                /// <summary>
                /// Gets the type of the task suspended cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task has been resumed
            /// </summary>
            public static class Resumed
            {

                const string TypeName = "resumed";
                /// <summary>
                /// Gets the type of the task resumed cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task is being retried
            /// </summary>
            public static class Retrying
            {

                const string TypeName = "retrying";
                /// <summary>
                /// Gets the type of the retrying task cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task has been skipped
            /// </summary>
            public static class Skipped
            {

                const string TypeName = "skipped";
                /// <summary>
                /// Gets the type of the task skipped cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task has faulted
            /// </summary>
            public static class Faulted
            {

                const string TypeName = "faulted";
                /// <summary>
                /// Gets the type of the task faulted cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that a task ran to completion
            /// </summary>
            public static class Completed
            {

                const string TypeName = "completed";
                /// <summary>
                /// Gets the type of the task completed cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that the execution of a task has been cancelled
            /// </summary>
            public static class Cancelled
            {

                const string TypeName = "cancelled";
                /// <summary>
                /// Gets the type of the task cancelled cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

            /// <summary>
            /// Exposes constants about the cloud event used to notify that the execution of a task has ended
            /// </summary>
            public static class Ended
            {

                const string TypeName = "ended";
                /// <summary>
                /// Gets the type of the task ended cloud event version 1
                /// </summary>
                public const string v1 = TypePrefix + TypeName + ".v1";

            }

        }

    }

}

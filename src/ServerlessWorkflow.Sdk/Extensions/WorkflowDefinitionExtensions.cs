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

using ServerlessWorkflow.Sdk.Models;
using System.Collections;
using System.Reflection;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="WorkflowDefinition"/>s
/// </summary>
public static class WorkflowDefinitionExtensions
{

    /// <summary>
    /// Builds a reference to the specified <see cref="TaskDefinition"/>
    /// </summary>
    /// <param name="workflow">The extended <see cref="WorkflowDefinition"/></param>
    /// <param name="task">The <see cref="TaskDefinition"/> to reference</param>
    /// <param name="path">The name or path to the task to reference</param>
    /// <param name="parentReference">A reference to the <see cref="TaskDefinition"/>'s parent, if any</param>
    /// <returns>A new <see cref="Uri"/> used to reference the <see cref="TaskDefinition"/></returns>
    public static Uri BuildReferenceTo(this WorkflowDefinition workflow, TaskDefinition task, string? path, Uri? parentReference = null)
    {
        ArgumentNullException.ThrowIfNull(workflow);
        ArgumentNullException.ThrowIfNull(task);
        if (string.IsNullOrWhiteSpace(path)) return parentReference ?? throw new ArgumentNullException(nameof(parentReference), "The parent must be set when the path to the task to execute is null (in case the task is a function)");
        return parentReference == null
            ? new Uri($"/{nameof(WorkflowDefinition.Do).ToCamelCase()}/{workflow.Do.Keys.ToList().IndexOf(path)}/{path}", UriKind.Relative)
            : new Uri($"{parentReference.OriginalString}/{path}", UriKind.Relative);
    }

    /// <summary>
    /// Builds a reference to the specified <see cref="TaskDefinition"/>
    /// </summary>
    /// <param name="workflow">The extended <see cref="WorkflowDefinition"/></param>
    /// <param name="task">The <see cref="TaskDefinition"/> to reference</param>
    /// <param name="parentReference">A reference to the <see cref="TaskDefinition"/>'s parent, if any</param>
    /// <returns>A new <see cref="Uri"/> used to reference the <see cref="TaskDefinition"/></returns>
    public static Uri BuildReferenceTo(this WorkflowDefinition workflow, KeyValuePair<string, TaskDefinition> task, Uri? parentReference = null) => workflow.BuildReferenceTo(task.Value, task.Key, parentReference);

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/>'s component at the specified path
    /// </summary>
    /// <typeparam name="TComponent">The type of component to get</typeparam>
    /// <param name="workflow">The extended <see cref="WorkflowDefinition"/></param>
    /// <param name="path">The path to the component to get</param>
    /// <returns>The component at the specified path</returns>
    public static TComponent GetComponent<TComponent>(this WorkflowDefinition workflow, string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        var pathSegments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var currentObject = workflow as object;
        foreach (var pathSegment in pathSegments)
        {
            if (currentObject!.GetType().IsEnumerable() && int.TryParse(pathSegment, out var index)) currentObject = ((IEnumerable)currentObject).OfType<object>().ToList().ElementAt(index);
            else
            {
                var mapEntryType = currentObject.GetType().GetGenericType(typeof(MapEntry<,>));
                if (mapEntryType == null)
                {
                    var property = currentObject.GetType().GetProperty(pathSegment, BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase) ?? throw new NullReferenceException($"Failed to find a component definition of type '{typeof(TComponent).Name}' at '{pathSegment}'");
                    currentObject = property.GetValue(currentObject) ?? throw new NullReferenceException($"Failed to find a component definition of type '{typeof(TComponent).Name}' at '{path}'");
                }
                else currentObject = mapEntryType.GetProperty(nameof(MapEntry<string, object>.Value))!.GetValue(currentObject);
            }
        }
        if (currentObject is not TComponent component) throw new InvalidCastException($"Component at '{path}' is not of type '{typeof(TComponent).Name}'");
        return component;
    }

    /// <summary>
    /// Gets the specified <see cref="AuthenticationPolicyDefinition"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> that defines the <see cref="AuthenticationPolicyDefinition"/> to get</param>
    /// <param name="nameOrReference">The name of/the reference to the policy to get</param>
    /// <returns>The specified <see cref="AuthenticationPolicyDefinition"/></returns>
    public static AuthenticationPolicyDefinition GetAuthenticationPolicy(this WorkflowDefinition workflow, string nameOrReference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameOrReference);
        if (nameOrReference.StartsWith('/') && Uri.TryCreate(nameOrReference, UriKind.Relative, out var uri) && uri != null) return workflow.GetComponent<AuthenticationPolicyDefinition>(nameOrReference);
        else return workflow.Use?.Authentications?.FirstOrDefault(a => string.Equals(a.Key, nameOrReference, StringComparison.OrdinalIgnoreCase)).Value ?? throw new NullReferenceException($"Failed to find an authentication policy definition with the specified name '{nameOrReference}'");
    }

}

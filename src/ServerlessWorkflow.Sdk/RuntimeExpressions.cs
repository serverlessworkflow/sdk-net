namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes statics and constants about ServerlessWorkflow runtime expressions
/// </summary>
public static class RuntimeExpressions
{

    /// <summary>
    /// Exposes the runtime expression language supported by default
    /// </summary>
    public static class Languages
    {

        /// <summary>
        /// Gets the 'jq' runtime expression language
        /// </summary>
        public const string JQ = "jq";
        /// <summary>
        /// Gets the 'js' runtime expression language
        /// </summary>
        public const string JavaScript = "js";

    }

    /// <summary>
    /// Exposes default ServerlessWorkflow runtime expression arguments
    /// </summary>
    public static class Arguments
    {

        /// <summary>
        /// Gets the name of the 'runtime' argument, used to access information about the current runtime
        /// </summary>
        public const string Runtime = "runtime";
        /// <summary>
        /// Gets the name of the 'workflow' argument, used to access the current workflow resource
        /// </summary>
        public const string Workflow = "workflow";
        /// <summary>
        /// Gets the name of the 'context' argument, used to access the current context data
        /// </summary>
        public const string Context = "context";
        /// <summary>
        /// Gets the name of the 'item' argument, used to access the current item of the collection being enumerated
        /// </summary>
        public const string Each = "item";
        /// <summary>
        /// Gets the name of the 'index' argument, used to access the index of the current item of the collection being enumerated
        /// </summary>
        public const string Index = "index";
        /// <summary>
        /// Gets the name of the 'output' argument, used to access the task's output
        /// </summary>
        public const string Output = "output";
        /// <summary>
        /// Gets the name of the 'secret' argument
        /// </summary>
        public const string Secret = "secret";
        /// <summary>
        /// Gets the name of the 'task' argument
        /// </summary>
        public const string Task = "task";
        /// <summary>
        /// Gets the name of the 'input' argument
        /// </summary>
        public const string Input = "input";
        /// <summary>
        /// Gets the name of the 'error' argument, used to access the current error, if any
        /// </summary>
        public const string Error = "error";
        /// <summary>
        /// Gets the name of the 'authorization' argument, used to access a task's resolved authorization
        /// </summary>
        public const string Authorization = "authorization";

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> that contains all supported runtime expression arguments
        /// </summary>
        /// <returns>A new <see cref="IEnumerable{T}"/> that contains all supported runtime expression arguments</returns>
        public static IEnumerable<string> AsEnumerable()
        {
            yield return Runtime;
            yield return Workflow;
            yield return Context;
            yield return Each;
            yield return Index;
            yield return Output;
            yield return Secret;
            yield return Task;
            yield return Input;
            yield return Error;
            yield return Authorization;
        }

    }

}

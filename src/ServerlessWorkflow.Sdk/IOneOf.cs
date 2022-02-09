namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Defines the fundamentals of a service that wraps around multiple alternative value types
    /// </summary>
    public interface IOneOf
    {

        /// <summary>
        /// Gets the object's current value
        /// </summary>
        /// <returns>The object's current value</returns>
        object GetValue();

    }

}

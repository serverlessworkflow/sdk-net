namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="EvaluationResults"/>
/// </summary>
public static class EvaluationResultsExtensions
{

    /// <summary>
    /// Gets the errors that have occured during evaluation
    /// </summary>
    /// <param name="results">The extended <see cref="EvaluationResults"/></param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing key/value mapping of the errors that have occured during evaluation</returns>
    public static IEnumerable<KeyValuePair<string, string>> GetErrors(this EvaluationResults results)
    {
        return results.Details.Where(d => !d.IsValid && d.Errors?.Any() == true).Select(e => new KeyValuePair<string, string>(e.EvaluationPath.ToString(), string.Join(Environment.NewLine, e.Errors!.Select(kvp => kvp.Value))));
    }

}

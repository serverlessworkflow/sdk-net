namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure the workflow's runtime expression evaluation
/// </summary>
[Description("Represents an object used to configure the workflow's runtime expression evaluation.")]
[DataContract]
public sealed record RuntimeExpressionEvaluationConfiguration
{

    /// <summary>
    /// Gets/sets the language used for writing runtime expressions. Defaults to <see cref="RuntimeExpressions.Languages.JQ"/>.
    /// </summary>
    [Description("The language used for writing runtime expressions. Defaults to JQ.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "language"), JsonPropertyOrder(1), JsonPropertyName("language")]
    public string Language { get; init; } = RuntimeExpressions.Languages.JQ;

    /// <summary>
    /// Gets/sets the language used for writing runtime expressions. Defaults to <see cref="RuntimeExpressionEvaluationMode.Strict"/>
    /// </summary>
    [Description("The mode used for evaluating runtime expressions. Defaults to Strict.")]
    [DataMember(Order = 2, Name = "mode"), JsonPropertyOrder(2), JsonPropertyName("mode")]
    public string? Mode { get; init; }

}
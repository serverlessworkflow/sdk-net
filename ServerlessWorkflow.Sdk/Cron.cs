using Cronos;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines helper methods to handle CRON expressions
/// </summary>
public static class Cron
{

    /// <summary>
    /// Parses the specified input into a new <see cref="CronExpression"/>
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <returns>A new <see cref="CronExpression"/></returns>
    public static CronExpression Parse(string input) => CronExpression.Parse(input);

    /// <summary>
    /// Parses the specified input into a new <see cref="CronExpression"/>
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <param name="cron">The parsed <see cref="CronExpression"/>, if any</param>
    /// <returns>A boolean indicating whether or not the specified input could be parsed</returns>
    public static bool TryParse(string input, out CronExpression? cron)
    {
        cron = default;
        try
        {
            cron = Parse(input);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
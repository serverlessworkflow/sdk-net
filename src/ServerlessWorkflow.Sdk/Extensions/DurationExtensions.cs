namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for Iso8601DurationHelper.Durations
/// </summary>
public static class DurationExtensions
{

    /// <summary>
    /// Converts the <see cref="Iso8601DurationHelper.Duration"/> into a <see cref="TimeSpan"/>
    /// </summary>
    /// <param name="duration">The <see cref="Iso8601DurationHelper.Duration"/> to convert</param>
    /// <returns>The converted <see cref="TimeSpan"/></returns>
    public static TimeSpan ToTimeSpan(this Iso8601DurationHelper.Duration duration)
    {
        return new TimeSpan((int)(duration.Days + duration.Weeks * 7 + duration.Months * 30 + duration.Years * 365), (int)duration.Hours, (int)duration.Minutes, (int)duration.Seconds);
    }

}

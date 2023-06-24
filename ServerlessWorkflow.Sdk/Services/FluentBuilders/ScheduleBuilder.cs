namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IScheduleBuilder"/> interface
/// </summary>
public class ScheduleBuilder
    : IScheduleBuilder
{

    /// <summary>
    /// Gets the <see cref="ScheduleDefinition"/> to build
    /// </summary>
    protected ScheduleDefinition Schedule { get; } = new();

    /// <inheritdoc/>
    public virtual IScheduleBuilder AtInterval(TimeSpan interval)
    {
        this.Schedule.Interval = interval;
        this.Schedule.CronExpression = null;
        this.Schedule.Cron = null;
        return this;
    }

    /// <inheritdoc/>
    public virtual IScheduleBuilder Every(string cronExpression, DateTime? validUntil = null)
    {
        if (string.IsNullOrWhiteSpace(cronExpression)) throw new ArgumentNullException(nameof(cronExpression));
        if (!Cron.TryParse(cronExpression, out _)) throw new ArgumentException($"The specified value '{cronExpression}' is not a valid CRON expression");
        if (validUntil.HasValue) this.Schedule.Cron = new CronDefinition() { Expression = cronExpression, ValidUntil = validUntil.Value };
        else this.Schedule.CronExpression = cronExpression;
        this.Schedule.Interval = null;
        return this;
    }

    /// <inheritdoc/>
    public virtual IScheduleBuilder UseTimezone(string? timezone)
    {
        this.Schedule.Timezone = timezone;
        return this;
    }

    /// <inheritdoc/>
    public virtual ScheduleDefinition Build() => this.Schedule;

}

﻿namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IEventBuilder"/> interface
/// </summary>
public class EventBuilder
    : MetadataContainerBuilder<IEventBuilder>, IEventBuilder
{

    /// <summary>
    /// Gets the <see cref="EventDefinition"/> to configure
    /// </summary>
    protected EventDefinition Event { get; } = new();

    /// <inheritdoc/>
    public override IDictionary<string, object>? Metadata => this.Event.Metadata;

    /// <inheritdoc/>
    public virtual IEventBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Event.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventBuilder WithSource(Uri source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        this.Event.Source = source.ToString();
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventBuilder WithType(string type)
    {
        this.Event.Type = type;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventBuilder CorrelateUsing(string contextAttributeName)
    {
        if (string.IsNullOrWhiteSpace(contextAttributeName)) throw new ArgumentNullException(nameof(contextAttributeName));
        var correlation = this.Event.Correlations?.FirstOrDefault(c => c.ContextAttributeName == contextAttributeName);
        if (this.Event.Correlations == null) this.Event.Correlations = new();
        if (correlation != null) this.Event.Correlations!.Remove(correlation);
        this.Event.Correlations.Add(new() { ContextAttributeName = contextAttributeName });
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventBuilder CorrelateUsing(string contextAttributeName, string contextAttributeValue)
    {
        if (string.IsNullOrWhiteSpace(contextAttributeName))  throw new ArgumentNullException(nameof(contextAttributeName));
        var correlation = this.Event.Correlations?.FirstOrDefault(c => c.ContextAttributeName == contextAttributeName);
        if (this.Event.Correlations == null) this.Event.Correlations = new();
        if (correlation != null)
        {
            if (correlation.ContextAttributeValue == contextAttributeValue) return this;
            this.Event.Correlations.Remove(correlation);
        } 
        this.Event.Correlations.Add(new() { ContextAttributeName = contextAttributeName, ContextAttributeValue = contextAttributeValue });
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventBuilder CorrelateUsing(IDictionary<string, string> correlations)
    {
        if (correlations == null) throw new ArgumentNullException(nameof(correlations));
        this.Event.Correlations = correlations.Select(kvp => new EventCorrelationDefinition() { ContextAttributeName = kvp.Key, ContextAttributeValue = kvp.Value }).ToList();
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventBuilder IsConsumed()
    {
        this.Event.Kind = EventKind.Consumed;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventBuilder IsProduced()
    {
        this.Event.Kind = EventKind.Produced;
        return this;
    }

    /// <inheritdoc/>
    public virtual EventDefinition Build() => this.Event;

}
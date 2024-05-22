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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a duration
/// </summary>
[DataContract]
public record Duration
{

    /// <summary>
    /// Gets/sets the numbers of days, if any
    /// </summary>
    [DataMember(Name = "days", Order = 1), JsonPropertyName("days"), JsonPropertyOrder(1), YamlMember(Alias = "days", Order = 1)]
    public virtual uint? Days { get; set; }

    /// <summary>
    /// Gets/sets the numbers of hours, if any
    /// </summary>
    [DataMember(Name = "hours", Order = 2), JsonPropertyName("hours"), JsonPropertyOrder(2), YamlMember(Alias = "hours", Order = 2)]
    public virtual uint? Hours { get; set; }

    /// <summary>
    /// Gets/sets the numbers of minutes, if any
    /// </summary>
    [DataMember(Name = "minutes", Order = 3), JsonPropertyName("minutes"), JsonPropertyOrder(3), YamlMember(Alias = "minutes", Order = 3)]
    public virtual uint? Minutes { get; set; }

    /// <summary>
    /// Gets/sets the numbers of seconds, if any
    /// </summary>
    [DataMember(Name = "seconds", Order = 4), JsonPropertyName("seconds"), JsonPropertyOrder(4), YamlMember(Alias = "seconds", Order = 4)]
    public virtual uint? Seconds { get; set; }

    /// <summary>
    /// Gets/sets the numbers of milliseconds, if any
    /// </summary>
    [DataMember(Name = "milliseconds", Order = 5), JsonPropertyName("milliseconds"), JsonPropertyOrder(5), YamlMember(Alias = "milliseconds", Order = 5)]
    public virtual uint? Milliseconds { get; set; }

    /// <summary>
    /// Gets the the duration's total amount of days
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual double TotalDays => this.TotalHours / 24;

    /// <summary>
    /// Gets the the duration's total amount of hours
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual double TotalHours => this.TotalMinutes / 60;

    /// <summary>
    /// Gets the the duration's total amount of minutes
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual double TotalMinutes => this.TotalSeconds / 60;

    /// <summary>
    /// Gets the the duration's total amount of seconds
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual double TotalSeconds => this.TotalMilliseconds / 1000;

    /// <summary>
    /// Gets the the duration's total amount of milliseconds
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual uint TotalMilliseconds
    {
        get
        {
            var milliseconds = this.Days.HasValue ? this.Days * 24 * 60 * 60 * 1000 : 0;
            milliseconds += this.Hours.HasValue ? this.Hours * 60 * 60 * 1000 : 0;
            milliseconds += this.Minutes.HasValue ? this.Minutes * 60 * 1000 : 0;
            milliseconds += this.Seconds.HasValue ? this.Seconds * 1000 : 0;
            milliseconds += this.Milliseconds.HasValue ? this.Milliseconds : 0;
            return milliseconds ?? 0;
        }
    }

    /// <summary>
    /// Converts the <see cref="Duration"/> to a new <see cref="TimeSpan"/>
    /// </summary>
    /// <returns>A new <see cref="TimeSpan"/></returns>
    public virtual TimeSpan ToTimeSpan() => new((int)(this.Days ?? 0), (int)(this.Hours ?? 0), (int)(this.Minutes ?? 0), (int)(this.Seconds ?? 0), (int)(this.Milliseconds ?? 0));

    /// <summary>
    /// Gets a zero <see cref="Duration"/> value
    /// </summary>
    public static readonly Duration Zero = new();

    /// <summary>
    /// Creates a new <see cref="Duration"/> object representing the specified number of days.
    /// </summary>
    /// <param name="days">The number of days.</param>
    /// <returns>A new <see cref="Duration"/> object with the specified number of days.</returns>
    public static Duration FromDays(uint days) => new() { Days = days };

    /// <summary>
    /// Creates a new <see cref="Duration"/> object representing the specified number of hours.
    /// </summary>
    /// <param name="hours">The number of hours.</param>
    /// <returns>A new <see cref="Duration"/> object with the specified number of hours.</returns>
    public static Duration FromHours(uint hours) => new() { Hours = hours };

    /// <summary>
    /// Creates a new <see cref="Duration"/> object representing the specified number of minutes.
    /// </summary>
    /// <param name="minutes">The number of minutes.</param>
    /// <returns>A new <see cref="Duration"/> object with the specified number of minutes.</returns>
    public static Duration FromMinutes(uint minutes) => new() { Minutes = minutes };

    /// <summary>
    /// Creates a new <see cref="Duration"/> object representing the specified number of seconds.
    /// </summary>
    /// <param name="seconds">The number of seconds.</param>
    /// <returns>A new <see cref="Duration"/> object with the specified number of seconds.</returns>
    public static Duration FromSeconds(uint seconds) => new() { Seconds = seconds };

    /// <summary>
    /// Creates a new <see cref="Duration"/> object representing the specified number of milliseconds.
    /// </summary>
    /// <param name="milliseconds">The number of milliseconds.</param>
    /// <returns>A new <see cref="Duration"/> object with the specified number of milliseconds.</returns>
    public static Duration FromMilliseconds(uint milliseconds) => new() { Milliseconds = milliseconds };

    /// <summary>
    /// Creates a new <see cref="Duration"/> representing the specified <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="timeSpan">The <see cref="TimeSpan"/> to convert.</param>
    /// <returns>A new <see cref="Duration"/> representing the specified <see cref="TimeSpan"/>.</returns>
    public static Duration FromTimeSpan(TimeSpan timeSpan) => new()
    {
        Days = (uint)timeSpan.Days,
        Hours = (uint)timeSpan.Hours,
        Minutes = (uint)timeSpan.Minutes,
        Seconds = (uint)timeSpan.Seconds,
        Milliseconds = (uint)timeSpan.Milliseconds
    };

    /// <summary>
    /// Converts the specified <see cref="Duration"/> into a new <see cref="TimeSpan"/>
    /// </summary>
    /// <param name="duration">The <see cref="Duration"/> to convert</param>
    public static implicit operator TimeSpan?(Duration? duration) => duration?.ToTimeSpan();

    /// <summary>
    /// Converts the specified <see cref="TimeSpan"/> into a new <see cref="Duration"/>
    /// </summary>
    /// <param name="timeSpan">The <see cref="TimeSpan"/> to convert</param>
    public static implicit operator Duration?(TimeSpan? timeSpan) => timeSpan == null ? null : FromTimeSpan(timeSpan.Value);

}
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
[Description("Represents a duration")]
[DataContract]
public sealed record Duration
{

    /// <summary>
    /// Gets/sets the number of days, if any
    /// </summary>
    [Description("The number of days, if any")]
    [DataMember(Order = 1, Name = "days"), JsonPropertyOrder(1), JsonPropertyName("days")]
    public uint? Days { get; init; }

    /// <summary>
    /// Gets/sets the number of hours, if any
    /// </summary>
    [Description("The number of hours, if any")]
    [DataMember(Order = 2, Name = "hours"), JsonPropertyOrder(2), JsonPropertyName("hours")]
    public uint? Hours { get; init; }

    /// <summary>
    /// Gets/sets the number of minutes, if any
    /// </summary>
    [Description("The number of minutes, if any")]
    [DataMember(Order = 3, Name = "minutes"), JsonPropertyOrder(3), JsonPropertyName("minutes")]
    public uint? Minutes { get; init; }

    /// <summary>
    /// Gets/sets the number of seconds, if any
    /// </summary>
    [Description("The number of seconds, if any")]
    [DataMember(Order = 4, Name = "seconds"), JsonPropertyOrder(4), JsonPropertyName("seconds")]
    public uint? Seconds { get; init; }

    /// <summary>
    /// Gets/sets the number of milliseconds, if any
    /// </summary>
    [Description("The number of milliseconds, if any")]
    [DataMember(Order = 5, Name = "milliseconds"), JsonPropertyOrder(5), JsonPropertyName("milliseconds")]
    public uint? Milliseconds { get; init; }

    /// <summary>
    /// Gets the the duration's total amount of days
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public double TotalDays => TotalHours / 24;

    /// <summary>
    /// Gets the the duration's total amount of hours
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public double TotalHours => TotalMinutes / 60;

    /// <summary>
    /// Gets the the duration's total amount of minutes
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public double TotalMinutes => TotalSeconds / 60;

    /// <summary>
    /// Gets the the duration's total amount of seconds
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public double TotalSeconds => TotalMilliseconds / 1000;

    /// <summary>
    /// Gets the the duration's total amount of milliseconds
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public uint TotalMilliseconds
    {
        get
        {
            var milliseconds = Days.HasValue ? Days * 24 * 60 * 60 * 1000 : 0;
            milliseconds += Hours.HasValue ? Hours * 60 * 60 * 1000 : 0;
            milliseconds += Minutes.HasValue ? Minutes * 60 * 1000 : 0;
            milliseconds += Seconds.HasValue ? Seconds * 1000 : 0;
            milliseconds += Milliseconds.HasValue ? Milliseconds : 0;
            return milliseconds ?? 0;
        }
    }

    /// <summary>
    /// Converts the <see cref="Duration"/> to a new <see cref="TimeSpan"/>
    /// </summary>
    /// <returns>A new <see cref="TimeSpan"/></returns>
    public TimeSpan ToTimeSpan() => new((int)(Days ?? 0), (int)(Hours ?? 0), (int)(Minutes ?? 0), (int)(Seconds ?? 0), (int)(Milliseconds ?? 0));

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

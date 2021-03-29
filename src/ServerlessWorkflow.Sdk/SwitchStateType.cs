using System.Runtime.Serialization;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Enumerates all types of switch states
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.StringEnumConverterFactory))]
    public enum SwitchStateType
    {
        /// <summary>
        /// Indicates a data switch
        /// </summary>
        [EnumMember(Value = "data")]
        Data,
        /// <summary>
        /// Indicates an event switch
        /// </summary>
        [EnumMember(Value = "event")]
        Event
    }

}

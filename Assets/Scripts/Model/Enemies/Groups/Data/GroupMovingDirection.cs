using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

[JsonConverter(typeof(StringEnumConverter))]
public enum GroupMovingDirection
{
    [EnumMember(Value = "Up")]
    UP,

    [EnumMember(Value = "Down")]
    DOWN
}

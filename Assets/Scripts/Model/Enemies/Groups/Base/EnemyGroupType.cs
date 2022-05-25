using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[JsonConverter(typeof(StringEnumConverter))]
public enum EnemyGroupType
{
    [EnumMember(Value = "StaticCannon")]
    STATIC_CANNONS,

    [EnumMember(Value = "RocketWall")]
    ROCKET_WALL,

    [EnumMember(Value = "MeteorsBelt")]
    METEORS_BELT
}

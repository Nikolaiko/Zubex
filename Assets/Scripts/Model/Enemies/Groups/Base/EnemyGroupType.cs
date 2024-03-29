﻿using System.Runtime.Serialization;
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
    METEORS_BELT,

    [EnumMember(Value = "VerticalSnake")]
    VERTICAL_SNAKE,

    [EnumMember(Value = "SinusoidLines")]
    SINUSOID_LINES,

    [EnumMember(Value = "DiagonalGroup")]
    DIAGONAL_GROUP,

    [EnumMember(Value = "VerticalStaticCannons")]
    VERTICAL_STATIC_CANNONS,

    [EnumMember(Value = "StepGroup")]
    STEP_GROUP
}

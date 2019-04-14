using System;

namespace Hazard
{
    [Flags, Serializable]
    public enum HazardType
    {
        None = 0,
        Fire = 1,
        Water = 2,
        Electricity = 4,
        Rock = 16,
        Salt = 32,
        Sandwich = 64,
        Kerosine = 128,
    }
}
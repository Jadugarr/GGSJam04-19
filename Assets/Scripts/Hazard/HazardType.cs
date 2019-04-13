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
        Blades = 8,
        Rock = 16,
        Pet = 32,
        Salt = 64,
        Blade = 128,
    }
}
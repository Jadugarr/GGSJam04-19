using System;

namespace Hazard
{
    [Flags]
    public enum HazardType
    {
        None = 0,
        Fire = 1,
        Water = 2,
        Electricity = 4,
        Blades = 8,
    }
}
using Hazard;

namespace GameEvents
{
    public class DeathEventParams : IGameEvent
    {
        private HazardType _hazardType;

        public DeathEventParams(HazardType hazardType)
        {
            _hazardType = hazardType;
        }

        public HazardType HazardType
        {
            get { return _hazardType; }
        }
    }
}
using Hazard;

namespace GameEvents
{
    public class AddHazardEventParams : IGameEvent
    {
        private HazardType _hazardType;

        public HazardType HazardType
        {
            get { return _hazardType; }
        }

        public AddHazardEventParams(HazardType hazardType)
        {
            _hazardType = hazardType;
        }
    }
}
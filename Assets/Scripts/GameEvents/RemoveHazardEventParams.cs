using Hazard;

namespace GameEvents
{
    public class RemoveHazardEventParams : IGameEvent
    {
        private HazardType _hazardToRemove;

        public HazardType HazardToRemove
        {
            get { return _hazardToRemove; }
        }

        public RemoveHazardEventParams(HazardType hazardToRemove)
        {
            _hazardToRemove = hazardToRemove;
        }
    }
}
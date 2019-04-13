using Hazard;

namespace GameEvents
{
    public class HazardRemovedEventParams : IGameEvent
    {
        private HazardType _removedHazard;

        public HazardType RemovedHazard
        {
            get { return _removedHazard; }
        }

        public HazardRemovedEventParams(HazardType removedHazard)
        {
            _removedHazard = removedHazard;
        }
    }
}
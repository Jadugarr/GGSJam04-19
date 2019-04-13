using Hazard;

namespace GameEvents
{
    public class HazardAddedEventParams : IGameEvent
    {
        private HazardType _addedHazard;

        public HazardType AddedHazard
        {
            get { return _addedHazard; }
        }

        public HazardAddedEventParams(HazardType addedHazard)
        {
            _addedHazard = addedHazard;
        }
    }
}
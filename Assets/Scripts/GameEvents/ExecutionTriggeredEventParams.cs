using Hazard;

namespace GameEvents
{
    public class ExecutionTriggeredEventParams : IGameEvent
    {
        private HazardType _selectedHazards;

        public ExecutionTriggeredEventParams(HazardType selectedHazards)
        {
            _selectedHazards = selectedHazards;
        }

        public HazardType SelectedHazards
        {
            get { return _selectedHazards; }
        }
    }
}
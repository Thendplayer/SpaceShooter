using Events;

namespace Core.Events
{
    public class LevelOpenedEvent : IBaseEvent
    {
        public int LevelId;
    }
}
using Events;

namespace Core.Events
{
    public class PlayerScoredEvent : IBaseEvent
    {
        public int Score;
    }
}
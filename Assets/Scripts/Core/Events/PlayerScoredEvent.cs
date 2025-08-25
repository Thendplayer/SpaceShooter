using SpaceShooter.Events;

namespace SpaceShooter.Core.Events
{
    public class PlayerScoredEvent : IBaseEvent
    {
        public int Score;
    }
}
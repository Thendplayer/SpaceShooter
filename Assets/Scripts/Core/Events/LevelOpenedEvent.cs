using SpaceShooter.Events;

namespace SpaceShooter.Core.Events
{
    public class LevelOpenedEvent : IBaseEvent
    {
        public int LevelId;
    }
}
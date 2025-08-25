using SpaceShooter.Core.DisplaceableElement.PowerUp;
using SpaceShooter.Events;

namespace SpaceShooter.Core.Events
{
    public class PowerUpActivatedEvent : IBaseEvent
    {
        public PowerUpEffect Effect;
        public int Value;
    }
}
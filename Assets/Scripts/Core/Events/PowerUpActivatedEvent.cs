using Core.DisplaceableElement.PowerUp;
using Events;

namespace Core.Events
{
    public class PowerUpActivatedEvent : IBaseEvent
    {
        public PowerUpEffect Effect;
        public int Value;
    }
}
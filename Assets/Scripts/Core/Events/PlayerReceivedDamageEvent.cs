using Events;

namespace Core.Events
{
    public class PlayerReceivedDamageEvent : IBaseEvent
    {
        public int Damage;
    }
}
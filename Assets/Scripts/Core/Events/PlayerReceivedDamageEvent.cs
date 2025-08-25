using SpaceShooter.Events;

namespace SpaceShooter.Core.Events
{
    public class PlayerReceivedDamageEvent : IBaseEvent
    {
        public int Damage;
    }
}
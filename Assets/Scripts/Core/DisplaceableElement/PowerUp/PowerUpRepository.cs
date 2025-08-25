using SpaceShooter.Objects;
using UnityEngine;

namespace SpaceShooter.Core.DisplaceableElement.PowerUp
{
    public class PowerUpRepository : MonoBehaviour
    {
        [SerializeField] private PowerUpView _view;

        public PowerUpObject Create(PowerUpData data)
        {
            return ObjectFactory.Get<PowerUpObject>(_view, data);
        }
    }
}
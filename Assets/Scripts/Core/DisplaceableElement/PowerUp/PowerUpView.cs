using SpaceShooter.Flow;
using UnityEngine;

namespace SpaceShooter.Core.DisplaceableElement.PowerUp
{
    public class PowerUpView : DisplaceableElementView
    {
        [SerializeField, Layer] private int _layer;
        
        public override int Layer
        {
            get => _layer;
            set => _layer = value;
        }
        
        public override void OnCollision(ICollider other)
        {
            OnCollisionConfirmed?.Invoke();
        }
    }
}
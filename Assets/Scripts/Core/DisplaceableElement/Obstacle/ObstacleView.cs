using SpaceShooter.Core.DisplaceableElement.PowerUp;
using SpaceShooter.Flow;
using SpaceShooter.ServiceRegister;
using UnityEngine;

namespace SpaceShooter.Core.DisplaceableElement.Obstacle
{
    public class ObstacleView : DisplaceableElementView
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
        
        public void InstantiatePowerUp(PowerUpData data)
        {
            var powerUp = ServiceLocator.Instance.GetService<PowerUpRepository>().Create(data);
            powerUp.View.transform.position = Position;
            powerUp.View.transform.up = transform.up;
        }
    }
}
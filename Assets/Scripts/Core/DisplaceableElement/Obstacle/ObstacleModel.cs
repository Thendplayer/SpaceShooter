using Core.Animation;
using Core.DisplaceableElement.PowerUp;
using Objects;
using UnityEngine;

namespace Core.DisplaceableElement.Obstacle
{
    public class ObstacleModel : DisplaceableElementModel
    {
        private int _health;
        private int _score;
        private PowerUpData _powerUpDrop;
        private float _powerUpDropProvability;
        private AnimationData _animationOnDispose;

        public int Health => _health;
        public PowerUpData PowerUpDrop => _powerUpDrop;
        public int Score => _score;
        public AnimationData AnimationOnDispose => _animationOnDispose;

        public override void Configure(ObjectData data)
        {
            base.Configure(data);

            var obstacleData = (ObstacleData)data;
            _health = obstacleData.Lives;
            _powerUpDrop = obstacleData.PowerUpDrop;
            _powerUpDropProvability = obstacleData.PowerUpDropProvability;
            _score = obstacleData.Score;
            _animationOnDispose = obstacleData.AnimationOnDispose;
        }

        public void Hit()
        {
            _health--;
        }
        
        public bool DropPowerUp()
        {
            return Random.Range(0f, 100f) <= _powerUpDropProvability;
        }

        public void ResetPowerUpProvability()
        {
            _powerUpDropProvability = -1;
        }
    }
}
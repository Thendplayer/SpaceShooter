using SpaceShooter.Core.DisplaceableElement.Bullet;
using SpaceShooter.Objects;

namespace SpaceShooter.Core.DisplaceableElement.Obstacle.Enemy
{
    public class EnemyModel : ObstacleModel
    {
        private BulletData _bullet;
        
        private float _originalRecoil;
        private float _currentRecoil;
        private float _recoilTimeLeft;
        private bool _startingLocationReached;
        
        public BulletData Bullet => _bullet;
        public float Recoil => _currentRecoil;
        public bool StartingLocationReached
        {
            get => _startingLocationReached;
            set => _startingLocationReached = value;
        }

        public override void Configure(ObjectData data)
        {
            base.Configure(data);
            
            var simpleEnemyData = (EnemyData)data;
            _recoilTimeLeft = _currentRecoil = _originalRecoil = simpleEnemyData.Recoil;
            _bullet = simpleEnemyData.Bullet;

            _startingLocationReached = false;
        }
        
        public bool UpdateRecoil(float dt)
        {
            _recoilTimeLeft -= dt;
            if (_recoilTimeLeft <= 0)
            {
                _recoilTimeLeft = _currentRecoil;
                return true;
            }

            return false;
        }
    }
}
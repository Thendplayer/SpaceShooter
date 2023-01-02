using Core.Animation;
using Core.DisplaceableElement.Bullet;
using Objects;
using UnityEngine;

namespace Core.Ship
{
    public class ShipModel : ObjectModel
    {
        private BulletData _bullet;
        private AnimationData _damageAnimation;
        private Sprite _sprite;
        private float _radius;
        
        private float _currentRecoil;
        private float _recoilTimeLeft;
        
        private float _shieldTimeLeft;

        private int _activeSpawnPoints;
        private int _bulletsPerSpawnPoint;
        private bool _shieldActive;

        public BulletData Bullet => _bullet;
        public Sprite Sprite => _sprite;
        public float Radius => _radius;
        public bool ShieldActive => _shieldActive;
        public int ActiveSpawnPoints
        {
            get => _activeSpawnPoints;
            set => _activeSpawnPoints = value;
        }

        public int BulletsPerSpawnPoint
        {
            get => _bulletsPerSpawnPoint;
            set => _bulletsPerSpawnPoint = value;
        }

        public AnimationData DamageAnimation => _damageAnimation;

        public override void Configure(ObjectData data)
        {
            var shipData = (ShipData)data;
            _recoilTimeLeft = _currentRecoil = shipData.Recoil;
            _radius = shipData.CollisionRadius;
            _bullet = shipData.Bullet;
            _sprite = shipData.Sprite;
            _damageAnimation = shipData.DamageAnimation;

            _activeSpawnPoints = _bulletsPerSpawnPoint = 1;
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

        public bool UpdateShield(float dt)
        {
            _shieldTimeLeft -= dt;
            if (_shieldTimeLeft <= 0)
            {
                _shieldActive = false;
                _shieldTimeLeft = 0;
                return true;
            }

            return false;
        }

        public void StartShieldCountdown(int time)
        {
            _shieldTimeLeft = time;
            _shieldActive = true;
        }
    }
}
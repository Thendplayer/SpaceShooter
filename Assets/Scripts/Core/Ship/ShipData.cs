using Core.Animation;
using Core.DisplaceableElement.Bullet;
using Objects;
using UnityEngine;

namespace Core.Ship
{
    [CreateAssetMenu(menuName = "Create Data/Ship", fileName = "ShipData")]
    public class ShipData : ObjectData
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _collisionRadius;
        [SerializeField] private float _recoil;
        [SerializeField] private BulletData _bullet;
        [SerializeField] private AnimationData _damageAnimation;

        public Sprite Sprite => _sprite;
        public float CollisionRadius => _collisionRadius;
        public float Recoil => _recoil;
        public BulletData Bullet => _bullet;
        public AnimationData DamageAnimation => _damageAnimation;
    }
}
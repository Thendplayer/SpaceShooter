using UnityEngine;

namespace SpaceShooter.Core.DisplaceableElement.Bullet
{
    [CreateAssetMenu(menuName = "Create Data/Bullet", fileName = "BulletData")]
    public class BulletData : DisplaceableElementData
    {
        [SerializeField] private float _bulletAngleIncremental;

        public float BulletAngleIncremental => _bulletAngleIncremental;
    }
}
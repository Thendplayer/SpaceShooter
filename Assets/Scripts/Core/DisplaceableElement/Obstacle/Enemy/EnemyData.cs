using Core.DisplaceableElement.Bullet;
using UnityEngine;

namespace Core.DisplaceableElement.Obstacle.Enemy
{
    [CreateAssetMenu(menuName = "Create Data/Enemy", fileName = "EnemyData")]
    public class EnemyData : ObstacleData
    {
        [SerializeField] private float _recoil;
        [SerializeField] private BulletData bullet;

        public float Recoil => _recoil;
        public BulletData Bullet => bullet;
    }
}
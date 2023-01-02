using Core.DisplaceableElement.Bullet;
using Core.Level;
using Flow;
using ServiceRegister;
using UnityEngine;

namespace Core.DisplaceableElement.Obstacle.Enemy
{
    public class EnemyView : ObstacleView
    {
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField, Layer] private int _bulletLayer;

        private Vector3 _currentPathPositionHolder;

        private Vector3 _targetPosition;
        private int _targetNode;
        private Path _path;

        public void Shoot(BulletData data)
        {
            var bullet = ServiceLocator.Instance.GetService<BulletRepository>().Create(data);
            ((BulletView)bullet.View).Layer = _bulletLayer;
            
            bullet.View.transform.position = _bulletSpawnPoint.position;
            bullet.View.transform.up = -transform.up;
        }
        
        public void SetPath(Vector3 targetPosition, Path path)
        {
            _targetPosition = targetPosition;
            _path = path;

            _targetNode = 0;
            _currentPathPositionHolder = Position = targetPosition + path.Nodes[0].transform.localPosition;
        }

        public bool UpdatePath(float dt)
        {
            if (Vector3.Distance(Position, _currentPathPositionHolder) > 0.1f)
            {
                Position = Vector3.MoveTowards(Position, _currentPathPositionHolder, dt * _path.Velocity);
                return false;
            }

            if (_targetNode < _path.Nodes.Length - 1)
            {
                _targetNode++;
                
                _currentPathPositionHolder = _targetPosition + _path.Nodes[_targetNode].transform.localPosition;
                Position = Vector3.MoveTowards(Position, _currentPathPositionHolder, dt * _path.Velocity);
                return false;
            }
            
            return true;
        }
    }
}
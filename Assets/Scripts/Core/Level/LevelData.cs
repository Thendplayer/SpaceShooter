using System;
using SpaceShooter.Core.DisplaceableElement.Obstacle;
using SpaceShooter.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Core.Level
{
    [CreateAssetMenu(menuName = "Create Data/Level", fileName = "LevelData")]
    public class LevelData : ObjectData
    {
        [Serializable]
        public class Wave
        {
            [SerializeField] private float _obstacleInstanceRate;
            [SerializeField] private ObstacleData[] _availableObstacles;
            [SerializeField] private float _delayBetweenEntities;
            [SerializeField] private Path _path;
            [SerializeField] private Array2DEnemy _enemyDistribution;

            public float ObstacleInstanceRate => _obstacleInstanceRate;
            public float DelayBetweenEntities => _delayBetweenEntities;
            public Path Path => _path;
            public Array2DEnemy EnemyDistribution => _enemyDistribution;

            public ObstacleData GetObstacle()
            {
                return _availableObstacles[Random.Range(0, _availableObstacles.Length)];
            }
        }

        [SerializeField] private float _delayBetweenWaves;
        [SerializeField] private Wave[] _waves;
        
        public Wave[] Waves => _waves;
        public float DelayBetweenWaves => _delayBetweenWaves;
    }
}
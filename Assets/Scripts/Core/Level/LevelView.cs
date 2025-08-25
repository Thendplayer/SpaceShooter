using System;
using System.Collections;
using SpaceShooter.Core.DisplaceableElement.Obstacle;
using SpaceShooter.Core.DisplaceableElement.Obstacle.Enemy;
using SpaceShooter.Objects;
using SpaceShooter.ServiceRegister;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Core.Level
{
    public class LevelView : ObjectView
    {
        [SerializeField] private Transform _upperLeftCorner;
        [SerializeField] private Transform _obstacleSpawnZone;

        public Action OnWaveCleared;
        
        private bool _waveInstantiated = true;
        private int _entitiesLeft;
        
        private WaitForSeconds _waitForSeconds;
        
        public void InstantiateObstacle(ObstacleData data)
        {
            var enemyObject = ServiceLocator.Instance.GetService<ObstacleRepository>().Create(data);
            enemyObject.View.transform.position = new Vector3(
                Random.Range(_upperLeftCorner.position.x, -_upperLeftCorner.position.x),
                _obstacleSpawnZone.position.y
            );
        }
        
        public void InstantiateWave(LevelData.Wave wave, float delay = 0)
        {
            if(!_waveInstantiated) return;
            
            _waveInstantiated = false;
            _waitForSeconds = new WaitForSeconds(wave.DelayBetweenEntities);
            StartCoroutine(InstantiateWaveCoroutine(wave.EnemyDistribution, wave.Path, delay));
        }

        public void ResetWave() => _waveInstantiated = true;

        private IEnumerator InstantiateWaveCoroutine(Array2DEnemy entities, Path path, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            for (var y = 0; y < entities.GridSize.y; y++)
            {
                for (var x = 0; x < entities.GridSize.x; x++)
                {
                    var data = entities.GetCell(x, y);
                    if (data == null) continue;

                    yield return _waitForSeconds;
                    
                    var enemyObject = ServiceLocator.Instance.GetService<EnemyRepository>().Create(data);
                    var position = new Vector3(
                        _upperLeftCorner.position.x + (data.Diameter + data.Radius) * x,
                        _upperLeftCorner.position.y - (data.Diameter + data.Radius) * y
                    );
                    
                    ((EnemyView)enemyObject.View).SetPath(position, path);

                    void OnEnemyDispose()
                    {
                        enemyObject.Mediator.OnDispose -= OnEnemyDispose;
                        this.OnEnemyDispose();
                    }
                    
                    enemyObject.Mediator.OnDispose += OnEnemyDispose;

                    _entitiesLeft++;
                }
            }

            _waveInstantiated = true;
        }

        private void OnEnemyDispose()
        {
            _entitiesLeft--;
            if (_entitiesLeft <= 0 && _waveInstantiated)
            {
                OnWaveCleared?.Invoke();
            }
        }
    }
}
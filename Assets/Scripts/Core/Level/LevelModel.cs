using SpaceShooter.Objects;

namespace SpaceShooter.Core.Level
{
    public class LevelModel : ObjectModel
    {
        private int _currentWave;
        private float _delayBetweenWaves;
        private LevelData.Wave[] _waves;
        private float _obstacleInstanceTimeLeft;

        public LevelData.Wave CurrentWave => _waves[_currentWave];
        public float DelayBetweenWaves => _delayBetweenWaves;
        
        public override void Configure(ObjectData data)
        {
            var levelData = (LevelData)data;
            _waves = levelData.Waves;
            _delayBetweenWaves = levelData.DelayBetweenWaves;
            _currentWave = 0;
        }

        public bool NextWave()
        {
            if (_currentWave >= _waves.Length - 1) return false;
            
            _currentWave++;
            return true;

        }

        public bool UpdateObstacleInstanceRate(float dt)
        {
            _obstacleInstanceTimeLeft -= dt;
            if (_obstacleInstanceTimeLeft <= 0)
            {
                _obstacleInstanceTimeLeft = CurrentWave.ObstacleInstanceRate;
                return true;
            }

            return false;
        }

        public void RemoveNextWave() => _currentWave = _waves.Length;
    }
}
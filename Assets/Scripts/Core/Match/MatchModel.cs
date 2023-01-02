using Core.Level;
using Core.Ship;
using Objects;
using UnityEngine;

namespace Core.Match
{
    public class MatchModel : ObjectModel
    {
        public int SelectedShip = 0;
        public int SelectedLevel = 0;
        
        public LevelObject CurrentLevel;
        public ShipObject CurrentShip;

        private ShipData[] _ships;
        private LevelData[] _levels;
        private Vector3 _shipStartingPosition;
        
        private int _startingHealth;
        private int _currentHealth;
        private int _currentScore;

        public LevelData[] Levels => _levels;
        public ShipData[] Ships => _ships;
        public Vector3 ShipStartingPosition => _shipStartingPosition;

        public int CurrentHealth => _currentHealth;
        public int CurrentScore => _currentScore;
        
        public override void Configure(ObjectData data)
        {
            var matchData = (MatchData)data;
            
            _levels = matchData.Levels;
            _ships = matchData.Ships;
            _shipStartingPosition = matchData.ShipStartingPosition;
            
            _startingHealth = _currentHealth = matchData.StartingHealth;
            _currentScore = 0;
        }
        
        public void AddScore(int score)
        {
            _currentScore += score;
        }
        
        public void RecoverHealth(int health)
        {
            _currentHealth += health;
        }

        public bool DamageHealth(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _currentHealth = _startingHealth;
            _currentScore = 0;
        }
    }
}
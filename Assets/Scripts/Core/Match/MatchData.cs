using SpaceShooter.Core.Level;
using SpaceShooter.Core.Ship;
using SpaceShooter.Objects;
using UnityEngine;

namespace SpaceShooter.Core.Match
{
    [CreateAssetMenu(menuName = "Create Data/Match", fileName = "MatchData")]
    public class MatchData : ObjectData
    {
        [SerializeField] private int _startingHealth;
        [SerializeField] private Vector3 _shipStartingPosition;
        [SerializeField] private LevelData[] _levels;
        [SerializeField] private ShipData[] _ships;

        public int StartingHealth => _startingHealth;
        public Vector3 ShipStartingPosition => _shipStartingPosition;
        public LevelData[] Levels => _levels;
        public ShipData[] Ships => _ships;
    }
}
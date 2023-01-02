using Core.Level;
using Core.Ship;
using Objects;
using UnityEngine;

namespace Core.Match
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
using SpaceShooter.Core.Animation;
using SpaceShooter.Core.DisplaceableElement.PowerUp;
using UnityEngine;

namespace SpaceShooter.Core.DisplaceableElement.Obstacle
{
    [CreateAssetMenu(menuName = "Create Data/Obstacle", fileName = "ObstacleData")]
    public class ObstacleData : DisplaceableElementData
    {
        [SerializeField] private int _score;
        [SerializeField] private int _lives;
        [SerializeField] private PowerUpData _powerUpDrop;
        [SerializeField, Range(0, 100), Tooltip("%")] private float _powerUpDropProvability;
        [SerializeField] private AnimationData _animationOnDispose;

        public int Score => _score;
        public int Lives => _lives;
        public PowerUpData PowerUpDrop => _powerUpDrop;
        public float PowerUpDropProvability => _powerUpDropProvability;
        public AnimationData AnimationOnDispose => _animationOnDispose;
    }
}
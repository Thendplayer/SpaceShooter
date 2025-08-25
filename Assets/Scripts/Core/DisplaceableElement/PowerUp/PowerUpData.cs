using SpaceShooter.Core.Animation;
using UnityEngine;

namespace SpaceShooter.Core.DisplaceableElement.PowerUp
{
    [CreateAssetMenu(menuName = "Create Data/PowerUp", fileName = "PowerUpData")]
    public class PowerUpData : DisplaceableElementData
    {
        [SerializeField] private PowerUpEffect _effect;
        [SerializeField] private int _value;
        [SerializeField] private AnimationData _animationOnDispose;
        
        public PowerUpEffect Effect => _effect;
        public int Value => _value;
        public AnimationData AnimationOnDispose => _animationOnDispose;
    }
    
    public enum PowerUpEffect
    {
        Shield,
        IncrementSpawnPoints,
        IncrementBulletsPerSpawnPoint,
        RecoverHealth
    }
}
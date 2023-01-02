using Core.Animation;
using Objects;
using UnityEngine;

namespace Core.DisplaceableElement
{
    [CreateAssetMenu(menuName = "Create Data/Displaceable Element", fileName = "DisplaceableElementData")]
    public class DisplaceableElementData : ObjectData
    {
        [SerializeField] private AnimationData _animation;
        [SerializeField] private float _velocity;
        [SerializeField] private float _collisionRadius;
        
        public AnimationData Animation => _animation;
        public float Velocity => _velocity;
        public float Radius => _collisionRadius;
        public float Diameter => _collisionRadius * 2;
    }
}
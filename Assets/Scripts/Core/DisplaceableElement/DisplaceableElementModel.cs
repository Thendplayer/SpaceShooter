using Core.Animation;
using Objects;

namespace Core.DisplaceableElement
{
    public class DisplaceableElementModel : ObjectModel
    {
        private float _velocity;
        private float _collisionRadius;
        private AnimationData _animation;
        
        public AnimationObject CurrentAnimation;

        public float Velocity => _velocity;
        public float CollisionRadius => _collisionRadius;
        public AnimationData Animation => _animation;

        public override void Configure(ObjectData data)
        {
            var castData = (DisplaceableElementData)data;
            
            _velocity = castData.Velocity;
            _collisionRadius = castData.Radius;
            _animation = castData.Animation;
        }
    }
}
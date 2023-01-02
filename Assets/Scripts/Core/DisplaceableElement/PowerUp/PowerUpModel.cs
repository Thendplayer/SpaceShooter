using Core.Animation;
using Objects;

namespace Core.DisplaceableElement.PowerUp
{
    public class PowerUpModel : DisplaceableElementModel
    {
        private PowerUpEffect _effect;
        private int _value;
        private AnimationData _animationOnDispose;

        public PowerUpEffect Effect => _effect;
        public int Value => _value;
        public AnimationData AnimationOnDispose => _animationOnDispose;
        
        public override void Configure(ObjectData data)
        {
            base.Configure(data);

            var powerUpData = (PowerUpData) data;
            _effect = powerUpData.Effect;
            _value = powerUpData.Value;
            _animationOnDispose = powerUpData.AnimationOnDispose;
        }
    }
}
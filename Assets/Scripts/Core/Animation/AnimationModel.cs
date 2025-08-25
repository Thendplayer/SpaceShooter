using SpaceShooter.Objects;
using UnityEngine;

namespace SpaceShooter.Core.Animation
{
    public class AnimationModel : ObjectModel
    {
        private int _orderInLayer;
        private Sprite[] _frames;
        private float _frameRate;
        private bool _loop;
        private float _scale;

        public int OrderInLayer => _orderInLayer;
        public Sprite[] Frames => _frames;
        public float FrameRate => _frameRate;
        public bool Loop => _loop;
        public float Scale => _scale;

        public override void Configure(ObjectData data)
        {
            var animationData = (AnimationData)data;
            
            _frames = animationData.Frames;
            _frameRate = animationData.FrameRate;
            _loop = animationData.Loop;
            _orderInLayer = animationData.OrderInLayer;
            _scale = animationData.Scale;
        }
    }
}
using SpaceShooter.Objects;
using UnityEngine;

namespace SpaceShooter.Core.Animation
{
    [CreateAssetMenu(menuName = "Create Data/Animation", fileName = "AnimationData")]
    public class AnimationData : ObjectData
    {
        [SerializeField] private int _orderInLayer;
        [SerializeField] private Sprite[] _frames;
        [SerializeField] private float _frameRate;
        [SerializeField] private bool _loop;
        [SerializeField] private float _scale = 1;

        public int OrderInLayer => _orderInLayer;
        public Sprite[] Frames => _frames;
        public float FrameRate => _frameRate;
        public bool Loop => _loop;
        public float Scale => _scale;
    }
}
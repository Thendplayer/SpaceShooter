using SpaceShooter.Objects;
using UnityEngine;

namespace SpaceShooter.Core.Animation
{
    public class AnimationRepository : MonoBehaviour
    {
        [SerializeField] private AnimationView _view;

        public AnimationObject Create(AnimationData data)
        {
            return ObjectFactory.Get<AnimationObject>(_view, data);
        }
    }
}
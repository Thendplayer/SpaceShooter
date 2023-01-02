using System;
using Core.Animation;
using Flow;
using Objects;
using ServiceRegister;
using UnityEngine;

namespace Core.DisplaceableElement
{
    public abstract class DisplaceableElementView : ObjectView, ICollider
    {
        public Action OnCollisionConfirmed;

        public abstract int Layer { get; set; }
        public float Radius { get; set; }
        public Vector3 Forward => transform.up;
        public Vector3 Position
        {
            get => transform.position;
            set
            {
                value.z = transform.position.z;
                transform.position = value;
            }
        }

        public abstract void OnCollision(ICollider collider);

        public AnimationObject SetAnimation(AnimationData data)
        {
            var animationObject = ServiceLocator.Instance.GetService<AnimationRepository>().Create(data);
            animationObject.View.transform.parent = transform;
            animationObject.View.transform.localPosition = Vector3.zero;
            animationObject.View.transform.localRotation = Quaternion.identity;
            return animationObject;
        }
        
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}
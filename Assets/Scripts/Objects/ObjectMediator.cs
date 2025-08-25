using System;

namespace SpaceShooter.Objects
{
    public abstract class ObjectMediator
    {
        public Action OnDispose;
        protected ObjectMediator(ObjectView view, ObjectModel model) { }
        public abstract void Configure();

        public virtual void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}
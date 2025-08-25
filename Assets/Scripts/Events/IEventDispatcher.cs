using System;

namespace SpaceShooter.Events
{
    public interface IEventDispatcher
    {
        void Subscribe<T>(Action<T> callback) where T : IBaseEvent;
        void Unsubscribe<T>(Action<T> callback) where T : IBaseEvent;
        void Dispatch<T>(T arg = default) where T : IBaseEvent;
    }
    
    public interface IBaseEvent { }
}
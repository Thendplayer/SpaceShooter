using SpaceShooter.Core.Animation;
using SpaceShooter.Core.Events;
using SpaceShooter.Events;
using SpaceShooter.Flow;
using SpaceShooter.ServiceRegister;
using UnityEngine;

namespace SpaceShooter.Core.DisplaceableElement.Obstacle
{
    public class ObstacleMediator : DisplaceableElementMediator, IUpdatable
    {
        private new readonly ObstacleView _view;
        private new readonly ObstacleModel _model;
        protected readonly ServiceLocator _serviceLocator;

        public ObstacleMediator(ObstacleView view, ObstacleModel model) : base(view, model)
        {
            _view = view;
            _model = model;
            _serviceLocator = ServiceLocator.Instance;
        }

        public override void Configure()
        {
            base.Configure();
            
            _view.OnCollisionConfirmed += Hit;
            Application.quitting += OnApplicationQuit;
        }

        public override void Dispose()
        {
            if (_model.DropPowerUp())
                _view.InstantiatePowerUp(_model.PowerUpDrop);
            
            _serviceLocator.GetService<EventDispatcher>().Dispatch(new PlayerScoredEvent
            {
                Score = _model.Score
            });
            
            _view.OnCollisionConfirmed -= Hit;
            Application.quitting -= OnApplicationQuit;

            base.Dispose();
        }

        public virtual void OnUpdate(float dt)
        {
            _view.Position += -_view.Forward * _model.Velocity * dt;
        }

        private void Hit()
        {
            _model.Hit();
            if (_model.Health <= 0)
            {
                var animation = _serviceLocator.GetService<AnimationRepository>().Create(_model.AnimationOnDispose);
                animation.View.transform.position = _view.Position;
                Dispose();
            }
        }

        protected override void LevelDispatched(LevelClosedEvent @event)
        {
            _model.ResetPowerUpProvability();
            base.LevelDispatched(@event);
        }

        private void OnApplicationQuit() => LevelDispatched(null);
    }
}
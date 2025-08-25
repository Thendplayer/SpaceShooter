using SpaceShooter.Core.Animation;
using SpaceShooter.Core.Events;
using SpaceShooter.Events;
using SpaceShooter.Objects;
using SpaceShooter.ServiceRegister;

namespace SpaceShooter.Core.DisplaceableElement
{
    public class DisplaceableElementMediator : ObjectMediator
    {
        protected readonly DisplaceableElementView _view;
        protected readonly DisplaceableElementModel _model;
        protected readonly EventDispatcher _eventDispatcher;

        protected DisplaceableElementMediator(DisplaceableElementView view, DisplaceableElementModel model) : base(view, model)
        {
            _view = view;
            _model = model;
            _eventDispatcher = ServiceLocator.Instance.GetService<EventDispatcher>();
        }
        
        public override void Configure()
        {
            _view.Radius = _model.CollisionRadius;
            _model.CurrentAnimation = _view.SetAnimation(_model.Animation);
            _model.CurrentAnimation.Mediator.OnDispose += OnAnimationDisposed;
            
            _eventDispatcher.Subscribe<LevelClosedEvent>(LevelDispatched);
        }

        public override void Dispose()
        {
            if (_model.CurrentAnimation != null)
            {
                _model.CurrentAnimation.Mediator.OnDispose -= OnAnimationDisposed;
                ((AnimationView)_model.CurrentAnimation.View).Terminate();
                _model.CurrentAnimation = null;
            }
            
            _eventDispatcher.Unsubscribe<LevelClosedEvent>(LevelDispatched);

            base.Dispose();
        }

        protected virtual void LevelDispatched(LevelClosedEvent @event) => Dispose();

        private void OnAnimationDisposed()
        {
            _model.CurrentAnimation.Mediator.OnDispose -= OnAnimationDisposed;
            _model.CurrentAnimation = null;
            Dispose();
        }
    }
}
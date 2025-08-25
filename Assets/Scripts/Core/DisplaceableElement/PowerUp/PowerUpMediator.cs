using SpaceShooter.Core.Animation;
using SpaceShooter.Core.Events;
using SpaceShooter.Events;
using SpaceShooter.Flow;
using SpaceShooter.ServiceRegister;

namespace SpaceShooter.Core.DisplaceableElement.PowerUp
{
    public class PowerUpMediator : DisplaceableElementMediator, IUpdatable
    {
        private new readonly PowerUpModel _model;
        private readonly PowerUpActivatedEvent _event;
        
        public PowerUpMediator(PowerUpView view, PowerUpModel model) : base(view, model)
        {
            _model = model;
            _event = new PowerUpActivatedEvent();
        }
        
        public override void Configure()
        {
            base.Configure();

            _view.OnCollisionConfirmed += Hit;
        }

        public override void Dispose()
        {
            _view.OnCollisionConfirmed -= Hit;
            
            base.Dispose();
        }

        public virtual void OnUpdate(float dt)
        {
            _view.Position += -_view.Forward * _model.Velocity * dt;
        }

        private void Hit()
        {
            _event.Effect = _model.Effect;
            _event.Value = _model.Value;
            
            ServiceLocator.Instance.GetService<EventDispatcher>().Dispatch(_event);
            
            var animation = ServiceLocator.Instance.GetService<AnimationRepository>().Create(_model.AnimationOnDispose);
            animation.View.transform.position = _view.Position;
            
            Dispose();
        }
    }
}
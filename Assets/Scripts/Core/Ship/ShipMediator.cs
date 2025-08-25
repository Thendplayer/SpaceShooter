using SpaceShooter.Audio;
using SpaceShooter.Core.Animation;
using SpaceShooter.Core.DisplaceableElement.PowerUp;
using SpaceShooter.Core.Events;
using SpaceShooter.Events;
using SpaceShooter.Flow;
using SpaceShooter.Objects;
using SpaceShooter.ServiceRegister;

namespace SpaceShooter.Core.Ship
{
    public class ShipMediator : ObjectMediator, IUpdatable
    {
        private readonly ShipView _view;
        private readonly ShipModel _model;
        private readonly ServiceLocator _serviceLocator;
        private readonly EventDispatcher _eventDispatcher;
        
        public ShipMediator(ShipView view, ShipModel model) : base(view, model)
        {
            _view = view;
            _model = model;
            _serviceLocator = ServiceLocator.Instance;
            _eventDispatcher = _serviceLocator.GetService<EventDispatcher>();
        }

        public override void Configure()
        {
            _eventDispatcher.Subscribe<PowerUpActivatedEvent>(OnPowerUpActivated);
            _view.OnCollisionConfirmed += OnDamaged;

            _view.Sprite = _model.Sprite;
            _view.ActiveShield(false);
            _view.Radius = _model.Radius;
        }

        public override void Dispose()
        {
            _eventDispatcher.Unsubscribe<PowerUpActivatedEvent>(OnPowerUpActivated);
            _view.OnCollisionConfirmed -= OnDamaged;
            
            base.Dispose();
        }

        public void OnUpdate(float dt)
        {
            _view.UpdatePosition();
            if (_model.UpdateRecoil(dt))
            {
                _view.Shoot(_model.Bullet, _model.ActiveSpawnPoints, _model.BulletsPerSpawnPoint);
            }

            if (!_model.ShieldActive) return;
            if (_model.UpdateShield(dt))
            {
                _view.ActiveShield(false);
            }
        }
        
        private void OnDamaged()
        {
            if (_model.ShieldActive) return;
            
            _serviceLocator.GetService<AudioService>().Play(AudioTrack.Damage);
            var animation = _serviceLocator.GetService<AnimationRepository>().Create(_model.DamageAnimation);
            animation.View.transform.position = _view.Position;
            animation.View.transform.SetParent(_view.transform);
            
            _eventDispatcher.Dispatch(new PlayerReceivedDamageEvent
            {
                Damage = 1
            });
        }

        private void OnPowerUpActivated(PowerUpActivatedEvent @event)
        {
            _serviceLocator.GetService<AudioService>().Play(AudioTrack.Collect);

            switch (@event.Effect)
            {
                case PowerUpEffect.Shield:
                    _view.ActiveShield(true);
                    _model.StartShieldCountdown(@event.Value);
                    break;
                case PowerUpEffect.IncrementSpawnPoints:
                    _model.ActiveSpawnPoints += @event.Value;
                    break;
                case PowerUpEffect.IncrementBulletsPerSpawnPoint:
                    _model.BulletsPerSpawnPoint += @event.Value;
                    break;
            }
        }
    }
}
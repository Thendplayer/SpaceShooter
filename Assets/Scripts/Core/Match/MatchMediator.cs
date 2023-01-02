using System.Linq;
using Audio;
using Core.DisplaceableElement.PowerUp;
using Core.Events;
using Core.Level;
using Core.Ship;
using Events;
using Flow;
using Objects;
using ServiceRegister;
using UnityEngine;

namespace Core.Match
{
    public class MatchMediator : ObjectMediator
    {
        private readonly MatchView _view;
        private readonly MatchModel _model;
        
        private readonly ServiceLocator _serviceLocator;
        private readonly EventDispatcher _eventDispatcher;

        public MatchMediator(MatchView view, MatchModel model) : base(view, model)
        {
            _view = view;
            _model = model;
            _serviceLocator = ServiceLocator.Instance;
            _eventDispatcher = _serviceLocator.GetService<EventDispatcher>();

            _view.Init();
        }

        public override void Configure()
        {
            _view.Configure(_model.Levels.Length, _model.Ships.Select(ship => ship.Sprite).ToArray());
            _view.CanvasCamera = _serviceLocator.GetService<Camera>();

            _view.OnPause += OnPause;
            _view.OnOpenLevel += OnOpenLevel;
            _view.OnShipSelected += OnShipSelected;
            _view.OnCloseLevel += OnCloseLevel;
            _view.OnReplayLevel += OnReplayLevel;
            _view.OnCloseGame += OnApplicationQuit;
            
            _eventDispatcher.Subscribe<PlayerScoredEvent>(OnPlayerScored);
            _eventDispatcher.Subscribe<PlayerReceivedDamageEvent>(OnPlayerReceivedDamage);
            _eventDispatcher.Subscribe<LevelCompleted>(OnCompleteLevel);
            _eventDispatcher.Subscribe<PowerUpActivatedEvent>(OnPowerUpActivated);
        }

        public override void Dispose()
        {
            _eventDispatcher.Unsubscribe<PlayerScoredEvent>(OnPlayerScored);
            _eventDispatcher.Unsubscribe<PlayerReceivedDamageEvent>(OnPlayerReceivedDamage);
            _eventDispatcher.Unsubscribe<LevelCompleted>(OnCompleteLevel);
            _eventDispatcher.Unsubscribe<PowerUpActivatedEvent>(OnPowerUpActivated);

            _view.OnPause -= OnPause;
            _view.OnOpenLevel -= OnOpenLevel;
            _view.OnShipSelected -= OnShipSelected;
            _view.OnCloseLevel -= OnCloseLevel;
            _view.OnReplayLevel -= OnReplayLevel;
            _view.OnCloseGame -= OnApplicationQuit;

            base.Dispose();
        }
        
        private void OnPlayerScored(PlayerScoredEvent @event)
        {
            _model.AddScore(@event.Score);
            _view.Score = _model.CurrentScore;
        }
        
        private void OnPlayerReceivedDamage(PlayerReceivedDamageEvent @event)
        {
            var gameOver = _model.DamageHealth(@event.Damage);
            _view.Health = _model.CurrentHealth;
            
            if (gameOver)
            {
                OnPause(true);
                _view.ShowGameOverScreen(false, _model.CurrentScore);
                ServiceLocator.Instance.GetService<AudioService>().Play(AudioTrack.Loose);
            }
        }
        
        private void OnPowerUpActivated(PowerUpActivatedEvent @event)
        {
            if (@event.Effect != PowerUpEffect.RecoverHealth) return;

            _model.RecoverHealth(@event.Value);
            _view.Health = _model.CurrentHealth;
        }
        
        private void OnPause(bool isPause)
        {
            MainFlow.Pause = isPause;
            
            if (isPause) _view.ShowPauseScreen();
            else _view.ShowInGameScreen();
        }

        private void OnOpenLevel(int levelId)
        {
            _eventDispatcher.Dispatch(new LevelOpenedEvent
            {
                LevelId = levelId
            });
            
            _model.Reset();
            _view.Health = _model.CurrentHealth;
            _view.Score = 0;
            
            _view.ShowInGameScreen();
            _model.SelectedLevel = levelId;
            _model.CurrentLevel = _serviceLocator.GetService<LevelRepository>().Create(_model.Levels[levelId]);
            _model.CurrentShip = _serviceLocator.GetService<ShipRepository>().Create(_model.Ships[_model.SelectedShip]);
            _model.CurrentShip.View.transform.position = _model.ShipStartingPosition;
        }

        private void OnCloseLevel()
        {
            _eventDispatcher.Dispatch(new LevelClosedEvent());
            
            OnPause(false);
            _view.ShowLevelSelectorScreen();

            _model.CurrentShip?.Mediator.Dispose();
            _model.CurrentShip = null;
            
            _model.CurrentLevel?.Mediator.Dispose();
            _model.CurrentLevel = null;
        }
        
        private void OnCompleteLevel(LevelCompleted @event)
        {
            MainFlow.Pause = true;
            _view.ShowGameOverScreen(true, _model.CurrentScore);
            ServiceLocator.Instance.GetService<AudioService>().Play(AudioTrack.Win);
        }

        private void OnShipSelected(int shipId)
        {
            _model.SelectedShip = shipId;
            _view.ShowMainScreen();
        }

        private void OnReplayLevel()
        {
            _eventDispatcher.Dispatch(new LevelClosedEvent());
            
            _model.CurrentLevel?.Mediator.Dispose();
            _model.CurrentLevel = _serviceLocator.GetService<LevelRepository>().Create(_model.Levels[_model.SelectedLevel]);
            
            _model.CurrentShip?.Mediator.Dispose();
            _model.CurrentShip = _serviceLocator.GetService<ShipRepository>().Create(_model.Ships[_model.SelectedShip]);
            _model.CurrentShip.View.transform.position = _model.ShipStartingPosition;
            
            MainFlow.Pause = false;
            
            _model.Reset();
            _view.Health = _model.CurrentHealth;
            _view.Score = 0;
            
            _view.ShowInGameScreen();
        }
        
        private void OnApplicationQuit() => Application.Quit();
    }
}
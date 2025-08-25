using SpaceShooter.Core.DisplaceableElement.PowerUp;
using SpaceShooter.Core.Events;
using SpaceShooter.Events;
using SpaceShooter.Flow;
using SpaceShooter.Objects;
using SpaceShooter.ServiceRegister;
using UnityEngine;

namespace SpaceShooter.Core.Level
{
    public class LevelMediator : ObjectMediator, IUpdatable
    {
        private readonly LevelView _view;
        private readonly LevelModel _model;

        public LevelMediator(LevelView view, LevelModel model) : base(view, model)
        {
            _view = view;
            _model = model;
        }

        public override void Configure()
        {
            Application.quitting += OnApplicationQuit;
            _view.OnWaveCleared += NextWave;
            
            _view.ResetWave();
            _view.InstantiateWave(_model.CurrentWave);
        }

        public override void Dispose()
        {
            _view.OnWaveCleared -= NextWave;
            Application.quitting -= OnApplicationQuit;
            
            base.Dispose();
        }

        private void NextWave()
        {
            var nextWaveExists = _model.NextWave();
            if (nextWaveExists)
            {
                _view.InstantiateWave(_model.CurrentWave, _model.DelayBetweenWaves);
                return;
            }
            
            ServiceLocator.Instance.GetService<EventDispatcher>().Dispatch(new LevelCompleted());
            Dispose();
        }

        public void OnUpdate(float dt)
        {
            var instantiateObstacle = _model.UpdateObstacleInstanceRate(dt);
            if (instantiateObstacle)
            {
                _view.InstantiateObstacle(_model.CurrentWave.GetObstacle());
            }
        }

        private void OnApplicationQuit() => _model.RemoveNextWave();
    }
}
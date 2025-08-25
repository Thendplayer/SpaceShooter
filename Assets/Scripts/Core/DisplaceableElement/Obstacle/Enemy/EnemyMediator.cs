namespace SpaceShooter.Core.DisplaceableElement.Obstacle.Enemy
{
    public class EnemyMediator : ObstacleMediator
    {
        private new readonly EnemyView _view;
        private new readonly EnemyModel _model;

        public EnemyMediator(EnemyView view, EnemyModel model) : base(view, model)
        {
            _view = view;
            _model = model;
        }

        public override void OnUpdate(float dt)
        {
            if (_model.StartingLocationReached)
            {
                base.OnUpdate(dt);
                if (_model.UpdateRecoil(dt))
                {
                    _view.Shoot(_model.Bullet);
                }
                
                return;
            }
            
            _model.StartingLocationReached = _view.UpdatePath(dt);
        }
    }
}
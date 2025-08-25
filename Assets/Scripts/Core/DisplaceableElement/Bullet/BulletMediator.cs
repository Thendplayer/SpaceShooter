using SpaceShooter.Flow;

namespace SpaceShooter.Core.DisplaceableElement.Bullet
{
    public class BulletMediator : DisplaceableElementMediator, IUpdatable
    {
        public BulletMediator(BulletView view, DisplaceableElementModel model) : base(view, model) { }

        public override void Configure()
        {
            base.Configure();
            
            _view.OnCollisionConfirmed += Dispose;
        }

        public override void Dispose()
        {
            _view.OnCollisionConfirmed -= Dispose;
            
            base.Dispose();
        }

        public void OnUpdate(float dt)
        {
            _view.Position += _view.Forward * _model.Velocity * dt;
        }
    }
}
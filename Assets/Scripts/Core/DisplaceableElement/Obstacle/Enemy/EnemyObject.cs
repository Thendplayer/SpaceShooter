using Flow;
using Objects;

namespace Core.DisplaceableElement.Obstacle.Enemy
{
    public class EnemyObject : DisplaceableElementObject
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new EnemyModel();
            _mediator = new EnemyMediator((EnemyView)View, (EnemyModel)Model);
        }
        
        protected override void OnConfigure(ObjectData data)
        {
            MainFlow.Instance.Attach(View, Mediator);
        }

        protected override void OnDispose()
        {
            MainFlow.Instance.Release(View, Mediator);
            ObjectFactory.Store(this);
        }
    }
}
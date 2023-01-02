using Flow;
using Objects;

namespace Core.DisplaceableElement.Obstacle
{
    public class ObstacleObject : DisplaceableElementObject
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new ObstacleModel();
            _mediator = new ObstacleMediator((ObstacleView)View, (ObstacleModel)Model);
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
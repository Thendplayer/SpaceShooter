using Flow;
using Objects;

namespace Core.Ship
{
    public class ShipObject : ObjectRepresentation
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new ShipModel();
            _mediator = new ShipMediator((ShipView)View, (ShipModel)Model);
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
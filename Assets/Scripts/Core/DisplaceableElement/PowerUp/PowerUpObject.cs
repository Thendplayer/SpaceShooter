using Flow;
using Objects;

namespace Core.DisplaceableElement.PowerUp
{
    public class PowerUpObject : DisplaceableElementObject
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new PowerUpModel();
            _mediator = new PowerUpMediator((PowerUpView)View, (PowerUpModel)Model);
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
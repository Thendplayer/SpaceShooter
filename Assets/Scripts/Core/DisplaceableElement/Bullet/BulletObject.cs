using Core.DisplaceableElement.Bullet;
using Flow;
using Objects;

namespace Core.DisplaceableElement
{
    public class BulletObject : DisplaceableElementObject
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new DisplaceableElementModel();
            _mediator = new BulletMediator((BulletView)View, (DisplaceableElementModel)Model);
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
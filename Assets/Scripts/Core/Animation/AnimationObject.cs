using SpaceShooter.Flow;
using SpaceShooter.Objects;

namespace SpaceShooter.Core.Animation
{
    public class AnimationObject : ObjectRepresentation
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new AnimationModel();
            _mediator = new AnimationMediator((AnimationView)View, (AnimationModel)Model);
        }

        protected override void OnConfigure(ObjectData data)
        {
            MainFlow.Instance.Attach(View);
        }

        protected override void OnDispose()
        {
            MainFlow.Instance.Release(View);
            ObjectFactory.Store(this);
        }
    }
}
using SpaceShooter.Flow;
using SpaceShooter.Objects;

namespace SpaceShooter.Core.Level
{
    public class LevelObject : ObjectRepresentation
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new LevelModel();
            _mediator = new LevelMediator((LevelView)View, (LevelModel)Model);
        }
        
        protected override void OnConfigure(ObjectData data)
        {
            MainFlow.Instance.Attach(Mediator);
        }

        protected override void OnDispose()
        {
            MainFlow.Instance.Release(Mediator);
            ObjectFactory.Store(this);
        }
    }
}
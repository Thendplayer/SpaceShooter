using SpaceShooter.Objects;

namespace SpaceShooter.Core.Match
{
    public class MatchObject : ObjectRepresentation
    {
        protected override void OnCreate(ObjectView view)
        {
            _view = view;
            _model = new MatchModel();
            _mediator = new MatchMediator((MatchView)View, (MatchModel)Model);
        }
        
        protected override void OnConfigure(ObjectData data) { }

        protected override void OnDispose()
        {
            ObjectFactory.Store(this);
        }
    }
}
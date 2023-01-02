namespace Objects
{
    public abstract class ObjectRepresentation
    {
        protected ObjectView _view;
        protected ObjectModel _model;
        protected ObjectMediator _mediator;
        
        public ObjectView View => _view;
        public ObjectModel Model => _model;
        public ObjectMediator Mediator => _mediator;
        
        protected abstract void OnCreate(ObjectView view);
        protected abstract void OnConfigure(ObjectData data);
        protected abstract void OnDispose();

        public void Create(ObjectView view, ObjectData data)
        {
            OnCreate(view);
            Configure(data);
            
            Mediator.OnDispose += OnDispose;
        }

        public void Configure(ObjectData data)
        {
            Model.Configure(data);
            Mediator.Configure();
            
            OnConfigure(data);
        }
    }
}
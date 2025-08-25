using System.Collections.Generic;
using SpaceShooter.Objects;
using UnityEngine;

namespace SpaceShooter.Core.Animation
{
    public class AnimationMediator : ObjectMediator
    {
        private readonly AnimationView _view;
        private readonly AnimationModel _model;

        public AnimationMediator(AnimationView view, AnimationModel model) : base(view, model)
        {
            _view = view;
            _model = model;
        }

        public override void Configure()
        {
            _view.OnAnimationFinished += Dispose;
            _view.Play(_model.Frames, _model.FrameRate, _model.OrderInLayer, _model.Loop);
            _view.transform.localScale = new Vector3(_model.Scale, _model.Scale, _model.Scale);
        }

        public override void Dispose()
        {
            _view.OnAnimationFinished -= Dispose;
            
            base.Dispose();
        }
    }
}
using System;
using Flow;
using Objects;
using UnityEngine;

namespace Core.Animation
{
    public class AnimationView : ObjectView, IUpdatable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Action OnAnimationFinished;

        private Sprite[] _frames;
        private int _lastFrame;
        
        private float _currentFrame, _frameRate;
        private bool _playing, _loop;

        private bool IsParentActive => transform.parent == null || transform.parent.gameObject.activeSelf;

        public void Play(Sprite[] frames, float frameRate, int orderInLayer, bool loop = false)
        {
            if (frames.Length == 0)
            {
                Terminate();
                return;
            }

            if (frames.Length == 1)
            {
                _playing = false;
                _spriteRenderer.sortingOrder = orderInLayer;
            }
            else
            {
                _frames = frames;
                _frameRate = frameRate;

                _loop = loop;
                _playing = true;

            }
            
            _currentFrame = 0;
            _spriteRenderer.sprite = frames[0];
            _spriteRenderer.sortingOrder = orderInLayer;
        }
        
        public void OnUpdate(float dt)
        {
            if (!_playing) return;
            _currentFrame += _frameRate * dt;
            
            if(_currentFrame >= _frames.Length) 
            {
                _currentFrame -= _frames.Length;
                
                if(!_loop) 
                {
                    Terminate();
                    return;
                }
            }
            
            if (_lastFrame != (int)_currentFrame)
            {
                _lastFrame = (int)_currentFrame;
                _spriteRenderer.sprite = _frames[_lastFrame];
            }
        }
        
        public void Terminate()
        {
            transform.SetParent(null);
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            
            _playing = false;
            OnAnimationFinished?.Invoke();
        }

        private void OnBecameInvisible()
        {
            if (!IsParentActive) return;
            
            Terminate();
        }
    }
}
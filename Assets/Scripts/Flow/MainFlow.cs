using System.Collections.Generic;
using UnityEngine;

namespace Flow
{
    public class MainFlow : MonoBehaviour
    {
        private static MainFlow _instance;
        public static MainFlow Instance => _instance ??= new GameObject(nameof(MainFlow)).AddComponent<MainFlow>();
        
        private Dictionary<int, LayerMask> _collisionMatrix;
        public Dictionary<int, LayerMask> CollisionMatrix => _collisionMatrix ??= Resources.LoadAll<CollisionMatrix>("")[0].Get();

        public static bool Pause = false;
        
        private readonly List<IUpdatable> _updatableObjects = new List<IUpdatable>();
        private readonly List<ICollider> _colliderObjects = new List<ICollider>();

        public void Attach(params object[] objs)
        {
            for (var i = 0; i < objs.Length; i++)
            {
                Attach(objs[i]);
            }
        }
        
        public void Attach(object obj)
        {
            if (obj is IUpdatable updatable)
            {
                if (_updatableObjects.Contains(updatable)) return;
                _updatableObjects.Add(updatable);
            }
            
            if (obj is ICollider collider)
            {
                if (_colliderObjects.Contains(collider)) return;
                _colliderObjects.Add(collider);
            }
        }
        
        public void Release(params object[] objs)
        {
            for (var i = 0; i < objs.Length; i++)
            {
                Release(objs[i]);
            }
        }
        
        public void Release(object obj)
        {
            if (obj is IUpdatable updatable)
            {
                if (!_updatableObjects.Contains(updatable)) return;
                _updatableObjects.Remove(updatable);
            }
            
            if (obj is ICollider collider)
            {
                if (!_colliderObjects.Contains(collider)) return;
                _colliderObjects.Remove(collider);
            }
        }
        
        private void Update()
        {
            if (Pause) return;
            
            var dt = Time.deltaTime;
            for (var i = 0; i < _updatableObjects.Count; i++)
            {
                _updatableObjects[i].OnUpdate(dt);
            }
            
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (var i = 0; i < _colliderObjects.Count - 1; i++)
            {
                for (var j = i + 1; j < _colliderObjects.Count; j++)
                {
                    var distance = Vector3.Distance(_colliderObjects[i].Position, _colliderObjects[j].Position);
                    if (distance <= _colliderObjects[i].Radius + _colliderObjects[j].Radius)
                    {
                        var collider1 = _colliderObjects[i];
                        var collider2 = _colliderObjects[j];
                        
                        var layerMask = CollisionMatrix[collider1.Layer].value;
                        if ((layerMask | 1 << collider2.Layer) == layerMask)
                        {
                            collider1.OnCollision(collider2);
                        }
                        
                        layerMask = CollisionMatrix[collider2.Layer].value;
                        if ((layerMask | 1 << collider1.Layer) == layerMask)
                        {
                            collider2.OnCollision(collider1);
                        }
                    }
                }
            }
        }
    }
}
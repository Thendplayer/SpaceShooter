using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public static class ObjectFactory
    {
        public static T Get<T>(ObjectView prefab, ObjectData data) where T : ObjectRepresentation, new()
        {
            var representation = FactoryPool.Get<T>();
            if (representation != null)
            {
                representation.View.SetActive(true);
                representation.Configure(data);
                return (T)representation;
            }

            var newRepresentation = new T();

            var viewInstance = UnityEngine.Object.Instantiate(prefab);
            newRepresentation.Create(viewInstance, data);
            return newRepresentation;
        }

        public static void Store<T>(T representation) where T : ObjectRepresentation, new()
        {
            if (representation.View == null) return;
            
            representation.View.SetActive(false);
            FactoryPool.Store<T>(representation);
        }
        
        private static class FactoryPool
        {
            private static readonly Dictionary<Type, Stack<ObjectRepresentation>> _pooledObjects = new Dictionary<Type, Stack<ObjectRepresentation>>();

            public static ObjectRepresentation Get<T>()
            {
                if (!_pooledObjects.TryGetValue(typeof(T), out var representationOfTypeT))
                    return null;
        
                if (representationOfTypeT.Count <= 0)
                    return null;
        
                return representationOfTypeT.Pop();
            }
    
            public static void Store<T>(ObjectRepresentation representation)
            {
                if (!_pooledObjects.TryGetValue(typeof(T), out var pairStackOfTypeT))
                {
                    var stack = new Stack<ObjectRepresentation>();
                    stack.Push(representation);
                    _pooledObjects.Add(typeof(T), stack);
                }
                else
                {
                    pairStackOfTypeT.Push(representation);
                }
            }
        }
    }
}
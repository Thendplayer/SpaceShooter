using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Flow
{
    [CreateAssetMenu(menuName = "Collision Matrix", fileName = "CollisionMatrix", order = 0)]
    public class CollisionMatrix : ScriptableObject 
    {
        [Serializable]
        public struct Relation
        {
            [Layer] public int Layer;
            public LayerMask AffectedLayers;
        }

        [SerializeField] private Relation[] _collisions;

        public Dictionary<int, LayerMask> Get()
        {
            return _collisions.ToDictionary(value => value.Layer, value => value.AffectedLayers);
        }
    }
    
    public class LayerAttribute : PropertyAttribute { }
}
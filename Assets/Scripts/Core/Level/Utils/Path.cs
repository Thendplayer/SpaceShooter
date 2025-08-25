using UnityEngine;

namespace SpaceShooter.Core.Level
{
    public class Path : MonoBehaviour
    {
        [SerializeField, Tooltip("The first node in the list will be the starting point.")] 
        private Transform[] _pathNodes;
        
        [SerializeField]
        private float _velocity;

        public float Velocity => _velocity;
        public Transform[] Nodes => _pathNodes;
    }
}
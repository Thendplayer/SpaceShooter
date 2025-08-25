using UnityEngine;

namespace SpaceShooter.Flow
{
    public interface ICollider
    {
        int Layer { get; set; }
        float Radius { get; }
        Vector3 Position { get; }
        void OnCollision(ICollider other);
        void OnDrawGizmos();
    }
}
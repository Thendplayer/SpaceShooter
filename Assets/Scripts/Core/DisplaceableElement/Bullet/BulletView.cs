using Flow;

namespace Core.DisplaceableElement.Bullet
{
    public class BulletView : DisplaceableElementView
    {
        public override int Layer { get; set; }

        public override void OnCollision(ICollider other)
        {
            OnCollisionConfirmed?.Invoke();
        }
    }
}
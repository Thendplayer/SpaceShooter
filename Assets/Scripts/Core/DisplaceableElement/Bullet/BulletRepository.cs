using Objects;
using UnityEngine;

namespace Core.DisplaceableElement.Bullet
{
    public class BulletRepository : MonoBehaviour
    {
        [SerializeField] private BulletView _view;

        public BulletObject Create(BulletData data)
        {
            return ObjectFactory.Get<BulletObject>(_view, data);
        }
    }
}
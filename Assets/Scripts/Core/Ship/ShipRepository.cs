using Objects;
using UnityEngine;

namespace Core.Ship
{
    public class ShipRepository : MonoBehaviour
    {
        [SerializeField] private ShipView _view;

        public ShipObject Create(ShipData data)
        {
            return ObjectFactory.Get<ShipObject>(_view, data);
        }
    }
}
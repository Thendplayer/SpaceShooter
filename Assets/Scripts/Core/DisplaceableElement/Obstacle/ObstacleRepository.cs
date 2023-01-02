using Objects;
using UnityEngine;

namespace Core.DisplaceableElement.Obstacle
{
    public class ObstacleRepository : MonoBehaviour
    {
        [SerializeField] private ObstacleView _view;

        public ObstacleObject Create(ObstacleData data)
        {
            return ObjectFactory.Get<ObstacleObject>(_view, data);
        }
    }
}
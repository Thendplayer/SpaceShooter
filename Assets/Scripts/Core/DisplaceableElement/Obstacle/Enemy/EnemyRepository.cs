using Objects;
using UnityEngine;

namespace Core.DisplaceableElement.Obstacle.Enemy
{
    public class EnemyRepository : MonoBehaviour
    {
        [SerializeField] private EnemyView _view;

        public EnemyObject Create(EnemyData data)
        {
            return ObjectFactory.Get<EnemyObject>(_view, data);
        }
    }
}
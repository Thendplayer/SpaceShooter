using Objects;
using UnityEngine;

namespace Core.Level
{
    public class LevelRepository : MonoBehaviour
    {
        [SerializeField] private LevelView _view;
        
        public LevelObject Create(LevelData data)
        {
            return ObjectFactory.Get<LevelObject>(_view, data);
        }
    }
}
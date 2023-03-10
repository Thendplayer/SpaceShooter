using Objects;
using UnityEngine;

namespace Core.Match
{
    public class MatchRepository : MonoBehaviour
    {
        [SerializeField] private MatchView _view;
        [SerializeField] private MatchData _data;
        
        public MatchObject Create()
        {
            return ObjectFactory.Get<MatchObject>(_view, _data);
        }
    }
}
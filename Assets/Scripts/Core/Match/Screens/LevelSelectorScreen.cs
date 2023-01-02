using System;
using Core.Match.Screens.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Match.Screens
{
    public class LevelSelectorScreen : MonoBehaviour
    {
        [SerializeField] private Transform _levelsContentParent;
        [SerializeField] private LevelButton _level;
        [SerializeField] private Button _backButton;
        
        public Button.ButtonClickedEvent OnBackButtonPressed => _backButton.onClick;
        
        public void Init(int levels, Action<int> onOpenLevel)
        {
            if (_levelsContentParent.childCount > 1) return;
            
            _level.Init(() => onOpenLevel(0), 1);
            for (var i = 1; i < levels; i++)
            {
                var levelId = i;
                Instantiate(_level, _levelsContentParent).Init(() => onOpenLevel(levelId), levelId + 1);
            }
        }
    }
}
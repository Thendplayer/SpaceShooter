using System;
using Core.Match.Screens.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Match.Screens
{
    public class ShipSelectorScreen : MonoBehaviour
    {
        [SerializeField] private Transform _shipsContentParent;
        [SerializeField] private ShipButton _ship;
        [SerializeField] private Button _backButton;
        
        public Button.ButtonClickedEvent OnBackButtonPressed => _backButton.onClick;
        
        public void Init(Sprite[] ships, Action<int> onSelectShip)
        {
            if (_shipsContentParent.childCount > 1) return;
            
            _ship.Init(() => onSelectShip(0), ships[0]);
            for (var i = 1; i < ships.Length; i++)
            {
                var shipId = i;
                Instantiate(_ship, _shipsContentParent).Init(() => onSelectShip(shipId), ships[i]);
            }
        }
    }
}
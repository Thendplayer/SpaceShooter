using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.Match.Screens.Buttons
{
    public class ShipButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;

        public void Init(UnityAction onClick, Sprite icon)
        {
            _button.onClick.AddListener(onClick);
            _icon.sprite = icon;
        }
    }
}
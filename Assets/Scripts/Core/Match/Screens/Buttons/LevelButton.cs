using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.Match.Screens.Buttons
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _numberText;

        public void Init(UnityAction onClick, int number)
        {
            _button.onClick.AddListener(onClick);
            _numberText.text = number.ToString("00");
        }
    }
}
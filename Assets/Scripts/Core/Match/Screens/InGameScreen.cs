using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Core.Match.Screens
{
    public class InGameScreen : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Transform _healthParent;
        [SerializeField] private Image _healthIcon;

        public Button.ButtonClickedEvent OnBackButtonPressed => _pauseButton.onClick;
        
        public int Score
        {
            set => _scoreText.text = value.ToString();
        }

        public int Health
        {
            set
            {
                if (value > _healthParent.childCount)
                {
                    SetAllHealthIconsActive(true);
                    for (var i = 0; i <= value - _healthParent.childCount; i++)
                    {
                        Instantiate(_healthIcon, _healthParent);
                    }
                    return;
                }
                
                if (value <= _healthParent.childCount)
                {
                    SetAllHealthIconsActive(false);
                    for (var i = 0; i < value; i++)
                    {
                        _healthParent.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
        }

        private void SetAllHealthIconsActive(bool active)
        {
            foreach (Transform child in _healthParent)
            {
                child.gameObject.SetActive(active);
            }
        }
    }
}
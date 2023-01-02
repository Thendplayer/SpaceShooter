using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Match.Screens
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        [Space, SerializeField] private string _winnerTitle;
        [SerializeField] private string _looserTitle;

        public Button.ButtonClickedEvent OnReplayButtonPressed => _replayButton.onClick;
        public Button.ButtonClickedEvent OnExitButtonPressed => _exitButton.onClick;
        
        public void Show(bool win, int score)
        {
            _titleText.text = win ? _winnerTitle : _looserTitle;
            _scoreText.text = score.ToString();
        }
    }
}
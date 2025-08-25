using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Core.Match.Screens
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;

        public Button.ButtonClickedEvent OnResumeButtonPressed => _resumeButton.onClick;
        public Button.ButtonClickedEvent OnExitButtonPressed => _exitButton.onClick;
    }
}
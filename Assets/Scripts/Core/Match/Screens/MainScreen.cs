using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Core.Match.Screens
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _shipButton;
        [SerializeField] private Button _quitButton;
        
        [Space, SerializeField] private Button _soundButton;
        [SerializeField] private Image _soundImage;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;
        
        public Button.ButtonClickedEvent OnPlayButtonPressed => _playButton.onClick;
        public Button.ButtonClickedEvent OnShipSelectorButtonPressed => _shipButton.onClick;
        public Button.ButtonClickedEvent OnQuitButtonPressed => _quitButton.onClick;
        public Button.ButtonClickedEvent OnSoundButtonPressed => _soundButton.onClick;
        
        public void SetSoundButtonState(bool mute)
        {
            _soundImage.sprite = mute ? _soundOff : _soundOn;
        }
    }
}
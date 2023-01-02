using System;
using Audio;
using Core.Match.Screens;
using Objects;
using ServiceRegister;
using UnityEngine;

namespace Core.Match
{
    public class MatchView : ObjectView
    {
        [SerializeField] private Canvas[] _canvas;
        
        [Header("Screens")]
        [SerializeField] private MainScreen _mainScreen;
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private LevelSelectorScreen _levelSelectorScreen;
        [SerializeField] private ShipSelectorScreen _shipSelectorScreen;
        [SerializeField] private InGameScreen _inGameScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        
        private GameObject _activeScreen;

        public Action<bool> OnPause;
        public Action<int> OnOpenLevel;
        public Action OnCloseLevel;
        public Action<int> OnShipSelected;
        public Action OnCloseGame;
        public Action OnReplayLevel;
        
        public Camera CanvasCamera
        {
            set
            {
                foreach (var canvas in _canvas) 
                    canvas.worldCamera = value;
            }
        }

        public int Score
        {
            set => _inGameScreen.Score = value;
        }
        
        public int Health
        {
            set => _inGameScreen.Health = value;
        }
        
        public void Init()
        {
            _mainScreen.OnPlayButtonPressed.AddListener(ShowLevelSelectorScreen);
            _mainScreen.OnShipSelectorButtonPressed.AddListener(ShowShipSelectorScreen);
            _mainScreen.OnQuitButtonPressed.AddListener(() => OnCloseGame?.Invoke());
            _mainScreen.OnSoundButtonPressed.AddListener(() =>
            {
                AudioService.Mute = !AudioService.Mute;
                _mainScreen.SetSoundButtonState(AudioService.Mute);
            });

            _pauseScreen.OnResumeButtonPressed.AddListener(() => OnPause?.Invoke(false));
            _pauseScreen.OnExitButtonPressed.AddListener(() => OnCloseLevel?.Invoke());
                
            _levelSelectorScreen.OnBackButtonPressed.AddListener(ShowMainScreen);
            _shipSelectorScreen.OnBackButtonPressed.AddListener(ShowMainScreen);
            
            _inGameScreen.OnBackButtonPressed.AddListener(() => OnPause?.Invoke(true));
            
            _gameOverScreen.OnExitButtonPressed.AddListener(() => OnCloseLevel?.Invoke());
            _gameOverScreen.OnReplayButtonPressed.AddListener(() => OnReplayLevel?.Invoke());
            
            HideAllScreens();
            ShowMainScreen();
        }

        public void Configure(int levels, Sprite[] ships)
        {
            _levelSelectorScreen.Init(levels, levelId => OnOpenLevel?.Invoke(levelId));
            _shipSelectorScreen.Init(ships, shipId => OnShipSelected?.Invoke(shipId));
        }

        public void ShowMainScreen() => ShowScreen(_mainScreen.gameObject);
        public void ShowPauseScreen() => ShowScreen(_pauseScreen.gameObject);
        public void ShowLevelSelectorScreen() => ShowScreen(_levelSelectorScreen.gameObject);
        public void ShowShipSelectorScreen() => ShowScreen(_shipSelectorScreen.gameObject);
        public void ShowInGameScreen() => ShowScreen(_inGameScreen.gameObject);
        public void ShowGameOverScreen(bool win, int score)
        {
            ShowScreen(_gameOverScreen.gameObject);
            _gameOverScreen.Show(win, score);
        }

        private void ShowScreen(GameObject screen)
        {
            ServiceLocator.Instance.GetService<AudioService>().Play(AudioTrack.UIButton);

            if (_activeScreen != null)
                _activeScreen.SetActive(false);

            screen.SetActive(true);
            _activeScreen = screen;
        }
        
        private void HideAllScreens()
        {
            _mainScreen.gameObject.SetActive(false);
            _pauseScreen.gameObject.SetActive(false);
            _levelSelectorScreen.gameObject.SetActive(false);
            _shipSelectorScreen.gameObject.SetActive(false);
            _inGameScreen.gameObject.SetActive(false);
        }
    }
}
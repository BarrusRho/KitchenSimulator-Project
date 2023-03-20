using System;
using UnityEngine;

namespace KitchenSimulator.Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private enum GameState
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }

        private GameState _gameState;
        private float _waitingToStartTimer = 1f;
        private float _countdownToStartTimer = 3f;
        private float _gamePlayingTimer;
        private float _gamePlayingTimerMaximum = 10f;
        private bool _isGamePaused = false;

        public event EventHandler OnStateChanged;
        public event EventHandler OnGamePaused;
        public event EventHandler OnGameResumed;

        private void Awake()
        {
            Instance = this;

            _gameState = GameState.WaitingToStart;
        }

        private void Start()
        {
            InputManager.Instance.OnPauseAction += OnPauseAction;
        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.WaitingToStart:
                    _waitingToStartTimer -= Time.deltaTime;

                    if (_waitingToStartTimer < 0f)
                    {
                        _gameState = GameState.CountdownToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }

                    break;
                case GameState.CountdownToStart:
                    _countdownToStartTimer -= Time.deltaTime;

                    if (_countdownToStartTimer < 0f)
                    {
                        _gameState = GameState.GamePlaying;
                        _gamePlayingTimer = _gamePlayingTimerMaximum;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }

                    break;
                case GameState.GamePlaying:
                    _gamePlayingTimer -= Time.deltaTime;

                    if (_gamePlayingTimer < 0f)
                    {
                        _gameState = GameState.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }

                    break;
                case GameState.GameOver:
                    break;
            }

            Debug.Log(_gameState);
        }

        public bool IsGamePlaying()
        {
            return _gameState == GameState.GamePlaying;
        }

        public bool IsCountdownToStartActive()
        {
            return _gameState == GameState.CountdownToStart;
        }

        public float GetCountdownToStartTimer()
        {
            return _countdownToStartTimer;
        }

        public bool IsGameOver()
        {
            return _gameState == GameState.GameOver;
        }

        public float GetGamePlayingTimerNormalized()
        {
            return 1 - (_gamePlayingTimer / _gamePlayingTimerMaximum);
        }

        private void OnPauseAction(object sender, EventArgs eventArgs)
        {
            TogglePauseGame();
        }

        public void TogglePauseGame()
        {
            _isGamePaused = !_isGamePaused;

            if (_isGamePaused)
            {
                Time.timeScale = 0f;
                OnGamePaused?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Time.timeScale = 1f;
                OnGameResumed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
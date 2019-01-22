using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Logic
{
    public class StateManager
    {
        public delegate void GameStateDelegate(GameState gameState);
        public event GameStateDelegate OnGameStateChanged;

        public enum GameState
        {
            Error,
            Loading,
            Starting,
            Playing,
            Finished
        };

        public GameState CurrentState { get; private set; } = GameState.Loading;

        public void SetCurrentState(GameState state)
        {
            CurrentState = state;
            OnGameStateChanged?.Invoke(CurrentState);
        }

        public void GoToNextState()
        {
            switch (CurrentState)
            {
                case GameState.Loading:
                    CurrentState = GameState.Starting;
                    OnGameStateChanged?.Invoke(CurrentState);
                    break;
                case GameState.Starting:
                    CurrentState = GameState.Playing;
                    OnGameStateChanged?.Invoke(CurrentState);
                    break;
                case GameState.Playing:
                    CurrentState = GameState.Finished;
                    OnGameStateChanged?.Invoke(CurrentState);
                    break;
                case GameState.Finished:
                    CurrentState = GameState.Starting;
                    OnGameStateChanged?.Invoke(CurrentState);
                    break;
            }
        }
    }
}

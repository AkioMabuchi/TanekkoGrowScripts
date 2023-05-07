using System;
using Enums;
using UniRx;

namespace Models
{
    public class GameStateModel
    {
        private readonly ReactiveProperty<GameState> _reactivePropertyCurrentGameState = new(GameState.None);
        public IObservable<GameState> OnChangedGameState => _reactivePropertyCurrentGameState;
        public GameState CurrentGameState => _reactivePropertyCurrentGameState.Value;

        public bool CanTanekkoMove => _reactivePropertyCurrentGameState.Value is GameState.MainGame;

        public bool IsMainCameraChasing =>
            _reactivePropertyCurrentGameState.Value is GameState.MainGame;
        public void SetGameState(GameState gameState)
        {
            _reactivePropertyCurrentGameState.Value = gameState;
        }
    }
}
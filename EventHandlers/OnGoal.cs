using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnGoal : IInitializable
    {
        private readonly EnemiesModel _enemiesModel;
        private readonly GameStateModel _gameStateModel;
        private readonly MainGameTimeLimitModel _mainGameTimeLimitModel;
        private readonly ResultRankModel _resultRankModel;
        private readonly TanekkoModel _tanekkoModel;
        
        private readonly Goal _goal;
        private readonly MusicPlayer _musicPlayer;
        private readonly ResultScreen _resultScreen;
        private readonly SoundPlayer _soundPlayer;
        private readonly Tanekko _tanekko;

        [Inject]
        public OnGoal(EnemiesModel enemiesModel, GameStateModel gameStateModel,
            MainGameTimeLimitModel mainGameTimeLimitModel, ResultRankModel resultRankModel, TanekkoModel tanekkoModel,
            Goal goal, MusicPlayer musicPlayer, ResultScreen resultScreen, SoundPlayer soundPlayer, Tanekko tanekko)
        {
            _enemiesModel = enemiesModel;
            _gameStateModel = gameStateModel;
            _mainGameTimeLimitModel = mainGameTimeLimitModel;
            _resultRankModel = resultRankModel;
            _tanekkoModel = tanekkoModel;

            _goal = goal;
            _musicPlayer = musicPlayer;
            _resultScreen = resultScreen;
            _soundPlayer = soundPlayer;
            _tanekko = tanekko;
        }

        public void Initialize()
        {
            _goal.OnGoal
                .Where(_ => _gameStateModel.CurrentGameState == GameState.MainGame)
                .Subscribe(_ =>
                {
                    AsyncOnGoal().Forget();
                });
        }

        private async UniTask AsyncOnGoal()
        {
            _musicPlayer.ChangeMusic(MusicName.None);
            _soundPlayer.PlaySound(SoundName.Goal);
            _gameStateModel.SetGameState(GameState.ResultReady);
            _enemiesModel.ClearAllEnemies();
            _resultRankModel.CalculateResultRank(_mainGameTimeLimitModel.TickCountTimeLimit,
                _tanekkoModel.GrowthStatus);
            
            if (!_tanekkoModel.IsJumping)
            {
                _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Running);
            }

            _tanekko.MoveRight();

            await UniTask.Delay(TimeSpan.FromSeconds(3.0));
            
            _gameStateModel.SetGameState(GameState.Result);
            _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.None);
            
            _resultScreen.Show();
        }
    }
}
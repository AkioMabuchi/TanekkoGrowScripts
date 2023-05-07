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
    public class OnMainGameStart : IInitializable
    {
        private readonly EnemiesLayoutsStore _enemiesLayoutsStore;
        private readonly EnemiesModel _enemiesModel;
        private readonly GameStateModel _gameStateModel;
        private readonly MainFieldModel _mainFieldModel;
        private readonly MainGameTimeLimitModel _mainGameTimeLimitModel;
        private readonly ScoreModel _scoreModel;
        private readonly TanekkoExperienceModel _tanekkoExperienceModel;
        private readonly TanekkoModel _tanekkoModel;
        
        private readonly InputView _inputView;

        private readonly EnemyFactory _enemyFactory;
        private readonly GameOverScreen _gameOverScreen;
        private readonly MainCamera _mainCamera;
        private readonly MainGameTimeLimitView _mainGameTimeLimitView;
        private readonly MusicPlayer _musicPlayer;
        private readonly ResultScreen _resultScreen;
        private readonly SoundPlayer _soundPlayer;
        private readonly Tanekko _tanekko;
        private readonly TanekkoBody _tanekkoBody;
        private readonly TanekkoLifeCountView _tanekkoLifeCountView;
        private readonly TitleScreen _titleScreen;

        [Inject]
        public OnMainGameStart(EnemiesLayoutsStore enemiesLayoutsStore, EnemiesModel enemiesModel,
            GameStateModel gameStateModel,
            MainFieldModel mainFieldModel,
            MainGameTimeLimitModel mainGameTimeLimitModel, ScoreModel scoreModel,
            TanekkoExperienceModel tanekkoExperienceModel, TanekkoModel tanekkoModel,
            InputView inputView, EnemyFactory enemyFactory, GameOverScreen gameOverScreen, MainCamera mainCamera,
            MainGameTimeLimitView mainGameTimeLimitView, MusicPlayer musicPlayer, ResultScreen resultScreen,
            SoundPlayer soundPlayer,
            Tanekko tanekko, TanekkoBody tanekkoBody, TanekkoLifeCountView tanekkoLifeCountView,
            TitleScreen titleScreen)
        {
            _enemiesLayoutsStore = enemiesLayoutsStore;
            _enemiesModel = enemiesModel;
            _gameStateModel = gameStateModel;
            _mainFieldModel = mainFieldModel;
            _mainGameTimeLimitModel = mainGameTimeLimitModel;
            _scoreModel = scoreModel;
            _tanekkoExperienceModel = tanekkoExperienceModel;
            _tanekkoModel = tanekkoModel;

            _inputView = inputView;

            _enemyFactory = enemyFactory;
            _gameOverScreen = gameOverScreen;
            _mainCamera = mainCamera;
            _mainGameTimeLimitView = mainGameTimeLimitView;
            _musicPlayer = musicPlayer;
            _resultScreen = resultScreen;
            _soundPlayer = soundPlayer;
            _tanekko = tanekko;
            _tanekkoBody = tanekkoBody;
            _tanekkoLifeCountView = tanekkoLifeCountView;
            _titleScreen = titleScreen;
        }

        public void Initialize()
        {
            _inputView.OnMainGameStart
                .Where(_ => _gameStateModel.CurrentGameState == GameState.Title)
                .Subscribe(_ =>
                {
                    AsyncGameStart().Forget();
                });

            _inputView.OnMainGameRestart
                .Where(_ => _gameStateModel.CurrentGameState is GameState.MainGame or GameState.TimeUp or GameState.Defeated
                    or GameState.Result)
                .Subscribe(_ =>
                {
                    AsyncGameStart().Forget();
                });
        }
        
        private async UniTask AsyncGameStart()
        {
            _soundPlayer.PlaySound(SoundName.Decide);
            _enemiesModel.ClearAllEnemies();

            var enemiesLayout = _enemiesLayoutsStore.RandomEnemiesLayout;

            for (var i = 0; i < enemiesLayout.Count; i++)
            {
                var enemyLayout = enemiesLayout[i];
                switch (enemyLayout.type)
                {
                    case EnemyType.Red:
                    {
                        _enemiesModel.AddEnemyRed(i);
                        break;
                    }
                    case EnemyType.Blue:
                    {
                        _enemiesModel.AddEnemyBlue(i);
                        break;
                    }
                    case EnemyType.Purple:
                    {
                        _enemiesModel.AddEnemyPurple(i);
                        break;
                    }
                    default:
                    {
                        continue;
                    }
                }

                var enemyPosition = enemyLayout.position;
                _enemyFactory.SetPositionEnemy(i, new Vector2(enemyPosition.x, enemyPosition.y));
            }

            _gameStateModel.SetGameState(GameState.MainGameReady);
            _mainFieldModel.BuildMainField();
            _mainGameTimeLimitModel.SetTickCountRest(12000);
            _scoreModel.ResetScore();
            _tanekkoExperienceModel.ResetDefeatedEnemy();
            _tanekkoModel.SetGrowthStatus(TanekkoGrowthStatus.Seed);
            _tanekkoModel.SetElementStatus(TanekkoElementStatus.Normal);
            _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Idle);
            _tanekkoModel.SetLookDirection(TanekkoLookDirection.Right);
            _tanekkoModel.SetLifeCount(3);
            
            _gameOverScreen.Hide();
            _mainCamera.SequencePositionX();
            _mainGameTimeLimitView.Show();
            _musicPlayer.ChangeMusic(MusicName.Main);
            _resultScreen.Hide();
            _tanekko.Stop();
            _tanekko.TeleportToInitialPosition();
            _tanekkoBody.ResetSpriteColor();
            _tanekkoLifeCountView.Show();
            _titleScreen.Hide();

            await UniTask.Delay(TimeSpan.FromSeconds(5.0));
        
            _gameStateModel.SetGameState(GameState.MainGame);
            _enemyFactory.MakeAllEnemiesMove();
        }
    }
}
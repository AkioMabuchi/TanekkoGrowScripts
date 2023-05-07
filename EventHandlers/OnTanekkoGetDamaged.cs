using Enums;
using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnTanekkoGetDamaged : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TanekkoModel _tanekkoModel;

        private readonly GameOverScreen _gameOverScreen;
        private readonly MusicPlayer _musicPlayer;
        private readonly SoundPlayer _soundPlayer;
        private readonly Tanekko _tanekko;
        private readonly TanekkoBody _tanekkoBody;

        [Inject]
        public OnTanekkoGetDamaged(GameStateModel gameStateModel, TanekkoModel tanekkoModel,
            GameOverScreen gameOverScreen, MusicPlayer musicPlayer, SoundPlayer soundPlayer, Tanekko tanekko,
            TanekkoBody tanekkoBody)
        {
            _gameStateModel = gameStateModel;
            _tanekkoModel = tanekkoModel;

            _gameOverScreen = gameOverScreen;
            _musicPlayer = musicPlayer;
            _soundPlayer = soundPlayer;
            _tanekko = tanekko;
            _tanekkoBody = tanekkoBody;
        }

        public void Initialize()
        {
            _tanekko.OnDamaged
                .Where(_ => _gameStateModel.CurrentGameState == GameState.MainGame)
                .Where(_ => _tanekkoModel.CanGetDamaged)
                .Subscribe(_ =>
                {
                    _soundPlayer.PlaySound(SoundName.TanekkoGetDamaged);
                    _tanekkoModel.DecreaseLifeCount();
                    if (_tanekkoModel.LifeCount > 0)
                    {
                        _tanekkoModel.SetInvincibleTickCount(50);
                        _tanekkoBody.DrawEffectToGetDamaged();
                    }
                    else
                    {
                        _gameStateModel.SetGameState(GameState.Defeated);
                        _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Defeated);
                        
                        _gameOverScreen.Show();
                        _musicPlayer.ChangeMusic(MusicName.GameOver);
                        _tanekko.Stop();
                        _tanekkoBody.DrawEffectToGetDefeated();
                    }
                });
        }
    }
}
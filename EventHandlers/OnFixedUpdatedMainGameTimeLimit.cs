using Enums;
using Models;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnFixedUpdatedMainGameTimeLimit : IFixedTickable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly MainGameTimeLimitModel _mainGameTimeLimitModel;
        private readonly TanekkoModel _tanekkoModel;

        private readonly GameOverScreen _gameOverScreen;
        private readonly MusicPlayer _musicPlayer;
        private readonly Tanekko _tanekko;

        [Inject]
        public OnFixedUpdatedMainGameTimeLimit(GameStateModel gameStateModel,
            MainGameTimeLimitModel mainGameTimeLimitModel, TanekkoModel tanekkoModel, GameOverScreen gameOverScreen, 
            MusicPlayer musicPlayer, Tanekko tanekko)
        {
            _gameStateModel = gameStateModel;
            _mainGameTimeLimitModel = mainGameTimeLimitModel;
            _tanekkoModel = tanekkoModel;

            _gameOverScreen = gameOverScreen;
            _musicPlayer = musicPlayer;
            _tanekko = tanekko;
        }
        
        public void FixedTick()
        {
            if (_gameStateModel.CurrentGameState == GameState.MainGame)
            {
                _mainGameTimeLimitModel.DecreaseTickCountRest();
                if (_mainGameTimeLimitModel.IsTimeUp)
                {
                    _gameStateModel.SetGameState(GameState.TimeUp);
                    _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.TimeUp);
                    
                    _gameOverScreen.Show();
                    _musicPlayer.ChangeMusic(MusicName.GameOver);
                    _tanekko.Stop();
                }
            }
        }
    }
}
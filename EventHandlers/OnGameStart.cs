using Enums;
using Models;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnGameStart : IPostStartable
    {
        private readonly GameStateModel _gameStateModel;
        
        private readonly BlackScreen _blackScreen;
        private readonly GameOverScreen _gameOverScreen;
        private readonly MusicPlayer _musicPlayer;
        private readonly ResultScreen _resultScreen;
        private readonly TitleScreen _titleScreen;

        [Inject]
        public OnGameStart(GameStateModel gameStateModel, BlackScreen blackScreen, GameOverScreen gameOverScreen,
            MusicPlayer musicPlayer, ResultScreen resultScreen, TitleScreen titleScreen)
        {
            _gameStateModel = gameStateModel;

            _blackScreen = blackScreen;
            _gameOverScreen = gameOverScreen;
            _musicPlayer = musicPlayer;
            _resultScreen = resultScreen;
            _titleScreen = titleScreen;
        }

        public void PostStart()
        {
            _gameStateModel.SetGameState(GameState.Title);
                
            _blackScreen.Hide();
            _gameOverScreen.Hide();
            _musicPlayer.ChangeMusic(MusicName.Title);
            _resultScreen.Hide();
            _titleScreen.Show();
        }
    }
}
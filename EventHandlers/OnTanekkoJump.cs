using Enums;
using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnTanekkoJump : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TanekkoModel _tanekkoModel;

        private readonly SoundPlayer _soundPlayer;
        private readonly Tanekko _tanekko;
        private readonly InputView _inputView;

        [Inject]
        public OnTanekkoJump(GameStateModel gameStateModel, TanekkoModel tanekkoModel, SoundPlayer soundPlayer, Tanekko tanekko,
            InputView inputView)
        {
            _gameStateModel = gameStateModel;
            _tanekkoModel = tanekkoModel;

            _soundPlayer = soundPlayer;
            _tanekko = tanekko;
            _inputView = inputView;
        }
    
        public void Initialize()
        {
            _inputView.OnJump
                .Where(_ => _gameStateModel.CurrentGameState == GameState.MainGame)
                .Where(_ => _tanekkoModel.CanJump)
                .Subscribe(_ =>
                {
                    _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Jumping);
                    
                    _soundPlayer.PlaySound(SoundName.TanekkoJump);
                    _tanekko.Jump();
                });
        }
    }
}
using Enums;
using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnTanekkoAbsorb : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TanekkoModel _tanekkoModel;
        private readonly InputView _inputView;

        [Inject]
        public OnTanekkoAbsorb(GameStateModel gameStateModel, TanekkoModel tanekkoModel, InputView inputView)
        {
            _gameStateModel = gameStateModel;
            _tanekkoModel = tanekkoModel;
            _inputView = inputView;
        }

        public void Initialize()
        {
            _inputView.OnAbsorb
                .Where(_ => _gameStateModel.CurrentGameState == GameState.MainGame)
                .Where(_ => _tanekkoModel.CanAbsorb)
                .Subscribe(_ =>
                {
                    _tanekkoModel.ResetTickCountAbsorbing();
                    _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Absorbing);
                });

            _inputView.OnAbsorbed
                .Where(_ => _gameStateModel.CurrentGameState == GameState.MainGame)
                .Where(_ => _tanekkoModel.IsAbsorbing)
                .Subscribe(_ =>
                {
                    _tanekkoModel.ResetTickCountAbsorbing();
                    _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Idle);
                });
        }
    }
}
using Enums;
using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnTanekkoGrounded : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TanekkoModel _tanekkoModel;
        private readonly TanekkoGroundDetector _tanekkoGroundDetector;

        [Inject]
        public OnTanekkoGrounded(GameStateModel gameStateModel, TanekkoModel tanekkoModel, TanekkoGroundDetector tanekkoGroundDetector)
        {
            _gameStateModel = gameStateModel;
            _tanekkoModel = tanekkoModel;
            _tanekkoGroundDetector = tanekkoGroundDetector;
        }

        public void Initialize()
        {
            _tanekkoGroundDetector.OnGrounded.Subscribe(_ =>
            {
                switch (_gameStateModel.CurrentGameState)
                {
                    case GameState.ResultReady:
                    {
                        _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Running);
                        break;
                    }
                    default:
                    {
                        _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Idle);
                        break;
                    }
                }
            });
        }
    }
}
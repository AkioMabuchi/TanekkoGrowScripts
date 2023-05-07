using Models;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnTanekkoPositionChanged : ITickable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TanekkoModel _tanekkoModel;
        private readonly Tanekko _tanekko;
        private readonly MainCamera _mainCamera;

        [Inject]
        public OnTanekkoPositionChanged(GameStateModel gameStateModel, TanekkoModel tanekkoModel, Tanekko tanekko, MainCamera mainCamera)
        {
            _gameStateModel = gameStateModel;
            _tanekkoModel = tanekkoModel;
            _tanekko = tanekko;
            _mainCamera = mainCamera;
        }

        public void Tick()
        {
            var positionX = _tanekko.transform.position.x;
            _tanekkoModel.SetMainFieldPositionXWithActualPositionX(positionX);

            if (_gameStateModel.IsMainCameraChasing)
            {
                _mainCamera.MovePositionX(positionX);
            }
        }
    }
}
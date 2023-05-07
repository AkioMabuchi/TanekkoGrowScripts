using Enums;
using Models;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnTanekkoMove : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TanekkoModel _tanekkoModel;
        private readonly Tanekko _tanekko;
        private readonly InputView _inputView;

        [Inject]
        public OnTanekkoMove(GameStateModel gameStateModel, TanekkoModel tanekkoModel, Tanekko tanekko,
            InputView inputView)
        {
            _gameStateModel = gameStateModel;
            _tanekkoModel = tanekkoModel;
            _tanekko = tanekko;
            _inputView = inputView;
        }

        public void Initialize()
        {
            _inputView.OnMove
                .Where(_ => _gameStateModel.CanTanekkoMove)
                .Where(_ => _tanekkoModel.CanMove)
                .Subscribe(direction =>
                {
                    switch (direction)
                    {
                        case TanekkoMoveDirection.Right:
                        {
                            _tanekkoModel.ChangeMotionOnMove();
                            _tanekkoModel.SetLookDirection(TanekkoLookDirection.Right);
                            _tanekko.MoveRight();
                            break;
                        }
                        case TanekkoMoveDirection.Left:
                        {
                            _tanekkoModel.ChangeMotionOnMove();
                            _tanekkoModel.SetLookDirection(TanekkoLookDirection.Left);
                            _tanekko.MoveLeft();
                            break;
                        }
                        default:
                        {
                            _tanekkoModel.ChangeMotionOnMoveStop();
                            _tanekko.MoveStop();
                            break;
                        }
                    }
                });
        }
    }
}
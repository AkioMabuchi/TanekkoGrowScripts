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
    public class OnTanekkoAttack : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TanekkoModel _tanekkoModel;
        private readonly InputView _inputView;

        [Inject]
        public OnTanekkoAttack(GameStateModel gameStateModel, TanekkoModel tanekkoModel, InputView inputView)
        {
            _gameStateModel = gameStateModel;
            _tanekkoModel = tanekkoModel;
            _inputView = inputView;
        }

        public void Initialize()
        {
            _inputView.OnShoot
                .Where(_ => _gameStateModel.CurrentGameState == GameState.MainGame)
                .Where(_ => _tanekkoModel.CanAttack)
                .Subscribe(_ =>
                {
                    _tanekkoModel.DecreaseBulletCount();
                    _tanekkoModel.ResetTickCountAttacking();
                    _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Attacking);
                });
        }
    }
}
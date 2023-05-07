using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class TanekkoBodyPresenter : IInitializable
    {
        private readonly TanekkoModel _tanekkoModel;
        private readonly TanekkoBody _tanekkoBody;

        [Inject]
        public TanekkoBodyPresenter(TanekkoModel tanekkoModel, TanekkoBody tanekkoBody)
        {
            _tanekkoModel = tanekkoModel;
            _tanekkoBody = tanekkoBody;
        }
        public void Initialize()
        {
            _tanekkoModel.OnChangedGrowthStatus.Subscribe(growthStatus =>
            {
                _tanekkoBody.SetGrowthStatus(growthStatus);
            });

            _tanekkoModel.OnChangedElementStatus.Subscribe(elementStatus =>
            {
                _tanekkoBody.SetElementStatus(elementStatus);
            });

            _tanekkoModel.OnChangedMotionStatus.Subscribe(motionStatus =>
            {
                _tanekkoBody.ChangeMotion(motionStatus);
            });

            _tanekkoModel.OnChangedLookDirection.Subscribe(lookDirection =>
            {
                _tanekkoBody.LookDirection(lookDirection);
            });
        }
    }
}
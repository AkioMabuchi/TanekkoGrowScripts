using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class TanekkoLifeCountPresenter : IInitializable
    {
        private readonly TanekkoModel _tanekkoModel;
        private readonly TanekkoLifeCountView _tanekkoLifeCountView;

        [Inject]
        public TanekkoLifeCountPresenter(TanekkoModel tanekkoModel, TanekkoLifeCountView tanekkoLifeCountView)
        {
            _tanekkoModel = tanekkoModel;
            _tanekkoLifeCountView = tanekkoLifeCountView;
        }
    
        public void Initialize()
        {
            _tanekkoModel.OnChangedLifeCount.Subscribe(lifeCount =>
            {
                _tanekkoLifeCountView.SetLifeGauge(lifeCount);
            });
        }
    }
}
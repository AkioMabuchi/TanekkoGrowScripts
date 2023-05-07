using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class MainGameTimeLimitPresenter: IInitializable
    {
        private readonly MainGameTimeLimitModel _mainGameTimeLimitModel;
        private readonly MainGameTimeLimitView _mainGameTimeLimitView;

        [Inject]
        public MainGameTimeLimitPresenter(MainGameTimeLimitModel mainGameTimeLimitModel, MainGameTimeLimitView mainGameTimeLimitView)
        {
            _mainGameTimeLimitModel = mainGameTimeLimitModel;
            _mainGameTimeLimitView = mainGameTimeLimitView;
        }

        public void Initialize()
        {
            _mainGameTimeLimitModel.OnChangedTickCountRest.Subscribe(tickCountRest =>
            {
                _mainGameTimeLimitView.DrawTimeCount(tickCountRest);
            });
        }
    }
}
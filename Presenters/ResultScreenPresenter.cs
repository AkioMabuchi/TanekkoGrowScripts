using Enums;
using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class ResultScreenPresenter : IInitializable
    {
        private readonly ResultRankModel _resultRankModel;
        private readonly ScoreModel _scoreModel;
        private readonly MainGameTimeLimitModel _mainGameTimeLimitModel;

        private readonly TanekkoModel _tanekkoModel;
        private readonly ResultScreen _resultScreen;

        [Inject]
        public ResultScreenPresenter(ResultRankModel resultRankModel, MainGameTimeLimitModel mainGameTimeLimitModel,
            ScoreModel scoreModel, TanekkoModel tanekkoModel, ResultScreen resultScreen)
        {
            _resultRankModel = resultRankModel;
            _mainGameTimeLimitModel = mainGameTimeLimitModel;
            _scoreModel = scoreModel;
            _tanekkoModel = tanekkoModel;
            _resultScreen = resultScreen;
        }

        public void Initialize()
        {
            _resultRankModel.OnChangedResultRank.Subscribe(resultRank =>
            {
                _resultScreen.SetRankSprite(resultRank);
            });

            _scoreModel.OnChangedScore.Subscribe(score =>
            {
                _resultScreen.SetScoreSprites(score);
            });

            _mainGameTimeLimitModel.OnChangedTickCountRest.Subscribe(tickCountRest =>
            {
                _resultScreen.SetTimeSprites(tickCountRest);
            });

            _tanekkoModel.OnChangedGrowthStatus.Subscribe(growthStatus =>
            {
                _resultScreen.SetGrowthStatus(growthStatus);
            });

            _tanekkoModel.OnChangedElementStatus.Subscribe(elementStatus =>
            {
                _resultScreen.SetElementStatus(elementStatus);
            });
        }
    }
}
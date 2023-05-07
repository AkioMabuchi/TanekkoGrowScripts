using System;
using Enums;
using UniRx;

namespace Models
{
    public class ResultRankModel
    {
        private readonly ReactiveProperty<GameResultRank> _reactivePropertyResultRank = new(GameResultRank.None);
        public IObservable<GameResultRank> OnChangedResultRank => _reactivePropertyResultRank;

        public void ResetResultRank()
        {
            _reactivePropertyResultRank.Value = GameResultRank.None;
        }

        public void CalculateResultRank(int tickCountRestTime, TanekkoGrowthStatus tanekkoGrowthStatus)
        {
            switch (tanekkoGrowthStatus)
            {
                case TanekkoGrowthStatus.FlowerA:
                case TanekkoGrowthStatus.FlowerB:
                {
                    if (tickCountRestTime >= 4000)
                    {
                        _reactivePropertyResultRank.Value = GameResultRank.SSS;
                    }
                    else if (tickCountRestTime >= 2000)
                    {
                        _reactivePropertyResultRank.Value = GameResultRank.SS;
                    }
                    else
                    {
                        _reactivePropertyResultRank.Value = GameResultRank.A;
                    }
                    break;
                }
                case TanekkoGrowthStatus.Bud:
                case TanekkoGrowthStatus.Seed:
                {
                    if (tickCountRestTime >= 4000)
                    {
                        _reactivePropertyResultRank.Value = GameResultRank.S;
                    }
                    else if (tickCountRestTime >= 2000)
                    {
                        _reactivePropertyResultRank.Value = GameResultRank.A;
                    }
                    else
                    {
                        _reactivePropertyResultRank.Value = GameResultRank.B;
                    }
                    break;
                }
            }
        }
    }
}
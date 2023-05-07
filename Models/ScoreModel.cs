using System;
using UniRx;

namespace Models
{
    public class ScoreModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyScore = new(0);
        public IObservable<int> OnChangedScore => _reactivePropertyScore;


        public void ResetScore()
        {
            _reactivePropertyScore.Value = 0;
        }

        public void AddScore(int score)
        {
            _reactivePropertyScore.Value += score;
        }
    }
}
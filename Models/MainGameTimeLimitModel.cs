using System;
using UniRx;

namespace Models
{
    public class MainGameTimeLimitModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyTickCountRest = new(0);
        public IObservable<int> OnChangedTickCountRest => _reactivePropertyTickCountRest;
        public int TickCountTimeLimit => _reactivePropertyTickCountRest.Value;
        public bool IsTimeUp => _reactivePropertyTickCountRest.Value <= 0;

        public void SetTickCountRest(int tickCountRest)
        {
            _reactivePropertyTickCountRest.Value = tickCountRest;
        }

        public void DecreaseTickCountRest()
        {
            if (_reactivePropertyTickCountRest.Value > 0)
            {
                _reactivePropertyTickCountRest.Value--;
            }
        }
    }
}
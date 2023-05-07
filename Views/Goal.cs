using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    public class Goal : MonoBehaviour
    {
        private readonly Subject<Unit> _subjectOnGoal = new();
        public IObservable<Unit> OnGoal => _subjectOnGoal;

        private void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(col =>
                {
                    if (col.gameObject.GetComponent<Tanekko>())
                    {
                        _subjectOnGoal.OnNext(Unit.Default);
                    }
                }).AddTo(gameObject);
        }
    }
}

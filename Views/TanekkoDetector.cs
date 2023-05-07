using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    public class TanekkoDetector : MonoBehaviour
    {
        private readonly Subject<Unit> _subjectOnTanekkoDetected = new();
        public IObservable<Unit> OnTanekkoDetected => _subjectOnTanekkoDetected;
        private void Awake()
        {
            this.OnTriggerStay2DAsObservable()
                .Subscribe(col =>
                {
                    if (col.GetComponent<Tanekko>())
                    {
                        _subjectOnTanekkoDetected.OnNext(Unit.Default);
                    }
                }).AddTo(gameObject);
        }
    }
}

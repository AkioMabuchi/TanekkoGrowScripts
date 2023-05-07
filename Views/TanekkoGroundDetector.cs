using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    public class TanekkoGroundDetector : MonoBehaviour
    {
        private readonly Subject<Unit> _subjectOnGrounded = new();
        public IObservable<Unit> OnGrounded => _subjectOnGrounded;
        private void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(col =>
                {
                    if (col.gameObject.GetComponent<MainGround>())
                    {
                        _subjectOnGrounded.OnNext(Unit.Default);
                    }
                }).AddTo(gameObject);
        }
    }
}

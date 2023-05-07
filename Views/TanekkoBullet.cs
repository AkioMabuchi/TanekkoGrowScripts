using System;
using System.Collections.Generic;
using Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class TanekkoBullet : MonoBehaviour
    {
        private static readonly int _animatorTriggerNone = Animator.StringToHash("None");
        private static readonly int _animatorTriggerNormal = Animator.StringToHash("Normal");
        private static readonly int _animatorTriggerFire = Animator.StringToHash("Fire");
        private static readonly int _animatorTriggerIce = Animator.StringToHash("Ice");
        private static readonly int _animatorTriggerPoison = Animator.StringToHash("Poison");
    
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private Animator animator;

        private readonly Subject<TanekkoBullet> _subjectOnHitEnemy = new();
        public IObservable<TanekkoBullet> OnHitEnemy => _subjectOnHitEnemy;
        private readonly Subject<TanekkoBullet> _subjectOnExpired = new();
        public IObservable<TanekkoBullet> OnExpired => _subjectOnExpired;

        private TanekkoElementStatus _currentElementStatus = TanekkoElementStatus.Normal;
        public TanekkoElementStatus CurrentElementStatus => _currentElementStatus;

        private bool _isActiveCountToDurationTime;
        private float _expirationDurationTime = float.MaxValue;
        private float _currentDurationTime;
    
        private readonly Dictionary<TanekkoElementStatus, int> _dictionaryAnimatorTriggers = new()
        {
            {TanekkoElementStatus.Normal, _animatorTriggerNormal},
            {TanekkoElementStatus.Fire, _animatorTriggerFire},
            {TanekkoElementStatus.Ice, _animatorTriggerIce},
            {TanekkoElementStatus.Poison, _animatorTriggerPoison}
        };
    
        private void Reset()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_=>_isActiveCountToDurationTime)
                .Subscribe(_ =>
                {
                    _currentDurationTime += Time.deltaTime;
                    if (_currentDurationTime >= _expirationDurationTime)
                    {
                        _isActiveCountToDurationTime = false;
                        _subjectOnExpired.OnNext(this);
                    }
                }).AddTo(gameObject);
        
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(col =>
                {
                    if (col.gameObject.GetComponent<EnemyBase>())
                    {
                        _subjectOnHitEnemy.OnNext(this);
                    }
                }).AddTo(gameObject);
        }

        public void TakeObject()
        {
            rigidbody2d.simulated = true;
        }
        public void ReleaseObject()
        {
            _isActiveCountToDurationTime = false;
            rigidbody2d.simulated = false;
            animator.SetTrigger(_animatorTriggerNone);
        }


        public void SetElementStatus(TanekkoElementStatus elementStatus)
        {
            _currentElementStatus = elementStatus;
        }

        public void ChangeAnimationWithElementStatus(TanekkoElementStatus elementStatus)
        {
            animator.SetTrigger(_dictionaryAnimatorTriggers[elementStatus]);
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2d.velocity = velocity;
        }

        public void SetExpirationDurationTime(float durationTime)
        {
            _expirationDurationTime = durationTime;
        }

        public void StartCountingDurationTime()
        {
            _currentDurationTime = 0.0f;
            _isActiveCountToDurationTime = true;
        }
    }
}

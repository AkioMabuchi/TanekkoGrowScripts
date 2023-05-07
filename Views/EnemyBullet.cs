using System;
using System.Collections;
using DG.Tweening;
using Enums;
using Structs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private Sprite spriteBulletRed;
    
        private readonly Subject<EnemyBullet> _subjectOnDiminished = new();
        public IObservable<EnemyBullet> OnDiminished => _subjectOnDiminished;
        private readonly Subject<EnemyBullet> _subjectOnHitTanekko = new();
        public IObservable<EnemyBullet> OnHitTanekko => _subjectOnHitTanekko;

        private bool _isActive;
        private Coroutine _coroutine;
        private void Reset()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                .Where(_ => _isActive)
                .Subscribe(col =>
                {
                    if (col.gameObject.GetComponent<Tanekko>())
                    {
                        _subjectOnHitTanekko.OnNext(this);
                    }
                }).AddTo(gameObject);
        }
        
        public void OnTakeObject()
        {
            _isActive = true;
            rigidbody2d.simulated = true;
        }

        public void OnReleaseObject()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _isActive = false;
            rigidbody2d.simulated = false;
            spriteRenderer.sprite = null;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2d.velocity = velocity;
        }
        
        public void Shoot(EnemyBulletType enemyBulletType)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            switch (enemyBulletType)
            {
                case EnemyBulletType.None:
                {
                    _coroutine = StartCoroutine(CoroutineNone());
                    break;
                }
                case EnemyBulletType.Red:
                {
                    _coroutine = StartCoroutine(CoroutineRed());
                    break;
                }
            }
        }

        private IEnumerator CoroutineNone()
        {
            spriteRenderer.sprite = null;
            yield break;
        }

        private IEnumerator CoroutineRed()
        {
            spriteRenderer.sprite = spriteBulletRed;
            yield return new WaitForSeconds(3.0f);
            _subjectOnDiminished.OnNext(this);
        }
    }
}

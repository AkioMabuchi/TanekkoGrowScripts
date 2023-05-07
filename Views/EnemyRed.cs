using System;
using System.Collections;
using System.Collections.Generic;
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
    public class EnemyRed : EnemyBase
    {
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private TanekkoDetector tanekkoDetector;

        [SerializeField] private Sprite spriteMove1;
        [SerializeField] private Sprite spriteMove2;
        [SerializeField] private Sprite spriteMove3;
        [SerializeField] private Sprite spriteMove4;
        [SerializeField] private Sprite spriteAttack1;
        [SerializeField] private Sprite spriteAttack2;

        [SerializeField] private Color colorDamaged = Color.red;

        private int _enemyId;
        
        private readonly Subject<(int, TanekkoElementStatus)> _subjectOnGetDamaged = new();
        public IObservable<(int, TanekkoElementStatus)> OnGetDamaged => _subjectOnGetDamaged;
        private readonly Subject<int> _subjectOnAttack = new();
        public IObservable<int> OnAttack => _subjectOnAttack;
        
        private Tweener _tweenerColor;
        private Coroutine _coroutineAction;

        private bool _canAttack;
        
        private void Reset()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(col =>
                {
                    if (col.gameObject.TryGetComponent(out TanekkoBullet tanekkoBullet))
                    {
                        _subjectOnGetDamaged.OnNext((_enemyId, tanekkoBullet.CurrentElementStatus));
                    }
                }).AddTo(gameObject);

            tanekkoDetector.OnTanekkoDetected
                .Where(_ => _canAttack)
                .Subscribe(_ =>
                {
                    if (_coroutineAction != null)
                    {
                        StopCoroutine(_coroutineAction);
                    }

                    _coroutineAction = StartCoroutine(CoroutineActionAttack());
                }).AddTo(gameObject);
        }

        private void Start()
        {
            _coroutineAction = StartCoroutine(CoroutineActionStop());
        }

        public void SetEnemyId(int enemyId)
        {
            _enemyId = enemyId;
        }

        public override void Move()
        {
            _canAttack = true;
            
            if (_coroutineAction != null)
            {
                StopCoroutine(_coroutineAction);
            }

            _coroutineAction = StartCoroutine(CoroutineActionMove());
        }

        public override void Stop()
        {
            if (_coroutineAction != null)
            {
                StopCoroutine(_coroutineAction);
            }

            _coroutineAction = StartCoroutine(CoroutineActionStop());
        }

        public override void SetPosition(Vector2 position)
        {
            rigidbody2d.position = position;
        }

        public void GetDamaged()
        {
            _tweenerColor?.Kill();
            _tweenerColor = spriteRenderer.DOColor(Color.white, 1.0f).From(colorDamaged).SetLink(gameObject);
        }

        private IEnumerator CoroutineActionStop()
        {
            rigidbody2d.velocity = Vector2.zero;
            spriteRenderer.sprite = spriteMove1;
            yield break;
        }

        private IEnumerator CoroutineActionMove()
        {
            var moveSprites = new List<Sprite>
            {
                spriteMove1,
                spriteMove2,
                spriteMove3,
                spriteMove4
            };

            rigidbody2d.velocity = new Vector2(-1.0f, 0.0f);

            for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
            {
                foreach (var moveSprite in moveSprites)
                {
                    spriteRenderer.sprite = moveSprite;
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }

        private IEnumerator CoroutineActionAttack()
        {
            _canAttack = false;
            rigidbody2d.velocity = Vector2.zero;
            spriteRenderer.sprite = spriteAttack1;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = spriteAttack2;
            yield return new WaitForSeconds(0.5f);
            _subjectOnAttack.OnNext(_enemyId);
            spriteRenderer.sprite = spriteMove1;
            yield return new WaitForSeconds(0.2f);
            _canAttack = true;
            
            StopCoroutine(_coroutineAction);
            _coroutineAction = StartCoroutine(CoroutineActionMove());
        }
    }
}

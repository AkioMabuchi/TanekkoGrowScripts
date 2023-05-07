using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Tanekko : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2d;

        [SerializeField] private float jumpPower;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Vector3 initialPosition;
    
        private readonly Subject<Unit> _subjectOnDamaged = new();
        public IObservable<Unit> OnDamaged => _subjectOnDamaged;
        private void Reset()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(col =>
                {
                    if (col.gameObject.GetComponent<EnemyBase>())
                    {
                        _subjectOnDamaged.OnNext(Unit.Default);
                    }

                    if (col.gameObject.GetComponent<EnemyBullet>())
                    {
                        _subjectOnDamaged.OnNext(Unit.Default);
                    }
                }).AddTo(gameObject);
        }

        public void Jump()
        {
            var velocity = rigidbody2d.velocity;
            velocity.y = jumpPower;
            rigidbody2d.velocity = velocity;
        }

        public void MoveRight()
        {
            var velocity = rigidbody2d.velocity;
            velocity.x = moveSpeed;
            rigidbody2d.velocity = velocity;
        }

        public void MoveLeft()
        {
            var velocity = rigidbody2d.velocity;
            velocity.x = -moveSpeed;
            rigidbody2d.velocity = velocity;
        }

        public void MoveStop()
        {
            var velocity = rigidbody2d.velocity;
            velocity.x = 0.0f;
            rigidbody2d.velocity = velocity;
        }

        public void Stop()
        {
            rigidbody2d.velocity = Vector2.zero;
        }

        public void TeleportToInitialPosition()
        {
            transform.position = initialPosition;
        }
    }
}

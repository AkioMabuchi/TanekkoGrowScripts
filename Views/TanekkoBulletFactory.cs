using System;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace Views
{
    public class TanekkoBulletFactory : MonoBehaviour
    {
        [SerializeField] private TanekkoBullet prefabTanekkoBullet;

        private ObjectPool<TanekkoBullet> _objectPoolTanekkoBullets;

        private readonly Subject<Unit> _subjectOnReleasedBullet = new();
        public IObservable<Unit> OnReleasedBullet => _subjectOnReleasedBullet;
        private void Awake()
        {
            _objectPoolTanekkoBullets = new ObjectPool<TanekkoBullet>(() =>
            {
                var tanekkoBullet = Instantiate(prefabTanekkoBullet, transform);

                tanekkoBullet.OnExpired.Subscribe(expiredTanekkoBullet =>
                {
                    _subjectOnReleasedBullet.OnNext(Unit.Default);
                    _objectPoolTanekkoBullets.Release(expiredTanekkoBullet);
                }).AddTo(tanekkoBullet.gameObject);
            
                tanekkoBullet.OnHitEnemy.Subscribe(hitTanekkoBullet =>
                {
                    _subjectOnReleasedBullet.OnNext(Unit.Default);
                    _objectPoolTanekkoBullets.Release(hitTanekkoBullet);
                }).AddTo(tanekkoBullet.gameObject);
            
                return tanekkoBullet;
            }, takenTanekkoBullet =>
            {
                takenTanekkoBullet.TakeObject();
            }, releasedTanekkoBullet =>
            {
                releasedTanekkoBullet.ReleaseObject();
            }, destroyedTanekkoBullet =>
            {
                Destroy(destroyedTanekkoBullet);
            });
        }

        public void ShootTanekkoBullet(Vector3 position, TanekkoElementStatus elementStatus, Vector2 velocity, float durationTime)
        {
            var tanekkoBullet = _objectPoolTanekkoBullets.Get();
            tanekkoBullet.transform.position = position;
            tanekkoBullet.SetElementStatus(elementStatus);
            tanekkoBullet.ChangeAnimationWithElementStatus(elementStatus);
            tanekkoBullet.SetVelocity(velocity);
            tanekkoBullet.SetExpirationDurationTime(durationTime);
            tanekkoBullet.StartCountingDurationTime();
        }
    }
}

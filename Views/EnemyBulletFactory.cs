using Enums;
using Structs;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace Views
{
    public class EnemyBulletFactory : MonoBehaviour
    {
        [SerializeField] private EnemyBullet prefabEnemyBullet;

        private ObjectPool<EnemyBullet> _objectPoolEnemyBullets;
    
        private void Awake()
        {
            _objectPoolEnemyBullets = new ObjectPool<EnemyBullet>(() =>
            {
                var instanceEnemyBullet = Instantiate(prefabEnemyBullet, transform);

                instanceEnemyBullet.OnDiminished.Subscribe(expiredEnemyBullet =>
                {
                    _objectPoolEnemyBullets.Release(expiredEnemyBullet);
                }).AddTo(instanceEnemyBullet.gameObject);

                instanceEnemyBullet.OnHitTanekko.Subscribe(hitEnemyBullet =>
                {
                    _objectPoolEnemyBullets.Release(hitEnemyBullet);
                }).AddTo(instanceEnemyBullet.gameObject);

                return instanceEnemyBullet;
            }, takenEnemyBullet =>
            {
                takenEnemyBullet.OnTakeObject();
            }, releasedEnemyBullet =>
            {
                releasedEnemyBullet.OnReleaseObject();
            }, destroyedTanekkoBullet =>
            {
                Destroy(destroyedTanekkoBullet);
            });
        }

        public void ShootEnemyBullet(EnemyBulletType enemyBulletType, Vector3 position, Vector2 velocity)
        {
            var enemyBullet = _objectPoolEnemyBullets.Get();

            enemyBullet.Shoot(enemyBulletType);
            enemyBullet.SetPosition(position);
            enemyBullet.SetVelocity(velocity);
        }
    }
}

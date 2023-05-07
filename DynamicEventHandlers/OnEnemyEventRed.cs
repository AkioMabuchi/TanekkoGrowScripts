using CommonProcesses;
using Enums;
using Models;
using UniRx;
using UnityEngine;
using Views;

namespace DynamicEventHandlers
{
    public class OnEnemyEventRed : OnEnemyEventBase
    {
        public OnEnemyEventRed(DefeatEnemyProcess defeatEnemyProcess, EnemyModelRed enemyModelRed, EnemyRed enemyRed,
            EnemyBulletFactory enemyBulletFactory, SoundPlayer soundPlayer)
        {
            enemyRed.OnGetDamaged.Subscribe(tuple =>
            {
                var (enemyId, tanekkoBulletElementStatus) = tuple;
                enemyModelRed.Damage(tanekkoBulletElementStatus);
                if (enemyModelRed.IsDefeated)
                {
                    defeatEnemyProcess.DefeatEnemy(enemyId, EnemyType.Red, 20);
                }
                else
                {
                    enemyRed.GetDamaged();
                    soundPlayer.PlaySound(SoundName.EnemyDamagedNormal);
                }
            }).AddTo(compositeDisposable);

            enemyRed.OnAttack.Subscribe(enemyId =>
            {
                var pos = enemyRed.transform.position;
                var bulletPosition = new Vector3(pos.x - 0.3f, pos.y + 1.0f, 0.0f);
                var bulletVelocity = new Vector2(-15.0f, 0.0f);
                soundPlayer.PlaySound(SoundName.EnemyRedAttack);
                enemyBulletFactory.ShootEnemyBullet(EnemyBulletType.Red, bulletPosition, bulletVelocity);
            }).AddTo(compositeDisposable);
        }
    }
}
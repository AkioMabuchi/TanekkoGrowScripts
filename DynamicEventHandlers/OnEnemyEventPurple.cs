using CommonProcesses;
using Enums;
using Models;
using UniRx;
using Views;

namespace DynamicEventHandlers
{
    public class OnEnemyEventPurple : OnEnemyEventBase
    {
        public OnEnemyEventPurple(DefeatEnemyProcess defeatEnemyProcess, EnemyModelPurple enemyModelPurple,
            EnemyPurple enemyPurple, SoundPlayer soundPlayer)
        {
            enemyPurple.OnGetDamaged.Subscribe(tuple =>
            {
                var (enemyId, tanekkoBulletElementStatus) = tuple;
                enemyModelPurple.Damage(tanekkoBulletElementStatus);
                if (enemyModelPurple.IsDefeated)
                {
                    defeatEnemyProcess.DefeatEnemy(enemyId, EnemyType.Purple, 10);
                }
                else
                {
                    enemyPurple.GetDamaged();
                    soundPlayer.PlaySound(SoundName.EnemyDamagedNormal);
                }
            }).AddTo(compositeDisposable);
        }
    }
}
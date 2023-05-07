using CommonProcesses;
using Enums;
using Models;
using UniRx;
using Views;

namespace DynamicEventHandlers
{
    public class OnEnemyEventBlue : OnEnemyEventBase
    {
        public OnEnemyEventBlue(DefeatEnemyProcess defeatEnemyProcess, EnemyModelBlue enemyModelBlue,
            EnemyBlue enemyBlue, SoundPlayer soundPlayer)
        {
            enemyBlue.OnGetDamaged.Subscribe(tuple =>
            {
                var (enemyId, tanekkoBulletElementStatus) = tuple;
                enemyModelBlue.Damage(tanekkoBulletElementStatus);
                if (enemyModelBlue.IsDefeated)
                {
                    defeatEnemyProcess.DefeatEnemy(enemyId, EnemyType.Blue, 10);
                }
                else
                {
                    enemyBlue.GetDamaged();
                    soundPlayer.PlaySound(SoundName.EnemyDamagedNormal);
                }
            }).AddTo(compositeDisposable);

            enemyBlue.OnJump.Subscribe(enemyId =>
            {
                soundPlayer.PlaySound(SoundName.EnemyBlueJump);
            }).AddTo(compositeDisposable);
        }
    }
}
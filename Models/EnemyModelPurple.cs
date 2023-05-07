using Enums;
using UniRx;

namespace Models
{
    public class EnemyModelPurple: EnemyModelBase
    {
        private readonly ReactiveProperty<int> _reactivePropertyLifeCount = new(3);
        public bool IsDefeated => _reactivePropertyLifeCount.Value <= 0;
        public void Damage(TanekkoElementStatus tanekkoBulletElementStatus)
        {
            if (tanekkoBulletElementStatus == TanekkoElementStatus.Ice)
            {
                _reactivePropertyLifeCount.Value = 0;
            }
            else
            {
                _reactivePropertyLifeCount.Value--;
            }
        }
    }
}
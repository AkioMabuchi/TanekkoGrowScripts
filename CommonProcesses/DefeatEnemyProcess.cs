using Enums;
using Models;
using UniRx;
using UnityEngine;
using VContainer;
using Views;

namespace CommonProcesses
{
    public class DefeatEnemyProcess
    {
        private readonly EnemiesModel _enemiesModel;
        private readonly ScoreModel _scoreModel;
        private readonly TanekkoModel _tanekkoModel;
        private readonly TanekkoExperienceModel _tanekkoExperienceModel;

        private readonly EnemyFactory _enemyFactory;
        private readonly SoundPlayer _soundPlayer;

        [Inject]
        public DefeatEnemyProcess(EnemiesModel enemiesModel, ScoreModel scoreModel, TanekkoModel tanekkoModel,
            TanekkoExperienceModel tanekkoExperienceModel, EnemyFactory enemyFactory, SoundPlayer soundPlayer)
        {
            _enemiesModel = enemiesModel;
            _scoreModel = scoreModel;
            _tanekkoModel = tanekkoModel;
            _tanekkoExperienceModel = tanekkoExperienceModel;

            _enemyFactory = enemyFactory;
            _soundPlayer = soundPlayer;
        }

        public void DefeatEnemy(int enemyId, EnemyType enemyType, int score)
        {
            _soundPlayer.PlaySound(SoundName.EnemyDamagedNormal);
            
            _enemiesModel.ClearEnemy(enemyId);
            _tanekkoExperienceModel.AddDefeatedEnemy(enemyType);
            _scoreModel.AddScore(score);

            if (_tanekkoExperienceModel.DefeatedEnemies.Count >= 11 &&
                _tanekkoModel.GrowthStatus == TanekkoGrowthStatus.Bud)
            {
                var efficientEnemyType = _tanekkoModel.ElementStatus switch
                {
                    TanekkoElementStatus.Fire => EnemyType.Red,
                    TanekkoElementStatus.Ice => EnemyType.Blue,
                    TanekkoElementStatus.Poison => EnemyType.Purple,
                    _ => EnemyType.None
                };

                if (_tanekkoExperienceModel.IsBiggestCountOfEnemyTypesInRange(efficientEnemyType, 4,
                        int.MaxValue))
                {
                    _tanekkoModel.SetGrowthStatus(TanekkoGrowthStatus.FlowerA);
                }
                else
                {
                    _tanekkoModel.SetGrowthStatus(TanekkoGrowthStatus.FlowerB);
                }
            }
            else if (_tanekkoExperienceModel.DefeatedEnemies.Count >= 5 &&
                     _tanekkoModel.GrowthStatus == TanekkoGrowthStatus.Seed &&
                     _tanekkoModel.ElementStatus != TanekkoElementStatus.Normal)
            {
                _tanekkoModel.SetGrowthStatus(TanekkoGrowthStatus.Bud);
            }
        }
    }
}

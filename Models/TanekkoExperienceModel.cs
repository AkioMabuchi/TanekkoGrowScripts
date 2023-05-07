using System;
using System.Collections.Generic;
using Enums;
using UniRx;

namespace Models
{
    public class TanekkoExperienceModel
    {
        private readonly ReactiveCollection<EnemyType> _reactiveCollectionDefeatedEnemies = new();
        public IReadOnlyList<EnemyType> DefeatedEnemies => _reactiveCollectionDefeatedEnemies;

        public void ResetDefeatedEnemy()
        {
            _reactiveCollectionDefeatedEnemies.Clear();
        }
    
        public void AddDefeatedEnemy(EnemyType enemyType)
        {
            _reactiveCollectionDefeatedEnemies.Add(enemyType);
        }

        public bool IsBiggestCountOfEnemyTypesInRange(EnemyType targetEnemyType, int min, int max)
        {
            var dictionaryCount = new Dictionary<EnemyType, int>();
            for (var index = min; index <= max && index < _reactiveCollectionDefeatedEnemies.Count; index++)
            {
                var enemyType = _reactiveCollectionDefeatedEnemies[index];
                if (dictionaryCount.ContainsKey(enemyType))
                {
                    dictionaryCount[enemyType]++;
                }
                else
                {
                    dictionaryCount.Add(enemyType, 1);
                }
            }

            var mostCommonEnemyTypes = new HashSet<EnemyType>();
            var mostCommonEnemyTypeCount = 0;
            foreach (var keyValuePair in dictionaryCount)
            {
                if (keyValuePair.Value > mostCommonEnemyTypeCount)
                {
                    mostCommonEnemyTypes.Clear();
                    mostCommonEnemyTypes.Add(keyValuePair.Key);
                    mostCommonEnemyTypeCount = keyValuePair.Value;
                }
                else if (keyValuePair.Value == mostCommonEnemyTypeCount)
                {
                    mostCommonEnemyTypes.Add(keyValuePair.Key);
                }
            }

            return mostCommonEnemyTypes.Contains(targetEnemyType);
        }
    }
}
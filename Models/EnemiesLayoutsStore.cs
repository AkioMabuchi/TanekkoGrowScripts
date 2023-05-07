using System.Collections.Generic;
using Structs;
using UniRx;

namespace Models
{
    public class EnemiesLayoutsStore
    {
        private readonly ReactiveCollection<List<EnemyDistributeParamsGroup>> _reactiveCollectionEnemiesLayouts = new();

        public IReadOnlyList<EnemyDistributeParamsGroup> RandomEnemiesLayout =>
            _reactiveCollectionEnemiesLayouts[UnityEngine.Random.Range(0, _reactiveCollectionEnemiesLayouts.Count)];

        public void SetEnemiesLayouts(IEnumerable<IEnumerable<EnemyDistributeParamsGroup>> enemiesLayouts)
        {
            foreach (var enemiesLayout in enemiesLayouts)
            {
                _reactiveCollectionEnemiesLayouts.Add(new List<EnemyDistributeParamsGroup>(enemiesLayout));
            }
        }
    }
}
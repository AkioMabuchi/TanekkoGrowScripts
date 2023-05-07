using System.Collections.Generic;
using Structs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemiesLayout")]
    public class ScriptableObjectEnemiesLayout : ScriptableObject
    {
        [SerializeField] private EnemyDistributeParamsGroup[] distributeParamsGroups;
        public IEnumerable<EnemyDistributeParamsGroup> DistributeParamsGroups => distributeParamsGroups;
    }
}

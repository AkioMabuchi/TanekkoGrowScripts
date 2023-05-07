using System.Collections.Generic;
using ScriptableObjects;
using Structs;
using UnityEngine;

namespace Views
{
    public class ParamsDefiner : MonoBehaviour
    {
        [SerializeField] private ScriptableObjectEnemiesLayout[] enemiesLayouts;

        public IEnumerable<IEnumerable<EnemyDistributeParamsGroup>> EnemiesLayouts
        {
            get
            {
                var enemiesLayoutsList = new List<IEnumerable<EnemyDistributeParamsGroup>>();
                foreach (var enemiesLayout in enemiesLayouts)
                {
                    enemiesLayoutsList.Add(enemiesLayout.DistributeParamsGroups);
                }

                return enemiesLayoutsList;
            }
        }
    }
}


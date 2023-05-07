using System;
using Enums;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public struct EnemyDistributeParamsGroup
    {
        public EnemyType type;
        public Vector3 position;
    }
}
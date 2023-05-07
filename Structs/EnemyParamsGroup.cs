using System;
using Enums;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public struct EnemyParamsGroup
    {
        public EnemyType type;
        public Vector3 position;
    }
}
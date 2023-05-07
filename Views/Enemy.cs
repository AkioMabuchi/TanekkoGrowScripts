using UnityEngine;

namespace Views
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public abstract void Move();
        public abstract void Stop();
        public abstract void SetPosition(Vector2 position);
    }
}

using System;
using UniRx;

namespace Models
{
    public class EnemiesModel
    {
        private readonly ReactiveDictionary<int, EnemyModelBase> _reactiveDictionaryEnemies = new();
        public IObservable<DictionaryAddEvent<int, EnemyModelBase>> OnAddedEnemy => _reactiveDictionaryEnemies.ObserveAdd();

        public IObservable<DictionaryRemoveEvent<int, EnemyModelBase>> OnRemoveEnemy =>
            _reactiveDictionaryEnemies.ObserveRemove();

        public IObservable<Unit> OnResetEnemies => _reactiveDictionaryEnemies.ObserveReset();

        public void AddEnemyRed(int enemyId)
        {
            _reactiveDictionaryEnemies.Add(enemyId, new EnemyModelRed());
        }

        public void AddEnemyBlue(int enemyId)
        {
            _reactiveDictionaryEnemies.Add(enemyId, new EnemyModelBlue());
        }

        public void AddEnemyPurple(int enemyId)
        {
            _reactiveDictionaryEnemies.Add(enemyId, new EnemyModelPurple());
        }

        public void ClearEnemy(int enemyId)
        {
            _reactiveDictionaryEnemies.Remove(enemyId);
        }
        
        public void ClearAllEnemies()
        {
            _reactiveDictionaryEnemies.Clear();
        }
    }
}
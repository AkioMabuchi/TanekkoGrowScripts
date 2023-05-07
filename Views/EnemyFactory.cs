using System;
using System.Collections.Generic;
using Models;
using ScriptableObjects;
using Structs;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace Views
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private EnemyRed prefabEnemyRed;
        [SerializeField] private EnemyBlue prefabEnemyBlue;
        [SerializeField] private EnemyPurple prefabEnemyPurple;

        private readonly Dictionary<int, EnemyBase> _enemies = new();

        public EnemyRed GenerateEnemyRed(int enemyId)
        {
            var enemyRed = Instantiate(prefabEnemyRed, transform);
            enemyRed.SetEnemyId(enemyId);
            _enemies.Add(enemyId, enemyRed);
            return enemyRed;
        }

        public EnemyBlue GenerateEnemyBlue(int enemyId)
        {
            var enemyBlue = Instantiate(prefabEnemyBlue, transform);
            enemyBlue.SetEnemyId(enemyId);
            _enemies.Add(enemyId,enemyBlue);
            return enemyBlue;
        }

        public EnemyPurple GenerateEnemyPurple(int enemyId)
        {
            var enemyPurple = Instantiate(prefabEnemyPurple, transform);
            enemyPurple.SetEnemyId(enemyId);
            _enemies.Add(enemyId, enemyPurple);
            return enemyPurple;
        }

        public void MakeAllEnemiesMove()
        {
            foreach (var enemy in _enemies.Values)
            {
                enemy.Move();
            }
        }

        public void MakeAllEnemiesStop()
        {
            foreach (var enemy in _enemies.Values)
            {
                enemy.Stop();
            }
        }

        public void SetPositionEnemy(int enemyId, Vector2 position)
        {
            if (_enemies.TryGetValue(enemyId, out var enemy))
            {
                enemy.SetPosition(position);
            }
        }
        public void DestroyEnemy(int enemyId)
        {
            if (_enemies.TryGetValue(enemyId, out var enemy))
            {
                Destroy(enemy.gameObject);
            }

            _enemies.Remove(enemyId);
        }

        public void DestroyAllEnemies()
        {
            foreach (var enemy in _enemies.Values)
            {
                Destroy(enemy.gameObject);
            }
            
            _enemies.Clear();
        }
    }
}
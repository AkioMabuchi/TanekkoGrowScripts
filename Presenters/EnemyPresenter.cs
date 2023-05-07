using System.Collections.Generic;
using CommonProcesses;
using DynamicEventHandlers;
using EventHandlers;
using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class EnemyPresenter: IInitializable
    {
        private readonly DefeatEnemyProcess _defeatEnemyProcess;
        private readonly EnemiesModel _enemiesModel;
        private readonly EnemyBulletFactory _enemyBulletFactory;
        private readonly EnemyFactory _enemyFactory;
        private readonly SoundPlayer _soundPlayer;

        private readonly Dictionary<int, OnEnemyEventBase> _dictionaryEnemyEvents = new();

        [Inject]
        public EnemyPresenter(DefeatEnemyProcess defeatEnemyProcess, EnemiesModel enemiesModel,
            EnemyBulletFactory enemyBulletFactory, EnemyFactory enemyFactory, SoundPlayer soundPlayer)
        {
            _defeatEnemyProcess = defeatEnemyProcess;
            _enemiesModel = enemiesModel;
            _enemyBulletFactory = enemyBulletFactory;
            _enemyFactory = enemyFactory;
            _soundPlayer = soundPlayer;
        }

        public void Initialize()
        {
            _enemiesModel.OnAddedEnemy.Subscribe(enemyAddEvent =>
            {
                var enemyId = enemyAddEvent.Key;
                var enemyModel = enemyAddEvent.Value;
                
                switch (enemyModel)
                {
                    case EnemyModelRed enemyModelRed:
                    {
                        _dictionaryEnemyEvents.Add(enemyId, new OnEnemyEventRed(
                            _defeatEnemyProcess,
                            enemyModelRed,
                            _enemyFactory.GenerateEnemyRed(enemyId),
                            _enemyBulletFactory,
                            _soundPlayer
                        ));
                        break;
                    }
                    case EnemyModelBlue enemyModelBlue:
                    {
                        _dictionaryEnemyEvents.Add(enemyId, new OnEnemyEventBlue(
                            _defeatEnemyProcess,
                            enemyModelBlue,
                            _enemyFactory.GenerateEnemyBlue(enemyId),
                            _soundPlayer
                        ));
                        break;
                    }
                    case EnemyModelPurple enemyModelPurple:
                    {
                        _dictionaryEnemyEvents.Add(enemyId, new OnEnemyEventPurple(
                            _defeatEnemyProcess,
                            enemyModelPurple,
                            _enemyFactory.GenerateEnemyPurple(enemyId),
                            _soundPlayer
                        ));
                        break;
                    }
                }
            });

            _enemiesModel.OnRemoveEnemy.Subscribe(enemyRemoveEvent =>
            {
                var enemyId = enemyRemoveEvent.Key;
                if (_dictionaryEnemyEvents.TryGetValue(enemyId, out var enemyEvent))
                {
                    enemyEvent.CompositeDispose();
                }
                _dictionaryEnemyEvents.Remove(enemyId);
                _enemyFactory.DestroyEnemy(enemyId);
            });

            _enemiesModel.OnResetEnemies.Subscribe(_ =>
            {
                foreach (var enemyEvent in _dictionaryEnemyEvents.Values)
                {
                    enemyEvent.CompositeDispose();
                }
                _dictionaryEnemyEvents.Clear();
                _enemyFactory.DestroyAllEnemies();
            });
        }
    }
}
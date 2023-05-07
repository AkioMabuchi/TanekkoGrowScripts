using Models;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnDefineParams : IInitializable
    {
        private readonly EnemiesLayoutsStore _enemiesLayoutsStore;
        private readonly ParamsDefiner _paramsDefiner;

        [Inject]
        public OnDefineParams(EnemiesLayoutsStore enemiesLayoutsStore, ParamsDefiner paramsDefiner)
        {
            _enemiesLayoutsStore = enemiesLayoutsStore;
            _paramsDefiner = paramsDefiner;
        }
        
        public void Initialize()
        {
            _enemiesLayoutsStore.SetEnemiesLayouts(_paramsDefiner.EnemiesLayouts);
        }
    }
}
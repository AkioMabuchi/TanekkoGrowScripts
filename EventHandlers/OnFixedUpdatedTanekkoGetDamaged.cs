using Models;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnFixedUpdatedTanekkoGetDamaged : IFixedTickable
    {
        private readonly TanekkoModel _tanekkoModel;

        [Inject]
        public OnFixedUpdatedTanekkoGetDamaged(TanekkoModel tanekkoModel)
        {
            _tanekkoModel = tanekkoModel;
        }
        
        public void FixedTick()
        {
            if (_tanekkoModel.IsInvincible)
            {
                _tanekkoModel.DecreaseInvincibleTickCount();
            }
        }
    }
}
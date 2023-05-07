using UniRx;

namespace DynamicEventHandlers
{
    public abstract class OnEnemyEventBase
    {
        protected readonly CompositeDisposable compositeDisposable = new();

        public void CompositeDispose()
        {
            compositeDisposable.Dispose();
        }
    }
}
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class TitleScreenPresenter : IInitializable
    {
        private readonly TitleScreen _titleScreen;

        [Inject]
        public TitleScreenPresenter(TitleScreen titleScreen)
        {
            _titleScreen = titleScreen;
        }
    
        public void Initialize()
        {

        }
    }
}
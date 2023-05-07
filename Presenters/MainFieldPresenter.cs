using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class MainFieldPresenter : IInitializable
    {
        private readonly MainFieldModel _mainFieldModel;
        private readonly MainField _mainField;

        [Inject]
        public MainFieldPresenter(MainFieldModel mainFieldModel, MainField mainField)
        {
            _mainFieldModel = mainFieldModel;
            _mainField = mainField;
        }
        
        public void Initialize()
        {
            _mainFieldModel.OnAddedMainField.Subscribe(addEvent =>
            {
                var tilePositionX = addEvent.Index;
                var mainFieldType = addEvent.Value;
                _mainField.DrawTiles(tilePositionX, mainFieldType);
            });
        }
    }
}

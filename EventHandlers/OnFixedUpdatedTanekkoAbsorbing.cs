using Enums;
using Models;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnFixedUpdatedTanekkoAbsorbing : IFixedTickable
    {
        private readonly TanekkoModel _tanekkoModel;
        private readonly MainFieldModel _mainFieldModel;

        private readonly SoundPlayer _soundPlayer;

        [Inject]
        public OnFixedUpdatedTanekkoAbsorbing(TanekkoModel tanekkoModel, MainFieldModel mainFieldModel,
            SoundPlayer soundPlayer)
        {
            _tanekkoModel = tanekkoModel;
            _mainFieldModel = mainFieldModel;

            _soundPlayer = soundPlayer;
        }

        public void FixedTick()
        {
            if (_tanekkoModel.IsAbsorbing)
            {
                _tanekkoModel.IncreaseTickCountAbsorbing();
                switch (_tanekkoModel.TickCountAbsorbing)
                {
                    case 200:
                    {
                        _soundPlayer.PlaySound(SoundName.TanekkoAbsorbed);
                        
                        _tanekkoModel.ResetTickCountAbsorbing();
                        _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Idle);

                        switch (_mainFieldModel.GetMainFieldTypeByIndex(_tanekkoModel.MainFieldPositionX))
                        {
                            case MainFieldType.Desert:
                            {
                                _tanekkoModel.SetElementStatus(TanekkoElementStatus.Fire);
                                break;
                            }
                            case MainFieldType.Ice:
                            {
                                _tanekkoModel.SetElementStatus(TanekkoElementStatus.Ice);
                                break;
                            }
                            case MainFieldType.Poison:
                            {
                                _tanekkoModel.SetElementStatus(TanekkoElementStatus.Poison);
                                break;
                            } 
                        }
                        
                        break;
                    }
                }
            }
        }
    }
}
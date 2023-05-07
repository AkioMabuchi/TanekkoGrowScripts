using Enums;
using Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnFixedUpdatedTanekkoAttacking : IFixedTickable
    {
        private readonly TanekkoModel _tanekkoModel;

        private readonly SoundPlayer _soundPlayer;
        private readonly Tanekko _tanekko;
        private readonly TanekkoBulletFactory _tanekkoBulletFactory;

        [Inject]
        public OnFixedUpdatedTanekkoAttacking(TanekkoModel tanekkoModel, SoundPlayer soundPlayer, Tanekko tanekko,
            TanekkoBulletFactory tanekkoBulletFactory)
        {
            _tanekkoModel = tanekkoModel;

            _soundPlayer = soundPlayer;
            _tanekko = tanekko;
            _tanekkoBulletFactory = tanekkoBulletFactory;
        }
        

        public void FixedTick()
        {
            if (_tanekkoModel.IsAttacking)
            {
                _tanekkoModel.IncreaseTickCountAttacking();
                switch (_tanekkoModel.TickCountAttacking)
                {
                    case 30:
                    {
                        const float positionX = 1.0f;
                        const float velocityX = 15.0f;
                        var position = new Vector3(_tanekko.transform.position.x, _tanekko.transform.position.y + 1.0f,
                            0.0f);
                        var velocity = Vector2.zero;
                        switch (_tanekkoModel.LookDirection)
                        {
                            case TanekkoLookDirection.Right:
                            {
                                position.x -= positionX;
                                velocity.x += velocityX;
                                break;
                            }
                            case TanekkoLookDirection.Left:
                            {
                                position.x += positionX;
                                velocity.x -= velocityX;
                                break;
                            }
                        }

                        switch (_tanekkoModel.ElementStatus)
                        {
                            case TanekkoElementStatus.Normal:
                            {
                                _soundPlayer.PlaySound(SoundName.TanekkoAttackNormal);
                                break;
                            }
                            case TanekkoElementStatus.Fire:
                            {
                                _soundPlayer.PlaySound(SoundName.TanekkoAttackFire);
                                break;
                            }
                            case TanekkoElementStatus.Ice:
                            {
                                _soundPlayer.PlaySound(SoundName.TanekkoAttackIce);
                                break;
                            }
                            case TanekkoElementStatus.Poison:
                            {
                                _soundPlayer.PlaySound(SoundName.TanekkoAttackPoison);
                                break;
                            }
                        }
                        _tanekkoBulletFactory.ShootTanekkoBullet(position, _tanekkoModel.ElementStatus, velocity, 2.0f);
                        break;
                    }
                    case 50:
                    {
                        _tanekkoModel.ResetTickCountAttacking();
                        _tanekkoModel.SetMotionStatus(TanekkoMotionStatus.Idle);
                        break;
                    }
                }
            }
        }
    }
}
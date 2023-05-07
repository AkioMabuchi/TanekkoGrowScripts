using System;
using Enums;
using UniRx;
using UnityEngine;

namespace Models
{
    public class TanekkoModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyLifeCount = new(3);
        public IObservable<int> OnChangedLifeCount => _reactivePropertyLifeCount;
        public int LifeCount => _reactivePropertyLifeCount.Value;

        private readonly ReactiveProperty<TanekkoGrowthStatus>
            _reactivePropertyGrowthStatus = new(TanekkoGrowthStatus.Seed);

        public TanekkoGrowthStatus GrowthStatus => _reactivePropertyGrowthStatus.Value;

        public IObservable<TanekkoGrowthStatus> OnChangedGrowthStatus => _reactivePropertyGrowthStatus;

        private readonly ReactiveProperty<TanekkoElementStatus> _reactivePropertyElementStatus =
            new(TanekkoElementStatus.Normal);

        public IObservable<TanekkoElementStatus> OnChangedElementStatus => _reactivePropertyElementStatus;
        public TanekkoElementStatus ElementStatus => _reactivePropertyElementStatus.Value;

        private readonly ReactiveProperty<TanekkoMotionStatus>
            _reactivePropertyMotionStatus = new(TanekkoMotionStatus.Idle);

        public IObservable<TanekkoMotionStatus> OnChangedMotionStatus => _reactivePropertyMotionStatus;

        private readonly ReactiveProperty<TanekkoLookDirection> _reactivePropertyLookDirection =
            new(TanekkoLookDirection.Right);

        public IObservable<TanekkoLookDirection> OnChangedLookDirection => _reactivePropertyLookDirection;
        public TanekkoLookDirection LookDirection => _reactivePropertyLookDirection.Value;
        
        private readonly ReactiveProperty<int> _reactivePropertyMainFieldPositionX = new(0);
        public int MainFieldPositionX => _reactivePropertyMainFieldPositionX.Value;
        
        private readonly ReactiveProperty<int> _reactivePropertyBulletCount = new(3);


        private readonly ReactiveProperty<int> _reactivePropertyTickCountAttacking = new(0);
        public int TickCountAttacking => _reactivePropertyTickCountAttacking.Value;
        private readonly ReactiveProperty<int> _reactivePropertyTickCountAbsorbing = new(0);
        public int TickCountAbsorbing => _reactivePropertyTickCountAbsorbing.Value;
        private readonly ReactiveProperty<int> _reactivePropertyInvincibleTickCount = new(0);
        

        public bool CanJump =>
            _reactivePropertyMotionStatus.Value is TanekkoMotionStatus.Idle or TanekkoMotionStatus.Running;

        public bool CanMove =>
            _reactivePropertyMotionStatus.Value is TanekkoMotionStatus.Idle or TanekkoMotionStatus.Running
                or TanekkoMotionStatus.Jumping;

        public bool CanAttack => _reactivePropertyBulletCount.Value > 0 &&
                                 _reactivePropertyMotionStatus.Value == TanekkoMotionStatus.Idle;

        public bool CanAbsorb =>
            _reactivePropertyMotionStatus.Value == TanekkoMotionStatus.Idle &&
            _reactivePropertyGrowthStatus.Value is TanekkoGrowthStatus.Seed or TanekkoGrowthStatus.Bud;
        public bool CanGetDamaged => _reactivePropertyInvincibleTickCount.Value <= 0;

        public bool IsJumping =>
            _reactivePropertyMotionStatus.Value == TanekkoMotionStatus.Jumping;
        public bool IsAttacking =>
            _reactivePropertyMotionStatus.Value == TanekkoMotionStatus.Attacking;
        public bool IsAbsorbing =>
            _reactivePropertyMotionStatus.Value == TanekkoMotionStatus.Absorbing;

        public bool IsInvincible => _reactivePropertyInvincibleTickCount.Value > 0;
        
        
        public void SetGrowthStatus(TanekkoGrowthStatus growthStatus)
        {
            _reactivePropertyGrowthStatus.Value = growthStatus;
        }

        public void SetElementStatus(TanekkoElementStatus elementStatus)
        {
            _reactivePropertyElementStatus.Value = elementStatus;
        }

        public void SetMotionStatus(TanekkoMotionStatus motionStatus)
        {
            _reactivePropertyMotionStatus.Value = motionStatus;
        }

        public void ChangeMotionOnMove()
        {
            switch (_reactivePropertyMotionStatus.Value)
            {
                case TanekkoMotionStatus.Idle:
                {
                    _reactivePropertyMotionStatus.Value = TanekkoMotionStatus.Running;
                    break;
                }
            }
        }

        public void ChangeMotionOnMoveStop()
        {
            switch (_reactivePropertyMotionStatus.Value)
            {
                case TanekkoMotionStatus.Running:
                {
                    _reactivePropertyMotionStatus.Value = TanekkoMotionStatus.Idle;
                    break;
                }
            }
        }

        public void SetMainFieldPositionXWithActualPositionX(float actualPositionX)
        {
            _reactivePropertyMainFieldPositionX.Value = Mathf.FloorToInt(actualPositionX);
        }
    
        public void IncreaseBulletCount()
        {
            _reactivePropertyBulletCount.Value++;
        }

        public void DecreaseBulletCount()
        {
            _reactivePropertyBulletCount.Value--;
        }

        public void SetLifeCount(int lifeCount)
        {
            _reactivePropertyLifeCount.Value = lifeCount;
        }
        public void DecreaseLifeCount()
        {
            _reactivePropertyLifeCount.Value--;
        }

        public void SetLookDirection(TanekkoLookDirection lookDirection)
        {
            _reactivePropertyLookDirection.Value = lookDirection;
        }

        public void ResetTickCountAttacking()
        {
            _reactivePropertyTickCountAttacking.Value = 0;
        }

        public void IncreaseTickCountAttacking()
        {
            _reactivePropertyTickCountAttacking.Value++;
        }

        public void ResetTickCountAbsorbing()
        {
            _reactivePropertyTickCountAbsorbing.Value = 0;
        }

        public void IncreaseTickCountAbsorbing()
        {
            _reactivePropertyTickCountAbsorbing.Value++;
        }

        public void SetInvincibleTickCount(int invincibleTickCount)
        {
            _reactivePropertyInvincibleTickCount.Value = invincibleTickCount;
        }

        public void DecreaseInvincibleTickCount()
        {
            if (_reactivePropertyInvincibleTickCount.Value > 0)
            {
                _reactivePropertyInvincibleTickCount.Value--;
            }
        }
    }
}
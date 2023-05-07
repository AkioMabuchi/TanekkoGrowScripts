using System;
using Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Views
{
    public class InputView : MonoBehaviour
    {
        private readonly Subject<Unit> _subjectOnMainGameStart = new();
        public IObservable<Unit> OnMainGameStart => _subjectOnMainGameStart;
        private readonly Subject<Unit> _subjectOnMainGameRestart = new();
        public IObservable<Unit> OnMainGameRestart => _subjectOnMainGameRestart;

        private readonly Subject<TanekkoMoveDirection> _subjectOnMove = new();
        public IObservable<TanekkoMoveDirection> OnMove => _subjectOnMove;
        private readonly Subject<Unit> _subjectOnJump = new();
        public IObservable<Unit> OnJump => _subjectOnJump;
        private readonly Subject<Unit> _subjectOnAttack = new();
        public IObservable<Unit> OnShoot => _subjectOnAttack;

        private readonly Subject<Unit> _subjectOnAbsorb = new();
        public IObservable<Unit> OnAbsorb => _subjectOnAbsorb;
        private readonly Subject<Unit> _subjectOnAbsorbed = new();
        public IObservable<Unit> OnAbsorbed => _subjectOnAbsorbed;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Keyboard.current == null)
                    {
                        return;
                    }

                    if (Keyboard.current.enterKey.wasPressedThisFrame)
                    {
                        _subjectOnMainGameStart.OnNext(Unit.Default);
                    }

                    if (Keyboard.current.rKey.wasPressedThisFrame)
                    {
                        _subjectOnMainGameRestart.OnNext(Unit.Default);
                    }

                    if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                    {
                        _subjectOnMove.OnNext(TanekkoMoveDirection.Right);
                    }
                    else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                    {
                        _subjectOnMove.OnNext(TanekkoMoveDirection.Left);
                    }
                    else
                    {
                        _subjectOnMove.OnNext(TanekkoMoveDirection.None);
                    }

                    if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
                    {
                        _subjectOnJump.OnNext(Unit.Default);
                    }

                    if (Keyboard.current.spaceKey.wasPressedThisFrame)
                    {
                        _subjectOnAttack.OnNext(Unit.Default);
                    }

                    if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
                    {
                        _subjectOnAbsorb.OnNext(Unit.Default);
                    }

                    if (Keyboard.current.sKey.wasReleasedThisFrame ||
                        Keyboard.current.downArrowKey.wasReleasedThisFrame)
                    {
                        _subjectOnAbsorbed.OnNext(Unit.Default);
                    }

                }).AddTo(gameObject);

        }
    }
}
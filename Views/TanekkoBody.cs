using System.Collections;
using DG.Tweening;
using Enums;
using ScriptableObjects;
using Structs;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TanekkoBody : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private ScriptableObjectTanekkoBodySprites tanekkoBodySprites;
        [SerializeField] private Color colorDamaged = Color.red;

        private TanekkoGrowthStatus _currentGrowthStatus = TanekkoGrowthStatus.Seed;
        private TanekkoElementStatus _currentElementStatus = TanekkoElementStatus.Normal;

        private Coroutine _coroutineMotion;
        private Coroutine _coroutineColor;
        private Tweener _tweenerColor;

        private TanekkoBodySpritesGroup _currentSpritesGroup;

        private void Reset()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetGrowthStatus(TanekkoGrowthStatus growthStatus)
        {
            _currentGrowthStatus = growthStatus;
            ChangeSpritesGroup();
        }

        public void SetElementStatus(TanekkoElementStatus elementStatus)
        {
            _currentElementStatus = elementStatus;
            ChangeSpritesGroup();
        }

        public void ChangeMotion(TanekkoMotionStatus motionStatus)
        {
            if (_coroutineMotion != null)
            {
                StopCoroutine(_coroutineMotion);
            }

            switch (motionStatus)
            {
                case TanekkoMotionStatus.None:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionNone());
                    break;
                }
                case TanekkoMotionStatus.Idle:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionIdle());
                    break;
                }
                case TanekkoMotionStatus.Running:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionRunning());
                    break;
                }
                case TanekkoMotionStatus.Jumping:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionJumping());
                    break;
                }
                case TanekkoMotionStatus.Attacking:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionAttacking());
                    break;
                }
                case TanekkoMotionStatus.Absorbing:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionAbsorbing());
                    break;
                }
                case TanekkoMotionStatus.Damaged:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionDamaged());
                    break;
                }
                case TanekkoMotionStatus.Defeated:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionDamaged());
                    break;
                }
                case TanekkoMotionStatus.TimeUp:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionIdle());
                    break;
                }
                case TanekkoMotionStatus.Goal:
                {
                    _coroutineMotion = StartCoroutine(CoroutineMotionRunning());
                    break;
                }
            }
        }

        public void LookDirection(TanekkoLookDirection lookDirection)
        {
            switch (lookDirection)
            {
                case TanekkoLookDirection.Right:
                {

                    spriteRenderer.flipX = false;
                    break;
                }
                case TanekkoLookDirection.Left:
                {
                    spriteRenderer.flipX = true;
                    break;
                }
            }
        }

        public void ResetSpriteColor()
        {
            _tweenerColor?.Kill();
            if (_coroutineColor!=null)
            {
                StopCoroutine(_coroutineColor);
            }
            spriteRenderer.color = Color.white;
        }
        public void DrawEffectToGetDamaged()
        {
            _tweenerColor?.Kill();
            if (_coroutineColor!=null)
            {
                StopCoroutine(_coroutineColor);
            }

            _coroutineColor = StartCoroutine(CoroutineEffectGetDamaged());
        }

        private IEnumerator CoroutineEffectGetDamaged()
        {
            for (var i = 0; i < 60; i++)
            {
                spriteRenderer.color = Color.clear;
                yield return new WaitForSeconds(0.025f);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.025f);
            }

            spriteRenderer.color = Color.white;
        }
        public void DrawEffectToGetDefeated()
        {
            _tweenerColor?.Kill();
            if (_coroutineColor!=null)
            {
                StopCoroutine(_coroutineColor);
            }
            _tweenerColor = spriteRenderer.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.2f).From(Color.white)
                .SetLink(gameObject);
        }

        private void ChangeSpritesGroup()
        {
            var tanekkoBodySpritesByGrowth = _currentGrowthStatus switch
            {
                TanekkoGrowthStatus.Seed => tanekkoBodySprites.TanekkoBodySpritesSeed,
                TanekkoGrowthStatus.Bud => tanekkoBodySprites.TanekkoBodySpritesBud,
                TanekkoGrowthStatus.FlowerA => tanekkoBodySprites.TanekkoBodySpritesFlowerA,
                TanekkoGrowthStatus.FlowerB => tanekkoBodySprites.TanekkoBodySpritesFlowerB,
                _ => null
            };

            if (tanekkoBodySpritesByGrowth == null)
            {
                return;
            }

            var tanekkoBodySpritesByElement = _currentElementStatus switch
            {
                TanekkoElementStatus.Normal => tanekkoBodySpritesByGrowth.TanekkoBodySpritesNormal,
                TanekkoElementStatus.Fire => tanekkoBodySpritesByGrowth.TanekkoBodySpritesFire,
                TanekkoElementStatus.Ice => tanekkoBodySpritesByGrowth.TanekkoBodySpritesIce,
                TanekkoElementStatus.Poison => tanekkoBodySpritesByGrowth.TanekkoBodySpritesPoison,
                _ => null
            };

            if (tanekkoBodySpritesByElement == null)
            {
                return;
            }

            _currentSpritesGroup = tanekkoBodySpritesByElement.SpritesGroup;
        }

        private IEnumerator CoroutineMotionNone()
        {
            spriteRenderer.sprite = null;
            yield break;
        }
        
        private IEnumerator CoroutineMotionIdle()
        {
            for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
            {
                spriteRenderer.sprite = _currentSpritesGroup.spriteIdle;
                yield return null;
            }
        }

        private IEnumerator CoroutineMotionRunning()
        {
            for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
            {
                if (_currentSpritesGroup.spritesRunning == null)
                {
                    yield return null;
                    continue;
                }
                foreach (var spriteRunning in _currentSpritesGroup.spritesRunning)
                {
                    spriteRenderer.sprite = spriteRunning;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private IEnumerator CoroutineMotionJumping()
        {
            if (_currentSpritesGroup.spritesJumping == null)
            {
                yield break;
            }
        
            foreach (var spriteJumping in _currentSpritesGroup.spritesJumping)
            {
                spriteRenderer.sprite = spriteJumping;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator CoroutineMotionAttacking()
        {
            spriteRenderer.sprite = _currentSpritesGroup.spriteAttackingA;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = _currentSpritesGroup.spriteAttackingB;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = _currentSpritesGroup.spriteAttackingC;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = _currentSpritesGroup.spriteAttackingD;
            yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator CoroutineMotionAbsorbing()
        {
            spriteRenderer.sprite = _currentSpritesGroup.spriteAbsorbingA;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = _currentSpritesGroup.spriteAbsorbingB;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = _currentSpritesGroup.spriteAbsorbingC;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = _currentSpritesGroup.spriteAbsorbingD;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = _currentSpritesGroup.spriteAbsorbingE;
        }

        private IEnumerator CoroutineMotionDamaged()
        {
            spriteRenderer.sprite = _currentSpritesGroup.spriteDamaged;
            yield break;
        }
    }
}

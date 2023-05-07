using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image imageTanekko;
        [SerializeField] private Image imageRank;
        [SerializeField] private Image imageScoreNumberA;
        [SerializeField] private Image imageScoreNumberB;
        [SerializeField] private Image imageScoreNumberC;
        [SerializeField] private Image imageScoreNumberD;
        [SerializeField] private Image imageTimeNumberSmallA;
        [SerializeField] private Image imageTimeNumberSmallB;
        [SerializeField] private Image imageTimeNumberBigA;
        [SerializeField] private Image imageTimeNumberBigB;

        [SerializeField] private Sprite spriteTanekkoSeedNormal;
        [SerializeField] private Sprite spriteTanekkoSeedFire;
        [SerializeField] private Sprite spriteTanekkoSeedIce;
        [SerializeField] private Sprite spriteTanekkoSeedPoison;
        [SerializeField] private Sprite spriteTanekkoBudFire;
        [SerializeField] private Sprite spriteTanekkoBudIce;
        [SerializeField] private Sprite spriteTanekkoBudPoison;
        [SerializeField] private Sprite spriteTanekkoFlowerAFire;
        [SerializeField] private Sprite spriteTanekkoFlowerAIce;
        [SerializeField] private Sprite spriteTanekkoFlowerAPoison;
        [SerializeField] private Sprite spriteTanekkoFlowerBFire;
        [SerializeField] private Sprite spriteTanekkoFlowerBIce;
        [SerializeField] private Sprite spriteTanekkoFlowerBPoison;

        [SerializeField] private Sprite spriteRankB;
        [SerializeField] private Sprite spriteRankA;
        [SerializeField] private Sprite spriteRankS;
        // ReSharper disable once InconsistentNaming
        [SerializeField] private Sprite spriteRankSS;
        // ReSharper disable once InconsistentNaming
        [SerializeField] private Sprite spriteRankSSS;
        [SerializeField] private Sprite spriteRankDummy;
        
        [SerializeField] private Sprite[] spritesNumbers = new Sprite[10];
        [SerializeField] private Sprite spriteNumberDummy;
        
        private TanekkoGrowthStatus _growthStatus;
        private TanekkoElementStatus _elementStatus;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            canvasGroup.alpha = 1.0f;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0.0f;
        }

        public void SetRankSprite(GameResultRank rank)
        {
            imageRank.sprite = rank switch
            {
                GameResultRank.B => spriteRankB,
                GameResultRank.A => spriteRankA,
                GameResultRank.S => spriteRankS,
                GameResultRank.SS => spriteRankSS,
                GameResultRank.SSS => spriteRankSSS,
                _ => spriteRankDummy
            };
        }

        public void SetScoreSprites(int score)
        {
            var numberA = score % 10;
            var numberB = score / 10 % 10;
            var numberC = score / 100 % 10;
            var numberD = score / 1000 % 10;

            if (score >= 1000)
            {
                imageScoreNumberA.sprite = spritesNumbers[numberA];
                imageScoreNumberB.sprite = spritesNumbers[numberB];
                imageScoreNumberC.sprite = spritesNumbers[numberC];
                imageScoreNumberD.sprite = spritesNumbers[numberD];
            }
            else if (score >= 100)
            {
                imageScoreNumberA.sprite = spritesNumbers[numberA];
                imageScoreNumberB.sprite = spritesNumbers[numberB];
                imageScoreNumberC.sprite = spritesNumbers[numberC];
                imageScoreNumberD.sprite = spriteNumberDummy;
            }
            else if (score >= 10)
            {
                imageScoreNumberA.sprite = spritesNumbers[numberA];
                imageScoreNumberB.sprite = spritesNumbers[numberB];
                imageScoreNumberC.sprite = spriteNumberDummy;
                imageScoreNumberD.sprite = spriteNumberDummy;
            }
            else
            {
                imageScoreNumberA.sprite = spritesNumbers[numberA];
                imageScoreNumberB.sprite = spriteNumberDummy;
                imageScoreNumberC.sprite = spriteNumberDummy;
                imageScoreNumberD.sprite = spriteNumberDummy;
            }
        }

        public void SetTimeSprites(int tickCountRestTime)
        {
            var numberSmallA = tickCountRestTime % 10;
            var numberSmallB = tickCountRestTime / 10 % 10;
            var numberBigA = tickCountRestTime / 100 % 10;
            var numberBigB = tickCountRestTime / 1000 % 10;

            if (tickCountRestTime >= 1000)
            {
                imageTimeNumberSmallA.sprite = spritesNumbers[numberSmallA];
                imageTimeNumberSmallB.sprite = spritesNumbers[numberSmallB];
                imageTimeNumberBigA.sprite = spritesNumbers[numberBigA];
                imageTimeNumberBigB.sprite = spritesNumbers[numberBigB];
            }
            else
            {
                imageTimeNumberSmallA.sprite = spritesNumbers[numberSmallA];
                imageTimeNumberSmallB.sprite = spritesNumbers[numberSmallB];
                imageTimeNumberBigA.sprite = spritesNumbers[numberBigA];
                imageTimeNumberBigB.sprite = spriteNumberDummy;
            }
        }

        public void SetGrowthStatus(TanekkoGrowthStatus growthStatus)
        {
            _growthStatus = growthStatus;
            ChangeImageTanekko();
        }

        public void SetElementStatus(TanekkoElementStatus elementStatus)
        {
            _elementStatus = elementStatus;
            ChangeImageTanekko();
        }

        private void ChangeImageTanekko()
        {
            switch (_growthStatus)
            {
                case TanekkoGrowthStatus.Seed:
                {
                    switch (_elementStatus)
                    {
                        case TanekkoElementStatus.Normal:
                        {
                            imageTanekko.sprite = spriteTanekkoSeedNormal;
                            break;
                        }
                        case TanekkoElementStatus.Fire:
                        {
                            imageTanekko.sprite = spriteTanekkoSeedFire;
                            break;
                        }
                        case TanekkoElementStatus.Ice:
                        {
                            imageTanekko.sprite = spriteTanekkoSeedIce;
                            break;
                        }
                        case TanekkoElementStatus.Poison:
                        {
                            imageTanekko.sprite = spriteTanekkoSeedPoison;
                            break;
                        }
                    }

                    break;
                }
                case TanekkoGrowthStatus.Bud:
                {
                    switch (_elementStatus)
                    {
                        case TanekkoElementStatus.Fire:
                        {
                            imageTanekko.sprite = spriteTanekkoBudFire;
                            break;
                        }
                        case TanekkoElementStatus.Ice:
                        {
                            imageTanekko.sprite = spriteTanekkoBudIce;
                            break;
                        }
                        case TanekkoElementStatus.Poison:
                        {
                            imageTanekko.sprite = spriteTanekkoBudPoison;
                            break;
                        }
                    }

                    break;
                }
                case TanekkoGrowthStatus.FlowerA:
                {
                    switch (_elementStatus)
                    {
                        case TanekkoElementStatus.Fire:
                        {
                            imageTanekko.sprite = spriteTanekkoFlowerAFire;
                            break;
                        }
                        case TanekkoElementStatus.Ice:
                        {
                            imageTanekko.sprite = spriteTanekkoFlowerAIce;
                            break;
                        }
                        case TanekkoElementStatus.Poison:
                        {
                            imageTanekko.sprite = spriteTanekkoFlowerAPoison;
                            break;
                        }
                    }

                    break;
                }
                case TanekkoGrowthStatus.FlowerB:
                {
                    switch (_elementStatus)
                    {
                        case TanekkoElementStatus.Fire:
                        {
                            imageTanekko.sprite = spriteTanekkoFlowerBFire;
                            break;
                        }
                        case TanekkoElementStatus.Ice:
                        {
                            imageTanekko.sprite = spriteTanekkoFlowerBIce;
                            break;
                        }
                        case TanekkoElementStatus.Poison:
                        {
                            imageTanekko.sprite = spriteTanekkoFlowerBPoison;
                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
}

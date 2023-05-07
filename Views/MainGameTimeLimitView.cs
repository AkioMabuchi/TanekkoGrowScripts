using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MainGameTimeLimitView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image imageNumberBigA;
        [SerializeField] private Image imageNumberBigB;
        [SerializeField] private Image imageNumberBigC;
        [SerializeField] private Image imageSecond;
        [SerializeField] private Image imageNumberSmallA;
        [SerializeField] private Image imageNumberSmallB;
        [SerializeField] private Sprite[] spritesNumbers = new Sprite[10];
        [SerializeField] private Sprite spriteNumberDummy;

        [SerializeField] private Color colorNormal = Color.white;
        [SerializeField] private Color colorWarning = Color.yellow;
        [SerializeField] private Color colorDanger = Color.red;
        [SerializeField] private Color colorTimeUp = Color.white;
        
        [SerializeField] private int timeCountWarning = 3000;
        [SerializeField] private int timeCountDanger = 1000;
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

        public void DrawTimeCount(int timeCount)
        {
            var numberBigA = timeCount / 10000 % 10;
            var numberBigB = timeCount / 1000 % 10;
            var numberBigC = timeCount / 100 % 10;
            var numberSmallA = timeCount / 10 % 10;
            var numberSmallB = timeCount % 10;

            if (timeCount >= 10000)
            {
                imageNumberBigA.sprite = spritesNumbers[numberBigA];
                imageNumberBigB.sprite = spritesNumbers[numberBigB];
                imageNumberBigC.sprite = spritesNumbers[numberBigC];
                imageNumberSmallA.sprite = spritesNumbers[numberSmallA];
                imageNumberSmallB.sprite = spritesNumbers[numberSmallB];
            }
            else if (timeCount >= 1000)
            {
                imageNumberBigA.sprite = spriteNumberDummy;
                imageNumberBigB.sprite = spritesNumbers[numberBigB];
                imageNumberBigC.sprite = spritesNumbers[numberBigC];
                imageNumberSmallA.sprite = spritesNumbers[numberSmallA];
                imageNumberSmallB.sprite = spritesNumbers[numberSmallB];
            }
            else
            {
                imageNumberBigA.sprite = spriteNumberDummy;
                imageNumberBigB.sprite = spriteNumberDummy;
                imageNumberBigC.sprite = spritesNumbers[numberBigC];
                imageNumberSmallA.sprite = spritesNumbers[numberSmallA];
                imageNumberSmallB.sprite = spritesNumbers[numberSmallB];
            }

            var spriteColor = colorNormal;

            if (timeCount == 0)
            {
                spriteColor = colorTimeUp;
            }
            else if (timeCount < timeCountDanger)
            {
                spriteColor = colorDanger;
            }
            else if (timeCount < timeCountWarning)
            {
                spriteColor = colorWarning;
            }

            imageNumberBigA.color = spriteColor;
            imageNumberBigB.color = spriteColor;
            imageNumberBigC.color = spriteColor;
            imageSecond.color = spriteColor;
            imageNumberSmallA.color = spriteColor;
            imageNumberSmallB.color = spriteColor;
        }
    }
}

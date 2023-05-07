using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TanekkoLifeCountView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image[] imagesLife;
        [SerializeField] private Color colorActive = Color.white;
        [SerializeField] private Color colorDamaged = Color.white;

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

        public void SetLifeGauge(int life)
        {
            for (var i = 0; i < imagesLife.Length; i++)
            {
                imagesLife[i].color = life > i ? colorActive : colorDamaged;
            }
        }
    }
}

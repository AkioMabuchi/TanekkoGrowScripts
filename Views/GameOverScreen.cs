using System;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

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
    }
}

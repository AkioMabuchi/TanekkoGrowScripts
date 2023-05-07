using System;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BlackScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0.0f;
        }
    }
}

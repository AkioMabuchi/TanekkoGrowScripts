using DG.Tweening;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Camera cameraMain;

        [SerializeField] private float durationSequence;
        [SerializeField] private float minPositionX;
        [SerializeField] private float maxPositionX;
        private void Reset()
        {
            cameraMain = GetComponent<Camera>();
        }

        public void SequencePositionX()
        {
            cameraMain.transform.DOMoveX(minPositionX, durationSequence).From(maxPositionX).SetEase(Ease.InOutQuad)
                .SetLink(gameObject);
        }

        public void MovePositionX(float positionX)
        {
            var cameraPosition = cameraMain.transform.position;
            cameraPosition.x = Mathf.Clamp(positionX, minPositionX, maxPositionX);
            cameraMain.transform.position = cameraPosition;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace MultipleResolutions
{
    public class MultipleResolutionsKeeper : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private AspectKeeper _aspectKeeper;
        [SerializeField] private Vector2 _aspectVec = new Vector2(750, 1334);

        private DeviceOrientation _prevOrientation;

        private void Update()
        {
            var currentOrientation = GetOrientation();
            if (_prevOrientation != currentOrientation)
            {
                _prevOrientation = currentOrientation;
                ChangeAspect(_prevOrientation);
            }
        }

        /// <summary>
        /// Determine length and width
        /// </summary>
        private DeviceOrientation GetOrientation()
        {
            //Always Unknown on Editor
            var result = Input.deviceOrientation;

            //Unkown is determined by pixel count
            if (result == DeviceOrientation.Unknown)
            {
                if (Screen.width < Screen.height)
                {
                    result = DeviceOrientation.Portrait;
                }
                else
                {
                    result = DeviceOrientation.LandscapeLeft;
                }
            }
            return result;
        }

        /// <summary>
        /// aspect ratio exchange
        /// </summary>
        private void ChangeAspect(DeviceOrientation orientation)
        {
            if (orientation == DeviceOrientation.Portrait
                || orientation == DeviceOrientation.PortraitUpsideDown)
            {
                _canvasScaler.referenceResolution = _aspectVec;
                _aspectKeeper.SetAspectVec(_aspectVec);
            }
            else if (orientation == DeviceOrientation.LandscapeLeft
                || orientation == DeviceOrientation.LandscapeRight)
            {
                var replaceVec = new Vector2(_aspectVec.y, _aspectVec.x);
                _canvasScaler.referenceResolution = replaceVec;
                _aspectKeeper.SetAspectVec(replaceVec);
            }
        }
    }
}

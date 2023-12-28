using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MultipleResolutions
{
    public class MultipleResolutionsView : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private AspectKeeper _aspectKeeper;
        [SerializeField] private Vector2 _aspectVec = new Vector2(750, 1334);

        private ReactiveProperty<DeviceOrientation> _reactOrient = new ReactiveProperty<DeviceOrientation>();
        public ReactiveProperty<DeviceOrientation> ReactOrient => _reactOrient;

        private DeviceOrientation _prevOrientation;

        private void Update()
        {
            var currentOrientation = GetOrientation();
            if (_prevOrientation != currentOrientation)
            {
                _prevOrientation = currentOrientation;
                _reactOrient.Value = _prevOrientation;
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
        public void ChangeAspect(DeviceOrientation orientation)
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
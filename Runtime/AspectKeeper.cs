using UnityEngine;

namespace MultipleResolutions
{
    [ExecuteAlways]
    public class AspectKeeper : MonoBehaviour
    {
        [SerializeField] private Camera _targetCamera;
        [SerializeField] private Vector2 _aspectVec = new Vector2(750, 1334);

        private float _screenAspect;
        private float _targetAspect;
        private float _magRate;

        private Rect _viewportRect = new Rect(0, 0, 1, 1);

        private void Update()
        {
            _screenAspect = Screen.width / (float)Screen.height;
            _targetAspect = _aspectVec.x / _aspectVec.y;
            _magRate = _targetAspect / _screenAspect;

            if (1 <= _magRate)
            {
                //Landscape
                _viewportRect.width = _magRate;
            }
            else
            {
                //Vertical
                _viewportRect.height = 1 / _magRate;
            }

            //Centering
            _viewportRect.x = 0.5f - _viewportRect.width * 0.5f;
            _viewportRect.y = 0.5f - _viewportRect.height * 0.5f;
            _targetCamera.rect = _viewportRect;
        }

        public void SetAspectVec(Vector2 vec)
        {
            _aspectVec = vec;
        }
    }
}


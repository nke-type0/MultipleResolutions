using UnityEngine;

namespace MultipleResolutions
{
    [ExecuteAlways]
    public class AspectKeeper : MonoBehaviour
    {
        [SerializeField] private Camera _targetCamera;
        [SerializeField] private Vector2 _aspectVec;

        //画面のアスペクト比
        private float _screenAspect;
        //目的のアスペクト比
        private float _targetAspect;
        //目的アスペクト比にするための倍率
        private float _magRate;

        private Rect _viewportRect = new Rect(0, 0, 1, 1);

        private void Update()
        {
            _screenAspect = Screen.width / (float)Screen.height;

            _targetAspect = _aspectVec.x / _aspectVec.y;

            _magRate = _targetAspect / _screenAspect;

            if (1 <= _magRate)
            {
                //横幅
                _viewportRect.width = _magRate;
            }
            else
            {
                //縦幅
                _viewportRect.height = 1 / _magRate;
            }

            //中央寄せ
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

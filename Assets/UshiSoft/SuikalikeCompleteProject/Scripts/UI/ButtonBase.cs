using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UshiSoft.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonBase : MonoBehaviour
    {
        protected Button _button;

        private Tween _tween;

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (Common.IsAnimationPlaying(_tween))
            {
                _tween.Complete();
            }

            _tween = transform
                .DOPunchScale(new Vector3(-0.25f, -0.25f, 0f), 0.3f)
                .SetLink(gameObject);

            SoundManager.Instance.PlaySE("ClickButton");
        }
    }
}
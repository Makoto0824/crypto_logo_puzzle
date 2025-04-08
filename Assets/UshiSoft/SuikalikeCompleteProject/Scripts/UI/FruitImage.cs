using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UshiSoft.UI
{
    public class FruitImage : MonoBehaviour
    {
        private Image _image;
        private RectTransform _rectTransform;

        private Tween _tween;

        public RectTransform RectTransform
        {
            get => _rectTransform;
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public void Init(Sprite sprite, float maxSize)
        {
            _image.sprite = sprite;

            var aspect = (float)sprite.texture.width / (float)sprite.texture.height;
            if (sprite.texture.width >= sprite.texture.height)
            {
                _rectTransform.sizeDelta = new Vector2(maxSize, maxSize / aspect);
            }
            else
            {
                _rectTransform.sizeDelta = new Vector2(maxSize * aspect, maxSize);
            }
        }

        public void Move(Vector2 position, float duration, Ease ease)
        {
            if (Common.IsAnimationPlaying(_tween))
            {
                _tween.Complete();
            }

            _tween = _rectTransform.DOAnchorPos(position, duration)
                .SetEase(ease)
                .SetLink(gameObject);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace UshiSoft.UI
{
    public class BGMButton : ButtonBase
    {
        [SerializeField] private Sprite _unmuteSprite;
        [SerializeField] private Sprite _muteSprite;

        private Image _image;

        protected override void Awake()
        {
            base.Awake();

            _image = Common.GetComponentInChildren<Image>(gameObject);

            UpdateSprite();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Settings.BGMMute = !Settings.BGMMute;

            UpdateSprite();

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.BGMMute = Settings.BGMMute;
            }
        }

        private void UpdateSprite()
        {
            _image.sprite = Settings.BGMMute ? _muteSprite : _unmuteSprite;
        }
    }
}
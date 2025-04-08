using UnityEngine;
using UnityEngine.UI;

namespace UshiSoft.UI
{
    public class SEButton : ButtonBase
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
            Settings.SEMute = !Settings.SEMute;

            UpdateSprite();

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SEMute = Settings.SEMute;
            }
        }

        private void UpdateSprite()
        {
            _image.sprite = Settings.SEMute ? _muteSprite : _unmuteSprite;
        }
    }
}
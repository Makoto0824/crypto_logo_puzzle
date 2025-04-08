using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UshiSoft
{
    public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
    {
        [SerializeField] private Color _fadeColor = Color.black;
        [SerializeField] private float _fadeDuration = 0.5f;

        private float _alpha;
        private Tween _tween;

        private void OnGUI()
        {
            if (!Common.IsAnimationPlaying(_tween))
            {
                return;
            }

            var color = _fadeColor;
            color.a = _alpha;
            GUI.color = color;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }

        public void ChangeScene(string sceneName)
        {
            if (Common.IsAnimationPlaying(_tween))
            {
                return;
            }

            var seq = DOTween.Sequence();
            seq.Append(DOVirtual.Float(0f, 1f, _fadeDuration / 2f, x => _alpha = x));
            seq.AppendCallback(() => SceneManager.LoadScene(sceneName));
            seq.Append(DOVirtual.Float(1f, 0f, _fadeDuration / 2f, x => _alpha = x));
            seq.SetUpdate(true);
            _tween = seq;
        }
    }
}
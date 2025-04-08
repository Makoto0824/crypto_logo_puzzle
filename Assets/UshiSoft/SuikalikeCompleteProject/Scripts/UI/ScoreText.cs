using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UshiSoft.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private float _animationSpeed = 1f;

        private TMP_Text _text;

        private int _score;

        private Tween _tween;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            SetScore(-1, false, false);
        }

        public void SetScore(int score, bool punchAnimation, bool countUpAnimation)
        {
            if (Common.IsAnimationPlaying(_tween))
            {
                _tween.Complete();
            }

            var seq = DOTween.Sequence();

            if (punchAnimation)
            {
                seq.Insert(0f, transform.DOPunchScale(Vector3.one * 0.5f, _animationSpeed / 2f, 1));
            }

            if (countUpAnimation)
            {
                var currScore = _score;
                _score = score;
                seq.Insert(0f, DOVirtual.Int(
                    currScore,
                    _score,
                    _animationSpeed,
                    x => _text.text = GetFormattedScore(x))
                    .SetEase(Ease.Linear));
            }
            else
            {
                _score = score;
                _text.text = GetFormattedScore(_score);
            }

            _tween = seq;
        }

        private string GetFormattedScore(int score)
        {
            if (score >= 0)
            {
                return $"{score}";
            }
            else
            {
                return "-";
            }
        }
    }
}
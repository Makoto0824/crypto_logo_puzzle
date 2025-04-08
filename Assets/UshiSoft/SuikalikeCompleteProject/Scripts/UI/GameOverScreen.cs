using DG.Tweening;
using UnityEngine;

namespace UshiSoft.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private float _animationSpeed = 1f;

        [SerializeField] private ScoreText _scoreText;
        [SerializeField] private ScoreText _bestScoreText;

        private void Start()
        {
            var canvasGroup = gameObject.GetComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, _animationSpeed).SetEase(Ease.Linear);
        }

        public void Init(int score, int bestScore)
        {
            _scoreText.SetScore(score, false, true);
            _bestScoreText.SetScore(bestScore, false, true);
        }
    }
}
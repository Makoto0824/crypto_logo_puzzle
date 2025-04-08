using DG.Tweening;
using UnityEngine;

namespace UshiSoft
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameOverColliderTop _gameOverColliderTop;
        [SerializeField] private GameOverColliderBottom _gameOverColliderBottom;

        [SerializeField] private UI.ScoreText _scoreText;
        [SerializeField] private UI.ScoreText _bestScoreText;

        [SerializeField] private UI.GameOverScreen _gameOverScreenPrefab;
        [SerializeField] private RectTransform _canvas;

        [SerializeField] private FruitDropper _dropper;

        private int _score;
        private bool _gameOver;

        private void Start()
        {
            _gameOverColliderTop.OnGameOver.AddListener(OnGameOver);
            _gameOverColliderBottom.OnGameOver.AddListener(OnGameOver);

            _scoreText.SetScore(0, false, false);

            _bestScoreText.SetScore(ScoreManager.LoadBestScore(), false, true);

            if (SoundManager.Instance != null)
            {
                if (!SoundManager.Instance.BGMPlaying)
                {
                    SoundManager.Instance.PlayBGM("BGM");
                }
            }
        }

        public void OnGameOver()
        {
            if (_gameOver)
            {
                return;
            }

            _gameOver = true;

            ScoreManager.SaveScore(_score);

            _dropper.GameOver();

            _dropper.enabled = false;

            GameOverEffect();

            DOVirtual.DelayedCall(
                1f,
                () =>
                {
                    var gameOverScreen = Instantiate(_gameOverScreenPrefab, _canvas);
                    gameOverScreen.Init(_score, ScoreManager.LoadBestScore());
                });
        }

        public void AddScore(int score)
        {
            if (_gameOver)
            {
                return;
            }

            _score += score;
            _scoreText.SetScore(_score, true, true);
        }

        private void GameOverEffect()
        {
            var fruits = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
            foreach (var fruit in fruits)
            {
                fruit.GameOverEffect();
            }
        }
    }
}
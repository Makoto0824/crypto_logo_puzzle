using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UshiSoft.UI
{
    public class GameOverGraceTimeEffect : MonoBehaviour
    {
        [SerializeField] private GameOverColliderTop _gameOverColliderTop;

        [SerializeField] private float _bowlWidth = 4f;

        [SerializeField] private LineRenderer _gameOverLineRenderer;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Image _blinkImage;

        private void Awake()
        {
            _gameOverColliderTop.OnTimerUpdate.AddListener(OnRemainTimeChange);

            _gameOverLineRenderer.positionCount = 2;

            _blinkImage.raycastTarget = false;

            Hide();
        }

        private void OnRemainTimeChange(float timer, float gameOverGraceTime, float minFruitY, bool display)
        {
            if (display)
            {
                Show();

                _gameOverLineRenderer.SetPositions(new Vector3[]
                {
                    new Vector3(-_bowlWidth / 2f, minFruitY),
                    new Vector3(_bowlWidth / 2f, minFruitY),
                });

                var timeTextWorldPos = new Vector3(_bowlWidth / 2f, minFruitY);
                var timeTextScreenPos = Camera.main.WorldToScreenPoint(timeTextWorldPos);
                _timeText.transform.position = timeTextScreenPos;

                var remainTime = Mathf.Max(gameOverGraceTime - timer, 0f);
                _timeText.text = $"{remainTime:0.00}";

                var imageColor = _blinkImage.color;
                imageColor.a = ((Mathf.Sin(2f * Mathf.PI * (timer / gameOverGraceTime) * 5f) + 1f) / 2f) * 0.25f;
                _blinkImage.color = imageColor;
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            _gameOverLineRenderer.enabled = true;

            _timeText.enabled = true;

            _blinkImage.enabled = true;
        }

        private void Hide()
        {
            _gameOverLineRenderer.enabled = false;

            _timeText.enabled = false;

            _blinkImage.enabled = false;
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace UshiSoft
{
    public class BowlShaker : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField, Min(0f)] private float _shakeDuration = 2f;
        [SerializeField, Min(0f)] private float _shakeSpeed = 5f;
        [SerializeField, Min(0f)] private float _shakeWidth = 0.5f;

        private Rigidbody2D _rigidbody;

        private int _numRemainShakes;
        private UnityEvent<int, int> _onNumRemainShakesChange = new UnityEvent<int, int>();

        private float _shakeStartTime;
        private bool _shaking;

        private Vector3 _initialPosition;

        public int NumRemainShakes
        {
            get => _numRemainShakes;
            set
            {
                _numRemainShakes = value;

                _onNumRemainShakesChange?.Invoke(_numRemainShakes, _gameData.NumShakes);
            }
        }

        public UnityEvent<int, int> OnNumRemainShakesChange
        {
            get => _onNumRemainShakesChange;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _initialPosition = transform.position;

            NumRemainShakes = _gameData.NumShakes;
        }

        private void FixedUpdate()
        {
            if (!_shaking)
            {
                return;
            }

            var t = (Time.time - _shakeStartTime) / _shakeDuration;
            if (t > 1f)
            {
                t = 1f;
                _shaking = false;
            }

            var dx = Mathf.Sin(2f * Mathf.PI * t * _shakeSpeed) * _shakeWidth;

            var newPos = _initialPosition;
            newPos.x += dx;
            _rigidbody.MovePosition(newPos);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(
                transform.TransformPoint(Vector3.left * _shakeWidth),
                transform.TransformPoint(Vector3.right * _shakeWidth));
        }

        public void Shake()
        {
            if (_shaking)
            {
                return;
            }

            if (_numRemainShakes == 0)
            {
                return;
            }

            _shakeStartTime = Time.time;
            _shaking = true;

            NumRemainShakes--;
        }
    }
}
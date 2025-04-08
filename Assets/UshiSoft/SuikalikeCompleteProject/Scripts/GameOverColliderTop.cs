using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UshiSoft
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GameOverColliderTop : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        private BoxCollider2D _boxCollider;

        private List<Fruit> _fruits = new List<Fruit>();

        private float _timer;

        private bool _meetGameOverCondition;
        private float _minFruitY;

        private UnityEvent<float, float, float, bool> _onTimerUpdate = new UnityEvent<float, float, float, bool>();
        private UnityEvent _onGameOver = new UnityEvent();

        public UnityEvent<float, float, float, bool> OnTimerUpdate
        {
            get => _onTimerUpdate;
        }

        public UnityEvent OnGameOver
        {
            get => _onGameOver;
        }

        private void Awake()
        {
            _boxCollider = gameObject.AddComponent<BoxCollider2D>();

            _boxCollider.isTrigger = true;
        }

        private void FixedUpdate()
        {
            UpdateTimer();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var fruit = collider.GetComponentInParent<Fruit>();
            if (fruit == null)
            {
                return;
            }

            _fruits.Add(fruit);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            var fruit = collider.GetComponentInParent<Fruit>();
            if (fruit == null)
            {
                return;
            }

            _fruits.Remove(fruit);
        }

        private void CheckGameOver()
        {
            _meetGameOverCondition = false;
            _minFruitY = float.MaxValue;

            foreach (var fruit in _fruits)
            {
                if (fruit.IgnoreGameOverCollider)
                {
                    continue;
                }

                if (fruit.Collider.bounds.min.y > _boxCollider.bounds.min.y)
                {
                    _meetGameOverCondition = true;
                    _minFruitY = Mathf.Min(fruit.Collider.bounds.min.y, _minFruitY);
                }
            }
        }

        private void UpdateTimer()
        {
            CheckGameOver();

            if (_meetGameOverCondition)
            {
                _timer += Time.fixedDeltaTime;
                _onTimerUpdate?.Invoke(_timer, _gameData.GameOverGraceTime, _minFruitY, true);

                if (_timer > _gameData.GameOverGraceTime)
                {
                    _onGameOver?.Invoke();
                }
            }
            else
            {
                _timer = 0f;
                _onTimerUpdate?.Invoke(_timer, _gameData.GameOverGraceTime, _minFruitY, false);
            }
        }
    }
}
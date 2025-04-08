using UnityEngine;
using UnityEngine.Events;

namespace UshiSoft
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GameOverColliderBottom : MonoBehaviour
    {
        private UnityEvent _onGameOver = new UnityEvent();

        public UnityEvent OnGameOver
        {
            get => _onGameOver;
        }

        private void Awake()
        {
            var bc = gameObject.AddComponent<BoxCollider2D>();
            bc.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var fruit = collider.GetComponentInParent<Fruit>();
            if (fruit == null)
            {
                return;
            }

            _onGameOver?.Invoke();
        }
    }
}
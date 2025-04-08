using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UshiSoft
{
    public class Fruit : MonoBehaviour
    {
        [SerializeField, Min(0f)] private int _score;
        [SerializeField] private Sprite _uiSprite;
        [SerializeField] private ParticleSystem _combineParticlePrefab;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private FruitGenerator _fruitGenerator;
        private GameController _gameController;
        private CameraShaker _cameraShaker;

        private float _localBoundsLeft;
        private float _localBoundsRight;

        private int _size;
        private bool _toBeDelete;
        private bool _ignoreGameOverCollider;

        private UnityEvent<Fruit> _onHit = new UnityEvent<Fruit>();

        public int Score
        {
            get => _score;
        }

        public Sprite UISprite
        {
            get => _uiSprite;
        }

        public int Size
        {
            get => _size;
            set => _size = value;
        }

        public UnityEvent<Fruit> OnHit
        {
            get => _onHit;
        }

        public Collider2D Collider
        {
            get => _collider;
        }

        public float LocalBoundsLeft
        {
            get => _localBoundsLeft;
        }

        public float LocalBoundsRight
        {
            get => _localBoundsRight;
        }

        public bool ToBeDelete
        {
            get => _toBeDelete;
        }

        public bool IgnoreGameOverCollider
        {
            get => _ignoreGameOverCollider;
            set => _ignoreGameOverCollider = value;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponentInChildren<Collider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _fruitGenerator = FindAnyObjectByType<FruitGenerator>();
            _gameController = FindAnyObjectByType<GameController>();
            _cameraShaker = FindAnyObjectByType<CameraShaker>();

            _localBoundsLeft = transform.InverseTransformPoint(_collider.bounds.min).x;
            _localBoundsRight = transform.InverseTransformPoint(_collider.bounds.max).x;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _onHit?.Invoke(this);

            if (_toBeDelete)
            {
                return;
            }

            var otherFruit = collision.collider.GetComponentInParent<Fruit>();
            if (otherFruit == null)
            {
                return;
            }

            if (otherFruit.ToBeDelete)
            {
                return;
            }

            if (_size != otherFruit.Size)
            {
                return;
            }

            var newPos = Vector3.Lerp(transform.position, otherFruit.transform.position, 0.5f);
            var newRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            Delete();
            otherFruit.Delete();

            var newSize = _size + 1;
            var newFruit = _fruitGenerator.Generate(newSize);
            if (newFruit != null)
            {
                newFruit.transform.position = newPos;
                newFruit.transform.rotation = newRot;

                _gameController.AddScore(newFruit.Score);

                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlaySE("CombineFruits");
                }

                if (_combineParticlePrefab != null)
                {
                    var combineParticle = Instantiate(_combineParticlePrefab);
                    combineParticle.transform.position = newPos;
                }

                if (_cameraShaker != null)
                {
                    _cameraShaker.Shake(newSize);
                }
            }
        }

        public void EnablePhysics()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;

            _collider.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        public void DisablePhysics()
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;

            _rigidbody.bodyType = RigidbodyType2D.Kinematic;

            _collider.gameObject.layer = LayerMask.NameToLayer("BeforeDrop");

            _ignoreGameOverCollider = true;
        }

        public void Delete()
        {
            DisablePhysics();

            _spriteRenderer.sortingOrder = -100;

            DOVirtual
                .Float(0f, 1f, 0.5f, x => _spriteRenderer.material.SetFloat("_Threshold", x))
                .SetEase(Ease.Linear)
                .OnComplete(() => Destroy(gameObject));

            _toBeDelete = true;
        }

        public void GameOverEffect()
        {
            DisablePhysics();

            transform
                .DOShakePosition(1f, 0.05f, 100, fadeOut: false)
                .SetEase(Ease.Linear);
        }
    }
}
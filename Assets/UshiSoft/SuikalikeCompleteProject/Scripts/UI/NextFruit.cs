using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UshiSoft.UI
{
    public class NextFruit : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private NextFruitManager _nextFruitManager;

        [SerializeField] private FruitImage _imagePrefab;
        [SerializeField] private float _minImageSize = 50f;
        [SerializeField] private float _maxImageSize = 100f;
        [SerializeField] private float _spacing = 10f;
        [SerializeField] private float _animationSpeed = 0.3f;

        private RectTransform _rectTransform;

        private float _minSpriteSize;
        private float _maxSpriteSize;

        private List<FruitImage> _fruitImages = new List<FruitImage>();

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            _minSpriteSize = float.MaxValue;
            _maxSpriteSize = float.MinValue;
            for (var i = 0; i <= _gameData.MaxInitialFruitSize; i++)
            {
                var sprite = _gameData.FruitPrefabs[i].UISprite;
                var spriteSize = Mathf.Max(sprite.texture.width, sprite.texture.height);
                _minSpriteSize = Mathf.Min(spriteSize, _minSpriteSize);
                _maxSpriteSize = Mathf.Max(spriteSize, _maxSpriteSize);
            }

            _nextFruitManager.OnChange.AddListener(OnNextFruitChange);
        }

        public void OnNextFruitChange(List<int> fruitSizes, bool reset)
        {
            if (reset)
            {
                DestoryAllFruitImages();

                foreach (var fruitSize in fruitSizes)
                {
                    AddFruitImage(fruitSize);
                }
            }
            else
            {
                AddFruitImage(fruitSizes[fruitSizes.Count - 1]);
                ShiftFruitImage();
            }
        }

        private void AddFruitImage(int fruitSize)
        {
            var y = _rectTransform.sizeDelta.y / 2f;
            foreach (var fruitImage in _fruitImages)
            {
                y -= fruitImage.RectTransform.sizeDelta.y;
                y -= _spacing;
            }

            var newFruitImage = Instantiate(_imagePrefab, transform);

            var sprite = _gameData.FruitPrefabs[fruitSize].UISprite;
            var spriteSize = Mathf.Max(sprite.texture.width, sprite.texture.height);
            var normSpriteSize = Mathf.InverseLerp(_minSpriteSize, _maxSpriteSize, spriteSize);
            var imageSize = Mathf.Lerp(_minImageSize, _maxImageSize, normSpriteSize);
            newFruitImage.Init(sprite, imageSize);

            newFruitImage.RectTransform.anchoredPosition = new Vector2(0f, y - newFruitImage.RectTransform.sizeDelta.y / 2f);
            _fruitImages.Add(newFruitImage);
        }

        private void ShiftFruitImage()
        {
            Destroy(_fruitImages[0].gameObject);
            _fruitImages.RemoveAt(0);

            var y = _rectTransform.sizeDelta.y / 2f;
            foreach (var fruitImage in _fruitImages)
            {
                y -= fruitImage.RectTransform.sizeDelta.y / 2f;
                fruitImage.Move(new Vector2(0f, y), _animationSpeed, Ease.Linear);
                y -= fruitImage.RectTransform.sizeDelta.y / 2f;
                y -= _spacing;
            }
        }

        private void DestoryAllFruitImages()
        {
            foreach (var fruitImage in _fruitImages)
            {
                Destroy(fruitImage.gameObject);
            }

            _fruitImages.Clear();
        }
    }
}
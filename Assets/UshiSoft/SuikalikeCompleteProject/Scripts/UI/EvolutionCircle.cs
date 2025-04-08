using UnityEngine;

namespace UshiSoft.UI
{
    public class EvolutionCircle : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        [SerializeField] private FruitImage _imagePrefab;

        [SerializeField] private float _startAngle = 22.5f;
        [SerializeField] private float _angleRange = 315f;
        [SerializeField] private float _radius = 100f;
        [SerializeField] private float _imageSize = 50f;

        private void Awake()
        {
            for (var i = 0; i < _gameData.FruitPrefabs.Length; i++)
            {
                var fruitPrefab = _gameData.FruitPrefabs[i];

                var t = (float)i / (float)(_gameData.FruitPrefabs.Length - 1);
                var radian = (_startAngle - _angleRange * t) * Mathf.Deg2Rad;
                var pos = new Vector2(
                    Mathf.Cos(radian) * _radius,
                    Mathf.Sin(radian) * _radius);

                var image = Instantiate(_imagePrefab, transform);
                image.Init(fruitPrefab.UISprite, _imageSize);

                image.RectTransform.anchoredPosition = pos;
            }
        }
    }
}
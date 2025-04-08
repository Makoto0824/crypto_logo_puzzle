using UnityEngine;

namespace UshiSoft
{
    [CreateAssetMenu(fileName = "GameData", menuName = "UshiSoft/GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private Fruit[] _fruitPrefabs;

        [SerializeField, Min(0f)] private int _numNextFruitsToDisplay = 3;
        [SerializeField, Min(0f)] private int _maxInitialFruitSize = 5;
        [SerializeField, Min(0f)] private int _numChanges = 3;
        [SerializeField, Min(0f)] private int _numShakes = 5;
        [SerializeField, Min(0f)] private float _gameOverGraceTime = 1f;

        public Fruit[] FruitPrefabs
        {
            get => _fruitPrefabs;
        }

        public int NumNextFruitsToDisplay
        {
            get => _numNextFruitsToDisplay;
        }

        public int MaxInitialFruitSize
        {
            get => _maxInitialFruitSize;
        }

        public int NumChanges
        {
            get => _numChanges;
        }

        public int NumShakes
        {
            get => _numShakes;
        }

        public float GameOverGraceTime
        {
            get => _gameOverGraceTime;
        }
    }
}
using UnityEngine;

namespace UshiSoft
{
    public class FruitGenerator : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        public Fruit Generate(int size)
        {
            if (size < 0 || size >= _gameData.FruitPrefabs.Length)
            {
                return null;
            }

            var fruit = Instantiate(_gameData.FruitPrefabs[size]);
            fruit.Size = size;

            return fruit;
        }
    }
}
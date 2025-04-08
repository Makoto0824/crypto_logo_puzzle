using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UshiSoft
{
    public class NextFruitManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        private List<int> _nextFruits = new List<int>();

        private UnityEvent<List<int>, bool> _onChange = new UnityEvent<List<int>, bool>();

        public UnityEvent<List<int>, bool> OnChange
        {
            get => _onChange;
        }
        
        public void ResetNextFruits()
        {
            _nextFruits.Clear();

            for (var i = 0; i < _gameData.NumNextFruitsToDisplay; i++)
            {
                _nextFruits.Add(GetRandomFruit());
            }

            _onChange?.Invoke(_nextFruits, true);
        }

        public int GetNextFruit()
        {
            var fruit = _nextFruits[0];
            _nextFruits.RemoveAt(0);

            _nextFruits.Add(GetRandomFruit());

            _onChange?.Invoke(_nextFruits, false);

            return fruit;
        }
        
        private int GetRandomFruit()
        {
            return Random.Range(0, _gameData.MaxInitialFruitSize + 1);
        }
    }
}
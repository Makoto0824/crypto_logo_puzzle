using UnityEngine;
using UnityEngine.Events;

namespace UshiSoft
{
    public class FruitChanger : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private FruitDropper _fruitDropper;

        private int _numRemainChanges;

        private UnityEvent<int, int> _onNumRemainChangesChange = new UnityEvent<int, int>();

        public int NumRemainChanges
        {
            get => _numRemainChanges;
            set
            {
                _numRemainChanges = value;

                _onNumRemainChangesChange?.Invoke(_numRemainChanges, _gameData.NumChanges);
            }
        }

        public UnityEvent<int, int> OnNumRemainChangesChange
        {
            get => _onNumRemainChangesChange;
        }

        private void Start()
        {
            NumRemainChanges = _gameData.NumChanges;
        }

        public void ChangeFruits()
        {
            if (_numRemainChanges == 0)
            {
                return;
            }

            _fruitDropper.ChangeFruits();

            NumRemainChanges--;
        }
    }
}
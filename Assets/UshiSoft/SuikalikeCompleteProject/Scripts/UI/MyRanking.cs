using UnityEngine;

namespace UshiSoft.UI
{
    public class MyRanking : MonoBehaviour
    {
        [SerializeField] private MyRankingItem _itemPrefab;

        [SerializeField] private int numDisplayItems = 5;

        private void Awake()
        {
            for (var i = 0; i < numDisplayItems; i++)
            {
                var item = Instantiate(_itemPrefab, transform);

                var rank = i + 1;
                var score = ScoreManager.LoadScore(rank);
                item.Init(rank, score);
            }
        }
    }
}
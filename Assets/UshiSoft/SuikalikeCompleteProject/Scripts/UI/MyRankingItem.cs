using TMPro;
using UnityEngine;

namespace UshiSoft.UI
{
    public class MyRankingItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rankText;
        [SerializeField] private ScoreText _scoreText;

        public void Init(int rank, int score)
        {
            var ap = Common.GetOrdinalNumberAlphabeticPart(rank).ToUpper();
            _rankText.text = $"{rank}<size=30>{ap}</size>";

            _scoreText.SetScore(score, false, true);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UshiSoft
{
    public static class ScoreManager
    {
        private const int MaxNumber = 10;

        public static int LoadScore(int rank)
        {
            return PlayerPrefs.GetInt($"score-{rank}", -1);
        }

        private static List<int> LoadScores()
        {
            var scores = new List<int>();
            for (var i = 0; i < MaxNumber; i++)
            {
                var rank = i + 1;
                scores.Add(LoadScore(rank));
            }
            return scores;
        }

        public static int LoadBestScore()
        {
            return LoadScore(1);
        }

        public static void SaveScore(int score)
        {
            var scores = LoadScores();
            scores.Add(score);
            scores = scores.OrderByDescending(x => x).ToList();
            SaveScores(scores);
        }

        private static void SaveScores(List<int> scores)
        {
            for (var i = 0; i < MaxNumber; i++)
            {
                SaveScore(i + 1, scores[i]);
            }
        }

        private static void SaveScore(int rank, int score)
        {
            PlayerPrefs.SetInt($"score-{rank}", score);
            PlayerPrefs.Save();
        }
    }
}
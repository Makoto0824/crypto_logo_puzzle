using UnityEngine;

namespace UshiSoft
{
    public static class Settings
    {
        private const string SEMuteKey = "se-mute";
        private const string BGMMuteKey = "bgm-mute";

        public static bool SEMute
        {
            get => PlayerPrefs.GetInt(SEMuteKey, 0) == 1;
            set
            {
                PlayerPrefs.SetInt(SEMuteKey, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public static bool BGMMute
        {
            get => PlayerPrefs.GetInt(BGMMuteKey, 0) == 1;
            set
            {
                PlayerPrefs.SetInt(BGMMuteKey, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
    }
}
using DG.Tweening;
using UnityEngine;

namespace UshiSoft
{
    public static class Common
    {
        public static string GetOrdinalNumberAlphabeticPart(int number)
        {
            var mod100 = number % 100;
            if (mod100 >= 11 && mod100 <= 13)
            {
                return "th";
            }

            var mod10 = number % 10;
            if (mod10 == 1)
            {
                return "st";
            }
            if (mod10 == 2)
            {
                return "nd";
            }
            if (mod10 == 3)
            {
                return "rd";
            }
            return "th";
        }

        public static bool IsAnimationPlaying(Tween tween)
        {
            return tween != null && tween.IsActive() && tween.IsPlaying();
        }

        public static T GetComponentInChildren<T>(GameObject gameObject) where T : MonoBehaviour
        {
            var components = gameObject.GetComponentsInChildren<T>();
            foreach (var component in components)
            {
                if (component.gameObject != gameObject)
                {
                    return component;
                }
            }
            return null;
        }
    }
}
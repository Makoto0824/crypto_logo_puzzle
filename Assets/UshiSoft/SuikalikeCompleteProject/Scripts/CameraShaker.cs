using DG.Tweening;
using UnityEngine;

namespace UshiSoft
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _durationCoef = 0.1f;
        [SerializeField, Min(0f)] private float _strengthCoef = 0.1f;
        [SerializeField, Min(0f)] private float _vibrateCoef = 50f;

        private Tween _tween;

        public void Shake(int fruitSize)
        {
            if (Common.IsAnimationPlaying(_tween))
            {
                _tween.Complete();
            }

            var dur = _durationCoef * fruitSize;
            var vib = Mathf.FloorToInt(_vibrateCoef * dur);
            _tween = transform.DOShakePosition(dur, _strengthCoef * fruitSize, vib);
        }
    }
}
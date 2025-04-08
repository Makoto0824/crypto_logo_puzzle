using TMPro;
using UnityEngine;

namespace UshiSoft.UI
{
    public class ShakeBowlButton : ButtonBase
    {
        [SerializeField] private BowlShaker _bowlShaker;

        private TMP_Text _text;

        protected override void Awake()
        {
            base.Awake();

            _text = GetComponentInChildren<TMP_Text>();

            _bowlShaker.OnNumRemainShakesChange.AddListener(OnNumRemainShakesChange);
        }

        private void OnNumRemainShakesChange(int numRemainShakes, int numShakes)
        {
            _text.text = $"SHAKE({numRemainShakes}/{numShakes})";

            _button.interactable = numRemainShakes > 0;
        }
    }
}
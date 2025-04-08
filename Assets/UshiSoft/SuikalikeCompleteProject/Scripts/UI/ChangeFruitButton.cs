using TMPro;
using UnityEngine;

namespace UshiSoft.UI
{
    public class ChangeFruitButton : ButtonBase
    {
        [SerializeField] private FruitChanger _fruitChanger;

        private TMP_Text _text;

        protected override void Awake()
        {
            base.Awake();

            _text = GetComponentInChildren<TMP_Text>();

            _fruitChanger.OnNumRemainChangesChange.AddListener(OnNumRemainChangesChange);
        }

        private void OnNumRemainChangesChange(int numRemainChanges, int numChanges)
        {
            _text.text = $"CHANGE({numRemainChanges}/{numChanges})";

            _button.interactable = numRemainChanges > 0;
        }
    }
}
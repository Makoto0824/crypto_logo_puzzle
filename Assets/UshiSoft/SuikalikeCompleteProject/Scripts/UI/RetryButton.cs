namespace UshiSoft.UI
{
    public class RetryButton : ButtonBase
    {
        protected override void Awake()
        {
            base.Awake();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneChanger.Instance.ChangeScene("Game");
        }
    }
}
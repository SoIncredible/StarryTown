using UI.Core;

namespace UI.Settings
{
    public class SettingsPage : BasePage
    {
        private SettingsUI _ui;

        protected override void Init()
        {
            base.Init();
            _ui = BaseUI as SettingsUI;
        }

        protected override void Prepare(params object[] args)
        {
            base.Prepare(args);
        }

        protected override void AddEvent()
        {
            base.AddEvent();
            _ui.closeButton.onClick.AddListener(OnCloseBtnClicked);
            foreach (var btn in _ui.settingsBindButtonList)
            {
                // btn.onClick.AddListener();
            }
        }

        protected override void RemoveEvent()
        {
            base.RemoveEvent();
            _ui.closeButton.onClick.RemoveAllListeners();
            foreach (var btn in _ui.settingsBindButtonList)
            {
            }
        }

        private void OnCloseBtnClicked()
        {
            UIManager.Instance.ClosePage(UIType.Settings);
        }
    }
}
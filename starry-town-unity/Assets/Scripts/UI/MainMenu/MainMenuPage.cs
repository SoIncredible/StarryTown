using UI.Core;
using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuPage : BasePage
    {
        private MainMenuUI _ui;


        protected override void Init()
        {
            base.Init();
            _ui = BaseUI as MainMenuUI;
        }

        protected override void Prepare(params object[] args)
        {
            base.Prepare();
        }

        protected override void AddEvent()
        {
            base.AddEvent();
            _ui.settingsButton.onClick.AddListener(OnSettingsBtnClicked);
            _ui.newGameButton.onClick.AddListener(OnNewGameBtnClicked);
            _ui.loadGameButton.onClick.AddListener(OnLoadGameBtnClicked);
            _ui.internetGameButton.onClick.AddListener(OnInternetGameBtnClicked);
            _ui.quitGameButton.onClick.AddListener(OnQuitGameBtnClicked);
        }

        protected override void RemoveEvent()
        {
            base.RemoveEvent();
            _ui.settingsButton.onClick.RemoveAllListeners();
            _ui.newGameButton.onClick.RemoveAllListeners();
            _ui.loadGameButton.onClick.RemoveAllListeners();
            _ui.internetGameButton.onClick.RemoveAllListeners();
            _ui.quitGameButton.onClick.RemoveAllListeners();
        }


        private void OnSettingsBtnClicked()
        {
            UIManager.Instance.OpenPage(UIType.Settings);
        }

        private void OnNewGameBtnClicked()
        {
            Debug.Log("调用新游戏方法");
        }

        private void OnLoadGameBtnClicked()
        {
            Debug.Log("调用加载游戏方法");
        }

        private void OnQuitGameBtnClicked()
        {
            Debug.Log("调用退出游戏方法");
        }

        private void OnInternetGameBtnClicked()
        {
            Debug.Log("调用联机游戏方法");
        }
    }
}
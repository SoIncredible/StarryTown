using UI.Core;

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
    }
}
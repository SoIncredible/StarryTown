using System;
using UI.Core;

namespace UI.Settings
{
    public class SettingsPage : BasePage
    {
        private BaseUI _ui;

        protected override void Init()
        {
            base.Init();
            _ui = BaseUI as SettingsUI;
        }

        protected override void Prepare(params object[] args)
        {
            base.Prepare(args);
        }
    }
}
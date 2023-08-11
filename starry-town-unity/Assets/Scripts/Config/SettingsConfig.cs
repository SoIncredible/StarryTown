namespace Config
{
    public class SettingsConfig
    {
        public struct SingleInputSettingItemConfig
        {
            private string _actionText;
            private string _defaultBindBtnText;
            private string _curBindBtnText;
            private string _alternateBindBtnText;


            public string ActionText
            {
                get => _actionText;
                set => _actionText = value;
            }

            public string DefaultBindBtnText
            {
                get => _defaultBindBtnText;
                set => _defaultBindBtnText = value;
            }

            public string CurBindBtnText
            {
                get => _curBindBtnText;
                set => _curBindBtnText = value;
            }

            public string AlternateBindBtnText
            {
                get => _alternateBindBtnText;
                set => _alternateBindBtnText = value;
            }

            public SingleInputSettingItemConfig(string actionText, string defaultBindBtnText, string curBindBtnText,
                string alternateBindBtnText)
            {
                _actionText = actionText;
                _defaultBindBtnText = defaultBindBtnText;
                _curBindBtnText = curBindBtnText;
                _alternateBindBtnText = alternateBindBtnText;
            }
        }
    }
}
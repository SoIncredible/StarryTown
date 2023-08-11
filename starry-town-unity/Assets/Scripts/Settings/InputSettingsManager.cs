using System.Collections.Generic;
using Config;
using DefaultNamespace;
using Message;
using UnityEngine;

namespace Settings
{
    public class InputSettingsManager : SettingsManager
    {
        // 输入相关设置
        public static InputSettingsManager Instance;


        private Dictionary<string, SettingsConfig.SingleInputSettingItemConfig> _settingDic;

        public static void Creat()
        {
            if (Instance == null)
            {
                Instance = new InputSettingsManager();
            }

            Instance.LoadSettingDic();

            Instance.AddListener();
        }


        private void LoadSettingDic()
        {
            _settingDic = ConfigManager.Instance.GetSettingDic();
        }


        public Dictionary<string, SettingsConfig.SingleInputSettingItemConfig> GetSettingDic()
        {
            return _settingDic;
        }

        private void AddListener()
        {
            MessageCenter.Add<SettingsConfig.SingleInputSettingItemConfig, string, bool>(
                MessageCmd.ChangeInputSettingSuccess,
                RefreshAllSettings);
        }

        public void ResetInputSettings()
        {
            // 重制所有的设置
        }

        public bool IsKeyBound(string key,
            out SettingsConfig.SingleInputSettingItemConfig config)
        {
            // 检查Key是否被绑定

            foreach (var item in _settingDic.Values)
            {
                if (key == item.CurBindBtnText || key == item.AlternateBindBtnText)
                {
                    config = item;
                    return true;
                }
            }

            config = new SettingsConfig.SingleInputSettingItemConfig();

            return false;
        }

        public void ChangBindKey(SettingsConfig.SingleInputSettingItemConfig co, string key, bool changeCurBtn)
        {
            bool flag = Instance.IsKeyBound(key,
                out SettingsConfig.SingleInputSettingItemConfig config);

            if (flag)
            {
                var singleInputSettingItemConfig = _settingDic[co.ActionText];
                if (config.CurBindBtnText == key)
                {
                    singleInputSettingItemConfig.CurBindBtnText = "";
                }
                else if (config.AlternateBindBtnText == key)
                {
                    singleInputSettingItemConfig.AlternateBindBtnText = "";
                }

                _settingDic[config.ActionText] = singleInputSettingItemConfig;
            }

            var singleInputSettingItemConfig1 = _settingDic[co.ActionText];
            if (changeCurBtn)
            {
                singleInputSettingItemConfig1.CurBindBtnText = key;
            }
            else
            {
                singleInputSettingItemConfig1.AlternateBindBtnText = key;
            }

            _settingDic[co.ActionText] = singleInputSettingItemConfig1;

            ConfigManager.Instance.UpdateSettingDic(_settingDic);
            _settingDic = ConfigManager.Instance.GetSettingDic();
        }

        private void RefreshAllSettings(SettingsConfig.SingleInputSettingItemConfig config, string bindBtnText,
            bool changeCurBtn)
        {
            ChangBindKey(config, bindBtnText, changeCurBtn);
            MessageCenter.Dispatch(MessageCmd.SettingsUIRefresh);
        }
    }
}
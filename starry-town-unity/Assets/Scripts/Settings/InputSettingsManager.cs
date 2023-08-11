using System.Collections.Generic;
using Config;
using DefaultNamespace;
using UnityEngine;

namespace Settings
{
    public class InputSettingsManager : SettingsManager
    {
        // 输入相关设置
        public static InputSettingsManager Instance;

        // const 和 readonly 的区别
        private Dictionary<string, SettingsConfig.SingleInputSettingItemConfig> _settingDic;

        public static void Creat()
        {
            if (Instance == null)
            {
                Instance = new InputSettingsManager();
            }

            Instance.LoadSettingDic();
        }


        private void LoadSettingDic()
        {
            _settingDic = ConfigManager.Instance.GetSettingDic();
        }


        public Dictionary<string, SettingsConfig.SingleInputSettingItemConfig> GetSettingDic()
        {
            return _settingDic;
        }

        public void ResetInputSettings()
        {
            // 重制所有的设置
        }
    }
}
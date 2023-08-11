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

        private const string InputSettingsKey = "InputSettings";

        public static void Creat()
        {
            if (Instance == null)
            {
                Instance = new InputSettingsManager();
            }

            Instance.LoadDefaultSettings();
        }


        public void ResetInputSettings()
        {
            // 重制所有的设置
        }


        private void LoadSettingsFromJson(string json)
        {
            // data
        }

        private void LoadSettingsFromConfig()
        {
        }

        private void LoadDefaultSettings()
        {
            var list = ConfigManager.Instance.GetConfigList();

            foreach (var item in list)
            {
                _settingDic.Add(item.ActionText, item);
            }
        }

        private void LoadPlayerPref()
        {
            foreach (var item in _settingDic.Keys)
            {
                if (PlayerPrefs.HasKey(item))
                {
                    // PlayerPrefs中保存了玩家自定义的数据，如果在Prefs中找不到key，说明这是新加入游戏的配置，可以从Config中加载
                    string json = PlayerPrefs.GetString(InputSettingsKey);
                    Instance.LoadSettingsFromJson(json);
                }
                else
                {
                }
            }
        }

        private string SaveSettingsToJson()
        {
            SettingsConfig.SingleInputSettingItemConfig config = new SettingsConfig.SingleInputSettingItemConfig();

            string json = JsonUtility.ToJson(config);

            PlayerPrefs.SetString(InputSettingsKey, json);

            return json;
        }
    }
}
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

            Instance.LoadAllSettingsConfig();

            Instance.LoadAllBindSetting();
        }


        public void ResetInputSettings()
        {
            // 重制所有的设置
        }


        private void LoadSettingsFromJson(string json)
        {
            // data
            var item = JsonUtility.FromJson<SettingsConfig.SingleInputSettingItemConfig>(json);
            _settingDic[item.ActionText] = item;
        }


        private void LoadAllSettingsConfig()
        {
            var list = ConfigManager.Instance.GetConfigList();

            foreach (var item in list)
            {
                _settingDic.Add(item.ActionText, item);
            }
        }

        private SettingsConfig.SingleInputSettingItemConfig GetDefaultSettingFromConfig(string config)
        {
            SettingsConfig.SingleInputSettingItemConfig item = ConfigManager.Instance.GetOneConfig(config);
            return item;
        }

        private void LoadAllBindSetting()
        {
            foreach (var item in _settingDic.Keys)
            {
                if (PlayerPrefs.HasKey(item))
                {
                    // PlayerPrefs中保存了玩家自定义的数据，如果在Prefs中找不到key，说明这是新加入游戏的配置，可以从Config中加载
                    string json = PlayerPrefs.GetString(item);
                    Instance.LoadSettingsFromJson(json);
                }
                else
                {
                    // 对于没有在Prefs中保存的数据
                    // 从Config中读取
                    var config = GetDefaultSettingFromConfig(item);

                    // 将读取到的 config 先保存到Prefs中
                    Instance.SaveSettingsToJson(config.ActionText);

                    _settingDic[config.ActionText] = config;
                }
            }
        }

        private void SaveSettingsToJson(string inputSettingsKey)
        {
            SettingsConfig.SingleInputSettingItemConfig config = new SettingsConfig.SingleInputSettingItemConfig();

            string json = JsonUtility.ToJson(config);

            PlayerPrefs.SetString(inputSettingsKey, json);
        }
    }
}
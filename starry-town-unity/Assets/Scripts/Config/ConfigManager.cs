using System.Collections.Generic;
using UnityEngine;
using SingleInputSettingItemConfig = Config.SettingsConfig.SingleInputSettingItemConfig;

namespace Config
{
    public class ConfigManager : MonoBehaviour
    {
        public static ConfigManager Instance;

        // TODO: 将该数据替换成Config
        // 暂时使用PlayerPrefs持久化存储玩家的设置
        private static readonly List<SingleInputSettingItemConfig> _list =
            new List<SingleInputSettingItemConfig>()
            {
                new SingleInputSettingItemConfig("Fire", "W", "W", ""),
                new SingleInputSettingItemConfig("SwapWeapon", "A", "A", ""),
                new SingleInputSettingItemConfig("Lurch", "S", "S", ""),
                new SingleInputSettingItemConfig("Jump", "D", "D", "")
            };


        // ConfigManager对SettingsManager应该只暴露这一个Dic，而不会暴露其他的字段
        public Dictionary<string, SingleInputSettingItemConfig> _configDic =
            new Dictionary<string, SingleInputSettingItemConfig>(128);


        public static void Create()
        {
            if (Instance == null)
            {
                Instance = new ConfigManager();
            }


            // 确保所有的Config都在
            // 由于有些Config可能是后面加上去的，所以在每一次Create的时候都要检查一遍Prefs中有没有包含所有的config

            foreach (var item in _list)
            {
                if (PlayerPrefs.HasKey(item.ActionText))
                {
                    // 自定义过
                    // 从Prefs中获取
                    // 不需要保存到Prefs中
                    string json = PlayerPrefs.GetString(item.ActionText);
                    Instance.LoadSettingsFromJson(json);
                }
                else
                {
                    // 没有自定义过
                    // 从config中获取
                    var temp = Instance.GetOneConfig(item.ActionText);
                    // 加载到Dic中去
                    Instance.LoadSettingsFromConfig(temp);
                    // 保存到Prefs中
                    Instance.SaveSettingsToJson(item);
                }
            }
        }


        public SingleInputSettingItemConfig GetOneConfig(string item)
        {
            foreach (var i in _list)
            {
                if (item == i.ActionText)
                {
                    return i;
                }
            }

            Debug.LogError("配置表中没有相关信息！");
            return new SingleInputSettingItemConfig("", "", "", "");
        }

        private void LoadSettingsFromJson(string json)
        {
            var item = JsonUtility.FromJson<SingleInputSettingItemConfig>(json);
            _configDic.Add(item.ActionText, item);
        }

        private void LoadSettingsFromConfig(SingleInputSettingItemConfig item)
        {
            _configDic.Add(item.ActionText, item);
        }

        private void LoadDefaultSettings()
        {
            // 用来重制设置
        }

        public void SaveSettingsToJson(SingleInputSettingItemConfig config)
        {
            string json = JsonUtility.ToJson(config);

            PlayerPrefs.SetString(config.ActionText, json);

            PlayerPrefs.Save();
        }

        public Dictionary<string, SingleInputSettingItemConfig> GetSettingDic()
        {
            return _configDic;
        }

        public void UpdateSettingDic(Dictionary<string, SingleInputSettingItemConfig> dic)
        {
            // 更新了字典
            _configDic = dic;

            foreach (var config in _configDic.Values)
            {
                SaveSettingsToJson(config);
            }
        }
    }
}
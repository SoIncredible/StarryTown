using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using SingleInputSettingItemConfig = Config.SettingsConfig.SingleInputSettingItemConfig;

namespace Config
{
    public class ConfigManager : MonoBehaviour
    {
        public static ConfigManager Instance;

        private const string InputSettingsKey = "InputSettings";


        public static void Create()
        {
            if (Instance == null)
            {
                Instance = new ConfigManager();
            }

            if (PlayerPrefs.HasKey(InputSettingsKey))
            {
                string json = PlayerPrefs.GetString(InputSettingsKey);
                Instance.LoadSettingsFromJson(json);
            }
            else
            {
                // 没有自定义过
                Instance.LoadDefaultSettings();
            }
        }


        public List<SingleInputSettingItemConfig> GetConfigList()
        {
            return _list;
        }

        public void GetOneConfig()
        {
        }

        private void LoadSettingsFromJson(string json)
        {
            // data
        }

        private void LoadDefaultSettings()
        {
        }

        private string SaveSettingsToJson()
        {
            SettingsConfig config = new SettingsConfig();

            string json = JsonUtility.ToJson(config);

            PlayerPrefs.SetString(InputSettingsKey, json);

            return json;
        }

        // 如果在PlayerPrefs中没有存储有用户自定义的配置 则从配置文件中加载


        // TODO: 将该数据替换成Config
        // 暂时使用PlayerPrefs持久化存储玩家的设置
        private readonly List<SingleInputSettingItemConfig> _list =
            new List<SingleInputSettingItemConfig>()
            {
                new SingleInputSettingItemConfig("Fire", "W", "W", ""),
                new SingleInputSettingItemConfig("SwapWeapon", "A", "A", ""),
                new SingleInputSettingItemConfig("Lurch", "S", "S", ""),
                new SingleInputSettingItemConfig("Jump", "D", "D", "")
            };


        public List<SingleInputSettingItemConfig> LoadConfig()
        {
            return _list;
        }
    }
}
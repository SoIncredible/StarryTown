using System.Collections.Generic;
using UnityEngine;
using SingleInputSettingItemConfig = Config.SettingsConfig.SingleInputSettingItemConfig;

namespace Config
{
    public class ConfigManager : MonoBehaviour
    {
        public static ConfigManager Instance;

        public static void Create()
        {
            if (Instance == null)
            {
                Instance = new ConfigManager();
            }
        }


        // TODO: 将该数据替换成Config
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
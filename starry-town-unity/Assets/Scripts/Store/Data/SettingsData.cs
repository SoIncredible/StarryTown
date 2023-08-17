using System.Collections.Generic;
using Config;

namespace Store.Data
{
    public class SettingsData : IStoreData
    {
        public static SettingsData Data => StoreRoot.Inst.Settings;

        public List<SettingsConfig.SingleInputSettingItemConfig> InputSettingsList;

        public void Reset()
        {
            InputSettingsList =
                new List<SettingsConfig.SingleInputSettingItemConfig>()
                {
                    new SettingsConfig.SingleInputSettingItemConfig("Fire", "W", "W", ""),
                    new SettingsConfig.SingleInputSettingItemConfig("SwapWeapon", "A", "A", ""),
                    new SettingsConfig.SingleInputSettingItemConfig("Lurch", "S", "S", ""),
                    new SettingsConfig.SingleInputSettingItemConfig("Jump", "D", "D", "")
                };
        }
    }
};
using System.Collections.Generic;
using Config;

namespace Store.Data;

public class SettingsData : IStoreData
{
    public static SettingsData Data => StoreRoot.Inst.Settings;

    public List<SettingsConfig.SingleInputSettingItemConfig> InputSettingsList;

    public void Reset()
    {
        InputSettingsList = ReadConfig();
    }

    // 读配置表
    List<SettingsConfig.SingleInputSettingItemConfig> ReadConfig()
    {
        return new List<SettingsConfig.SingleInputSettingItemConfig>();
    }
}
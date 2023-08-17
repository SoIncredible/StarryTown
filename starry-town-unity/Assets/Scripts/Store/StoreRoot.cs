using Store.Data;

namespace Store
{
    // TODO:将StoreRoot配置为自动生成
    public class StoreRoot
    {
        public static StoreRoot Inst;

        public SettingsData Settings;


        public void Init()
        {
            if (Settings == null)
            {
                Settings = new SettingsData();
                Settings.Reset();
            }
        }


        public void Reset()
        {
            Settings.Reset();
        }
    }
}
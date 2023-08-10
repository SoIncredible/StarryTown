using DefaultNamespace;

namespace Settings
{
    public class DisplaySettingsManager : SettingsManager
    {
        // 显示相关设置
        public static DisplaySettingsManager Instance;

        public static void Creat()
        {
            if (Instance == null)
            {
                Instance = new DisplaySettingsManager();
            }
        }
    }
}
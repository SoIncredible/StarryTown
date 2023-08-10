using UnityEngine;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        // DataManager要做的事情有哪些？
        // 管理数据
        // 加载数据
        // DataManager和ResManager应不应该分开？
        // 配置文件输入Res还是Data？

        public static DataManager Instance;

        public static void Create()
        {
            if (Instance == null)
            {
                Instance = new DataManager();
            }
        }


        public void LoadConfig()
        {
            // 加载配置数据
        }


        public void RefreshData()
        {
            // 更新数据
        }
    }
}
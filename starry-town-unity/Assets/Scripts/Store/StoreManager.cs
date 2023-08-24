using System;

namespace Store
{
    public class StoreManager
    {
        public static StoreManager Instance;

        public static void Create()
        {
            Instance = new StoreManager();
            Instance.Init();
        }

        private void Init()
        {
        }

        private void Release()
        {
            StoreRoot.Inst = null;
        }

        // 私有构造函数
        private StoreManager()
        {
        }

        private const string FileName = "StoreData";
        private static readonly Version StoreVersion = new Version(1, 0, 0);
        private const int Version = 1;

        private StoreFile _storeFile;


        public void Load()
        {
        }

        // public T Load<T>()
        // {
        // }

        public void Save()
        {
            // 
        }
    }
};
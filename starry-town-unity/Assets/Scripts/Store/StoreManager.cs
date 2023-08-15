namespace Store;

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


    public void Save()
    {
        // 
    }
}
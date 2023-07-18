namespace UI.Core
{
    public class UIDefine
    {
    }

    public enum UIType
    {
        MainMenu, // 主菜单
        Settings, // 设置页面
        LoadProgress, // 存档
        Bag, // 背包
        GetReward, // 获得道具or奖励
        Dialog, // 对话框
    }

    public enum PopupType
    {
        None,
        Up,
        Down,
        Scale,
        OldScale,
    }

    public enum UILayer
    {
        Background = 0,
        Play = 200,
    }
}
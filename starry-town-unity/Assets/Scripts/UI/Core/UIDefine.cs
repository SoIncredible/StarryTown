using UnityEngine;

namespace UI.Core
{
    public static class UIDefine
    {
        public const int GameObjectLayerValue = 5;
        public const string SortingLayerName = "UI";


        public const float MatchWidth = 0;
        public const float MatchHeight = 1.0f;

        public static class Settings
        {
            public const string RootPath = "UI";
        }


        public static void NormalizeTransform(RectTransform trans)
        {
            trans.sizeDelta = Vector2.zero;
            trans.anchorMin = Vector2.zero;
            trans.anchorMax = Vector2.zero;
            trans.pivot = NormalizePivot;
            trans.anchoredPosition = Vector2.zero;
        }

        private static readonly Vector2 NormalizePivot = new Vector2(0.5f, 0.5f);
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
        Dialog = 400,

        OverLay = 800,
    }
}
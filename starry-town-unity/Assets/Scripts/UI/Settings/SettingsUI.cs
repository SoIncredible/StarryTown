using System.Collections.Generic;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public class SettingsUI : BaseUI
    {
        // TODO：将设置页面中可以修改的按键配置信息配成excel表的形式进行操作
        // TODO: 所有的UI改为自动生成
        public List<Text> settingsBindButtonTexts = new List<Text>();
        public List<Button> settingsBindButtonList = new List<Button>();

        // public Dictionary<Button, Text> settings
        public Button closeButton;


        // TODO: 改为无限滑动
        public RectTransform InputSettingsRect;
    }
}
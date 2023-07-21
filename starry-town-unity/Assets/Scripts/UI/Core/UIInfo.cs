using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Core
{
    public class UIInfo
    {
        public readonly UIType UIType;
        public readonly UILayer Layer;
        public readonly Type PageType;
        public readonly Type BaseUI;
        public Button Button;

        public UIInfo(UIType type, UILayer layer, Type pageType, Type baseUI)
        {
            UIType = type;
            Layer = layer;
            PageType = pageType;
            BaseUI = baseUI;
        }
    }
}
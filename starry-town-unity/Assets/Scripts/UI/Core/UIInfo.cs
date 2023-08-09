using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Core
{
    public class UIInfo
    {
        private readonly UIType _uiType;
        private readonly Type _pageType;
        private readonly Type _baseUI;
        private readonly string _assetPath;


        public UIType UIType
        {
            get => _uiType;
        }

        public Type PageType
        {
            get => _pageType;
        }

        public Type BaseUI
        {
            get => _baseUI;
        }

        public string AssetPath
        {
            get => _assetPath;
        }

        public UIInfo(UIType type, Type pageType, Type baseUI, string assetPath)
        {
            _uiType = type;
            _pageType = pageType;
            _baseUI = baseUI;
            _assetPath = assetPath;
        }
    }
}
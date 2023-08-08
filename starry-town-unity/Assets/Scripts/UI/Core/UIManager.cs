using System.Collections.Generic;
using System.IO;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Core
{
    public class UIManager : AbstractUIManager
    {
        public static UIManager Instance { get; private set; }


        protected readonly Dictionary<int, BasePage> _pages = new Dictionary<int, BasePage>(128);


        // 在执行一个关闭和打开操作的时候不是直接打开，而是由事件中心在中间操控
        public void OpenPage(UIType uiType, params object[] args)
        {
        }

        public void ClosePage()
        {
        }


        protected override T Load<T>(string assetPath, Transform parent)
        {
            // retur
        }


        protected BasePage GetPage(UIInfo uiInfo)
        {
            if (!_pages.TryGetValue((int)uiInfo.UIType, out var page))
            {
                return page;
            }

            var assetPath = GetAssetPath(uiInfo.BaseUI.Name);
            var go = Load<GameObject>(assetPath, parent);
            go.name = uiInfo.UIType + "UI";

            page = go.AddComponent(uiInfo.PageType) as BasePage;
            page.OnCreate(uiInfo.UIType);
            return page;
        }

        public T GetPage<T>(UIType uiType) where T : BasePage
        {
            if (!_infos.TryGetValue((int)uiType, out var info))
            {
                Debug.LogError("[UIManager] Info is null");
                return null;
            }

            return GetPage(info) as T;
        }


        protected readonly Dictionary<int, UIInfo> _infos = new Dictionary<int, UIInfo>(128);


        protected void AddInfo(UIInfo uiInfo)
        {
            if (_infos.ContainsKey((int)uiInfo.UIType))
            {
                return;
            }

            _infos.Add((int)uiInfo.UIType, uiInfo);
        }

        protected string GetAssetPath(string assetName)
        {
            return Path.Combine(UIDefine.Settings.RootPath, assetName);
        }
    }
}
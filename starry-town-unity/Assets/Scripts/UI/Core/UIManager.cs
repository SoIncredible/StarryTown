using System.Collections.Generic;
using System.IO;
using System.Resources;
using Resource;
using UI.MainMenu;
using UI.Settings;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using View;
using static UI.Core.UIDefine;

namespace UI.Core
{
    public class UIManager : AbstractUIManager
    {
        public static UIManager Instance { get; private set; }


        public static void Creat(GameObject go)
        {
            Instance = go.AddComponent<UIManager>();
            Instance.Init();
        }

        private void Init()
        {
            Instance.InitInternal(GameObject.Find("Canvas").GetComponent<Canvas>());
            Instance.LoadInfo();
            // var tablet = PlayViewDefine.GetViewTablet();
            // ResizeMatch(tablet == ViewTablet.Pad);
        }

        public void ResizeMatch(bool isLow)
        {
            UIScaler.matchWidthOrHeight = isLow ? UIDefine.MatchHeight : UIDefine.MatchWidth;
        }


        protected readonly Dictionary<int, BasePage> _pages = new Dictionary<int, BasePage>(128);


        private void LoadInfo()
        {
            AddInfo(new UIInfo(UIType.MainMenu, UILayer.Background, typeof(MainMenuPage), typeof(MainMenuUI)));
            AddInfo(new UIInfo(UIType.Settings, UILayer.Dialog, typeof(SettingsPage), typeof(SettingsUI)));
        }


        // 在执行一个关闭和打开操作的时候不是直接打开，而是由事件中心在中间操控
        public bool OpenPage(UIType uiType, params object[] args)
        {
            if (!_infos.TryGetValue((int)uiType, out var info))
            {
                Debug.LogError("[UIManager] Info is null");
                return false;
            }

            var page = GetPage(info);

            if (page.IsOpening)
            {
                Debug.LogError("页面已经打开！不能重复打开！");

                return false;
            }

            AddPageInOrder(info.Layer, page);
            page.OnOpen(args);
            return true;
        }


        protected void AddPageInOrder(UILayer uiLayer, BasePage target)
        {
            var baseOrder = (int)uiLayer;
        }

        public void ClosePage(UIType uiType, bool isRemove = false)
        {
            if (_infos.TryGetValue((int)uiType, out var info))
            {
                Debug.LogError("[UIManager Info is null");
                return;
            }

            var page = GetPage(info);
            if (page == null)
            {
                return;
            }

            if (!page.IsOpening)
            {
                return;
            }

            page.OnClose(delegate
            {
                // RemovePageInOrder(info.Layer, page);
                // if (_queuePage == page)
                // {
                //     _
                // }
            });
        }


        protected override T Load<T>(string assetPath, Transform parent)
        {
            return ResManager.Load<T>(assetPath, parent);
        }


        protected BasePage GetPage(UIInfo uiInfo)
        {
            if (!_pages.TryGetValue((int)uiInfo.UIType, out var page))
            {
                return page;
            }

            if (!_uiLayers.TryGetValue((int)uiInfo.Layer, out var parent))
            {
                AddLayer(uiInfo.Layer);
                parent = _uiLayers[(int)uiInfo.Layer];
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
        protected readonly Dictionary<int, Transform> _uiLayers = new Dictionary<int, Transform>(10);
        protected readonly Dictionary<int, List<BasePage>> _pagesInOrder = new Dictionary<int, List<BasePage>>(10);

        protected void AddLayer(UILayer uiLayer)
        {
            var go = new GameObject();
            go.gameObject.name = uiLayer.ToString();

            go.layer = UIDefine.GameObjectLayerValue;

            var rectTrans = go.AddComponent<RectTransform>();

            UIDefine.NormalizeTransform(rectTrans);

            go.transform.SetParent(Root, false);

            _uiLayers.Add((int)uiLayer, go.transform);

            var canvas = go.AddComponent<Canvas>();

            canvas.overrideSorting = true;
            canvas.sortingLayerName = UIDefine.SortingLayerName;
            canvas.sortingOrder = (int)uiLayer;
        }


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
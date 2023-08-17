using System;
using System.Collections.Generic;
using System.IO;
using Resource;
using UI.MainMenu;
using UI.Settings;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Core
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        private RectTransform RootRect { get; set; }
        private Canvas UICanvas { get; set; }

        private CanvasScaler UIScaler { get; set; }

        private EventSystem EventSystem { get; set; }

        // 已经存在于场景中的Page
        private readonly Dictionary<int, BasePage> _existingPages = new Dictionary<int, BasePage>(128);

        // 所有的Page
        private readonly Dictionary<int, UIInfo> _uiInfos = new Dictionary<int, UIInfo>(128);

        public static void Creat(GameObject go)
        {
            if (Instance == null)
            {
                Instance = go.AddComponent<UIManager>();
            }

            Instance.Init();
        }

        public void Destroy()
        {
            foreach (var page in _existingPages)
            {
                //TODO: 在销毁UIManager之前先清空游戏中所有存在的Page
            }

            Instance = null;
        }

        private void Init()
        {
            // 加载Info
            LoadInfo();
            FindUIRootNode(GameObject.Find("Canvas"));
        }

        private void FindUIRootNode(GameObject canvas)
        {
            UICanvas = canvas.GetComponent<Canvas>();
            RootRect = canvas.GetComponent<RectTransform>();
            // EventSystem = canvas.GetComponent<EventSystem>();
            // UIScaler = canvas.GetComponent<CanvasScaler>();
        }

        private void LoadInfo()
        {
            AddInfo(new UIInfo(UIType.MainMenu, typeof(MainMenuPage), typeof(MainMenuUI),
                ResDefine.PrefabUI.MainMenuUI));
            AddInfo(new UIInfo(UIType.Settings, typeof(SettingsPage), typeof(SettingsUI),
                ResDefine.PrefabUI.SettingsUI));
        }


        // 有一些Page可能在整个Session中的生命周期只有一次，那么应该在该页面被关闭了之后执行Destroy
        public void CreatePage()
        {
        }

        public void DestroyPage()
        {
        }


        public bool OpenPage(UIType uiType, params object[] args)
        {
            if (!_uiInfos.TryGetValue((int)uiType, out var info))
            {
                Debug.LogError("info中没有该UI相关信息！");
                return false;
            }

            var page = GetPage(info);

            if (page == null)
            {
                Debug.LogError("UI加载失败！");
                return false;
            }


            page.OnOpen(args);
            _existingPages.Add((int)uiType, page);

            return true;
        }

        public void ClosePage(UIType uiType)
        {
            if (_uiInfos.TryGetValue((int)uiType, out var info))
            {
                Debug.LogError("没有该UI的信息");
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

            page.OnClose(delegate { });
        }


        private GameObject LoadRes(string assetPath)
        {
            var prefab = Resources.Load(assetPath);

            if (prefab != null)
            {
                var go = Instantiate(prefab, RootRect);
                return go as GameObject;
            }

            Debug.LogError("无法加载资源！资源路径出错！");
            return null;
        }


        private BasePage GetPage(UIInfo uiInfo)
        {
            // TODO:为UI添加Layer的设置

            if (_existingPages.TryGetValue((int)uiInfo.UIType, out var page))
            {
                return page;
            }
            else
            {
                var assetPath = uiInfo.AssetPath;
                var go = LoadRes(assetPath);
                if (go != null)
                {
                    page = go.AddComponent(uiInfo.PageType) as BasePage;

                    if (page != null)
                    {
                        page.OnCreate(uiInfo.UIType);
                        return page;
                    }
                    else
                    {
                        Debug.LogError("相关Page脚本读取失败");
                        return null;
                    }
                }
                else
                {
                    Debug.LogError("资源路径错误！无法加载资源");
                    return null;
                }
            }
        }


        private void AddInfo(UIInfo uiInfo)
        {
            if (_uiInfos.ContainsKey((int)uiInfo.UIType))
            {
                return;
            }

            _uiInfos.Add((int)uiInfo.UIType, uiInfo);
        }

        // 禁止操作

        public void ForbidInput()
        {
        }
    }
}
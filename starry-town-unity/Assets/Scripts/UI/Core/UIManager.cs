using System;
using System.Collections.Generic;
using System.IO;
using Resource;
using UI.MainMenu;
using UI.Settings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Core
{
    // 所有的Manager都会挂载到Launcher脚本的game object上面
    // 不希望使用到AbstractUIManager
    // 在执行一个关闭和打开操作的时候不是直接打开，而是由事件中心在中间操控
    public class UIManager : MonoBehaviour
    {
        // 所有的UI相关的Prefab都要放在这个路径下
        private const string AssetPath = "Prefab/UI/";
        public static UIManager Instance;
        private RectTransform RootRect { get; set; }
        private Canvas UICanvas { get; set; }

        private CanvasScaler UIScaler { get; set; }

        private EventSystem EventSystem { get; set; }

        private readonly Dictionary<int, BasePage> _existingPages = new Dictionary<int, BasePage>(128);

        private readonly Dictionary<int, UIInfo> _infos = new Dictionary<int, UIInfo>(128);

        public static void Creat(GameObject go, Action callBack)
        {
            if (Instance == null)
            {
                Instance = new UIManager();
            }

            Instance = go.AddComponent<UIManager>();
            Instance.Init();

            callBack.Invoke();
        }

        private void Init()
        {
            Instance.LoadInfo();
            InitInternal(GameObject.Find("Canvas").GetComponent<Canvas>());
        }

        private void InitInternal(Canvas canvas)
        {
            UICanvas = canvas;
            RootRect = canvas.gameObject.GetComponent<RectTransform>();
            EventSystem = canvas.gameObject.GetComponent<EventSystem>();
            UIScaler = canvas.GetComponent<CanvasScaler>();
        }

        private void LoadInfo()
        {
            AddInfo(new UIInfo(UIType.MainMenu, typeof(MainMenuPage), typeof(MainMenuUI),
                AssetPath + nameof(MainMenuUI)));
            AddInfo(new UIInfo(UIType.Settings, typeof(SettingsPage), typeof(SettingsUI),
                AssetPath + nameof(SettingsUI)));
        }

        public bool OpenPage(UIType uiType, params object[] args)
        {
            if (!_infos.TryGetValue((int)uiType, out var info))
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
            if (_infos.TryGetValue((int)uiType, out var info))
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
            if (_infos.ContainsKey((int)uiInfo.UIType))
            {
                return;
            }

            _infos.Add((int)uiInfo.UIType, uiInfo);
        }
    }
}
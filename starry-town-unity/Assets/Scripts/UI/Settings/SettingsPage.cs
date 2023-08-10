using System.Collections.Generic;
using Config;
using Data;
using Item;
using Resource;
using UI.Core;
using UnityEngine;
using Utils;

namespace UI.Settings
{
    public class SettingsPage : BasePage
    {
        private SettingsUI _ui;

        private List<SettingsConfig.SingleInputSettingItemConfig> _singleInputSettingsItemConfigList;

        private List<SingleInputSettingItem> _shownSingleInputSettingItemList;

        // 使用缓存池加载
        private ItemCache<SingleInputSettingItem> _cache;


        protected override void Init()
        {
            base.Init();
            _ui = BaseUI as SettingsUI;

            var temp = ConfigManager.Instance.LoadConfig();

            _singleInputSettingsItemConfigList = temp;

            _cache = new ItemCache<SingleInputSettingItem>(OnCreateSingleInputSettingItem, _ui.InputSettingsRect, 4);

            _shownSingleInputSettingItemList = new List<SingleInputSettingItem>(4);

            foreach (var conf in _singleInputSettingsItemConfigList)
            {
                var item = _cache.Pop();
                _shownSingleInputSettingItemList.Add(item);
                item.OnCreate(conf.ActionText, conf.CurBindBtnText);
            }
        }

        protected override void Prepare(params object[] args)
        {
            base.Prepare(args);
        }

        protected override void Clear()
        {
            base.Clear();
            // 回池
            while (_shownSingleInputSettingItemList.Count > 0)
            {
                _cache.Push(_shownSingleInputSettingItemList[0]);
            }
        }

        protected override void AddEvent()
        {
            base.AddEvent();
            _ui.closeButton.onClick.AddListener(OnCloseBtnClicked);
            foreach (var item in _shownSingleInputSettingItemList)
            {
                item.AddEvent();
            }
        }

        protected override void RemoveEvent()
        {
            base.RemoveEvent();
            _ui.closeButton.onClick.RemoveAllListeners();
            foreach (var item in _shownSingleInputSettingItemList)
            {
                item.RemoveEvent();
            }
        }

        private void OnCloseBtnClicked()
        {
            UIManager.Instance.ClosePage(UIType.Settings);
        }

        //TODO: 编写自定义的资源管理器
        private GameObject OnCreateSingleInputSettingItem(Transform parent)
        {
            var prefab = ResManager.Load(ResDefine.PrefabItem.SingleInputSettingItem);
            var item = Instantiate(prefab, parent) as GameObject;
            return item;
        }
    }
}
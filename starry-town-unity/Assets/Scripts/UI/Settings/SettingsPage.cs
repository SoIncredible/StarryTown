using System.Collections.Generic;
using Config;
using Item;
using Message;
using Resource;
using Settings;
using UI.Core;
using UnityEngine;
using Utils;

namespace UI.Settings
{
    public class SettingsPage : BasePage
    {
        private SettingsUI _ui;

        private List<SingleInputSettingItem> _shownSingleInputSettingItemList;

        private ItemCache<SingleInputSettingItem> _cache;

        private Dictionary<string, SettingsConfig.SingleInputSettingItemConfig> _settingDic;

        protected override void Init()
        {
            base.Init();
            _ui = BaseUI as SettingsUI;

            var temp = InputSettingsManager.Instance.GetSettingDic();

            _settingDic = temp;

            _cache = new ItemCache<SingleInputSettingItem>(OnCreateSingleInputSettingItem, _ui.InputSettingsRect, 4);

            _shownSingleInputSettingItemList = new List<SingleInputSettingItem>(4);
        }

        protected override void Prepare(params object[] args)
        {
            base.Prepare(args);
            var temp = InputSettingsManager.Instance.GetSettingDic();

            _settingDic = temp;

            _cache = new ItemCache<SingleInputSettingItem>(OnCreateSingleInputSettingItem, _ui.InputSettingsRect, 4);

            _shownSingleInputSettingItemList = new List<SingleInputSettingItem>(4);

            foreach (var i in _settingDic.Keys)
            {
                var item = _cache.Pop();
                var conf = _settingDic[i];
                _shownSingleInputSettingItemList.Add(item);
                item.OnEnter(conf.ActionText, conf.CurBindBtnText, conf.AlternateBindBtnText);
            }
        }

        protected override void Clear()
        {
            base.Clear();
            // 回池
            while (_shownSingleInputSettingItemList.Count > 0)
            {
                var head = _shownSingleInputSettingItemList[0];
                _cache.Push(head);
                _shownSingleInputSettingItemList.Remove(head);
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

            MessageCenter.Add(MessageCmd.SettingsUIRefresh, RefreshAllItem);
        }

        protected override void RemoveEvent()
        {
            base.RemoveEvent();
            _ui.closeButton.onClick.RemoveAllListeners();
            foreach (var item in _shownSingleInputSettingItemList)
            {
                item.RemoveEvent();
            }

            MessageCenter.Remove(MessageCmd.SettingsUIRefresh, RefreshAllItem);
        }

        private void OnCloseBtnClicked()
        {
            UIManager.Instance.ClosePage(UIType.Settings);
        }

        // TODO: 编写自定义的资源管理器
        private GameObject OnCreateSingleInputSettingItem(Transform parent)
        {
            var prefab = ResManager.Load(ResDefine.PrefabItem.SingleInputSettingItem);
            var item = Instantiate(prefab, parent) as GameObject;
            return item;
        }


        private void RefreshAllItem()
        {
            var temp = InputSettingsManager.Instance.GetSettingDic();
            _settingDic = temp;
            while (_shownSingleInputSettingItemList.Count > 0)
            {
                var head = _shownSingleInputSettingItemList[0];
                _cache.Push(head);
                _shownSingleInputSettingItemList.Remove(head);
            }

            foreach (var i in _settingDic.Keys)
            {
                var item = _cache.Pop();
                var conf = _settingDic[i];
                _shownSingleInputSettingItemList.Add(item);
                item.OnEnter(conf.ActionText, conf.CurBindBtnText, conf.AlternateBindBtnText);
            }
        }
    }
}
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

        private List<SettingsConfig.SingleInputSettingItemConfig> singleInputSetingsItemConfigList;

        protected override void Init()
        {
            base.Init();
            _ui = BaseUI as SettingsUI;

            var temp = ConfigManager.Instance.LoadConfig();

            singleInputSetingsItemConfigList = temp;


            // 使用缓存池加载
            ItemCache<SingleInputSettingItem> cache =
                new ItemCache<SingleInputSettingItem>(OnCreateSingleInputSettingItem, _ui.InputSettingsRect, 4);

            foreach (var conf in singleInputSetingsItemConfigList)
            {
                var item = cache.Pop();
                item.ActionText.text = conf.ActionText;
                item.BindButtonText.text = conf.CurBindBtnText;
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
        }

        protected override void AddEvent()
        {
            base.AddEvent();
            _ui.closeButton.onClick.AddListener(OnCloseBtnClicked);
            foreach (var btn in _ui.settingsBindButtonList)
            {
                // btn.onClick.AddListener();
            }
        }

        protected override void RemoveEvent()
        {
            base.RemoveEvent();
            _ui.closeButton.onClick.RemoveAllListeners();
            foreach (var btn in _ui.settingsBindButtonList)
            {
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
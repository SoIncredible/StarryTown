using System.Collections.Generic;
using Config;
using DefaultNamespace;
using Message;
using UnityEngine;

namespace Settings
{
    public class InputSettingsManager : SettingsManager
    {
        // 输入相关设置
        public static InputSettingsManager Instance;

        // const 和 readonly 的区别
        private Dictionary<string, SettingsConfig.SingleInputSettingItemConfig> _settingDic;

        public static void Creat()
        {
            if (Instance == null)
            {
                Instance = new InputSettingsManager();
            }

            Instance.LoadSettingDic();
        }


        private void LoadSettingDic()
        {
            _settingDic = ConfigManager.Instance.GetSettingDic();
        }


        public Dictionary<string, SettingsConfig.SingleInputSettingItemConfig> GetSettingDic()
        {
            return _settingDic;
        }

        public void ResetInputSettings()
        {
            // 重制所有的设置
        }

        public bool IsKeyBound(string key,
            out SettingsConfig.SingleInputSettingItemConfig? config)
        {
            // 检查Key是否被绑定

            foreach (var item in _settingDic.Values)
            {
                if (key == item.CurBindBtnText || key == item.AlternateBindBtnText)
                {
                    config = item;
                    return true;
                }
            }

            config = null;
            return false;
        }

        public void ChangBindKey(string key)
        {
            bool flag = Instance.IsKeyBound(key,
                out SettingsConfig.SingleInputSettingItemConfig? config);

            if (!flag)
            {
                // 指定按键没有绑定
            }
            else
            {
                // 指定按键有绑定
                // 清除原来绑定的按键
                // 提示用户原来绑定的按键被删除
                // 有的操作会有空的按键绑定的情况
                // 每次更改按键都需要遍历字典
            }

            OnSettingsSuccess();
        }


        // 在此处写一个方法，当修改成功之后可以作为回调保存修改的数据
        private void OnSettingsSuccess()
        {
            // 如果是冲突的话
            // 需要替换掉之前的Prefs
            // 还需要保存新的Prefs
            MessageCenter.Dispatch(MessageCmd.ChangeInputSettingSuccess);
        }
    }
}
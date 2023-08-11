using System;
using System.Collections.Generic;
using Config;
using Message;
using UnityEngine;

namespace Listener
{
    public class KeyBoardListenerManager : MonoBehaviour
    {
        public static KeyBoardListenerManager Instance;

        public Dictionary<int, KeyCode> KeyCodes;

        private Action _callBack;

        private bool _isCurBtn;

        private SettingsConfig.SingleInputSettingItemConfig _config;
        public bool IsOnListening { get; set; }

        public static void Creat()
        {
            if (Instance == null)
            {
                Instance = new KeyBoardListenerManager();
            }
        }

        public void TurnOnKeyBoardInputListening(SettingsConfig.SingleInputSettingItemConfig config, bool isCurBtn,
            Action callback = null)
        {
            _isCurBtn = isCurBtn;
            _callBack = callback;
            IsOnListening = true;
            _config = config;
        }

        public void TurnOffKeyBoardInputListening(SettingsConfig.SingleInputSettingItemConfig config, bool isCurBtn,
            Action callback = null)
        {
            IsOnListening = false;
        }

        public void ListenKeyBoardInput(Action callback = null)
        {
            foreach (var key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown((KeyCode)key))
                {
                    TurnOffKeyBoardInputListening(_config, _isCurBtn);
                    MessageCenter.Dispatch<SettingsConfig.SingleInputSettingItemConfig, string, bool>(MessageCmd
                        .ChangeInputSettingSuccess, _config, key.ToString(), _isCurBtn);
                }
            }
        }
    }
};
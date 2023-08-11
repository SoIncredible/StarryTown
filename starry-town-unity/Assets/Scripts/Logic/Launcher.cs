using System;
using Config;
using Data;
using Message;
using Settings;
using UI.Core;
using UnityEngine;

namespace Logic
{
    public class Launcher : MonoBehaviour
    {
        private void Start()
        {
            MessageCenter.Add(MessageCmd.OnUIManagerFinishCreate,
                delegate { UIManager.Instance.OpenPage(UIType.MainMenu); });


            ConfigManager.Create();


            // 每一个Manager创建成功之后才会创建下一个manager
            InputSettingsManager.Creat();


            DataManager.Create();


            UIManager.Creat(gameObject, delegate { MessageCenter.Dispatch(MessageCmd.OnUIManagerFinishCreate); });
        }
    }
}
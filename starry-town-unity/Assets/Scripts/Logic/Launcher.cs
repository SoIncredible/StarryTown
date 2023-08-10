using System;
using Config;
using Data;
using Message;
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


            DataManager.Create();


            UIManager.Creat(gameObject, delegate { MessageCenter.Dispatch(MessageCmd.OnUIManagerFinishCreate); });
        }
    }
}
using System;
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
            UIManager.Creat(gameObject, delegate { MessageCenter.Dispatch(MessageCmd.OnUIManagerFinishCreate); });
        }
    }
}
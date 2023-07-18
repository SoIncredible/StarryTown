using System;
using UnityEngine;

namespace UI.Core
{
    public class BasePage : MonoBehaviour
    {
        public float InDuration { get; protected set; } = 0.3f;

        public float OutDuration { get; protected set; } = 0.2f;
        
        public UIType UIType { get; private set; }
        
        public BaseUI BaseUI { get; private set; }
        
        public PopupType Popup { get; protected set; }
        
        public PopupType Disappear { get; protected set; }
        
        public bool IsOpening { get; protected set; }
        
        public bool IsShowAnim { get; protected set; }
        
        public bool IsRemove { get; protected set; }

        protected AbstractUIManager _abstractUIManager;
        protected Action _closeCallback;

        
        

    }
}
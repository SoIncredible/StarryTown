using System;
using System.ComponentModel;
using UnityEngine;

namespace UI.Core
{
    public class BasePage : MonoBehaviour
    {
        // public float InDuration { get; protected set; } = 0.3f;
        //
        // public float OutDuration { get; protected set; } = 0.2f;
        //
        // public UIType UIType { get; private set; }
        //
        // public BaseUI BaseUI { get; private set; }
        //
        // public PopupType Popup { get; protected set; }
        //
        // public PopupType Disappear { get; protected set; }
        //
        // public bool IsOpening { get; protected set; }
        //
        // public bool IsShowAnim { get; protected set; }
        //
        // public bool IsRemove { get; protected set; }
        //
        // protected AbstractUIManager _abstractUIManager;
        // protected Action _closeCallback;


        public bool IsOpening { get; protected set; }
        protected BaseUI BaseUI { get; private set; }

        public void OnCreate(UIType uiType)
        {
            BaseUI = GetComponent<BaseUI>();

            Init();
        }


        // 页面被创建到场景中之后会一直存在，不会被销毁
        protected virtual void OnTerminate()
        {
        }

        protected virtual void Init()
        {
        }


        public virtual void OnOpen(params object[] args)
        {
            IsOpening = true;
            try
            {
                Prepare(args);
                AddEvent();
            }
            catch (Exception e)
            {
                Debug.LogError($"页面没有被正常打开！Error:{e}");
                OnClose(null);
                throw;
            }
        }

        public void OnClose(Action callback)
        {
            IsOpening = false;
            RemoveEvent();
        }


        protected virtual void Prepare(params object[] args)
        {
        }

        protected virtual void Clear()
        {
        }

        protected virtual void AddEvent()
        {
        }

        protected virtual void RemoveEvent()
        {
        }
    }
}
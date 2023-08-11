using Config;
using DefaultNamespace;
using Listener;
using Message;
using Settings;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Item
{
    public class SingleInputSettingItem : BaseItem
    {
        [SerializeField] private Text inputCommandText;
        [SerializeField] private Button bindButton;
        [SerializeField] private Text bindButtonText;
        [SerializeField] private Button alternateBindButton;
        [SerializeField] private Text alternateBindButtonText;


        public override void OnCreate(params object[] args)
        {
            if (args.Length == 3)
            {
                inputCommandText.text = args[0] as string;
                bindButtonText.text = args[1] as string;
                alternateBindButtonText.text = args[2] as string;
            }
            else
            {
                Debug.LogError("传入的参数不正确！");
            }
        }

        public override void AddEvent()
        {
            bindButton.onClick.AddListener(OnChangeBindButton);
            alternateBindButton.onClick.AddListener(OnChangeAlternateBindButton);
        }

        public override void RemoveEvent()
        {
            bindButton.onClick.RemoveAllListeners();
            alternateBindButton.onClick.RemoveAllListeners();
        }


        private void OnChangeBindButton()
        {
            // MessageCenter.Dispatch(MessageCmd.OnGameEnd);

            // 此处应该不是Text，而是一个Input
            // 如果SettingDic中已经有一个Key了，那么就要提醒

            // 真实的场景，设置被点击，设置按钮会被清空
            bindButtonText.text = "";


            // 有打开就要有关闭
            KeyBoardListenerManager.Instance.TurnOnKeyBoardInputListening();

            // 需要及时保存修改的按键设置
        }

        private void OnChangeAlternateBindButton()
        {
            alternateBindButtonText.text = "";
        }

        // 修改成功之后需要refresh所有的setting item
    }
}
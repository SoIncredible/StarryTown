using Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Item
{
    public class SingleInputSettingItem : BaseItem
    {
        // 此处要做成一个无限循环列表
        [SerializeField] private Text inputCommandText;

        [SerializeField] private Button bindButton;
        [SerializeField] private Text bindButtonText;

        [SerializeField] private Button alternateBinButton;
        [SerializeField] private Text alternateBindButtonText;

        // 备用按键


        // public void OnCreate(string actionText, string bindButtonText)
        // {
        //     // 读取Config中的数据
        //     // 数据来源有两部分
        //     // 1.系统默认的按键设置
        //     // 2.玩家自定义后的按键设置
        //     ActionText.text = actionText;
        //     BindButtonText.text = bindButtonText;
        // }
        public override void OnCreate(params object[] args)
        {
            if (args.Length == 4)
            {
                inputCommandText.text = args[0] as string;
                bindButtonText.text = args[1] as string;
            }
        }

        public override void AddEvent()
        {
            bindButton.onClick.AddListener(OnChangeBindButton);
            alternateBinButton.onClick.AddListener(OnChangeAlternateBindButton);
        }

        public override void RemoveEvent()
        {
            bindButton.onClick.RemoveAllListeners();
            alternateBinButton.onClick.RemoveAllListeners();
        }


        private void OnChangeBindButton()
        {
            MessageCenter.Dispatch(MessageCmd.OnGameEnd);
        }

        private void OnChangeAlternateBindButton()
        {
        }
    }
}
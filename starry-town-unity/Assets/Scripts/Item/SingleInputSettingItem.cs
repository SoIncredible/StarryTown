using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class SingleInputSettingItem : BaseItem
    {
        // 此处要做成一个无限循环列表

        // 每一个Item需要
        // public string ActionText;
        // public string BindButtonText;
        [SerializeField] private Button BindButton;

        [SerializeField] private Text ActionText;

        [SerializeField] private Text BindButtonText;

        // 备用按键
        [SerializeField] private Text BinButtonAlternateText;

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
                ActionText.text = args[0] as string;
                BindButtonText.text = args[1] as string;
            }
        }

        public override void AddEvent()
        {
            BindButton.onClick.AddListener(OnChangeBindButton);
        }


        private void OnChangeBindButton()
        {
        }
    }
}
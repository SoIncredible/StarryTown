using UnityEngine.UI;

namespace Item
{
    public class SingleInputSettingItem : BaseItem
    {
        // 此处要做成一个无限循环列表

        // 每一个Item需要
        // public string ActionText;
        // public string BindButtonText;
        public Button BindButton;

        public Text ActionText;
        public Text BindButtonText;


        public void OnCreate(string actionText, string bindButtonText)
        {
            // 读取Config中的数据
            ActionText.text = actionText;
            BindButtonText.text = bindButtonText;
        }
    }
}
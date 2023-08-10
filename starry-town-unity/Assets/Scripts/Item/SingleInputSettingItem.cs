using UnityEngine.UI;

namespace Item
{
    public class SingleInputSettingItem : BaseItem
    {
        // 此处要做成一个无限循环列表

        // 每一个Item需要
        public string ActionText;
        public string BindButtonText;
        public Button BindButton;

        protected override void OnCreate()
        {
            base.OnCreate();
            // 读取Config中的数据
            
            
        }

        protected override void OnDestroy()
        {
        }
    }
}
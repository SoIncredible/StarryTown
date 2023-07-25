using Message;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestSliderPropertyOnSlider : MonoBehaviour
    {
        // 将这个脚本挂在到挂在有Slider组件的节点上
        [SerializeField] private Slider _slider;

        private void Start()
        {
            // MessageCenter.Add(MessageCmd.OnTestBtnClicked, ChangeSliderEnableState);
            // 如果ChangeSliderEnableState()带一个参数为什么不在Add方法中体现出来？

            // TODO:还需再理解Action和Delegate之间的关系
            MessageCenter.Add(MessageCmd.OnTestBtnClicked, ChangeSliderEnableState);
        }

        private void ChangeSliderEnableState()
        {
            // var temp = _slider.enabled;
            // _slider.enabled = !temp;

            var temp = _slider.interactable;
            _slider.interactable = !temp;
        }
    }
}
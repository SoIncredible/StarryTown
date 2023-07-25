using Message;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestSliderProperty : MonoBehaviour
    {
        // 本代码是用来测试Unity UGUI中Slider属性的测试代码
        // 注意变量命名规范，私有的属性成员用"_" + 小驼峰的命名规则
        [SerializeField] private Button _btn;

        private void Start()
        {
            _btn.onClick.AddListener(OnBtnClick);
        }

        private void OnBtnClick()
        {
            MessageCenter.Dispatch(MessageCmd.OnTestBtnClicked);
        }
    }
}
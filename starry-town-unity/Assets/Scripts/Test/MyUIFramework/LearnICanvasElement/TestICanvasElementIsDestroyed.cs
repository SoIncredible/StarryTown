using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Test.MyUIFramework.LearnICanvasElement
{
    public class TestICanvasElementIsDestroyed : MonoBehaviour
    {
        [SerializeField] private Button _changeStateBtn;
        [SerializeField] private Image _image;
        [SerializeField] private Button _changeImageBtn;

        [SerializeField] private MySliderPro _mySliderPro;

        private void Start()
        {
            _changeStateBtn.onClick.AddListener(ChangeImageState);
            _changeImageBtn.onClick.AddListener(ChangeImage);
        }

        private void ChangeImageState()
        {
            // 本方法用来测试 当将Image物体隐藏掉之后 是否会调用ICanvasElement中的IsDestroyed方法
            // var temp = _image.enabled;
            // _image.enabled = !temp;
            // Destroy(_image.gameObject);

            // 现用该方法查看 mySliderPro 的 IsDestroy 方法

            // 破案了
            // Selectable 继承子UIBehaviour 在该类中已经实现了该方法
            // 明天需要调研一下 一个类中只是实现了一个接口中的一个方法，如何判断这个方法实现的是这接口中的方法呢
        }

        private void ChangeImage()
        {
            if (_image.IsDestroyed())
            {
                Debug.LogError("出错了！图片都不存在了！");
            }
            else
            {
                var temp = _image.raycastTarget;
                _image.raycastTarget = !temp;
            }
        }
    }
}
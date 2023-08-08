using UnityEngine;
using UnityEngine.UI;

namespace UI.MyUIFramework.LearnICanvasElement
{
    public class TestICanvasElementIsDestroyed : MonoBehaviour
    {
        [SerializeField] private Button _changeStateBtn;
        [SerializeField] private Image _image;
        [SerializeField] private Button _changeImageBtn;

        private Animation _animation;

        private Text _text;

        private AudioClip _audioSource;

        private Canvas _canvas;

        private ScriptableObject _scriptableObject;

        [SerializeField] private MySliderPro _mySliderPro;

        private void Start()
        {
            // Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
            //
            // Like the Awake function, Start is called exactly once in the lifetime of the script. However, Awake is called when the script object is initialised, regardless of whether or not the script is enabled. Start may not be called on the same frame as Awake if the script is not enabled at initialisation time.
            //
            // The Awake function is called on all objects in the Scene before any object's Start function is called. This fact is useful in cases where object A's initialisation code needs to rely on object B's already being initialised; B's initialisation should be done in Awake, while A's should be done in Start.
            //
            // Where objects are instantiated during gameplay, their Awake function is called after the Start functions of Scene objects have already completed.
            //
            // The Start function can be defined as a Coroutine, which allows Start to suspend its execution (yield).


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
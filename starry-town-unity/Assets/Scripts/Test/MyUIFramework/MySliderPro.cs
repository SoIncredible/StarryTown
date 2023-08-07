using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Test.MyUIFramework
{
    public class MySliderPro : Selectable, ICanvasElement, IDragHandler, IEventSystemHandler,
        IInitializePotentialDragHandler
    {
        // ICanvasElement 中需要实现的方法

        public void Rebuild(CanvasUpdate executing)
        {
            // 重建
        }


        // CanvasUpdate: 一个枚举类型


        public void LayoutComplete()
        {
        }

        public void GraphicUpdateComplete()
        {
        }

        // 为什么 IsDestroyed 方法不需要实现？
        // 因为在Selectable继承的 UIBehaviour类中已经实现的该方法


        // IDragHandler 中要实现的方法
        public void OnDrag(PointerEventData eventData)
        {
        }


        // IEventSystemHandler 中需要实现的方法
        // 空接口，可以作为基接口被其他接口继承，形成继承体系

        // IInitializePotentialDragHandler 中需要实现的方法
        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
        }

        // 我可以通过挂载该脚本的方式然后调用该类的 IsDestroy 方法，通过打断点的方式查看被调用
    }
}
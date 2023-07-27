using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Test.InterfaceTest
{
    public class SliderInterface : Selectable, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
    {
        // 该脚本用以理解和学习Slider实现的接口和继承的类


        // PointerEventData是什么？
        // 它继承自什么？


        public void OnDrag(PointerEventData eventData)
        {
            // 此方法是IDragHandler要求实现的方法
            // OnDrag是什么时候调用的？ 为什么会被调用？
            // 它可以接收OnDrag的回调
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            // 此方法是IInitializePotentialDragHandler要求实现的方法
            // 它可以接收OnInitializePotentialDrag的回调
        }

        public void Rebuild(CanvasUpdate executing)
        {
        }

        public void LayoutComplete()
        {
        }

        public void GraphicUpdateComplete()
        {
        }
    }
}
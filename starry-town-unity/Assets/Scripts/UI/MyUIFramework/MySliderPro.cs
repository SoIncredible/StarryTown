using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MyUIFramework
{
    public class MySliderPro : Selectable, ICanvasElement, IDragHandler, IEventSystemHandler,
        IInitializePotentialDragHandler
    {
        // 表示Slider的滑动方向
        public enum Direction
        {
            LeftToRight,

            RightToLeft,

            TopToBottom,

            BottomToTop
        }

        // Slider 首先可以控制三个元素（不一定是图片）的显示 handle fill background

        private RectTransform m_FillRect;

        public RectTransform FillRect
        {
            get => m_FillRect;
            set => m_FillRect = value;
        }


        //
        private RectTransform m_HandleRect;

        public RectTransform HandleRect
        {
            get => m_HandleRect;
            set => m_HandleRect = value;
        }

        //
        private RectTransform m_Background;

        // 实现可以给Slider设置方向的功能

        public void SetDirection()
        {
        }

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
    }
}
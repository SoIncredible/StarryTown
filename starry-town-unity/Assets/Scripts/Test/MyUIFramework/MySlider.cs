using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Test.MyUIFramework
{
    [AddComponentMenu("UI/MySlider", 40)]
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class MySlider : Selectable, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
    {
        // 该脚本用以理解和学习Slider实现的接口和继承的类


        // Slider的滑动方向
        public enum Direction
        {
            LeftToRight,

            RightToLeft,

            BottomToTop,

            TopToBottom,
        }

        [Serializable]
        public class MySliderEvent : UnityEvent<float>
        {
        }

        [SerializeField] private RectTransform m_FillRect;

        public RectTransform fillRect
        {
            get { return m_FillRect; }
            set
            {
                if (SetPropertyUtility.SetClass(ref m_FillRect, value))
                {
                    UpdateCachedReferences();
                    UpdateVisuals();
                }
            }
        }


        [SerializeField] private RectTransform m_HandleRect;

        public RectTransform handleRect
        {
            get { return m_HandleRect; }
            set
            {
                if (SetPropertyUtility.SetClass(ref m_HandleRect, value))
                {
                    UpdateCachedReferences();
                    UpdateVisuals();
                }
            }
        }

        [Space] [SerializeField] private Direction m_Direction = Direction.LeftToRight;

        public Direction direction
        {
            get { return m_Direction; }
            set
            {
                if (SetPropertyUtility.SetStruct(ref m_Direction, value))
                {
                    UpdateVisuals();
                }
            }
        }

        [SerializeField] private float m_MinValue = 0;

        public float minValue
        {
            get { return m_MinValue; }
            set
            {
                if (SetPropertyUtility.SetStruct(ref m_MinValue, value))
                {
                    Set(m_Value);
                    UpdateVisuals();
                }
            }
        }

        [SerializeField] private float m_MaxValue = 1;

        public float maxValue
        {
            get { return m_MaxValue; }
            set
            {
                if (SetPropertyUtility.SetStruct(ref m_MaxValue, value))
                {
                    Set(m_Value);
                    UpdateVisuals();
                }
            }
        }

        [SerializeField] private bool m_WholeNumbers = false;

        public bool wholeNumbers
        {
            get { return m_WholeNumbers; }
            set
            {
                if (SetPropertyUtility.SetStruct(ref m_WholeNumbers, value))
                {
                    Set(m_Value);
                    UpdateVisuals();
                }
            }
        }

        [SerializeField] protected float m_Value;

        public virtual float value
        {
            get { return wholeNumbers ? Mathf.Round(m_Value) : m_Value; }
            set { Set(value); }
        }

        public virtual void SetValueWithoutNotify(float input)
        {
            Set(input, false);
        }

        public float normalizedValue
        {
            get
            {
                if (Mathf.Approximately(minValue, maxValue)) return 0;
                return Mathf.InverseLerp(minValue, maxValue, value);
            }
            set { this.value = Mathf.Lerp(minValue, maxValue, value); }
        }

        // 事件由我们自己管理，因此在我们自己的代码实现中不需要这个字段成员
        [Space] [SerializeField] private MySliderEvent m_OnValueChanged = new MySliderEvent();

        public MySliderEvent onValueChanged
        {
            get { return m_OnValueChanged; }
            set { m_OnValueChanged = value; }
        }

        private Image m_FillImage;
        private Transform m_FillTransform;
        private RectTransform m_FillContainerRect;
        private Transform m_HandleTransform;
        private RectTransform m_HandleContainerRect;

        private Vector2 m_Offset = Vector2.zero;

#pragma warning  disable 649
        private DrivenRectTransformTracker m_Tracker;
#pragma warning restore 649


        private bool m_DealedUpdateVisuals = false;

        float stepSize
        {
            get { return wholeNumbers ? 1 : (maxValue - minValue) * 0.1f; }
        }

        // 构造函数
        protected MySlider()
        {
        }


#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (wholeNumbers)
            {
                m_MinValue = Mathf.Round(m_MinValue);
                m_MaxValue = Mathf.Round(m_MaxValue);
            }

            if (IsActive())
            {
                UpdateCachedReferences();
                m_DealedUpdateVisuals = true;
            }

            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
            {
                CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
            }
        }
#endif


        public virtual void Rebuild(CanvasUpdate executing)
        {
            // 此方法实现了了CanvasElement接口中的Rebuild方法
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
            {
                onValueChanged.Invoke(value);
            }
#endif
        }

        public virtual void LayoutComplete()
        {
        }

        public virtual void GraphicUpdateComplete()
        {
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateCachedReferences();
            Set(m_Value, false);
            UpdateVisuals();
        }

        protected override void OnDisable()
        {
            m_Tracker.Clear();
            base.OnDisable();
        }

        protected void Update()
        {
            if (m_DealedUpdateVisuals)
            {
                m_DealedUpdateVisuals = false;
                Set(m_Value, false);
                UpdateVisuals();
            }
        }

        protected override void OnDidApplyAnimationProperties()
        {
            m_Value = ClampValue(m_Value);
            float oldNormalizedValue = normalizedValue;
            if (m_FillContainerRect != null)
            {
                if (m_FillImage != null && m_FillImage.type == Image.Type.Filled)
                {
                    oldNormalizedValue = m_FillImage.fillAmount;
                }
                else
                {
                    oldNormalizedValue =
                        (reverseValue ? 1 - m_FillRect.anchorMin[(int)axis] : m_FillRect.anchorMax[(int)axis]);
                }
            }
            else if (m_HandleContainerRect != null)
            {
                oldNormalizedValue =
                    (reverseValue ? 1 - m_HandleRect.anchorMin[(int)axis] : m_FillRect.anchorMin[(int)axis]);
            }

            UpdateVisuals();

            if (oldNormalizedValue != normalizedValue)
            {
                UISystemProfilerApi.AddMarker("MySlider.value", this);
                onValueChanged.Invoke(m_Value);
            }
        }

        void UpdateCachedReferences()
        {
            if (m_FillRect && m_FillRect != (RectTransform)transform)
            {
                m_FillTransform = m_FillRect.transform;
                m_FillImage = m_FillRect.GetComponent<Image>();
                if (m_FillTransform.parent != null)
                {
                    m_FillContainerRect = m_FillTransform.parent.GetComponent<RectTransform>();
                }
                else
                {
                    m_FillRect = null;
                    m_FillContainerRect = null;
                    m_FillImage = null;
                }
            }

            if (m_HandleRect && m_HandleRect != (RectTransform)transform)
            {
                m_HandleTransform = m_HandleRect.transform;
                if (m_HandleTransform.parent != null)
                {
                    m_HandleContainerRect = m_HandleTransform.parent.GetComponent<RectTransform>();
                }
                else
                {
                    m_HandleRect = null;
                    m_HandleContainerRect = null;
                }
            }
        }

        float ClampValue(float input)
        {
            float newValue = Mathf.Clamp(input, minValue, maxValue);
            if (wholeNumbers)
            {
                newValue = Mathf.Round(newValue);
            }

            return newValue;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();

            if (!IsActive())
            {
                return;
            }

            UpdateVisuals();
        }


        enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        Axis axis
        {
            get
            {
                return (m_Direction == Direction.LeftToRight || m_Direction == Direction.RightToLeft)
                    ? Axis.Horizontal
                    : Axis.Vertical;
            }
        }

        bool reverseValue
        {
            get { return m_Direction == Direction.RightToLeft || m_Direction == Direction.TopToBottom; }
        }

        // PointerEventData是什么？
        // 它继承自什么？
        public void OnDrag(PointerEventData eventData)
        {
            // 此方法是IDragHandler要求实现的方法
            // OnDrag是什么时候调用的？ 为什么会被调用？
            // 它可以接收OnDrag的回调

            if (!MayDrag(eventData))
            {
                return;
            }

            UpdateDrag(eventData, eventData.pressEventCamera);
        }

        public virtual void OnMove(AxisEventData eventData)
        {
            if (!IsActive() || !IsInteractable())
            {
                base.OnMove(eventData);
                return;
            }

            switch (eventData.moveDir)
            {
                case MoveDirection.Left:
                    if (axis == Axis.Horizontal && FindSelectableOnLeft() == null)
                        Set(reverseValue ? value + stepSize : value - stepSize);
                    else
                        base.OnMove(eventData);
                    break;
                case MoveDirection.Right:
                    if (axis == Axis.Horizontal && FindSelectableOnRight() == null)
                        Set(reverseValue ? value - stepSize : value + stepSize);
                    else
                        base.OnMove(eventData);
                    break;
                case MoveDirection.Up:
                    if (axis == Axis.Vertical && FindSelectableOnUp() == null)
                        Set(reverseValue ? value - stepSize : value + stepSize);
                    else
                        base.OnMove(eventData);
                    break;
                case MoveDirection.Down:
                    if (axis == Axis.Vertical && FindSelectableOnDown() == null)
                        Set(reverseValue ? value + stepSize : value - stepSize);
                    else
                        base.OnMove(eventData);
                    break;
            }
        }


        public override Selectable FindSelectableOnLeft()
        {
            if (navigation.mode == Navigation.Mode.Automatic && axis == Axis.Horizontal) return null;
            return base.FindSelectableOnLeft();
        }

        public override Selectable FindSelectableOnRight()
        {
            if (navigation.mode == Navigation.Mode.Automatic && axis == Axis.Horizontal) return null;
            return base.FindSelectableOnRight();
        }

        public override Selectable FindSelectableOnUp()
        {
            if (navigation.mode == Navigation.Mode.Automatic && axis == Axis.Vertical)
                return null;
            return base.FindSelectableOnUp();
        }

        public override Selectable FindSelectableOnDown()
        {
            if (navigation.mode == Navigation.Mode.Automatic && axis == Axis.Vertical) return null;
            return base.FindSelectableOnDown();
        }


        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            // 此方法是IInitializePotentialDragHandler要求实现的方法
            // 它可以接收OnInitializePotentialDrag的回调
            eventData.useDragThreshold = false;
        }


        public void SetDirection(Direction direction, bool includeRectLayouts)
        {
            Axis oldAxis = axis;
            bool oldReverse = reverseValue;
            this.direction = direction;

            if (!includeRectLayouts) return;

            if (axis != oldAxis) RectTransformUtility.FlipLayoutAxes(transform as RectTransform, true, true);

            if (reverseValue != oldReverse)
                RectTransformUtility.FlipLayoutOnAxis(transform as RectTransform, (int)axis, true, true);
        }

        private void UpdateVisuals()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                UpdateCachedReferences();
            }
#endif
            m_Tracker.Clear();

            if (m_FillContainerRect != null)
            {
                m_Tracker.Add(this, m_FillRect, DrivenTransformProperties.Anchors);
                Vector2 anchorMin = Vector2.zero;
                Vector2 anchorMax = Vector2.one;

                if (m_FillImage != null && m_FillImage.type == Image.Type.Filled)
                {
                    m_FillImage.fillAmount = normalizedValue;
                }
                else
                {
                    if (reverseValue)
                    {
                        anchorMin[(int)axis] = 1 - normalizedValue;
                    }
                    else
                    {
                        anchorMax[(int)axis] = normalizedValue;
                    }
                }

                m_FillRect.anchorMin = anchorMin;
                m_FillRect.anchorMax = anchorMax;
            }

            if (m_HandleContainerRect != null)
            {
                m_Tracker.Add(this, m_HandleRect, DrivenTransformProperties.Anchors);
                Vector2 anchorMin = Vector2.zero;
                Vector2 anchorMax = Vector2.one;

                anchorMin[(int)axis] = anchorMax[(int)axis] = (reverseValue ? (1 - normalizedValue) : normalizedValue);

                m_HandleRect.anchorMin = anchorMin;
                m_HandleRect.anchorMax = anchorMax;
            }
        }

        void UpdateDrag(PointerEventData eventData, Camera cam)
        {
            RectTransform clickRect = m_HandleContainerRect ?? m_FillContainerRect;
            if (clickRect != null && clickRect.rect.size[(int)axis] > 0)
            {
                Vector2 position = Vector2.zero;
                if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position)) return;
                Vector2 localCursor;
                if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(clickRect, position, cam, out localCursor))
                    return;

                localCursor -= clickRect.rect.position;

                float val = Mathf.Clamp01((localCursor - m_Offset)[(int)axis] / clickRect.rect.size[(int)axis]);
                normalizedValue = (reverseValue ? 1f - val : val);
            }
        }


        private bool MayDrag(PointerEventData eventData)
        {
            return IsActive() && IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!MayDrag(eventData)) return;

            base.OnPointerDown(eventData);

            m_Offset = Vector2.zero;

            if (m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(m_HandleRect,
                    eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera))
            {
                Vector2 localMousePos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_HandleRect,
                        eventData.pointerPressRaycast.screenPosition,
                        eventData.pressEventCamera, out localMousePos))
                {
                    m_Offset = localMousePos;
                }
                else
                {
                    UpdateDrag(eventData, eventData.pressEventCamera);
                }
            }
        }


        protected virtual void Set(float input, bool sendCallback = true)
        {
            float newValue = ClampValue(input);

            if (m_Value == newValue)
            {
                return;
            }

            m_Value = newValue;

            UpdateVisuals();

            if (sendCallback)
            {
                // 此处需要留意！！！！！！！！！
                UISystemProfilerApi.AddMarker("MySlider.value", this);
                m_OnValueChanged.Invoke(newValue);
            }
        }
    }
}
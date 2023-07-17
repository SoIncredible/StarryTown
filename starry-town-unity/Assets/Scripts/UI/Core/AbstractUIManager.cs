using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Core
{
    public class AbstractUIManager : MonoBehaviour
    {
        // 根Canvas
        public Canvas UICanvas { get; private set; }

        // 根UIScaler缩放，为了适配
        public CanvasScaler UIScaler { get; private set; }

        // 根节点的RectTransform
        public RectTransform Root { get; private set; }

        // 事件系统
        public EventSystem EventSystem { get; private set; }

        protected void InitInternal(Canvas canvas)
        {
            UICanvas = canvas;
            UIScaler = canvas.GetComponent<CanvasScaler>();
            Root = canvas.GetComponent<RectTransform>();
            EventSystem = EventSystem.current;
        }

        protected void ReleaseInternal()
        {
        }
    }
}
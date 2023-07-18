using System.Collections.Generic;
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
            CloseAll();
            ClearTransform();
            _pages.Clear();
            _pagesList.Clear();

            foreach (var layer in _uiLayers.Values)
            {
                Destroy(layer.gameObject);
            }

            _uiLayers.Clear();
        }

        public virtual void SetInputActive(bool flag)
        {
            EventSystem.enabled = flag;
        }



        protected readonly Dictionary<int, UIInfo> _infos = new Dictionary<int, UIInfo>(128);

        protected void AddInfo(UIInfo info)
        {
            if(_infos.ContainsKey((int)info.UIType))
            {
                
            }
        }





        private readonly Dictionary<int, BasePage> _pages = new Dictionary<int, BasePage>(128);
        private readonly List<BasePage> _pageList = new List<BasePage>(128);
        private readonly Queue<BasePage> _pageQueues = new Queue<BasePage>();
    }
}
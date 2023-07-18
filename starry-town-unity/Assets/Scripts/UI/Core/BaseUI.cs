using System.Collections.Concurrent;
using UI.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Core
{
    [RequireComponent(typeof(Canvas)), RequireComponent(typeof(GraphicRaycaster))]
    public class BaseUI : MonoBehaviour
    {
        public Canvas Canvas;
        public GraphicRaycaster Raycaster;
        public RectTransform Content;
        public CanvasGroup ContentCanvasGroup;
        public Image BlackImage;
        public UIRaycast Raycast;


        public virtual void Create()
        {
            Canvas = GetComponent<Canvas>();
            Raycaster = GetComponent<GraphicRaycaster>();
            var content = transform.Find("Content");
            if (content != null)
            {
                Content = content.GetComponent<RectTransform>();
                ContentCanvasGroup = content.GetComponent<CanvasGroup>();
            }

            BlackImage = GetComponent<Image>();

            var raycastTrans = transform.Find("Raycast");

            if (raycastTrans != null)
            {
                Raycast = raycastTrans.GetComponent<UIRaycast>();
            }
        }
        
    }
    
}
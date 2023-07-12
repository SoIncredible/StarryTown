using Mono.CompilerServices.SymbolWriter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class AbstractUIManager : MonoBehaviour
    {
        public Canvas UICanvas { get; private set; }
        
        public CanvasScaler UIScaler { get; private set; }
        
        public RectTransform Root { get; private set; }

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
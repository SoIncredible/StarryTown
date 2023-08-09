using System.Collections.Concurrent;
using UI.Extension;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Core
{
    [RequireComponent(typeof(Canvas)), RequireComponent(typeof(GraphicRaycaster))]
    public class BaseUI : MonoBehaviour
    {
        public virtual void Create()
        {
        }
    }
}
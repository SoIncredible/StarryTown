using UnityEngine;

namespace Item
{
    public abstract class BaseItem : MonoBehaviour
    {
        protected RectTransform Parent;

        public abstract void OnEnter(params object[] args);

        public virtual void AddEvent()
        {
        }

        public virtual void RemoveEvent()
        {
        }
    }
}
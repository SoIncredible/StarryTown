using Unity.VisualScripting.Dependencies.NCalc;
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

        public void Function()
        {
            // 此方法为测试如何撤回Git修改的测试
        }
    }
}
using UnityEngine;

namespace Item
{
    public class BaseItem
    {
        protected RectTransform Parent;


        protected virtual void OnCreate()
        {
        }

        protected virtual void OnDestroy()
        {
        }
    }
}
using System;
using UnityEngine;

namespace Test
{
    public class LearnLayerMask : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int layer = 10;


        private int layerAsLayerMask;

        /**
         * 将单个Layer转变为LayerMask
         * 使用左移运算符
         */
        /**
         * 将一个Layer添加到一个LayerMask中
         * 使用逻辑或运算符
         */
        /**
         * 在LayerMask中删除某一个Layer
         * 
         */
        private void Start()
        {
            layerAsLayerMask = (1 << layer);
        }


        private void Update()
        {
            // 将该脚本挂载在一个面向摄像机的物体上，Ray cast是从摄像机发射出来的
            int layer = 1 << 8;
            if (Physics.Raycast(transform.position, Vector3.forward, Mathf.Infinity, this.layer))
            {
                Debug.Log("The ray hit the player");
            }
        }
    }
}
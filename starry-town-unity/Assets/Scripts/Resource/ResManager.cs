using System.IO;
using UnityEngine;

namespace Resource
{
    // ResManager是一个静态类
    // TODO: 梳理什么情况下Manager可以用静态 什么时候需要用动态
    public static class ResManager
    {
        public static GameObject Load(string path, Transform parent = null)
        {
            // TODO: 将加载方法改为范型

            var asset = Resources.Load(path);
            if (asset == null)
            {
                Debug.LogError($"无法加下如下路径的资源！{path}");
                return null;
            }

            var go = Object.Instantiate(asset, parent);
            return go as GameObject;
        }
    }
}
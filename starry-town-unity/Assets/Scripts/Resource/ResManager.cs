using System.IO;
using UnityEngine;

namespace Resource
{
    public static class ResManager
    {
        public static GameObject Load(string path, Transform parent = null)
        {
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
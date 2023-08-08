using System.IO;
using UnityEngine;

namespace Resource
{
    public static class ResManager
    {
        public static T Load<T>(string path, Transform parent = null) where T : Object
        {
            var type = typeof(T);
            var asset = Resources.Load<T>(path);
            if (asset == null)
            {
                Debug.LogError($"无法加下如下路径的资源！{path}");
                return null;
            }

            if (type != typeof(GameObject))
            {
                return asset;
            }

            var go = Object.Instantiate(asset, parent);
            return go;
        }
    }
}
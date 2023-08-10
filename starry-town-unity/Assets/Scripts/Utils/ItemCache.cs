using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class ItemCache<T> where T : Component
    {
        // 该缓存池创建的是物体

        private readonly Stack<T> _cache;

        private readonly Func<Transform, GameObject> _loadRes;

        private readonly GameObject _itemGameObject;

        private readonly Transform _parentTransform;

        private readonly int _capacity;

        public ItemCache(Func<Transform, GameObject> loadRes, Transform parentTransform, int capacity)
        {
            _loadRes = loadRes;
            _parentTransform = parentTransform;
            _capacity = capacity;
            _cache = new Stack<T>(_capacity);
        }


        public ItemCache(GameObject itemGameObject, Transform parentTransform, int capacity)
        {
            _itemGameObject = itemGameObject;
            _parentTransform = parentTransform;
            _capacity = capacity;
            _cache = new Stack<T>(_capacity);
        }


        // 创建Item
        private T Create()
        {
            var gameObject = _loadRes != null
                ? _loadRes.Invoke(_parentTransform)
                : Object.Instantiate(_itemGameObject, _parentTransform);
            gameObject.SetActive(true);
            return gameObject.GetComponent<T>();
        }


        // 将Item从池子拿出来
        public T Pop()
        {
            if (_cache.Count > 0)
            {
                var item = _cache.Pop();
                item.gameObject.SetActive(true);
                return item;
            }

            return Create();
        }


        // 将Item还回池子
        public void Push(T item)
        {
            _cache.Push(item);
            item.gameObject.SetActive(false);
        }

        public void Preload()
        {
            // 预加载
            for (int i = 0; i < _capacity; i++)
            {
                var item = Create();
                Push(item);
            }
        }
    }
}
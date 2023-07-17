using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Message
{
    /*
    public struct MessageCmdComparer : IEqualityComparer<MessageCmd>{
        public bool Equals(MessageCmd x, MessageCmd y)
        {
            return x == y;
        }

        public int GetHashCode(MessageCmd obj)
        {
            return (int)obj;
        }
    }
    */

    // 事件中心负责游戏中所有的跨场景、跨页面、跨代码的通信
    // 是基于观察者模式编写的

    public static class MessageCenter
    {
        private static readonly Dictionary<MessageCmd, Delegate> MessageDict =
            new Dictionary<MessageCmd, Delegate>();

        private static void Add(MessageCmd messageCmd, Delegate handle)
        {
            if (MessageDict.TryGetValue(messageCmd, out var d))
            {
                d = Delegate.Combine(d, handle);
            }
            else
            {
                MessageDict.Add(messageCmd, handle);
            }
        }

        public static void Add(MessageCmd messageCmd, Action action)
        {
            Add(messageCmd, action);
        }

        public static void Add<T>(MessageCmd messageCmd, Action<T> action)
        {
            Add(messageCmd, action);
        }

        public static void Add<T, U>(MessageCmd messageCmd, Action<T, U> action)
        {
            Add(messageCmd, action);
        }


        public static void Add<T, U, V>(MessageCmd messageCmd, Action<T, U, V> action)
        {
            Add(messageCmd, action);
        }

        public static void Add<T, U, V, P>(MessageCmd messageCmd, Action<T, U, V, P> action)
        {
            Add(messageCmd, action);
        }


        public static void Remove(MessageCmd messageCmd, Delegate handle)
        {
            if (!MessageDict.TryGetValue(messageCmd, out var d)) return;
            d = Delegate.Remove(d, handle);
            MessageDict[messageCmd] = d;
        }

        public static void Remove(MessageCmd messageCmd, Action action)
        {
            Remove(messageCmd, action);
        }

        public static void Remove<T>(MessageCmd messageCmd, Action<T> action)
        {
            Remove(messageCmd, action);
        }

        public static void Remove<T, U>(MessageCmd messageCmd, Action<T, U> action)
        {
            Remove(messageCmd, action);
        }

        public static void Remove<T, U, V>(MessageCmd messageCmd, Action<T, U, V> action)
        {
            Remove(messageCmd, action);
        }

        public static void Remove<T, U, V, P>(MessageCmd messageCmd, Action<T, U, V, P> action)
        {
            Remove(messageCmd, action);
        }


        public static void Dispatch(MessageCmd messageCmd)
        {
            if (!MessageDict.TryGetValue(messageCmd, out var d)) return;
            var action = d as Action;
            action?.Invoke();
        }

        public static void Dispatch<T>(MessageCmd messageCmd, T t)
        {
            if (!MessageDict.TryGetValue(messageCmd, out var d)) return;
            var action = d as Action<T>;
            action?.Invoke(t);
        }

        public static void Dispatch<T, U>(MessageCmd messageCmd, T t, U u)
        {
            if (!MessageDict.TryGetValue(messageCmd, out var d)) return;
            var action = d as Action<T, U>;
            action?.Invoke(t, u);
        }

        public static void Dispatch<T, U, V>(MessageCmd messageCmd, T t, U u, V v)
        {
            if (!MessageDict.TryGetValue(messageCmd, out var d)) return;
            var action = d as Action<T, U, V>;
            action?.Invoke(t, u, v);
        }

        public static void Dispatch<T, U, V, P>(MessageCmd messageCmd, T t, U u, V v, P p)
        {
            if (!MessageDict.TryGetValue(messageCmd, out var d)) return;
            var action = d as Action<T, U, V, P>;
            action?.Invoke(t, u, v, p);
        }

        public static void Clear()
        {
            MessageDict.Clear();
        }
    }
}
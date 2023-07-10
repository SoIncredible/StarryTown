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
        // 知识点
        // 1.方法的重载
        // 2.委托

        private static void Add(MessageCmd messageCmd, Delegate handle)
        {
            if (MessageDict.TryGetValue(messageCmd, out var d))
            {
                d = Delegate.Combine(d, handle);
            }
            else
            {
                MessageDict.Add(messageCmd,handle);
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
            Add(messageCmd,action);
        }


        public static void Add<T, U, V>(MessageCmd messageCmd, Action<T, U, V> action)
        {
            Add(messageCmd,action);
        }

        public static void AddM<T, U, V, P>(MessageCmd messageCmd, Action<T, U, V, P> action)
        {
            Add(messageCmd, action);
        }

        public static void Remove()
        {
        
        }



        public static void DisPatch(MessageCmd messageCmd)
        {
            if(MessageDict.TryGetValue(messageCmd,out var d))
            {
                var action = d as Action;
                action?.Invoke();
            }
        }
    
    }
}


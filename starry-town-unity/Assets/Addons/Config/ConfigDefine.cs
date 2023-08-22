using System;
using System.Collections.Generic;
using Log;

namespace Config
{
    public abstract class IConfigItem
    {
        public int id;
    }


    public interface IConfigData
    {
        void Release();
    }


    public class ConfigData<T> : IConfigData where T : IConfigItem
    {
        public readonly Dictionary<int, T> Data = new Dictionary<int, T>();

        public T GetItem(int id)
        {
            if (Data.TryGetValue(id, out var temp))
            {
                return temp;
            }

            D.Error($"[ConfigData] {typeof(T).Name} item is null, key: {id}");
            return null;
        }

        public void Release()
        {
            Data.Clear();
        }
    }


    /*
     * 格式 id;count
     */
    public class ConfigIdCount
    {
        public readonly int Id;
        public readonly int Count;

        public ConfigIdCount(int id, int count)
        {
            Id = id;
            Count = count;
        }
    }


    /*
     * id;float
     */
    public class ConfigIdFloat
    {
        public readonly int Id;
        public readonly float Value;

        public ConfigIdFloat(int id, float value)
        {
            Id = id;
            Value = value;
        }
    }


    /*
     * id;param1,param2...
     */
    public class ConfigIdParams
    {
        public readonly int Id;
        public readonly int[] Parameters;

        public ConfigIdParams(int id, int[] parameters)
        {
            Id = id;
            Parameters = parameters;
        }
    }


    public class ConfigIdEnum<T> where T : struct, IConvertible
    {
        public readonly int Id;
        public readonly T TypeValue;

        public ConfigIdEnum(int id, T typeValue)
        {
            Id = id;
            TypeValue = typeValue;
        }
    }
}
using System;

namespace Config
{
    public abstract class IConfigLoader
    {
        public abstract bool Next();

        public abstract string GetString(string name);

        public abstract byte GetByte(string name);

        public abstract bool GetBool(string name);

        public abstract short GetShort(string name);

        public abstract int GetInt(string name);

        public abstract long GetLong(string name);

        public abstract float GetFloat(string name);

        public abstract double GetDouble(string name);

        public abstract bool[] GetBoolArray(string name);

        public abstract int[] GetIntArray(string name);

        public abstract float[] GetFloatArray(string name);

        public abstract double[] GetDoubleArray(string name);

        public abstract long[] GetLongArray(string name);

        public abstract string[] GetStringArray(string name);


        //--------------------------------------------------------------------------------
        // 解析 IdCount 和 IdFloat 类型
        //--------------------------------------------------------------------------------

        public abstract ConfigIdCount GetIdCount(string name);

        public abstract ConfigIdCount[] GetIdCountArray(string name);

        public abstract ConfigIdFloat GetIdFloat(string name);

        public abstract ConfigIdFloat[] GetIdFloatArray(string name);


        //--------------------------------------------------------------------------------
        // 解析 IdParams 类型
        //--------------------------------------------------------------------------------

        public abstract ConfigIdParams GetIdParams(string name);

        public abstract ConfigIdParams[] GetIdParamsArray(string name);


        //--------------------------------------------------------------------------------
        // 解析枚举类型
        //--------------------------------------------------------------------------------

        public abstract T GetEnum<T>(string name);

        public abstract ConfigIdEnum<T> GetIdEnum<T>(string name) where T : struct, IConvertible;

        public abstract ConfigIdEnum<T>[] GetIdEnumArray<T>(string name) where T : struct, IConvertible;
    }
}
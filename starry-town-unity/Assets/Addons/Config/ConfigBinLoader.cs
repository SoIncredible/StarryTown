using System;
using Log;

namespace Config
{
    public class ConfigBinLoader : IConfigLoader, IDisposable
    {
        private readonly string _file;
        private readonly ByteArray _buff;

        public ConfigBinLoader(string file, byte[] buff)
        {
            _file = file;
            _buff = new ByteArray(buff);
        }

        public override bool Next()
        {
            return _buff.ReadAvailable;
        }

        public override string GetString(string name)
        {
            return _buff.ReadUTF();
        }

        public override byte GetByte(string name)
        {
            return _buff.ReadByte();
        }

        public override bool GetBool(string name)
        {
            byte v = _buff.ReadByte();
            return v != 0;
        }

        public override short GetShort(string name)
        {
            return _buff.ReadShort();
        }

        public override int GetInt(string name)
        {
            return _buff.ReadInt();
        }

        public override long GetLong(string name)
        {
            return _buff.ReadLong();
        }

        public override float GetFloat(string name)
        {
            return _buff.ReadFloat();
        }

        public override double GetDouble(string name)
        {
            return _buff.ReadDouble();
        }

        public override bool[] GetBoolArray(string name)
        {
            int length = _buff.ReadInt();
            bool[] ret = new bool[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    byte v = _buff.ReadByte();
                    ret[i] = v != 0;
                }
            }

            return ret;
        }

        public override int[] GetIntArray(string name)
        {
            int length = _buff.ReadInt();
            int[] ret = new int[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    ret[i] = _buff.ReadInt();
                }
            }

            return ret;
        }

        public override float[] GetFloatArray(string name)
        {
            int length = _buff.ReadInt();
            float[] ret = new float[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    ret[i] = _buff.ReadFloat();
                }
            }

            return ret;
        }

        public override double[] GetDoubleArray(string name)
        {
            int length = _buff.ReadInt();
            double[] ret = new double[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    ret[i] = _buff.ReadDouble();
                }
            }

            return ret;
        }

        public override long[] GetLongArray(string name)
        {
            int length = _buff.ReadInt();
            long[] ret = new long[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    ret[i] = _buff.ReadLong();
                }
            }

            return ret;
        }

        public override string[] GetStringArray(string name)
        {
            int length = _buff.ReadInt();
            string[] ret = new string[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    ret[i] = _buff.ReadUTF();
                }
            }

            return ret;
        }


        //--------------------------------------------------------------------------------
        // 解析 IdCount 类型
        //--------------------------------------------------------------------------------

        public override ConfigIdCount GetIdCount(string name)
        {
            byte flag = _buff.ReadByte();
            if (flag == 0)
            {
                return null;
            }

            int id = _buff.ReadInt();
            int count = _buff.ReadInt();
            return new ConfigIdCount(id, count);
        }

        public override ConfigIdCount[] GetIdCountArray(string name)
        {
            int length = _buff.ReadInt();
            ConfigIdCount[] ret = new ConfigIdCount[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    ret[i] = GetIdCount("internal");
                }
            }

            return ret;
        }

        public override ConfigIdFloat GetIdFloat(string name)
        {
            byte flag = _buff.ReadByte();
            if (flag == 0)
            {
                return null;
            }

            int id = _buff.ReadInt();
            float value = _buff.ReadFloat();
            return new ConfigIdFloat(id, value);
        }

        public override ConfigIdFloat[] GetIdFloatArray(string name)
        {
            int length = _buff.ReadInt();
            ConfigIdFloat[] ret = new ConfigIdFloat[length];
            if (length > 0)
            {
                for (int i = 0; i < length; ++i)
                {
                    ret[i] = GetIdFloat("internal");
                }
            }

            return ret;
        }


        //--------------------------------------------------------------------------------
        // 解析 IdParams 类型
        //--------------------------------------------------------------------------------

        public override ConfigIdParams GetIdParams(string name)
        {
            string value = _buff.ReadUTF();
            return CreateIdParams(value);
        }

        public override ConfigIdParams[] GetIdParamsArray(string name)
        {
            string value = _buff.ReadUTF();
            if (string.IsNullOrEmpty(value))
            {
                return new ConfigIdParams[0];
            }

            string[] strArr = value.Split('|');

            ConfigIdParams[] arr = new ConfigIdParams[strArr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                ConfigIdParams data = CreateIdParams(strArr[i]);
                arr[i] = data;
            }

            return arr;
        }

        private ConfigIdParams CreateIdParams(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            var contents = value.Split(';');
            if (2 != contents.Length)
                return null;

            int id;
            int.TryParse(contents[0], out id);

            var values = contents[1].Split(',');
            var parameters = new int[values.Length];
            for (var i = 0; i < values.Length; ++i)
            {
                parameters[i] = 0;
                int.TryParse(values[i], out parameters[i]);
            }

            return new ConfigIdParams(id, parameters);
        }


        //--------------------------------------------------------------------------------
        // 解析枚举类型
        //--------------------------------------------------------------------------------

        public override T GetEnum<T>(string name)
        {
            string value = _buff.ReadUTF();
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T) Enum.Parse(typeof(T), value, true);
            }

            if (!string.IsNullOrEmpty(value))
            {
                D.Warn("[ConfigBinLoader] Config enum parse error [{0}:{1}:{2}]", _file, name, value);
            }

            return default(T);
        }

        public override ConfigIdEnum<T> GetIdEnum<T>(string name)
        {
            string value = _buff.ReadUTF();
            return CreateIdEnum<T>(value);
        }

        public override ConfigIdEnum<T>[] GetIdEnumArray<T>(string name)
        {
            string value = _buff.ReadUTF();
            if (string.IsNullOrEmpty(value))
            {
                return new ConfigIdEnum<T>[0];
            }

            string[] strArr = value.Split('|');

            ConfigIdEnum<T>[] arr = new ConfigIdEnum<T>[strArr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                var data = CreateIdEnum<T>(strArr[i]);
                arr[i] = data;
            }

            return arr;
        }

        private ConfigIdEnum<T> CreateIdEnum<T>(string value) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            var tType = typeof(T);
            string[] strArr = value.Split(';');
            if (strArr.Length == 2)
            {
                int id;
                int.TryParse(strArr[0], out id);
                if (Enum.IsDefined(tType, strArr[1]))
                {
                    var e = (T) Enum.Parse(tType, strArr[1], true);
                    return new ConfigIdEnum<T>(id, e);
                }

                D.Error("[ConfigBinLoader] Enum:{0} is not defined! Will use enum:{1} default value:{2}", strArr[1],
                    typeof(T).FullName, default(T));
                return new ConfigIdEnum<T>(id, default(T));
            }

            return null;
        }

        public void Dispose()
        {
            ((IDisposable) _buff).Dispose();
        }
    }
}
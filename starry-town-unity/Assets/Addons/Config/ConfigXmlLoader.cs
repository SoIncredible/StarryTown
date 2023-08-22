using System;
using System.Globalization;
using System.Xml;
using Log;

namespace Config
{
    public class ConfigXmlLoader : IConfigLoader
    {
        private static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("en-US");

        private readonly string _file;
        private readonly XmlNodeList _list;
        private XmlAttributeCollection _attributes;

        private int _count;
        private int _index;

        public ConfigXmlLoader(string file, XmlNodeList list)
        {
            _file = file;
            _list = list;
            _count = list.Count;
            _index = -1;
        }

        public override bool Next()
        {
            ++_index;
            if (_index < _count)
            {
                var node = _list.Item(_index);
                if (node != null)
                {
                    _attributes = node.Attributes;
                    return true;
                }
            }

            return false;
        }

        public override string GetString(string name)
        {
            return _attributes[name].Value;
        }

        public override byte GetByte(string name)
        {
            byte value = 0;
            byte.TryParse(_attributes[name].Value, NumberStyles.Number, Culture, out value);
            return value;
        }

        public override bool GetBool(string name)
        {
            return GetBoolValue(_attributes[name].Value);
        }

        private bool GetBoolValue(string str)
        {
            return str != "0" && !string.Equals(str, "false", StringComparison.CurrentCultureIgnoreCase);
        }

        public override short GetShort(string name)
        {
            short value = 0;
            short.TryParse(_attributes[name].Value, NumberStyles.Number, Culture, out value);
            return value;
        }

        public override int GetInt(string name)
        {
            int value = 0;
            int.TryParse(_attributes[name].Value, NumberStyles.Number, Culture, out value);
            return value;
        }

        public override long GetLong(string name)
        {
            long value = 0;
            long.TryParse(_attributes[name].Value, NumberStyles.Number, Culture, out value);
            return value;
        }

        public override float GetFloat(string name)
        {
            float value = 0;
            float.TryParse(_attributes[name].Value, NumberStyles.Float, Culture, out value);
            return value;
        }

        public override double GetDouble(string name)
        {
            double value = 0;
            double.TryParse(_attributes[name].Value, NumberStyles.Number, Culture, out value);
            return value;
        }

        public override bool[] GetBoolArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new bool[0];
            }

            string[] strArr = value.Split(',');
            bool[] arr = new bool[strArr.Length];
            for (int i = 0; i < strArr.Length; ++i)
            {
                arr[i] = GetBoolValue(strArr[i]);
            }

            return arr;
        }

        public override int[] GetIntArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new int[0];
            }

            string[] strArr = value.Split(',');
            int[] arr = new int[strArr.Length];
            for (int i = 0; i < strArr.Length; ++i)
            {
                int v = 0;
                int.TryParse(strArr[i], NumberStyles.Number, Culture, out v);
                arr[i] = v;
            }

            return arr;
        }

        public override float[] GetFloatArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new float[0];
            }

            string[] strArr = value.Split(',');
            float[] arr = new float[strArr.Length];
            for (int i = 0; i < strArr.Length; ++i)
            {
                float v = 0;
                float.TryParse(strArr[i], NumberStyles.Float, Culture, out v);
                arr[i] = v;
            }

            return arr;
        }

        public override double[] GetDoubleArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new double[0];
            }

            string[] strArr = value.Split(',');
            double[] arr = new double[strArr.Length];
            for (int i = 0; i < strArr.Length; ++i)
            {
                double v = 0;
                double.TryParse(strArr[i], NumberStyles.Number, Culture, out v);
                arr[i] = v;
            }

            return arr;
        }

        public override long[] GetLongArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new long[0];
            }

            string[] strArr = value.Split(',');
            long[] arr = new long[strArr.Length];
            for (int i = 0; i < strArr.Length; ++i)
            {
                long v = 0;
                long.TryParse(strArr[i], NumberStyles.Number, Culture, out v);
                arr[i] = v;
            }

            return arr;
        }

        public override string[] GetStringArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new string[0];
            }

            string[] strArr = value.Split(',');
            return strArr;
        }


        //--------------------------------------------------------------------------------
        // 解析 IdCount 类型
        //--------------------------------------------------------------------------------

        public override ConfigIdCount GetIdCount(string name)
        {
            string value = _attributes[name].Value;
            return CreateIdCount(value);
        }

        public override ConfigIdCount[] GetIdCountArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new ConfigIdCount[0];
            }

            return CreateIdCountArray(value);
        }

        private static ConfigIdCount[] CreateIdCountArray(string value)
        {
            string[] strArr = value.Split('|');
            ConfigIdCount[] arr = new ConfigIdCount[strArr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                ConfigIdCount data = CreateIdCount(strArr[i]);
                arr[i] = data;
            }

            return arr;
        }

        private static ConfigIdCount CreateIdCount(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            string[] strArr = value.Split(';');
            int[] arr = new int[strArr.Length];
            for (int i = 0; i < strArr.Length; ++i)
            {
                int v = 0;
                int.TryParse(strArr[i], NumberStyles.Number, Culture, out v);
                arr[i] = v;
            }

            if (arr.Length == 2)
            {
                return new ConfigIdCount(arr[0], arr[1]);
            }

            return new ConfigIdCount(0, 0);
        }

        public override ConfigIdFloat GetIdFloat(string name)
        {
            string value = _attributes[name].Value;
            return CreateIdFloat(value);
        }

        public override ConfigIdFloat[] GetIdFloatArray(string name)
        {
            string value = _attributes[name].Value;
            if (string.IsNullOrEmpty(value))
            {
                return new ConfigIdFloat[0];
            }

            return CreateIdFloatArray(value);
        }

        private static ConfigIdFloat[] CreateIdFloatArray(string value)
        {
            string[] strArr = value.Split('|');
            ConfigIdFloat[] arr = new ConfigIdFloat[strArr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                ConfigIdFloat data = CreateIdFloat(strArr[i]);
                arr[i] = data;
            }

            return arr;
        }

        private static ConfigIdFloat CreateIdFloat(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            string[] strArr = value.Split(';');
            if (strArr.Length == 2)
            {
                int id = 0;
                int.TryParse(strArr[0], NumberStyles.Number, Culture, out id);

                float fv = 0;
                float.TryParse(strArr[1], NumberStyles.Float, Culture, out fv);

                return new ConfigIdFloat(id, fv);
            }

            return new ConfigIdFloat(0, 0);
        }


        //--------------------------------------------------------------------------------
        // 解析 IdParams 类型
        //--------------------------------------------------------------------------------

        public override ConfigIdParams GetIdParams(string name)
        {
            string value = _attributes[name].Value;
            return CreateIdParams(value);
        }

        public override ConfigIdParams[] GetIdParamsArray(string name)
        {
            string value = _attributes[name].Value;
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
            int.TryParse(contents[0], NumberStyles.Number, Culture, out id);

            var values = contents[1].Split(',');
            var parameters = new int[values.Length];
            for (var i = 0; i < values.Length; ++i)
            {
                parameters[i] = 0;
                int.TryParse(values[i], NumberStyles.Number, Culture, out parameters[i]);
            }

            return new ConfigIdParams(id, parameters);
        }


        //--------------------------------------------------------------------------------
        // 解析枚举类型
        //--------------------------------------------------------------------------------

        public override T GetEnum<T>(string name)
        {
            string value = _attributes[name].Value;
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }

            if (!string.IsNullOrEmpty(value))
            {
                D.Warn("[ConfigXmlLoader] Config enum parse error [{0}:{1}:{2}]", _file, name, value);
            }

            return default(T);
        }

        public override ConfigIdEnum<T> GetIdEnum<T>(string name)
        {
            string value = _attributes[name].Value;
            return CreateIdEnum<T>(value);
        }

        public override ConfigIdEnum<T>[] GetIdEnumArray<T>(string name)
        {
            string value = _attributes[name].Value;
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
                int.TryParse(strArr[0], NumberStyles.Number, Culture, out id);
                if (Enum.IsDefined(tType, strArr[1]))
                {
                    var e = (T)Enum.Parse(tType, strArr[1], true);
                    return new ConfigIdEnum<T>(id, e);
                }

                D.Error("[ConfigXmlLoader] Enum:{0} is not defined! Will use enum:{1} default value:{2}", strArr[1],
                    typeof(T).FullName, default(T));
                return new ConfigIdEnum<T>(id, default(T));
            }

            return null;
        }
    }
}
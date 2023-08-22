using System;
using System.Collections.Generic;
using System.Xml;
using Log;

namespace Config
{
    public class ConfigLoader
    {
        public delegate void ConfigCreateFunc(ConfigLoader loader);

        public class ConfigLoaderItem
        {
            public string ItemFile { get; private set; }
            public ConfigCreateFunc Callback { get; private set; }

            public ConfigLoaderItem(string file, ConfigCreateFunc cb)
            {
                ItemFile = file;
                Callback = cb;
            }
        }

        public static Dictionary<string, ConfigLoaderItem> LoadItems { get; private set; }

        public static void Add(string file, ConfigCreateFunc callback)
        {
            if (null == LoadItems)
            {
                LoadItems = new Dictionary<string, ConfigLoaderItem>(200);
            }

            LoadItems.Add(file, new ConfigLoaderItem(file, callback));
        }

        public static ConfigLoaderItem GetLoaderItem(string file)
        {
            return LoadItems.TryGetValue(file, out var item) ? item : null;
        }

        public static void CreateConfig(string xml, string file, ConfigCreateFunc callback)
        {
            if (string.IsNullOrEmpty(xml) || null == callback)
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode rootNode = xmlDoc.SelectSingleNode(file);

            if (rootNode != null)
            {
                XmlNodeList list = rootNode.ChildNodes;

                if (list.Count > 0)
                {
                    try
                    {
                        callback(new ConfigLoader(file, list));
                    }
                    catch (Exception e)
                    {
                        D.Error("[ConfigLoader] Parse xml of [{0}] error. \n Exception: {1}", file, e.StackTrace);
                    }
                }
            }
        }

        public static void LoadConfig(byte[] byteArray)
        {
            if (byteArray == null)
            {
                D.Error("[ConfigLoader] Byte is null");
                return;
            }

            using (var ba = new ByteArray(byteArray))
            {
                while (ba.ReadAvailable)
                {
                    var file = ba.ReadUTF();
                    var count = ba.ReadInt();
                    var length = ba.ReadInt();
                    var bytes = ba.ReadBytes(length);

                    var loaderItem = GetLoaderItem(file);
                    if (loaderItem != null && length > 0)
                    {
                        CreateConfig(bytes, loaderItem.ItemFile, count, loaderItem.Callback);
                    }
                    else
                    {
                        D.Warn("[ConfigLoader] Config {0} cannot load, length {1}", file, length);
                    }
                }
            }
        }

        public static void CreateConfig(byte[] bytes, string file, int dataCount, ConfigCreateFunc callback)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return;
            }

            try
            {
                callback(new ConfigLoader(file, bytes, dataCount));
            }
            catch (Exception e)
            {
                D.Error("[ConfigLoader] Read buff of [{0}] error. \n Exception: {1}", file, e.StackTrace);
            }
        }


        //--------------------------------------------------------------------------------
        // 处理数据读取
        //--------------------------------------------------------------------------------

        private readonly IConfigLoader _loader;

        private ConfigLoader(string file, XmlNodeList list)
        {
            Count = list.Count;
            _loader = new ConfigXmlLoader(file, list);
        }

        private ConfigLoader(string file, byte[] datas, int dataCount)
        {
            Count = dataCount;
            _loader = new ConfigBinLoader(file, datas);
        }

        public int Count { get; private set; }

        public bool Next()
        {
            return _loader.Next();
        }

        public string GetString(string name)
        {
            return _loader.GetString(name);
        }

        public byte GetByte(string name)
        {
            return _loader.GetByte(name);
        }

        public bool GetBool(string name)
        {
            return _loader.GetBool(name);
        }

        public short GetShort(string name)
        {
            return _loader.GetShort(name);
        }

        public int GetInt(string name)
        {
            return _loader.GetInt(name);
        }

        public long GetLong(string name)
        {
            return _loader.GetLong(name);
        }

        public float GetFloat(string name)
        {
            return _loader.GetFloat(name);
        }

        public double GetDouble(string name)
        {
            return _loader.GetDouble(name);
        }

        public bool[] GetBoolArray(string name)
        {
            return _loader.GetBoolArray(name);
        }

        public int[] GetIntArray(string name)
        {
            return _loader.GetIntArray(name);
        }

        public float[] GetFloatArray(string name)
        {
            return _loader.GetFloatArray(name);
        }

        public double[] GetDoubleArray(string name)
        {
            return _loader.GetDoubleArray(name);
        }

        public long[] GetLongArray(string name)
        {
            return _loader.GetLongArray(name);
        }

        public string[] GetStringArray(string name)
        {
            return _loader.GetStringArray(name);
        }


        //--------------------------------------------------------------------------------
        // 解析 IdCount 和 IdFloat类型
        //--------------------------------------------------------------------------------

        public ConfigIdCount GetIdCount(string name)
        {
            return _loader.GetIdCount(name);
        }

        public ConfigIdCount[] GetIdCountArray(string name)
        {
            return _loader.GetIdCountArray(name);
        }

        public ConfigIdFloat GetIdFloat(string name)
        {
            return _loader.GetIdFloat(name);
        }

        public ConfigIdFloat[] GetIdFloatArray(string name)
        {
            return _loader.GetIdFloatArray(name);
        }


        //--------------------------------------------------------------------------------
        // 解析 IdParams 类型
        //--------------------------------------------------------------------------------

        public ConfigIdParams GetIdParams(string name)
        {
            return _loader.GetIdParams(name);
        }

        public ConfigIdParams[] GetIdParamsArray(string name)
        {
            return _loader.GetIdParamsArray(name);
        }


        //--------------------------------------------------------------------------------
        // 解析枚举类型
        //--------------------------------------------------------------------------------

        public T GetEnum<T>(string name)
        {
            return _loader.GetEnum<T>(name);
        }

        public ConfigIdEnum<T> GetIdEnum<T>(string name) where T : struct, IConvertible
        {
            return _loader.GetIdEnum<T>(name);
        }

        public ConfigIdEnum<T>[] GetIdEnumArray<T>(string name) where T : struct, IConvertible
        {
            return _loader.GetIdEnumArray<T>(name);
        }


        //--------------------------------------------------------------------------------
        // 特殊字符串解析
        //--------------------------------------------------------------------------------

        public static ConfigIdCount[] CreateIdCountArray(string value)
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
                int v;
                int.TryParse(strArr[i], out v);
                arr[i] = v;
            }

            if (arr.Length == 2)
            {
                return new ConfigIdCount(arr[0], arr[1]);
            }

            return new ConfigIdCount(0, 0);
        }
    }
}
using System.Collections.Generic;

namespace Config
{
    public class Configs
    {
        public static void Init()
        {
            ConfigLoader.Add("videosettings", ConfVideoSettings.Create);
            ConfigLoader.Add("inputsettings", ConfInputSettings.Create);
        }
    }


    public class ConfInputSettings
    {
        public static Dictionary<int, ConfInputSettings> Data;

        public static void Create(ConfigLoader loader)
        {
            if (null != loader)
            {
                Data = new Dictionary<int, ConfInputSettings>(loader.Count);
                while (loader.Next())
                {
                    var conf = new ConfInputSettings(loader);
                    Data.Add(conf.ID, conf);
                }
            }
            else
            {
                Data = new Dictionary<int, ConfInputSettings>();
            }
        }

        public static ConfInputSettings Get(int id)
        {
            ConfInputSettings conf;
            return Data.TryGetValue(id, out conf) ? conf : null;
        }

        public ConfInputSettings(ConfigLoader loader)
        {
            ID = loader.GetInt("ID");
            Command = loader.GetString("Command");
            DefaultKey = loader.GetString("DefaultKey");
        }

        public readonly int ID;
        public readonly string Command;
        public readonly string DefaultKey;
    }


    public class ConfVideoSettings
    {
        public static Dictionary<int, ConfVideoSettings> Data;

        public static void Create(ConfigLoader loader)
        {
            if (null != loader)
            {
                Data = new Dictionary<int, ConfVideoSettings>(loader.Count);
                while (loader.Next())
                {
                    var conf = new ConfVideoSettings(loader);
                    Data.Add(conf.ID, conf);
                }
            }
            else
            {
                Data = new Dictionary<int, ConfVideoSettings>();
            }
        }

        public static ConfVideoSettings Get(int id)
        {
            ConfVideoSettings conf;
            return Data.TryGetValue(id, out conf) ? conf : null;
        }

        public ConfVideoSettings(ConfigLoader loader)
        {
            ID = loader.GetInt("ID");
            Resolution = loader.GetString("Resolution");
        }

        public readonly int ID;
        public readonly string Resolution;
    }
}
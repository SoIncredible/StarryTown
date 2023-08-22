using System.Collections.Generic;

namespace Config
{

    public class Configs
    {
        public static void Init()
        {
            ConfigLoader.Add("cardasset", ConfCardAsset.Create);
            ConfigLoader.Add("carddata", ConfCardData.Create);
            ConfigLoader.Add("common", ConfCommon.Create);
            ConfigLoader.Add("deskbg", ConfDeskBg.Create);
            ConfigLoader.Add("deskface", ConfDeskFace.Create);
            ConfigLoader.Add("deskback", ConfDeskBack.Create);
            ConfigLoader.Add("dcindex", ConfDCIndex.Create);
            ConfigLoader.Add("pushrules", ConfPushRules.Create);
            ConfigLoader.Add("music", ConfMusic.Create);
            ConfigLoader.Add("sound", ConfSound.Create);
        }
    }


	public class ConfCardAsset
	{
    	public static Dictionary<int, ConfCardAsset> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfCardAsset>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfCardAsset(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfCardAsset>();
			}
		}

		public static ConfCardAsset Get(int id)
		{
			ConfCardAsset conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfCardAsset(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			CardStyle = loader.GetString("CardStyle");
			CardValue = loader.GetString("CardValue");
			CardCenter = loader.GetString("CardCenter");
			CardSmall = loader.GetString("CardSmall");
			CardNum = loader.GetString("CardNum");
			CardFace = loader.GetString("CardFace");
		}

		public readonly int ID;
		public readonly string CardStyle;
		public readonly string CardValue;
		public readonly string CardCenter;
		public readonly string CardSmall;
		public readonly string CardNum;
		public readonly string CardFace;
	}


	public class ConfCardData
	{
    	public static Dictionary<int, ConfCardData> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfCardData>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfCardData(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfCardData>();
			}
		}

		public static ConfCardData Get(int id)
		{
			ConfCardData conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfCardData(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			CardArray = loader.GetString("CardArray");
		}

		public readonly int ID;
		public readonly string CardArray;
	}


	public class ConfCommon
	{
    	public static Dictionary<int, ConfCommon> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfCommon>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfCommon(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfCommon>();
			}
		}

		public static ConfCommon Get(int id)
		{
			ConfCommon conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfCommon(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Draw1EasyStart = loader.GetInt("Draw1EasyStart");
			Draw1EasyCount = loader.GetInt("Draw1EasyCount");
			Draw1HardStart = loader.GetInt("Draw1HardStart");
			Draw1HardCount = loader.GetInt("Draw1HardCount");
			Draw3EasyStart = loader.GetInt("Draw3EasyStart");
			Draw3EasyCount = loader.GetInt("Draw3EasyCount");
			Draw3HardStart = loader.GetInt("Draw3HardStart");
			Draw3HardCount = loader.GetInt("Draw3HardCount");
		}

		public readonly int ID;
		public readonly int Draw1EasyStart;
		public readonly int Draw1EasyCount;
		public readonly int Draw1HardStart;
		public readonly int Draw1HardCount;
		public readonly int Draw3EasyStart;
		public readonly int Draw3EasyCount;
		public readonly int Draw3HardStart;
		public readonly int Draw3HardCount;
	}


	public class ConfDCIndex
	{
    	public static Dictionary<int, ConfDCIndex> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfDCIndex>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfDCIndex(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfDCIndex>();
			}
		}

		public static ConfDCIndex Get(int id)
		{
			ConfDCIndex conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfDCIndex(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Draw1 = loader.GetInt("Draw1");
			Draw3 = loader.GetInt("Draw3");
		}

		public readonly int ID;
		public readonly int Draw1;
		public readonly int Draw3;
	}


	public class ConfDeskBack
	{
    	public static Dictionary<int, ConfDeskBack> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfDeskBack>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfDeskBack(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfDeskBack>();
			}
		}

		public static ConfDeskBack Get(int id)
		{
			ConfDeskBack conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfDeskBack(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Atlas = loader.GetString("Atlas");
			Image = loader.GetString("Image");
		}

		public readonly int ID;
		public readonly string Atlas;
		public readonly string Image;
	}


	public class ConfDeskBg
	{
    	public static Dictionary<int, ConfDeskBg> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfDeskBg>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfDeskBg(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfDeskBg>();
			}
		}

		public static ConfDeskBg Get(int id)
		{
			ConfDeskBg conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfDeskBg(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Atlas = loader.GetString("Atlas");
			Image = loader.GetString("Image");
			Type = loader.GetInt("Type");
			PosX = loader.GetFloat("PosX");
			PosY = loader.GetFloat("PosY");
		}

		public readonly int ID;
		public readonly string Atlas;
		public readonly string Image;
		public readonly int Type;
		public readonly float PosX;
		public readonly float PosY;
	}


	public class ConfDeskFace
	{
    	public static Dictionary<int, ConfDeskFace> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfDeskFace>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfDeskFace(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfDeskFace>();
			}
		}

		public static ConfDeskFace Get(int id)
		{
			ConfDeskFace conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfDeskFace(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Atlas = loader.GetString("Atlas");
			Type = loader.GetInt("Type");
		}

		public readonly int ID;
		public readonly string Atlas;
		public readonly int Type;
	}


	public class ConfMusic
	{
    	public static Dictionary<int, ConfMusic> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfMusic>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfMusic(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfMusic>();
			}
		}

		public static ConfMusic Get(int id)
		{
			ConfMusic conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfMusic(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Clip = loader.GetString("Clip");
		}

		public readonly int ID;
		public readonly string Clip;
	}


	public class ConfPushRules
	{
    	public static Dictionary<int, ConfPushRules> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfPushRules>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfPushRules(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfPushRules>();
			}
		}

		public static ConfPushRules Get(int id)
		{
			ConfPushRules conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfPushRules(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Type = loader.GetInt("Type");
			Day = loader.GetIntArray("Day");
			Title = loader.GetInt("Title");
			TextKey = loader.GetIntArray("TextKey");
			Time = loader.GetDouble("Time");
		}

		public readonly int ID;
		public readonly int Type;
		public readonly int[] Day;
		public readonly int Title;
		public readonly int[] TextKey;
		public readonly double Time;
	}


	public class ConfSound
	{
    	public static Dictionary<int, ConfSound> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ConfSound>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ConfSound(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ConfSound>();
			}
		}

		public static ConfSound Get(int id)
		{
			ConfSound conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ConfSound(ConfigLoader loader)
		{
			ID = loader.GetInt("ID");
			Clip = loader.GetString("Clip");
		}

		public readonly int ID;
		public readonly string Clip;
	}

}
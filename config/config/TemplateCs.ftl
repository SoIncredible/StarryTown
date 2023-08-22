
	public class ${className}
	{
    	public static Dictionary<int, ${className}> Data;

		public static void Create(ConfigLoader loader)
		{
    		if (null != loader)
    		{
    			Data = new Dictionary<int, ${className}>(loader.Count);
    			while (loader.Next())
    			{
    				var conf = new ${className}(loader);
    				Data.Add(conf.ID, conf);
    			}
			}
			else
			{
    			Data = new Dictionary<int, ${className}>();
			}
		}

		public static ${className} Get(int id)
		{
			${className} conf;
    		return Data.TryGetValue(id, out conf) ? conf : null;
		}

		public ${className}(ConfigLoader loader)
		{
			<#list fields as field>
			${field.fieldName} = loader.Get${field.csFieldMethodType}("${field.fieldName}");
			</#list>
		}

		<#list fields as field>
		public readonly ${field.csFieldType} ${field.fieldName};
		</#list>
	}

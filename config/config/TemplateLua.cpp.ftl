
	static ${className}* m_pSingleton = nullptr;
	${className}* ${className}::Instance()
	{
		return m_pSingleton;
	}
	void ${className}::Destroy()
	{
		delete m_pSingleton;
	}

	/*
	* 初始化数据
	*/
	void ${className}::Init()
	{
		if (m_pSingleton == nullptr)
		{
			m_pSingleton = new ${className}();
			ScriptManager::Instance()->readTemplateData(${className}::Create, "TemplateData.${className}");
		}
	}

	/*
	* 创建数据
	*/
	void ${className}::Create(lua_State* pl)
	{
		${className} data;
		data.id = ScriptUtil::getInt(pl, "id");
		<#list fields as field>
			data.${field.fieldName} = ScriptUtil::get${field.cppFieldMethodType}(pl, "${field.fieldName}");
		</#list>
		if (m_pSingleton)
		{
			m_pSingleton->add(data);
		}
	}

	/*
	* 查看数据
	*/
	${className}* ${className}::get(int id)
	{
		${className}::Map::iterator it = m_datas.find(id);
		return ( (it != m_datas.end()) ? &(it->second) : nullptr );
	}

	/*
	* 添加数据
	*/
	void ${className}::add(const ${className}& data)
	{
		m_datas[data.id] = data;
	}
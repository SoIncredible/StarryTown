
	class ${className}
	{
	public:
		typedef map<int, ${className}> Map;

	public:
		static ${className}* Instance();
		static void Destroy();
		static void Init();
		static void Create(lua_State* pl);

	public:
		void add(const ${className}& data);
		${className}* get(int id);
		Map& getAll() { return m_datas; };

	public:
		int		id;
		<#list fields as field>
			${field.cppFieldType} ${field.fieldName};
		</#list>

	private:
		Map		m_datas;

	};
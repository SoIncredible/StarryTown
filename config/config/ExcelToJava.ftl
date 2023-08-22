package ${packageName};

import java.net.URI;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import org.gof.core.gen.conf.Conf;
import org.gof.core.support.ConfigJSON;
import org.gof.core.support.Utils;

import com.alibaba.fastjson.JSONArray;
import com.alibaba.fastjson.JSONObject;

/**
 * ${entityNameCN}
 * ${excelFileName}
 * @author System
 * 此类是系统自动生成类 不要直接修改，修改后也会被覆盖
 */
@ConfigJSON
public class ${entityName} implements Conf {
	<#-- 字段 -->
	<#list properties as prop>
	public final ${prop.type} ${prop.name};			//${prop.note}
	</#list>

	<#-- 构造方法 -->
	public ${entityName}(${entityName} conf) {
		this(<#list properties as prop>conf.${prop.name}<#if prop_has_next>, </#if></#list>);
	}

	public ${entityName}(${paramMethod}) {
	<#list properties as prop>
		this.${prop.name} = ${prop.name};
	</#list>
	}

	public ${entityName} copy() {
		return new ${entityName}(this);
	}

	public static void reLoad() {
		DATA._init();
	}

	/**
	 * 全部数据的数量
	 * @return
	 */
	public static int size() {
		return DATA.getMap().size();
	}

	/**
	 * 获取全部数据
	 * @return
	 */
	public static Collection<${entityName}> findAll() {
		return DATA.getList();
	}

	/**
	 * 通过SN获取数据
	 */
	public static ${entityName} get(${idType} sn) {
		return DATA.getMap().get(sn);
	}

	/**
	 * 通过SN获取数据，如果sn获取不到，则通过defaultSn获取
	 */
	public static ${entityName} getOrDefault(${idType} sn, ${idType} defaultSn) {
		${entityName} v = get(sn);
		if (v != null) {
			return v;
		}
		return get(defaultSn);
	}

	/**
	 * 记录流
	 */
	public static Stream<${entityName}> stream() {
		return DATA.getList().stream();
	}

	/**
	 * 筛选出符合条件的记录
	 * 
	 * @param predicate
	 *            判断
	 */
	public static List<${entityName}> filter(Predicate<${entityName}> predicate) {
		return filter(predicate, null);
	}

	/**
	 * 筛选出符合条件的记录
	 * 
	 * @param predicate
	 *            判断
	 * @param comparator
	 *            排序器
	 */
	public static List<${entityName}> filter(Predicate<${entityName}> predicate, Comparator<${entityName}> comparator) {
		Stream<${entityName}> stream = stream().filter(predicate);
		if (comparator != null) {
			stream = stream.sorted(comparator);
		}
		return stream.collect(Collectors.toList());
	}

	/**
	 * 数据集
	 * 单独提出来也是为了做数据延迟初始化
	 * 避免启动遍历类时，触发了static静态块
	 */
	private static final class DATA {
		//全部数据
		private static volatile Map<${idType}, ${entityName}> _map;

		/**
		 * 获取数据的值集合
		 * @return
		 */
		public static Collection<${entityName}> getList() {
			return getMap().values();
		}

		/**
		 * 获取Map类型数据集合
		 * @return
		 */
		public static Map<${idType}, ${entityName}> getMap() {
			//延迟初始化
			if(_map == null) {
				synchronized (DATA.class) {
					if(_map == null) {
						_init();
					}
				}
			}
			
			return _map;
		}

		/**
		 * 初始化数据
		 */
		private static void _init() {
			Map<${idType}, ${entityName}> dataMap = new HashMap<>();

			//JSON数据
			String confJSON = _readConfFile();
			if(Utils.isBlank(confJSON)) return;
			
			//填充实体数据
			JSONArray confs = JSONArray.parseArray(confJSON);
			for(int i = 0; i < confs.size(); i++){
				JSONObject conf = confs.getJSONObject(i);
				${entityName} object = new ${entityName}(
					${paramInit}
				);
				dataMap.put(object.sn, object);
			}

			//保存数据
			_map = Collections.unmodifiableMap(dataMap);
		}

		/**
		 * 读取游戏配置
		 */
		private static String _readConfFile() {
			try {
				URI baseBath = ${entityName}.class.getResource("json/${entityName}.json").toURI();
				Path path = Paths.get(baseBath);
				byte[] readAllBytes = Files.readAllBytes(path);
				return new String(readAllBytes, StandardCharsets.UTF_8);
			} catch (Exception e) {
				throw new RuntimeException(e);
			}
		}
	}

	@Override
	public String toString(){
		return <#list properties as prop>"${entityName}[${prop.name}=" + ${prop.name} + "]";<#if prop_index == 0><#break/></#if></#list>
	}

	public String toDetailString(){
		StringBuffer sb = new StringBuffer();
		sb.append("${entityName}[");
<#list properties as prop>
		sb.append("${prop.name}=" + ${prop.name} + ", ");
</#list>
		sb.append("]");
		return sb.toString();
	}

}
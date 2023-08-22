package ${packageName};

import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

import org.gof.core.support.log.LogCore;

/**
 * ${entityName}
 * @author System
 * 此类是系统自动生成类 不要直接修改，修改后也会被覆盖
 */
public class ${entityName} {
	@FunctionalInterface
	private static interface ConfReloadable {
		public void reload();
	}

	private static Map<String, ConfReloadable> ___allConfReloads = null;

	private static void init() {
		Map<String, ConfReloadable> allConfReloads = new HashMap<>();
		<#list confList as confName>
		allConfReloads.put("${confName}", ${packageName}.${confName}::reLoad);
		</#list>
		___allConfReloads = Collections.unmodifiableMap(allConfReloads);
	}

	public static Map<String, ConfReloadable> getConfReloadableMap() {
		// 延迟初始化
		if (___allConfReloads == null) {
			synchronized (ConfReloadable.class) {
				if (___allConfReloads == null) {
					init();
				}
			}
		}
		return ___allConfReloads;
	}

	public static void reload() {
		LogCore.core.info("load all game confs");
		getConfReloadableMap().forEach((k, v) -> v.reload());
	}

	public static boolean reload(String name) {
		LogCore.core.info("load game conf: {}", name);
		ConfReloadable reloadable = getConfReloadableMap().get(name);
		if (reloadable == null) {
			return false;
		}
		reloadable.reload();
		return true;
	}

}
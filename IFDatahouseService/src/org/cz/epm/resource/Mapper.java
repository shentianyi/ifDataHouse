package org.cz.epm.resource;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

import org.cz.epm.conf.Conf;
import org.cz.epm.conf.MongoConf;
import org.cz.epm.data.manager.RedisManager;
import org.cz.epm.thrift.service.DatahouseBase;
import org.cz.epm.util.BaseUtil;

public class Mapper {
	private String access_key;

	public Mapper(String access_key) {
		//this.access_key = access_key;
		this.access_key=Conf.getIfDatahouseMapAccessKey();
	}

	public String GetMapKey(String model, String map_value) {
		return getMapKeyValue(model, map_value, MapperType.ValueKey);
	}

	public String GetMapValue(String model, String map_key) {
		return getMapKeyValue(model, map_key, MapperType.KeyValue);
	}

	public Map<String, String> GetMapValues(String model, Set<String> map_values) {
		Map<String, String> keyMaps = new HashMap<String, String>();
		for (String map_value : map_values) {
			keyMaps.put(map_value, this.GetMapKey(model, map_value));
		}
		return keyMaps;
	}

	public String getAccess_key() {
		return access_key;
	}

	public void setAccess_key(String access_key) {
		this.access_key = access_key;
	}

	private String getMapKeyValue(String model, String kv, MapperType type) {
		String redis_key = BaseUtil.GetCombainRow(this.access_key, model,
				type.getIdentifier());
		String kvresult = RedisManager.HGet(redis_key, kv);
		if (kvresult == null) {
			Map<String, String> q = new HashMap<String, String>();
			q.put("model", model);
			q.put("access_key", this.access_key);
			q.put(type.getkField(), kv);

			Map<?, ?> result = DatahouseBase.getData(MongoConf.getMmapperitemcoll(),
					q, type.getvField());
			if (result != null)
				kvresult = result.get(type.getvField()).toString();
		}
		return kvresult;
	}

}

package org.cz.epm.resource;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import com.mongodb.BasicDBObject;
import com.mongodb.DBObject;

public class MongoUtil {
	public static BasicDBObject MapToBasicDBObject(Map<String, String> dataMap) {
		BasicDBObject doc = new BasicDBObject();
		for (Entry<String, String> entry : dataMap.entrySet())
			doc.append(entry.getKey(), entry.getValue());
		return doc;
	}

	public static Map<?, ?> DBObjectToMap(DBObject obj) {
		return obj.toMap();
	}

	public static List<Map> DBObjectsToMaps(List<DBObject> objs) {
		List<Map> r = new ArrayList<Map>();
		for (DBObject obj : objs)
			r.add(obj.toMap());
		return r;
	}
}

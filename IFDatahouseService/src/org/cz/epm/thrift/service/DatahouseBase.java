package org.cz.epm.thrift.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

import org.bson.types.ObjectId;
import org.cz.epm.data.manager.MongoManager;
import org.cz.epm.resource.Conf;
import org.cz.epm.resource.MongoUtil;
import com.mongodb.BasicDBObject;
import com.mongodb.BasicDBObjectBuilder;

public class DatahouseBase {

	// ************ part methods
	public static boolean AddPart(Map<String, String> dataMap) {
		return insertData(Conf.getMpartcoll(), dataMap);
	}

	// get parts
	public static List<Map> GetParts(String key, List<String> ins,
			String... fields) {
		return getdatas(Conf.getMpartcoll(), key, ins, fields);
	}

	// ************ attendance methods
	// add attendance
	public static boolean AddAttendance(Map<String, String> dataMap) {
		return insertData(Conf.getMattendcoll(), dataMap);
	}

	public static List<Map> GetAttendances(String key, String value,
			String scopeKey, String start, String end, String... fields) {
		return getdatas(Conf.getMattendcoll(), key, value, scopeKey, start,
				end, fields);
	}

	public static List<Map> GetAttendances(Map keyValue, String scopeKey,
			String start, String end, String... fields) {
		return getdatas(Conf.getMattendcoll(), keyValue, scopeKey, start, end,
				fields);
	}

	public static List<Map> GetAttendances(Map keyValue, String scopeKey,
			String start, String end, String sortKey, int order, int limit,
			String... fields) {
		return getdatas(Conf.getMattendcoll(), keyValue, scopeKey, start, end,
				sortKey, order, limit, fields);
	}
	
	public static Map GetAttendance(Map keyValue,String scopeKey, String start, String end,String... fields) {
		return getdata(Conf.getMattendcoll(), keyValue,scopeKey,start,end, fields);
	}

	// ************ product methods
	// add product
	public static boolean AddProduct(Map<String, String> dataMap) {
		return insertData(Conf.getMproductcoll(), dataMap);
	}

	// get products
	public static List<Map> GetProducts(String key, List<String> ins,
			String... fields) {
		return getdatas(Conf.getMproductcoll(), key, ins, fields);
	}

	public static List<Map> GetProducts(String key, String value,
			String scopeKey, String start, String end, String... fields) {
		return getdatas(Conf.getMproductcoll(), key, value, scopeKey, start,
				end, fields);
	}

	// update
	public static boolean UpdateProduct(String key, String value,
			Map<String, String> fields) {
		return updateData(Conf.getMproductinspectcoll(), key, value, fields,
				true);
	}

	// ************ inspect methods
	// add product inspect
	public static boolean AddProductInspect(Map<String, String> dataMap) {
		return insertData(Conf.getMproductinspectcoll(), dataMap);
	}

	public static boolean AddOperatingStates(Map<String, String> dataMap) {
		return insertData(Conf.getMoperatingstatecoll(), dataMap);
	}

	// ************ base methods
	// insert data
	private static boolean insertData(String collName,
			Map<String, String> dataMap) {
		try {
			MongoManager.InsertData(
					collName,
					MongoUtil.MapToBasicDBObject(dataMap)
							.append("created_at", (new Date()).getTime())
							.append("updated_at", (new Date()).getTime()));
			return true;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return false;
	}

	// update data
	private static boolean updateData(String collName, String key,
			String value, Map<String, String> dataMap, boolean upsert) {
		try {
			BasicDBObject query = key.equals("_id") ? new BasicDBObject(key,
					new ObjectId(value)) : new BasicDBObject(key, value);
			BasicDBObject o = new BasicDBObject("$set", MongoUtil
					.MapToBasicDBObject(dataMap).append("created_at",
							(new Date()).getTime()));
			MongoManager.UpdateData(collName, query, o, upsert);
			return true;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return false;
	}

	// get datas
	// in
	private static List<Map> getdatas(String collName, String key,
			List<String> ins, String... fields) {
		try {
			BasicDBObject query = new BasicDBObject();
			List values = null;
			if (key.equals("_id")) {
				values = new ArrayList<ObjectId>();
				for (String id : ins) {
					values.add(new ObjectId(id));
				}
			} else {
				values = ins;
			}
			query.put(key, new BasicDBObject("$in", values));

			return MongoUtil.DBObjectsToMaps(MongoManager.Find(collName, query,
					generateField(fields)));

		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	// get datas
	// between
	private static List<Map> getdatas(String collName, String key,
			String value, String scopeKey, String start, String end,
			String... fields) {
		try {
			BasicDBObject query = key.equals("_id") ? new BasicDBObject(key,
					new ObjectId(value)) : new BasicDBObject(key, value);
			query.put(key, value);
			query.put(scopeKey,
					BasicDBObjectBuilder.start("$gte", start).add("$lte", end)
							.get());
			return MongoUtil.DBObjectsToMaps(MongoManager.Find(collName, query,
					generateField(fields)));
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	// get datas
	// between
	private static List<Map> getdatas(String collName, Map<?, ?> keyValue,
			String scopeKey, String start, String end, String... fields) {
		try {
			BasicDBObject query = new BasicDBObject();
			for (Entry entry : keyValue.entrySet()) {
				query.append(entry.getKey().toString(),
						entry.equals("_id") ? new ObjectId(entry.getValue()
								.toString()) : entry.getValue());
			}
			if (start == null) {
				if (end != null)
					query.put(scopeKey, new BasicDBObject("$gte", end));
			} else if (end == null) {
				query.put(scopeKey, new BasicDBObject("$lte", start));
			} else {
				query.put(scopeKey, BasicDBObjectBuilder.start("$gte", start)
						.add("$lte", end).get());
			}
			return MongoUtil.DBObjectsToMaps(MongoManager.Find(collName, query,
					generateField(fields)));
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	// get datas
	// order limit
	private static List<Map> getdatas(String collName, Map<?, ?> keyValue,
			String scopeKey, String start, String end, String orderKey,
			int order, int limit, String... fields) {
		try {
			BasicDBObject query = new BasicDBObject();
			for (Entry entry : keyValue.entrySet()) {
				query.append(entry.getKey().toString(),
						entry.equals("_id") ? new ObjectId(entry.getValue()
								.toString()) : entry.getValue());
			}
			if (start == null) {
				if (end != null)
					query.put(scopeKey, new BasicDBObject("$gte", end));
			} else if (end == null) {
				query.put(scopeKey, new BasicDBObject("$lte", start));
			} else {
				query.put(scopeKey, BasicDBObjectBuilder.start("$gte", start)
						.add("$lte", end).get());
			}
			BasicDBObject orderBy = new BasicDBObject(orderKey, order);
			return MongoUtil.DBObjectsToMaps(MongoManager.Find(collName, query,
					orderBy, limit, generateField(fields)));
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	// find one
	public static Map getdata(String collName, Map<?, ?> keyValue,
			String... fields) {
		BasicDBObject query = new BasicDBObject();
		for (Entry entry : keyValue.entrySet()) {
			query.append(entry.getKey().toString(),
					entry.equals("_id") ? new ObjectId(entry.getValue()
							.toString()) : entry.getValue());
		}
//		System.out.println(query.toString());
		return MongoManager.FineOne(collName, query, generateField(fields));
	}
	
	private static Map getdata(String collName, Map<?, ?> keyValue,
			String scopeKey, String start, String end, String... fields) {
		try {
			BasicDBObject query = new BasicDBObject();
			for (Entry entry : keyValue.entrySet()) {
				query.append(entry.getKey().toString(),
						entry.equals("_id") ? new ObjectId(entry.getValue()
								.toString()) : entry.getValue());
			}
			if (start == null) {
				if (end != null)
					query.put(scopeKey, new BasicDBObject("$gte", end));
			} else if (end == null) {
				query.put(scopeKey, new BasicDBObject("$lte", start));
			} else {
				query.put(scopeKey, BasicDBObjectBuilder.start("$gte", start)
						.add("$lte", end).get());
			}
//			System.out.println(query.toString());
			return MongoManager.FineOne(collName, query,generateField(fields));
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	// generate query filed
	private static BasicDBObject generateField(String... fields) {
		if (fields.length > 0) {
			BasicDBObject field = new BasicDBObject();
			for (String key : fields) {
				field.put(key, 1);
			}
			return field;
		} else
			return null;
	}
}

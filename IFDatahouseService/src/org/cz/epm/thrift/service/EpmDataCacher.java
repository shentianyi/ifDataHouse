package org.cz.epm.thrift.service;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.UUID;

import org.cz.epm.data.manager.RedisManager;
import org.cz.epm.thrift.generated.ProductInspectHandledType;
import org.cz.epm.thrift.generated.ProductInspectType;
import org.cz.epm.util.EnumTypeUtil;

public class EpmDataCacher {

	// worker attend redis cache
	public static String SetAttendCounter(String entityId, long incrValue) {
		String key = generateAttendCounterKey(entityId);
		RedisManager.Incr(key, incrValue);
		return key;
	}

	public static long GetAttendCount(String entityId) {
		return Long.parseLong(RedisManager.GetIncr(
				generateAttendCounterKey(entityId)));
	}

	public static String SetAttendStaffLocus(String entityId, String staffNr) {
		String key = generateAttendStaffLocusKey(entityId);
		RedisManager.SAdd(key, staffNr);
		return key;
	}

	public static Set<String> GetAttendStaffLocus(String entityId) {
		return RedisManager.SMembers(
				generateAttendStaffLocusKey(entityId));
	}

	// product origin out put cache
	public static String SetProductOriOutputCacheZSet(String entityId,
			long produceTime, String productId) {
		String key = generateProductOriOutputCacheZSetKey(entityId);
		RedisManager.ZAdd(key, produceTime, productId);
		return key;
	}

	public static long GetProductOriOutputCacheCount(String entityId,
			long startTime, long endTime) {
		return RedisManager.ZCount(
				generateProductOriOutputCacheZSetKey(entityId), startTime,
				endTime);
	}

	// product cache
	public static String SetProductOutputCacheZSet(String entityId,
			long produceTime, String productId) {
		String key = generateProductOutputCacheZSetKey(entityId);
		RedisManager.ZAdd(key, produceTime, productId);
		return key;
	}

	public static long GetProductOutputCacheCount(String entityId,
			long startTime, long endTime) {
		return RedisManager.ZCount(generateProductOutputCacheZSetKey(entityId), startTime,
						endTime);
	}

	public static Set<String> GetProductOutputIDRangeCache(String entityId,
			long startTime, long endTime) {
		return RedisManager.ZRange(generateProductOutputCacheZSetKey(entityId), startTime,
						endTime);
	}

	// product inspect cache
	public static String SetProductInspectTimeCacheZSet(String entityId,
			long inspectTime, String productId) {
		String key = generateProductInspectTimeZSetKey(entityId);
		if (RedisManager.ZScore(key, productId) == null) {
			RedisManager.ZAdd(key, inspectTime, productId);
		}
		return key;
	}

	public static String SetProductInspectTypeCacheZSet(String entityId,
			ProductInspectType type, String productId) {
		String key = generateProductInspectTypeZSetKey(entityId);
		Double currentType = RedisManager.ZScore(key, productId);
		ProductInspectHandledType targetType = EnumTypeUtil
				.GetProductInspectHandledType(currentType, type);		
    	RedisManager.ZAdd(
				generateProductInspectTypeZSetKey(entityId),
				targetType.getValue(), productId);
		return key;
	}

	public static long GetProductInspectCacheCount(String entityId,
			long startTime, long endTime, ProductInspectHandledType type) {
		String timeKey = generateProductInspectTimeZSetKey(entityId);
		if (type == null) {
			return RedisManager.ZCount(timeKey, startTime, endTime);
		}
		String typeKey = generateProductInspectTypeZSetKey(entityId);
		Set<String> timeItems = RedisManager.ZRangeByScore(timeKey,
				startTime, endTime);
		Set<String> typeItems = RedisManager.ZRangeByScore(typeKey,
				type.getValue(), type.getValue());
		timeItems.retainAll(typeItems);
		return timeItems.size();
	}

	// private method
	// generate key
	private static String generateAttendCounterKey(String entityId) {
		HashMap<String, String> keyData = new HashMap<String, String>();
		keyData.put("attend", "incr");
		keyData.put("entityId", entityId);
		return generateRedisCacheKey(keyData);
	}

	private static String generateAttendStaffLocusKey(String entityId) {
		HashMap<String, String> keyData = new HashMap<String, String>();
		keyData.put("attend:staff:locus", "set");
		keyData.put("entityId", entityId);
		return generateRedisCacheKey(keyData);
	}

	private static String generateProductOriOutputCacheZSetKey(String entityId) {
		HashMap<String, String> keyData = new HashMap<String, String>();
		keyData.put("product:ori:output", "zset");
		keyData.put("entityId", entityId);
		return generateRedisCacheKey(keyData);
	}

	private static String generateProductOutputCacheZSetKey(String entityId) {
		HashMap<String, String> keyData = new HashMap<String, String>();
		keyData.put("product:output", "zset");
		keyData.put("entityId", entityId);
		return generateRedisCacheKey(keyData);
	}

	private static String generateProductInspectTimeZSetKey(String entityId) {
		HashMap<String, String> keyData = new HashMap<String, String>();
		keyData.put("productinspecttime", "zset");
		keyData.put("entityId", entityId);
		return generateRedisCacheKey(keyData);
	}

	private static String generateProductInspectTypeZSetKey(String entityId) {
		HashMap<String, String> keyData = new HashMap<String, String>();
		keyData.put("productinspecttype", "zset");
		keyData.put("entityId", entityId);
		return generateRedisCacheKey(keyData);
	}

	private static String generateRedisCacheKey(Map<String, String> keyData) {
		Iterator<Entry<String, String>> i = keyData.entrySet().iterator();
		StringBuilder sb = new StringBuilder();
		sb.append("redis-cache:");
		while (i.hasNext()) {
			Map.Entry<String, String> entry = i.next();
			sb.append(entry.getKey()).append(':').append(entry.getValue())
					.append(':');
		}
		return sb.toString();
	}

}

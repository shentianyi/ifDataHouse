package org.cz.epm.thrift.service;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.Map.Entry;

import org.apache.thrift.TException;
import org.cz.epm.job.CleanRedisZSetCacheJob;
import org.cz.epm.thrift.generated.ProductInspectHandledType;
import org.cz.epm.thrift.generated.ProductInspectType;

public class EpmDataBase {
	public static boolean AddAttendance(Map<String, String> dataMap) {
		return DatahouseBase.AddAttendance(dataMap);
	}

	public static List<Map> GetStaffAttendances(String staffId,
			String entityId, Long startTime, Long endTime, String... fields) {
		Map query = new HashMap();
		query.put("staffId", staffId);
		query.put("entityId", entityId);
		return DatahouseBase.GetAttendances(query, "attendTime",
				startTime == null ? null : Long.toString(startTime),
				endTime == null ? null : Long.toString(endTime), fields);
	}

	public static List<Map> GetStaffAttendances(String staffId,
			String entityId, Long startTime, Long endTime, String orderKey,
			int order, int limit, String... fields) {
		Map query = new HashMap();
		query.put("staffId", staffId);
		query.put("entityId", entityId);
		return DatahouseBase.GetAttendances(query, "attendTime",
				startTime == null ? null : Long.toString(startTime),
				endTime == null ? null : Long.toString(endTime), orderKey,
				order, limit, fields);
	}

	public static Map GetStaffAttendance(String staffId, String entityId,
			String scopeKey, Long startTime, Long endTime, String... fields) {
		Map query = new HashMap();
		query.put("staffId", staffId);
		query.put("entityId", entityId);
		return DatahouseBase.GetAttendance(query, scopeKey,
				startTime == null ? null : Long.toString(startTime),
				endTime == null ? null : Long.toString(endTime), fields);
	}

	// add entity attendance staff locus
	public static String AddStaffAttendanceLocus(String entityId, String staffId) {
		return EpmDataCacher.SetAttendStaffLocus(entityId, staffId);
	}

	public static boolean AddProduct(Map<String, String> dataMap) {
		return DatahouseBase.AddProduct(dataMap);
	}

	public static boolean AddProductPack(Map<String, String> dataMap) {
		return DatahouseBase.UpdateProduct("productNr",
				dataMap.get("productNr"), dataMap);
	}

	public static boolean AddProductInspectRecord(Map<String, String> dataMap) {
		return DatahouseBase.AddProductInspect(dataMap);
	}

	public static boolean AddOperatingState(Map<String, String> dataMap) {
		return DatahouseBase.AddOperatingStates(dataMap);
	}

	public static boolean AddPlanTarget(Map<String, String> dataMap) {
		return DatahouseBase.AddTarget(dataMap);
	}

	public static Map GetPlanTarget(Map<String, String> query) {
//		Map<String, String> result = DatahouseBase.GetTarget(query);
//		return converMapToString(result);
		return DatahouseBase.GetTarget(query);
	}

	public static void SetProductInspectState(Map<String, String> dataMap) {
		EpmDataBase.AddProductInspectTypeCache(dataMap.get("entityId"),
				ProductInspectType.findByValue(Integer.parseInt(dataMap
						.get("type"))), dataMap.get("productNr"));
	}

	public static Map<String, Long> GetCurrentOnJobWorkerNums(
			Set<String> entityIds) throws TException {
		HashMap<String, Long> result = new HashMap<String, Long>();
		for (String entityId : entityIds) {
			result.put(entityId, 0L);
			try {
				result.put(entityId, GetAttendanceCount(entityId));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return result;
	}

	public static Map<String, Long> GetProductInspectCount(
			Set<String> entityIds, long startTime, long endTime,
			ProductInspectHandledType type) {
		HashMap<String, Long> result = new HashMap<String, Long>();
		for (String entityId : entityIds) {
			result.put(entityId, 0L);
			try {
				result.put(
						entityId,
						GetProductInspectCount(entityId, startTime, endTime,
								type));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return result;
	}

	public static Map<String, Long> GetProductOutputCount(
			Set<String> entityIds, long startTime, long endTime)
			throws TException {
		HashMap<String, Long> result = new HashMap<String, Long>();
		for (String entityId : entityIds) {
			result.put(entityId, 0L);
			try {
				result.put(entityId,
						GetProductOutputCount(entityId, startTime, endTime));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return result;
	}

	public static Map<String, Long> GetProductOriOutputCount(
			Set<String> entityIds, long startTime, long endTime)
			throws TException {
		HashMap<String, Long> result = new HashMap<String, Long>();
		for (String entityId : entityIds) {
			result.put(entityId, 0L);
			try {
				result.put(entityId,
						GetProductOriOutputCount(entityId, startTime, endTime));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return result;
	}

	public static Set<Map<String, String>> GetProductOutputNumAndTime(
			String entityId, long startTime, long endTime) throws TException {

		Set<Map<String, String>> partNumTimes = new HashSet<Map<String, String>>();
		;

		List<Map> products = DatahouseBase.GetProducts("entityId", entityId,
				"outputTime", Long.toString(startTime), Long.toString(endTime),
				"partId");
		if (products.size() > 0) {
			// System.out.println(products);
			List<String> repeatedpartIds = new ArrayList<String>();
			for (Map product : products) {
				repeatedpartIds.add(product.get("partId").toString());
			}
			List<String> partIds = new ArrayList<String>(new HashSet<String>(
					repeatedpartIds));
			List<Map> parts = DatahouseBase.GetParts("_id", partIds, "_id",
					"unitTime");
			// Set<Map<String, String>> partNumTimes = new HashSet<Map<String,
			// String>>();
			// System.out.println(partIds);
			// System.out.println(parts);
			for (Map part : parts) {
				Map<String, String> unit = new HashMap<String, String>();
				unit.put("partId", part.get("_id").toString());
				unit.put("unitTime", part.get("unitTime").toString());
				unit.put("quantity",
						Integer.toString(java.util.Collections.frequency(
								repeatedpartIds, part.get("_id").toString())));
				partNumTimes.add(unit);
			}
		}
		return partNumTimes;
	}

	public static Map<String, Long> GetOnJobTotalTime(Set<String> entityIds,
			long startTime, long endTime) {
		try {
			Map<String, Long> jobTimes = new HashMap<String, Long>();
			for (String entityId : entityIds) {
				jobTimes.put(entityId, 0L);
				Set<String> staffs = EpmDataCacher
						.GetAttendStaffLocus(entityId);
				long time = 0;

				for (String staff : staffs) {
					List<Map> attends = GetStaffAttendances(staff, entityId,
							startTime, endTime, "attendTime", "type");

					if (attends == null || attends.size() == 0) {
						Map attend = GetStaffAttendance(staff, entityId,
								"attendTime", startTime, null, "attendTime",
								"type");
						// System.out.println(attend);
						if (attend != null && attend.size() > 0) {
							// System.out.println(attend.get("type").toString());
							if (Integer.parseInt(attend.get("type").toString()) == 1) {
								time += endTime - startTime;
							}
						}
					} else {
						int startIndex = 0;
						int endIndex = attends.size();
						// System.out.println(attends);
						if (Integer.parseInt(attends.get(0).get("type")
								.toString()) == 1) {
							// System.out.println(attends.size());
							time += endTime
									- Long.parseLong(attends.get(0)
											.get("attendTime").toString());
							startIndex = 1;
						}
						if (attends.size() > 1) {
							if (Integer.parseInt(attends
									.get(attends.size() - 1).get("type")
									.toString()) == -1) {
								endIndex = attends.size() - 1;
								time += Long.parseLong(attends.get(endIndex)
										.get("attendTime").toString())
										- startTime;
							}
						}
						for (int i = startIndex; i < endIndex - 1; i += 2) {
							time += (Long.parseLong(attends.get(i)
									.get("attendTime").toString()) - Long
									.parseLong(attends.get(i + 1)
											.get("attendTime").toString()));
						}
					}
				}
				jobTimes.put(entityId, time);
			}
			return jobTimes;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	public static long GetAttendanceCount(String entityId) {
		return EpmDataCacher.GetAttendCount(entityId);
	}

	public static long GetProductOriOutputCount(String entityId,
			long startTime, long endTime) {
		return EpmDataCacher.GetProductOriOutputCacheCount(entityId, startTime,
				endTime);
	}

	public static long GetProductOutputCount(String entityId, long startTime,
			long endTime) {
		return EpmDataCacher.GetProductOutputCacheCount(entityId, startTime,
				endTime);
	}

	public static Set<String> GetProductOutputID(String entityId,
			long startTime, long endTime) {
		return EpmDataCacher.GetProductOutputIDRangeCache(entityId, startTime,
				endTime);
	}

	public static long GetProductInspectCount(String entityId, long startTime,
			long endTime, ProductInspectHandledType type) {
		return EpmDataCacher.GetProductInspectCacheCount(entityId, startTime,
				endTime, type);
	}

	// add attendance record count cache
	public static void AddAttendanceCountCache(String entityId, long type) {
		EpmDataCacher.SetAttendCounter(entityId, type);
	}

	// add product ori out put cache
	public static void AddProductOriOutputCache(String entityId,
			long produceTime, String productNr) {
		CleanRedisZSetCacheJob
				.Enqueue(EpmDataCacher.SetProductOriOutputCacheZSet(entityId,
						produceTime, productNr));
	}

	// add product out put cache
	public static void AddProductOutputCache(String entityId, long produceTime,
			String productNr) {
		CleanRedisZSetCacheJob.Enqueue(EpmDataCacher.SetProductOutputCacheZSet(
				entityId, produceTime, productNr));
	}

	public static void AddProductInspectTimeCache(String entityId,
			long inspectTime, String productNr) {
		CleanRedisZSetCacheJob.Enqueue(EpmDataCacher
				.SetProductInspectTimeCacheZSet(entityId, inspectTime,
						productNr));
	}

	public static void AddProductInspectTypeCache(String entityId,
			ProductInspectType type, String productNr) {
		CleanRedisZSetCacheJob.Enqueue(EpmDataCacher
				.SetProductInspectTypeCacheZSet(entityId, type, productNr));
	}

//	private static Map<String, String> converMapToString(Map<String, String> datas) {
//		try {
//			for (Entry<String, String> data : datas.entrySet()) {
//				if (data.getValue().getClass() != String.class) {
//					datas.put(data.getKey(), (String) data.getValue());
//				}
//			}
//		} catch (Exception e) {
//			System.out.println(e.getMessage());
//		}
//		return datas;
//	}
}

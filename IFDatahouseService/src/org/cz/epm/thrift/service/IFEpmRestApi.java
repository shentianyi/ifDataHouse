package org.cz.epm.thrift.service;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.TimeZone;

import org.apache.thrift.TException;
import org.cz.epm.conf.ApiConf;
import org.cz.epm.conf.Conf;
import org.cz.epm.thrift.generated.ProductInspectHandledType;
import org.cz.epm.util.HttpRequestUtil;

public class IFEpmRestApi {

	private final static String method = "POST";
	private final static String contentType = "application/json";
	private static String kpiEntryUrl = "";
	private static DateFormat format = new SimpleDateFormat(
			"yyyy-MM-dd HH:00:00");
	private static Map<String, String> apiEntiyId;
	private final static Map<String, String> apiKpiId = new HashMap() {
		{
			// kpi_name, kpi_id
			put("WorkerAttendanceKpi", "1"); // 出勤人数KPI
			put("ProductTestTotalQuantityKpi", "2"); // 产品总测试数量KPI
			put("ProductTestPassQuantityKpi", "3"); // 产品测试通过数量KPI
			put("ProductTestFailQuantityKpi", "4"); // 产品测试失败数量KPI
			put("ProductQuantityKpi", "5"); // 产品产量 KPI
			put("WorkerAttendanceTimeKpi", "6"); // 出勤总时间KPI
			put("ProductTotalTargetTimeKpi", "7"); // 产品理论生产总时间KPI
		}
	};

	private static Map<String, String> mongoEntityId = null;
	private static Set<String> mongoEntityIdSet = null;
	static {
		if (mongoEntityId == null) {
			synchronized (IFEpmRestApi.class) {
				if (mongoEntityId == null) {
					apiEntiyId = ApiConf.getEntity();
					mongoEntityId = getEntityIds();
					mongoEntityIdSet = new HashSet<String>(
							mongoEntityId.values());
				}
				kpiEntryUrl = Conf.getIfEpmUrl() + "api/kpi_entries/entry";
				// format.setTimeZone(TimeZone.getTimeZone("UTC"));
			}
		}
	}

	// 出勤人数KPI
	public static void AddWorkerAttendanceKpiEntry(Date entry_at)
			throws Exception {
		// System.out.println(mongoEntityId);
		// System.out.println(mongoEntityIdSet);
		Map<String, Long> values = EpmDataBase
				.GetCurrentOnJobWorkerNums(mongoEntityIdSet);
		DoApiRequest(values, "WorkerAttendanceKpi", entry_at);
	}

	// 产品总测试数量KPI
	public static void AddProductTestTotalQuantityKpiEntry(long startTime,
			long endTime, Date entry_at) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime, null);
		DoApiRequest(values, "ProductTestTotalQuantityKpi", entry_at);
	}

	// 产品测试通过数量KPI
	public static void AddProductTestPassQuantityKpiEntry(long startTime,
			long endTime, Date entry_at) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime,
				ProductInspectHandledType.FIRSTPASS);
		DoApiRequest(values, "ProductTestPassQuantityKpi", entry_at);
	}

	// 产品测试失败数量KPI
	public static void AddProductTestFailQuantityKpiEntry(long startTime,
			long endTime, Date entry_at) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime,
				ProductInspectHandledType.FAIL);
		DoApiRequest(values, "ProductTestFailQuantityKpi", entry_at);
	}

	// 产品产量 KPI
	public static void AddProductQuantityKpiEntry(long startTime, long endTime,
			Date entry_at) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductOutputCount(
				mongoEntityIdSet, startTime, endTime);
		DoApiRequest(values, "ProductQuantityKpi", entry_at);
	}

	// 出勤总时间KPI
	public static void AddWorkerAttendanceTimeKpiEntry(long startTime,
			long endTime, Date entry_at) throws Exception {
		Map<String, Long> values = EpmDataBase.GetOnJobTotalTime(
				mongoEntityIdSet, startTime, endTime);
		DoApiRequest(values, "WorkerAttendanceTimeKpi", entry_at);
	}

	// 产品理论生产总时间KPI
	public static void AddProductTotalTargetTimeKpiEntry(long startTime,
			long endTime, Date entry_at) throws Exception {
		Map<String, Double> values = new HashMap<String, Double>();
		for (Entry<String, String> entry : mongoEntityId.entrySet()) {
			String entityId = entry.getValue();
			Set<Map<String, String>> partQuantityAndProTime = EpmDataBase
					.GetProductOutputNumAndTime(entityId, startTime, endTime);
			double time = 0;

			for (Map<String, String> partQT : partQuantityAndProTime) {
				time += Double.parseDouble(partQT.get("unitTime"))
						* Long.parseLong(partQT.get("quantity"));
			}
			values.put(entityId, time);
		}
		DoApiRequest(values, "ProductTotalTargetTimeKpi", entry_at);
	}

	private static <T> void DoApiRequest(Map<String, T> values, String kpiName,
			Date entry_at) throws Exception {
		String kpiId = apiKpiId.get(kpiName);
		for (Entry<String, String> entry : mongoEntityId.entrySet()) {
			Map params = generateKpiEntryApiParams(kpiId,
					apiEntiyId.get(entry.getKey()),
					values.get(entry.getValue()), entry_at);
			HttpRequestUtil request = new HttpRequestUtil();
			request.doRequest(kpiEntryUrl, method, contentType, params);
		}
	}

	private static Map getEntityIds() {
		Map<String, String> ids = new HashMap();
		int i = 0;
		for (final Entry<String, String> entry : apiEntiyId.entrySet()) {
			Map entity = DatahouseBase.GetEntity(new HashMap<String, String>() {
				{
					put("entityNr", entry.getKey());
				}
			}, "_id");
			if (entity != null && entity.get("_id") != null) {
				ids.put(entry.getKey(), entity.get("_id").toString());
			}
		}
		return ids;
	}

	private static <T> Map generateKpiEntryApiParams(final String kpiId,
			final String entityId, final T value, final Date entry_at) {
		Map<String, String> params = new HashMap() {
			{
				put("kpi_id", kpiId);
				put("entity_id", entityId);
				if (entry_at == null)
					put("entry_at", format.format(new Date()));
				else
					put("entry_at", format.format(entry_at));
				// put("value", Double.toString(value));
				// if(value instanceof Double){
				put("value", value.toString());
				// }
			}
		};
		return params;
	}

}

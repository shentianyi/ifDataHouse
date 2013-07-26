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

import org.apache.thrift.TException;
import org.cz.epm.conf.Conf;
import org.cz.epm.thrift.generated.ProductInspectHandledType;
import org.cz.epm.util.HttpRequestUtil;

public class IFEpmRestApi {

	private final static String method = "POST";
	private final static String contentType = "application/json";
	private static String kpiEntryUrl = "";
	private final static DateFormat format = new SimpleDateFormat(
			"yyyy-MM-dd HH:00:00");
	private final static Map<String, String> apiEntiyId = new HashMap() {
		{
			// entity_nr, entity_id
			put("C-RBA","2");
			put("G-RBA","3");
			put("E-RBA","4");
			put("C-COC","5");
			put("G-COC","6");
			put("E-COC","7");
			put("C-MRA","8");
			put("G-MRA","9");
			put("E-MRA","10");
			put("Minor","11");
			put("Motor","12");
			put("NCV2-COC","13");
			put("NCV2-INR","14");
			put("NCV2-MRA","15");
			put("NCV2-Minor","16");
			put("NCV3-COC","17");
			put("NCV3-BODY","18");
			put("NCV3-ROOF","19");
			put("NCV3-Minor","20");
			put("C-G-COC","21");
			put("C-G-MRA","22");
			put("NCV2-3","23");
		}
	};
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
					mongoEntityId = getEntityIds();
					mongoEntityIdSet = new HashSet<String>(
							mongoEntityId.values());
				}
				kpiEntryUrl = Conf.getIfEpmUrl() + "api/kpi_entries/entry";
			}
		}
	}

	// 出勤人数KPI
	public static void AddWorkerAttendanceKpiEntry() throws Exception {
		 System.out.println(mongoEntityId);
		 System.out.println(mongoEntityIdSet);
		Map<String, Long> values = EpmDataBase
				.GetCurrentOnJobWorkerNums(mongoEntityIdSet);
		DoApiRequest(values, "WorkerAttendanceKpi");
	}

	// 产品总测试数量KPI
	public static void AddProductTestTotalQuantityKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductOriOutputCount(
				mongoEntityIdSet, startTime, endTime);
		DoApiRequest(values, "ProductTestTotalQuantityKpi");
	}

	// 产品测试通过数量KPI
	public static void AddProductTestPassQuantityKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime,
				ProductInspectHandledType.FIRSTPASS);
		DoApiRequest(values, "ProductTestPassQuantityKpi");
	}

	// 产品测试失败数量KPI
	public static void AddProductTestFailQuantityKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime,
				ProductInspectHandledType.FAIL);
		DoApiRequest(values, "ProductTestFailQuantityKpi");
	}

	// 产品产量 KPI
	public static void AddProductQuantityKpiEntry(long startTime, long endTime)
			throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductOutputCount(
				mongoEntityIdSet, startTime, endTime);
		DoApiRequest(values, "ProductQuantityKpi");
	}

	// 出勤总时间KPI
	public static void AddWorkerAttendanceTimeKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values = EpmDataBase.GetOnJobTotalTime(
				mongoEntityIdSet, startTime, endTime);
		DoApiRequest(values, "WorkerAttendanceTimeKpi");
	}

	// 产品理论生产总时间KPI
	public static void AddProductTotalTargetTimeKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values=new HashMap<String,Long>();
		for (Entry<String, String> entry : mongoEntityId.entrySet()) {
			String entityId = entry.getValue();
			Set<Map<String, String>> partQuantityAndProTime = EpmDataBase
					.GetProductOutputNumAndTime(entityId, startTime, endTime);
			long time = 0;
			for (Map<String, String> partQT : partQuantityAndProTime) {
				time += Long.parseLong(partQT.get("unitTime"))
						* Long.parseLong(partQT.get("quantity"));
			}
			values.put(entityId, time);
		}
		DoApiRequest(values, "ProductTotalTargetTimeKpi");
	}

	private static void DoApiRequest(Map<String, Long> values, String kpiName)
			throws Exception {
		String kpiId = apiKpiId.get(kpiName);
		for (Entry<String, String> entry : mongoEntityId.entrySet()) {
			Map params = generateKpiEntryApiParams(kpiId,
					apiEntiyId.get(entry.getKey()),
					values.get(entry.getValue()));
			HttpRequestUtil request = new HttpRequestUtil();
			request.doRequest(kpiEntryUrl, method, contentType, params);
		}
	}

	private static Map getEntityIds() {
		Map<String, String> ids = new HashMap();
		int i = 0;
		for (final Entry<String, String> entry : apiEntiyId.entrySet()) {
			ids.put(entry.getKey(),
					DatahouseBase.GetEntity(new HashMap<String, String>() {
						{
							put("entityNr", entry.getKey());
						}
					}, "_id").get("_id").toString());
		}
		return ids;
	}

	private static Map generateKpiEntryApiParams(final String kpiId,
			final String entityId, final Long value) {
		Map<String, String> params = new HashMap() {
			{
				put("kpi_id", kpiId);
				put("entity_id", entityId);
				put("entry_at", format.format(new Date()));
				put("value", Long.toString(value));
			}
		};
		return params;
	}

}

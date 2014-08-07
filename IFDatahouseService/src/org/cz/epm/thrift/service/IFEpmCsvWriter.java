package org.cz.epm.thrift.service;

import java.math.BigDecimal;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
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
import org.cz.epm.util.CsvUtil;
import org.cz.epm.util.HttpRequestUtil;

public class IFEpmCsvWriter {

	private static DateFormat format = new SimpleDateFormat("yyyy-MM-dd");
	private static Map<String, String> apiEntiyId;
	private final static Map<String, String> apiKpiId = new HashMap() {
		{
			// kpi_name, kpi_id
			put("产品总测试数量KPI", "2"); // 产品总测试数量KPI
			put("产品测试通过数量KPI", "3"); // 产品测试通过数量KPI
			put("产品测试失败数量KPI", "4"); // 产品测试失败数量KPI
			put("产品产量KPI", "5"); // 产品产量KPI
			put("产品理论生产总时间KPI", "7"); // 产品理论生产总时间KPI
		}
	};

	private static Map<String, String> mongoEntityId = null;
	private static Set<String> mongoEntityIdSet = null;
	static {
		if (mongoEntityId == null) {
			synchronized (IFEpmCsvWriter.class) {
				if (mongoEntityId == null) {
					apiEntiyId = ApiConf.getEntity();
					mongoEntityId = getEntityIds();
					mongoEntityIdSet = new HashSet<String>(
							mongoEntityId.values());
				}
			}
		}
	}

	public static void StartWrite(Calendar cal) {
		// TODO Auto-generated method stub
		try {
			// cal.add(Calendar.MINUTE, -1);
			long endTime = cal.getTimeInMillis();
			cal.add(Calendar.DATE, -1);
			long startTime = cal.getTimeInMillis();
			Map<String, Map> data = new HashMap();
			data.put("产品总测试数量KPI", IFEpmCsvWriter
					.AddProductTestTotalQuantityKpiEntry(startTime, endTime));
			data.put("产品测试通过数量KPI", IFEpmCsvWriter
					.AddProductTestPassQuantityKpiEntry(startTime, endTime));
			data.put("产品测试失败数量KPI", IFEpmCsvWriter
					.AddProductTestFailQuantityKpiEntry(startTime, endTime));
			data.put("产品产量KPI", IFEpmCsvWriter.AddProductQuantityKpiEntry(
					startTime, endTime));
			data.put("产品理论生产总时间KPI", IFEpmCsvWriter
					.AddProductTotalTargetTimeKpiEntry(startTime, endTime));
			DoKPIEntryWriteCSV(data, format.format(cal.getTime()));
		} catch (Exception e) {
			e.printStackTrace();
			System.out.println(e);
		}
	}

	// 产品总测试数量KPI
	private static Map AddProductTestTotalQuantityKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime, null);
		return values;
	}

	// 产品测试通过数量KPI
	private static Map AddProductTestPassQuantityKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime,
				ProductInspectHandledType.FIRSTPASS);
		return values;
	}

	// 产品测试失败数量KPI
	private static Map AddProductTestFailQuantityKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductInspectCount(
				mongoEntityIdSet, startTime, endTime,
				ProductInspectHandledType.FAIL);
		return values;
	}

	// 产品产量 KPI
	private static Map AddProductQuantityKpiEntry(long startTime, long endTime)
			throws Exception {
		Map<String, Long> values = EpmDataBase.GetProductOutputCount(
				mongoEntityIdSet, startTime, endTime);
		return values;
	}

	// 产品理论生产总时间KPI
	private static Map AddProductTotalTargetTimeKpiEntry(long startTime,
			long endTime) throws Exception {
		Map<String, String> values = new HashMap<String, String>();
		for (Entry<String, String> entry : mongoEntityId.entrySet()) {
			String entityId = entry.getValue();
			Set<Map<String, String>> partQuantityAndProTime = EpmDataBase
					.GetProductOutputNumAndTime(entityId, startTime, endTime);
			double time = 0;

			for (Map<String, String> partQT : partQuantityAndProTime) {
				time += Double.parseDouble(partQT.get("unitTime"))
						* Long.parseLong(partQT.get("quantity"));
			}
			values.put(entityId, new BigDecimal(Double.toString(time)).toString());
		}
		return values;
	}

	private static <T> void DoKPIEntryWriteCSV(Map<String, Map> datas,
			String entry_at) throws Exception {
		List<String> header = Arrays.asList("EntityNr", "EntityID", "KPIID",
				"KPIName", "Date", "Value");
		List<List<String>> values = new ArrayList<List<String>>();
		for (Entry<String, Map> data : datas.entrySet()) {
			String kpiName = data.getKey();
			String kpiId = apiKpiId.get(kpiName);
			for (Entry<String, String> entry : mongoEntityId.entrySet()) {
				String entityNr = entry.getKey();
				String entityId = apiEntiyId.get(entityNr);
				List<String> value = new ArrayList<String>();
				value.add(entityNr);
				value.add(entityId);
				value.add(kpiId);
				value.add(kpiName);
				value.add(entry_at);
				value.add(data.getValue().get(entry.getValue()).toString());
				values.add(value);
			}
		}
		CsvUtil cu = new CsvUtil();
		cu.GenCsv("/webApps/ifEpmCsv/" + entry_at + ".csv", header, values, ";");
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

}

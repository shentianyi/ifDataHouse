package org.cz.epm.thrift.service;

import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.Map.Entry;

import org.apache.thrift.TException;
import org.cz.epm.resource.Mapper;
import org.cz.epm.thrift.generated.*;
import org.cz.epm.util.DataTransportLogger;

public class EpmDatahouseImpl implements Datahouse.Iface {

	DataTransportLogger log = DataTransportLogger.getLogger();

	@Override
	public void addAttendance(String accessKey, Map<String, String> dataMap) {
		// log.logger.info(dataMap);
		Mapper mapper = new Mapper(accessKey);
		dataMap.put("entityId",
				mapper.GetMapKey("entity", dataMap.get("entityId")));
		dataMap.put("staffId",
				mapper.GetMapKey("staff", dataMap.get("staffId")));

		if (EpmDataBase.AddAttendance(dataMap)) {
			EpmDataBase.AddAttendanceCountCache(dataMap.get("entityId"),
					Long.parseLong(dataMap.get("type")));
			EpmDataBase.AddStaffAttendanceLocus(dataMap.get("entityId"),
					dataMap.get("staffId"));
		}
	}

	@Override
	public void addAttendances(String accessKey, Map<String, String> dataMap) {
		// System.out.println(dataMap.remove("staffIds"));
		try {
			String[] staffIds = dataMap.remove("staffIds").split(",");
			Mapper mapper = new Mapper(accessKey);
			dataMap.put("entityId",
					mapper.GetMapKey("entity", dataMap.get("entityId")));
			if (dataMap.get("entityId") != null) {
				for (int i = 0; i < staffIds.length; i++) {
					dataMap.put("staffId",
							mapper.GetMapKey("staff", staffIds[i]));
					if (dataMap.get("staffId") != null) {
						if (EpmDataBase.AddAttendance(dataMap)) {
							EpmDataBase.AddAttendanceCountCache(
									dataMap.get("entityId"),
									Long.parseLong(dataMap.get("type")));
							EpmDataBase.AddStaffAttendanceLocus(
									dataMap.get("entityId"),
									dataMap.get("staffId"));
						}
					}
				}
			}
		} catch (Exception e) {
			System.out.println(e.getMessage());
		}
	}

	@Override
	public void addProductPack(String accessKey, Map<String, String> dataMap)
			throws TException {
		System.out.println(dataMap);
		// log.logger.info(dataMap);
		Mapper mapper = new Mapper(accessKey);
		String partId = mapper.GetMapKey("part", dataMap.get("partId"));
		Map part = EpmDataBase.GetPart(partId, "entity_id");
		if (part != null && part.get("entity_id") != null) {
			dataMap.put("entityId", part.get("entity_id").toString());
			dataMap.put("partId", partId);
			dataMap.put("state", Integer.toString(ProductState.PACK.getValue()));
			dataMap.put("outputTime", dataMap.get("packTime"));
			if (EpmDataBase.AddProduct(dataMap)) {
				// EpmDataBase.AddProductOutputCache(dataMap.get("entityId"),
				// Long.parseLong(dataMap.get("outputTime")),
				// dataMap.get("productNr"));
			}
		}
	}

	@Override
	public void setProductInspectState(String accessKey,
			Map<String, String> dataMap) throws TException {
		// EpmDataBase.SetProductInspectState(dataMap);
	}

	@Override
	public void addProductInspect(String accessKey, Map<String, String> dataMap)
			throws TException {
		// log.logger.info(dataMap);
		try {
			EpmDataBase.AddProudctInspectOriRecord(dataMap);
			Mapper mapper = new Mapper(accessKey);
			// some TSK table number is lower case, convert it to upper case
			String workstationId = mapper.GetMapKey("entity",
					dataMap.get("entityId").toUpperCase());
			dataMap.put("workstationId", workstationId);

			// Map entity = EpmDataBase.GetEntity(workstationId,
			// "entity_id");
			// dataMap.put("entityId", entity.get("entity_id").toString());
			dataMap.put("inspectTime",Long.toString(new Date().getTime()));
			String partId = mapper.GetMapKey("part", dataMap.get("partNr"));
			Map part = EpmDataBase.GetPart(partId, "entity_id");
			if (part != null && part.get("entity_id") != null) {
				dataMap.put("entityId", part.get("entity_id").toString());
				dataMap.put("partId", partId);
				if (EpmDataBase.AddProductInspectRecord(dataMap)) {
					// EpmDataBase.AddProductInspectTimeCache(dataMap.get("entityId"),
					// Long.parseLong(dataMap.get("inspectTime")),dataMap.get("productNr"));
					// EpmDataBase.AddProductOriOutputCache(dataMap.get("entityId"),
					// Long.parseLong(dataMap.get("inspectTime")),dataMap.get("productNr"));
					// setProductInspectState(accessKey, dataMap);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void addPlanTarget(String accessKey, Map<String, String> dataMap)
			throws TException {
		// log.logger.info(dataMap);

		Mapper mapper = new Mapper(accessKey);
		if (dataMap.containsKey("entityId"))
			dataMap.put("entityId",
					mapper.GetMapKey("entity", dataMap.get("entityId")));
		if (dataMap.containsKey("partId"))
			dataMap.put("partId",
					mapper.GetMapKey("part", dataMap.get("partId")));
		EpmDataBase.AddPlanTarget(dataMap);
	}

	@Override
	public Map<String, String> getPlanTarget(String accessKey,
			Map<String, String> dataMap) throws TException {
		Mapper mapper = new Mapper(accessKey);
		if (dataMap.containsKey("entityId"))
			dataMap.put("entityId",
					mapper.GetMapKey("entity", dataMap.get("entityId")));
		Map<String, String> result = EpmDataBase.GetPlanTarget(dataMap);
		if (result.containsKey("entityId"))
			result.put("entityId",
					mapper.GetMapValue("entity", result.get("entityId")));
		return converMapToString(result);
	}

	@Override
	public void updatePlanTarget(String accessKey, Map<String, String> query,
			Map<String, String> dataMap) throws TException {
		Mapper mapper = new Mapper(accessKey);
		if (dataMap.containsKey("entityId"))
			dataMap.put("entityId",
					mapper.GetMapKey("entity", dataMap.get("entityId")));
		EpmDataBase.UpdatePlanTarget(query, dataMap);
	}

	@Override
	public Map<String, Long> getCurrentOnJobWorkerNums(String accessKey,
			Set<String> entityIds) throws TException {
		Map<String, String> keyV = this.getMapEntityIds(accessKey, entityIds);
		return convertMapValue(keyV,
				EpmDataBase.GetCurrentOnJobWorkerNums(new HashSet<String>(keyV
						.values())));
	}

	@Override
	public void addOperatingState(String accessKey, Map<String, String> dataMap)
			throws TException {
		Mapper mapper = new Mapper(accessKey);
		dataMap.put("entityId",
				mapper.GetMapKey("entity", dataMap.get("entityId")));

		EpmDataBase.AddOperatingState(dataMap);
	}

	@Override
	public Map<String, Long> getOriProductOutputNums(String accessKey,
			Set<String> entityIds, long startTime, long endTime)
			throws TException {
		Map<String, String> keyV = this.getMapEntityIds(accessKey, entityIds);
		return convertMapValue(keyV, EpmDataBase.GetProductInspectCount(
				entityIds, startTime, endTime, null));
	}

	@Override
	public Map<String, Long> getFTRProductNums(String accessKey,
			Set<String> entityIds, long startTime, long endTime)
			throws TException {
		Map<String, String> keyV = this.getMapEntityIds(accessKey, entityIds);

		return convertMapValue(keyV, EpmDataBase.GetProductInspectCount(
				new HashSet<String>(keyV.values()), startTime, endTime,
				ProductInspectHandledType.FIRSTPASS));
	}

	@Override
	public Map<String, Long> getFailProductInspectNums(String accessKey,
			Set<String> entityIds, long startTime, long endTime)
			throws TException {
		Map<String, String> keyV = this.getMapEntityIds(accessKey, entityIds);
		return convertMapValue(keyV, EpmDataBase.GetProductInspectCount(
				new HashSet<String>(keyV.values()), startTime, endTime,
				ProductInspectHandledType.FAIL));
	}

	@Override
	public Map<String, Long> getProductOutputNums(String accessKey,
			Set<String> entityIds, long startTime, long endTime)
			throws TException {
		Map<String, String> keyV = this.getMapEntityIds(accessKey, entityIds);
		return convertMapValue(
				keyV,
				EpmDataBase.GetProductOutputCount(
						new HashSet<String>(keyV.values()), startTime, endTime));
	}

	@Override
	public long getProductOutputNumsByPartId(String accessKey, String entityId,
			String partId, String startTime, String endTime) throws TException {
		Mapper mapper = new Mapper(accessKey);
		entityId = mapper.GetMapKey("entity", entityId);
		partId = mapper.GetMapKey("part", partId);
		return EpmDataBase.CountProduct(entityId, partId, startTime, endTime);
	}

	@Override
	public Map<String, Long> getOnJobTotalTimes(String accessKey,
			Set<String> entityIds, long startTime, long endTime)
			throws TException {
		Map<String, String> keyV = this.getMapEntityIds(accessKey, entityIds);

		return convertMapValue(keyV, EpmDataBase.GetOnJobTotalTime(
				new HashSet<String>(keyV.values()), startTime, endTime));
	}

	@Override
	public Set<Map<String, String>> getProductOutputNumAndTime(
			String accessKey, String entityId, long startTime, long endTime)
			throws TException {
		Mapper mapper = new Mapper(accessKey);
		entityId = mapper.GetMapKey("entity", entityId);
		return convertSetMapValue(mapper,
				EpmDataBase.GetProductOutputNumAndTime(entityId, startTime,
						endTime), "part", "partId");
	}

	private Map<String, String> getMapEntityIds(String accessKey,
			Set<String> entityIds) {
		Mapper mapper = new Mapper(accessKey);
		return mapper.GetMapValues("entity", entityIds);
	}

	private <T> Map<String, T> convertMapValue(Map<String, String> keyV,
			Map<String, T> values) {
		Map<String, T> m = new HashMap<String, T>();
		for (Entry<String, String> key : keyV.entrySet()) {
			m.put(key.getKey().toString(), values.get(key.getValue()));
		}
		return m;
	}

	private Set<Map<String, String>> convertSetMapValue(Mapper mapper,
			Set<Map<String, String>> values, String model, String field) {
		for (Map<String, String> value : values) {
			value.put(field, mapper.GetMapValue(model, value.get(field)));
		}
		return values;
	}

	private Map<String, String> converMapToString(Map<?, ?> datas) {
		Map<String, String> result = new HashMap<String, String>();
		try {
			for (Entry<?, ?> data : datas.entrySet()) {
				result.put(data.getKey().toString(), data.getValue().toString());
			}
		} catch (Exception e) {
			System.out.println(">>" + e.getMessage());
		}
		return result;
	}

}

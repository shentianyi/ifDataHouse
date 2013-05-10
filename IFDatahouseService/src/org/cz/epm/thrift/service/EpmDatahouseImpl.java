package org.cz.epm.thrift.service;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.Map.Entry;

import org.apache.thrift.TException;
import org.cz.epm.data.manager.RedisManager;
import org.cz.epm.resource.Mapper;
import org.cz.epm.thrift.generated.*;

public class EpmDatahouseImpl implements Datahouse.Iface {
	// ********************************************************************
	// hbase service
	// ********************************************************************

	@Override
	public void addAttendance(String accessKey, Map<String, String> dataMap) {

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
	public void addProductPack(String accessKey, Map<String, String> dataMap)
			throws TException {
		Mapper mapper = new Mapper(accessKey);
		dataMap.put("entityId",
				mapper.GetMapKey("entity", dataMap.get("entityId")));
		dataMap.put("partId", mapper.GetMapKey("part", dataMap.get("partId")));

		dataMap.put("state", Integer.toString(ProductState.PACK.getValue()));
		dataMap.put("outputTime", dataMap.get("packTime"));
		if (EpmDataBase.AddProduct(dataMap)) {
			EpmDataBase.AddProductOutputCache(dataMap.get("entityId"),
					Long.parseLong(dataMap.get("outputTime")),
					dataMap.get("productNr"));
		}
	}

	@Override
	public void setProductInspectState(String accessKey,
			Map<String, String> dataMap) throws TException {
		EpmDataBase.SetProductInspectState(dataMap);
	}

	@Override
	public void addProductInspect(String accessKey, Map<String, String> dataMap)
			throws TException {
		Mapper mapper = new Mapper(accessKey);
		dataMap.put("entityId",
				mapper.GetMapKey("entity", dataMap.get("entityId")));

		if (EpmDataBase.AddProductInspectRecord(dataMap)) {
			EpmDataBase.AddProductInspectTimeCache(dataMap.get("entityId"),
					Long.parseLong(dataMap.get("inspectTime")),
					dataMap.get("productNr"));
			EpmDataBase.AddProductOriOutputCache(dataMap.get("entityId"),
					Long.parseLong(dataMap.get("inspectTime")),
					dataMap.get("productNr"));
			setProductInspectState(accessKey, dataMap);
		}
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
			Map<String, String> keyV = this.getMapEntityIds(accessKey,
					entityIds);
			return convertMapValue(keyV, EpmDataBase.GetProductOriOutputCount(
					new HashSet<String>(keyV.values()), startTime, endTime));
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
}

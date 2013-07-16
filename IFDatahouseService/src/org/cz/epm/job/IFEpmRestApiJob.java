package org.cz.epm.job;

import java.util.Calendar;
import java.util.Date;

import org.cz.epm.resource.Conf;
import org.cz.epm.thrift.service.IFEpmRestApi;
import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

public class IFEpmRestApiJob implements Job {

	@Override
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		// TODO Auto-generated method stub
		try {
			Calendar cal = Calendar.getInstance();
			long endTime = cal.getTimeInMillis();
			cal.add(Calendar.HOUR_OF_DAY, -24);
			long startTime = cal.getTimeInMillis();
			// 出勤人数KPI
			IFEpmRestApi.AddWorkerAttendanceKpiEntry();
			// 产品总测试数量KPI
			IFEpmRestApi
					.AddProductTestTotalQuantityKpiEntry(startTime, endTime);
			// 产品测试通过数量KPI
			IFEpmRestApi.AddProductTestPassQuantityKpiEntry(startTime, endTime);
			// 产品测试失败数量KPI
			IFEpmRestApi.AddProductTestFailQuantityKpiEntry(startTime, endTime);
			// 产品产量 KPI
			IFEpmRestApi.AddProductQuantityKpiEntry(startTime, endTime);
			// 出勤总时间KPI
			IFEpmRestApi.AddWorkerAttendanceTimeKpiEntry(startTime, endTime);
			// ProductTotalTargetTimeKpi
			IFEpmRestApi.AddProductTotalTargetTimeKpiEntry(startTime, endTime);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}

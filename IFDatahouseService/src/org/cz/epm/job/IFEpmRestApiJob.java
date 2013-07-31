package org.cz.epm.job;

import java.util.Calendar;
import java.util.Date;

import org.cz.epm.conf.Conf;
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
			cal.add(Calendar.HOUR, -1);
			long startTime = cal.getTimeInMillis();
			// 出勤人数KPI
			IFEpmRestApi.AddWorkerAttendanceKpiEntry(null);
			// 产品总测试数量KPI
			IFEpmRestApi
					.AddProductTestTotalQuantityKpiEntry(startTime, endTime,null);
			// 产品测试通过数量KPI
			IFEpmRestApi.AddProductTestPassQuantityKpiEntry(startTime, endTime,null);
			// 产品测试失败数量KPI
			IFEpmRestApi.AddProductTestFailQuantityKpiEntry(startTime, endTime,null);
			// 产品产量 KPI
			IFEpmRestApi.AddProductQuantityKpiEntry(startTime, endTime,null);
			// 出勤总时间KPI
			IFEpmRestApi.AddWorkerAttendanceTimeKpiEntry(startTime, endTime,null);
			// ProductTotalTargetTimeKpi
			IFEpmRestApi.AddProductTotalTargetTimeKpiEntry(startTime, endTime,null);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			System.out.println(e);
		}
	}

}

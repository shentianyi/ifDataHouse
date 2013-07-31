package org.cz.epm.job;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import org.cz.epm.conf.Conf;
import org.cz.epm.thrift.service.IFEpmRestApi;
import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

public class RedoIFEpmRestApiJob implements Job {

	@Override
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		// TODO Auto-generated method stub
		try {
			Conf.setIfRedoFlag(false);
			SimpleDateFormat format = new SimpleDateFormat(
					"yyyy-MM-dd hh:mm:ss");
			Calendar cal = Calendar.getInstance();
			cal.setTime(format.parse(Conf.getIfRedoStartDate()));
			Date end_date = format.parse(Conf.getIfRedoEndDate());
			while (!cal.getTime().equals(end_date)) {
				System.out.println(cal.getTime());
				long startTime = cal.getTimeInMillis();
				cal.add(Calendar.HOUR, 1);
				long endTime = cal.getTimeInMillis();
				// 出勤人数KPI
				IFEpmRestApi.AddWorkerAttendanceKpiEntry(cal.getTime());
				// 产品总测试数量KPI
				IFEpmRestApi.AddProductTestTotalQuantityKpiEntry(startTime,
						endTime, cal.getTime());
				// 产品测试通过数量KPI
				IFEpmRestApi.AddProductTestPassQuantityKpiEntry(startTime,
						endTime, cal.getTime());
				// 产品测试失败数量KPI
				IFEpmRestApi.AddProductTestFailQuantityKpiEntry(startTime,
						endTime, cal.getTime());
				// 产品产量 KPI
				IFEpmRestApi.AddProductQuantityKpiEntry(startTime, endTime,
						cal.getTime());
				// 出勤总时间KPI
				IFEpmRestApi.AddWorkerAttendanceTimeKpiEntry(startTime,
						endTime, cal.getTime());
				// ProductTotalTargetTimeKpi
				IFEpmRestApi.AddProductTotalTargetTimeKpiEntry(startTime,
						endTime, cal.getTime());
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			System.out.println(e);
		}
	}

}

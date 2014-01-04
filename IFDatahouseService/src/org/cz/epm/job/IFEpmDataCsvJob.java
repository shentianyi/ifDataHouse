package org.cz.epm.job;

import java.util.Calendar;
import java.util.Date;

import org.cz.epm.thrift.service.IFEpmCsvWriter;
import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

public class IFEpmDataCsvJob implements Job {
	@Override
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		IFEpmCsvWriter.StartWrite(Calendar.getInstance());
	}

	public void RegetPrev(int days) {
		System.out.println("---------START Generate Data For Previous: "+Integer.toString(days)+" days--");
		Calendar cal = Calendar.getInstance();
		for (int i = 0; i < days; i++) {
			IFEpmCsvWriter.StartWrite(cal);
			System.out.println(cal.getTime());
		}
		System.out.println("---------DONE-------------");
	}

}

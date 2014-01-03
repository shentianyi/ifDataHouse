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
		IFEpmCsvWriter.StartWrite();
	}

}

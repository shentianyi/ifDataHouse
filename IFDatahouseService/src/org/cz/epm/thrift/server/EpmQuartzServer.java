package org.cz.epm.thrift.server;

import static org.quartz.JobBuilder.newJob;
import static org.quartz.TriggerBuilder.newTrigger;
import static org.quartz.CronScheduleBuilder.dailyAtHourAndMinute;

import org.cz.epm.job.CleanRedisZSetCacheJob;
import org.cz.epm.job.IFEpmRestApiJob;
import org.quartz.JobDetail;
import org.quartz.Scheduler;
import org.quartz.SchedulerException;
import org.quartz.SchedulerFactory;
import org.quartz.SimpleTrigger;
import org.quartz.Trigger;
import org.quartz.impl.StdSchedulerFactory;
import static org.quartz.SimpleScheduleBuilder.simpleSchedule;

public class EpmQuartzServer {
	static Scheduler sched;

	public static void startServer() throws Exception {
		try {
			SchedulerFactory sf = new StdSchedulerFactory();
			sched = sf.getScheduler();
			// clean redis cache job
			JobDetail cleanRedisCacheJob = newJob(CleanRedisZSetCacheJob.class)
					.withIdentity("CleanRedisZSetCacheJob",
							"CleanRedisCacheJobGroup").build();
			Trigger cleanReisCacheTrigger = newTrigger()
					.withIdentity("CleanRedisCacheTrigger",
							"CleanRedisCacheTriggerGroup").startNow()
					.withSchedule(dailyAtHourAndMinute(0, 0)).build();
			// call epm rest api job
			JobDetail ifEpmApiJob = newJob(IFEpmRestApiJob.class).withIdentity(
					"IFEpmApiJob", "IFEpmApiEpmJobGroup").build();
 
			Trigger ifEpmApiTrigger = newTrigger()
					.withIdentity("IFEpmApiTrigger", "IFEpmApiTriggerGroup")
					.withSchedule(simpleSchedule().withIntervalInHours(1).repeatForever()).build();
//			Trigger ifEpmApiTrigger = newTrigger()
//					.withIdentity("IFEpmApiTrigger", "IFEpmApiTriggerGroup")
//					.withSchedule(simpleSchedule().withIntervalInMinutes(1).repeatForever()).build();
			
			sched.scheduleJob(cleanRedisCacheJob, cleanReisCacheTrigger);
			sched.scheduleJob(ifEpmApiJob, ifEpmApiTrigger);
			sched.start();
		} catch (Exception e) {
			e.printStackTrace();
			throw e;
		}
	}

	public static void stopServer() {
		try {
			if (sched != null) {
				System.out.println("stop quartz server");
				sched.shutdown();
			}
		} catch (SchedulerException e) {
			e.printStackTrace();
		}
	}

}

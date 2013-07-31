package org.cz.epm.thrift.server;

import static org.quartz.JobBuilder.newJob;
import static org.quartz.TriggerBuilder.newTrigger;
import static org.quartz.CronScheduleBuilder.dailyAtHourAndMinute;
import static org.quartz.DateBuilder.*;

import java.util.Calendar;
import org.cz.epm.conf.Conf;
import org.cz.epm.job.CleanRedisZSetCacheJob;
import org.cz.epm.job.IFEpmRestApiJob;
import org.quartz.JobDetail;
import org.quartz.Scheduler;
import org.quartz.SchedulerException;
import org.quartz.SchedulerFactory;
import org.quartz.Trigger;
import org.quartz.impl.StdSchedulerFactory;

import static org.quartz.SimpleScheduleBuilder.simpleSchedule;

public class EpmQuartzServer {
	static Scheduler sched;

	public static void startServer() throws Exception {
		try {
			SchedulerFactory sf = new StdSchedulerFactory();
			sched = sf.getScheduler();
			// call clean redis cache job
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
			// fire api data send next hour
			Calendar calendar = Calendar.getInstance();
			calendar.add(Calendar.HOUR, 1);
			Trigger ifEpmApiTrigger = newTrigger()
					.withIdentity("IFEpmApiTrigger", "IFEpmApiTriggerGroup")
					.startAt(dateOf(calendar.getTime().getHours(), 0, 0))
					.withSchedule(
							simpleSchedule().withIntervalInHours(1)
									.repeatForever()).build();
			// Trigger ifEpmApiTrigger = newTrigger()
			// .withIdentity("IFEpmApiTrigger",
			// "IFEpmApiTriggerGroup").startAt(dateOf(calendar.getTime().getHours(),
			// calendar.getTime().getMinutes(), 0))
			// .withSchedule(simpleSchedule().withIntervalInSeconds(3).repeatForever()).build();

		 
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

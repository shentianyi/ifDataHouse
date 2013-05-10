package org.cz.epm.thrift.server;

import static org.quartz.JobBuilder.newJob;
import static org.quartz.TriggerBuilder.newTrigger;
import static org.quartz.CronScheduleBuilder.dailyAtHourAndMinute;

import org.cz.epm.job.CleanRedisZSetCacheJob;
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
			JobDetail job = newJob(CleanRedisZSetCacheJob.class).withIdentity(
					"CleanRedisZSetCacheJob", "CleanRedisCacheGroup").build();
			Trigger trigger = newTrigger()
					.withIdentity("CleanRedisZSetCacheJobTrigger",
							"CleanRedisCacheTrigger").startNow()
					.withSchedule(dailyAtHourAndMinute(0, 0)).build();
			// Trigger trigger = newTrigger()
			// .withIdentity("CleanRedisZSetCacheJobTrigger",
			// "CleanRedisCacheTrigger")
			// .withSchedule(simpleSchedule().withIntervalInSeconds(2).repeatForever()).build();
			sched.scheduleJob(job, trigger);
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

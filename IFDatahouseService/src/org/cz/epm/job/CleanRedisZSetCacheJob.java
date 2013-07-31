package org.cz.epm.job;

import java.util.Calendar;
import java.util.Date;
import java.util.Set;

import org.cz.epm.conf.Conf;
import org.cz.epm.data.manager.RedisManager;
import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

public class CleanRedisZSetCacheJob implements Job {
	private final static String queue_name = "redis_zset_job_queue";

	public static void Enqueue(String zset_key) {
		if (RedisManager.ZScore(queue_name, zset_key) == null) {
			RedisManager.ZAdd(queue_name, new Date().getTime(), zset_key);
		}
	}

	public static Set<String> GetQueue() {
		return RedisManager.ZRange(queue_name, 0, -1);
	}

	@Override
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		Set<String> zsets = GetQueue();
		Calendar cal = Calendar.getInstance();
		cal.add(Calendar.DATE, 0 - Conf.getrCacheDefaultDay());
		long end = cal.getTimeInMillis();
		for (String zset : zsets) {
			 RedisManager.ZRemangeByScore(zset,0,end);
		}
	}
}

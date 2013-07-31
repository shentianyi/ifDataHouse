package org.cz.epm.data.manager;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

import org.cz.epm.resource.Conf;
import org.cz.epm.util.BaseUtil;

import com.trigonic.jedis.NamespaceJedisPool;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPoolConfig;

public class RedisManager {
	private static JedisPoolConfig conf;
	private static NamespaceJedisPool pool;
	private final static String UNIQ_ID_INCR_KEY = "EPM_UNIQ_ID_INCR_KEY";
	static {
		conf = new JedisPoolConfig();
		conf.setMaxActive(200);
		conf.setMaxIdle(50);
		conf.setMaxWait(10000);

		// conf.timeBetweenEvictionRunsMillis=
		pool = new NamespaceJedisPool(conf, Conf.getrHost(), Conf.getrPort())
				.withNamespace(Conf.getrNamespace());
	}

	private static Jedis getJedis() {
		Jedis jedis = pool.getResource();
		jedis.select(Conf.getrDb());
		return jedis;
	}
public static String HGet(String key,String field){
	Jedis jedis = getJedis();
	String value=jedis.hget(key, field);
	pool.returnResource(jedis);
	return value;
}
	public static Long ZAdd(String key, long score, String member) {
		Jedis jedis = getJedis();
		Long value =jedis.zadd(key, score, member);
		pool.returnResource(jedis);
		return value;
	}

	public static Double ZScore(String key, String member) {
		Jedis jedis = getJedis();
		Double value = jedis.zscore(key, member);
		pool.returnResource(jedis);
		return value;
	}

	public static Set<String> ZRange(String key, long start, long end) {
		Jedis jedis = getJedis();
		Set<String> value = jedis.zrange(key, start, end);
		pool.returnResource(jedis);
		return value;
	}
	
	public static Set<String> ZRangeByScore(String key, long min, long max) {
		Jedis jedis = getJedis();
		Set<String> value = jedis.zrangeByScore(key, min, max);
		pool.returnResource(jedis);
		return value;
	}
	
	public static Long ZRemangeByScore(String key, long start, long end){
		Jedis jedis = getJedis();
		Long value = jedis.zremrangeByScore(key, start, end);
		pool.returnResource(jedis);
		return value;
	}

	public static Long ZCount(String key, long min, long max) {
		Jedis jedis = getJedis();
		Long value = jedis.zcount(key, min, max);
		pool.returnResource(jedis);
		return value;
	}


	public static Long SAdd(String key,String... members) {
		Jedis jedis = getJedis();
		Long value = jedis.sadd(key,members);
		pool.returnResource(jedis);
		return value;
	}
	
	public static Set<String> SMembers(String key) {
		Jedis jedis = getJedis();
		Set<String> value = jedis.smembers(key);
		pool.returnResource(jedis);
		return value;
	}

	public static String GetIncr(String key) {
		Jedis jedis = getJedis();
		String value = jedis.get(key); 
		pool.returnResource(jedis);
		return value==null?"0":value;
	}
	
	public static Long Incr(String key,long incr) {
		Jedis jedis = getJedis();
		Long value = jedis.incrBy(key, incr);
		
		pool.returnResource(jedis);
		return value;
	}

	public static long generateID() {
		return getJedis().incr(UNIQ_ID_INCR_KEY);
	}

	public static Map<String, String> SaveHash(Map<String, String> mdata,
			String qualifier) {
		HashMap<String, String> result = null;
		try {
			String key = "";
			String id = "";
			if (mdata.containsKey("key")) {
				key = mdata.get("key");
			} else {
				id = Long.toString(generateID());
				if (qualifier != null)
					key = BaseUtil.GetCombainRow(qualifier, id);
				else
					key = id;
			}
			getJedis().hmset(key, mdata);
			result = new HashMap<String, String>();
			result.put(key, id);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return result;
	}
}

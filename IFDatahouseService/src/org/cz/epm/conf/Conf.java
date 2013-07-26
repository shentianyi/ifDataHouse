package org.cz.epm.conf;

import java.io.IOException;
import java.util.*;

public class Conf {
	private static Properties property;

	// thrift server conf
	private static int tPort = 9000;
	private static String rHost = "locahost";
	private static int rPort = 6379;
	private static int rDb = 0;
	private static String rNamespace = "";
	private static int rCacheDefaultDay = 7;

	// ifepm rest api
	private static String ifEpmAccessKey = "";
	private static String ifEpmUrl = "http://localhost:3000";

	// ifdatahouse map access key
	private static String ifDatahouseMapAccessKey = "";
	static {
		if (property == null) {
			synchronized (Conf.class) {
				if (property == null)
					property = new Properties();
				try {
					property.load(Conf.class.getClassLoader()
							.getResourceAsStream("config.properties"));
					// thrift port conf
					tPort = Integer.parseInt(getProperty("TPort"));

					// redis conf
					rHost = getProperty("RHost");
					rPort = Integer.parseInt(getProperty("RPort"));
					rDb = Integer.parseInt(getProperty("RDb"));
					rNamespace = getProperty("RNamespace");

					// cache conf
					rCacheDefaultDay = Integer
							.parseInt(getProperty("RZSetCacheDefaultDay"));

					// ifEpm rest api conf
					ifEpmAccessKey = getProperty("IFEpmAccessKey");
					ifEpmUrl = getProperty("IFEpmUrl");

					// ifDatahouse map access key
					ifDatahouseMapAccessKey = getProperty("IFDatahouseAccessKey");

				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}

	private static String getProperty(String key) {
		return property.getProperty(key);
	}

	public static int gettPort() {
		return tPort;
	}

	public static int getrPort() {
		return rPort;
	}

	public static String getrHost() {
		return rHost;
	}

	public static int getrDb() {
		return rDb;
	}

	public static String getrNamespace() {
		return rNamespace;
	}

	public static int getrCacheDefaultDay() {
		return rCacheDefaultDay;
	}

	public static String getIfEpmAccessKey() {
		return ifEpmAccessKey;
	}

	public static String getIfEpmUrl() {
		return ifEpmUrl;
	}

	public static String getIfDatahouseMapAccessKey() {
		return ifDatahouseMapAccessKey;
	}

}

package org.cz.epm.conf;

<<<<<<< HEAD
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
=======
>>>>>>> 6578ea1013fbd463dc76eb1a43553fa2f21d0620
import java.io.IOException;
import java.util.*;

public class Conf {
	private static Properties property;
<<<<<<< HEAD
	private static FileOutputStream fileOutPutStream;
=======

>>>>>>> 6578ea1013fbd463dc76eb1a43553fa2f21d0620
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
<<<<<<< HEAD
	private static boolean ifRedoFlag = false;
	private static String ifRedoStartDate = "";
	private static String ifRedoEndDate = "";
=======
>>>>>>> 6578ea1013fbd463dc76eb1a43553fa2f21d0620

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
<<<<<<< HEAD
					ifRedoFlag = Boolean
							.parseBoolean(getProperty("IFRedoFlag"));
					ifRedoStartDate = getProperty("IFRedoStartDate");
					ifRedoEndDate = getProperty("IFRedoEndDate");
=======

>>>>>>> 6578ea1013fbd463dc76eb1a43553fa2f21d0620
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

<<<<<<< HEAD
	private static void setProperty(String key, String value)
			throws IOException {
		try {
			// System.out.println(getProperty(key));
			property.setProperty(key, value);
			// //System.out.println(getProperty(key));
			// fileOutPutStream=new FileOutputStream("config.properties");
			// property.store(fileOutPutStream, "save property");
			// fileOutPutStream.close();
		} catch (Exception e) {
			System.out.println(e);
		}
	}

=======
>>>>>>> 6578ea1013fbd463dc76eb1a43553fa2f21d0620
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

<<<<<<< HEAD
	public static String getIfRedoStartDate() {
		return ifRedoStartDate;
	}

	public static String getIfRedoEndDate() {
		return ifRedoEndDate;
	}

	public static boolean isIfRedoFlag() {
		return ifRedoFlag;
	}

	public static void setIfRedoFlag(boolean ifRedoFlag) throws IOException {
		Conf.ifRedoFlag = ifRedoFlag;
		setProperty("IFRedoFlag", Boolean.toString(ifRedoFlag));
	}

=======
>>>>>>> 6578ea1013fbd463dc76eb1a43553fa2f21d0620
}

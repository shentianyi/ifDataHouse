package org.cz.epm.conf;

import java.io.IOException;
import java.util.*;

public class MongoConf {
	private static Properties property;

	// mongo conf
	private static String mHost = "localhost";
	private static int mPort = 27017;
	private static int mConPerHost = 10;
	private static int mConMutiplier = 5;
	private static String mDb = "ifdatadb";
	private static String mUser = "ifdataer";
	private static String mPwd = "ifdataer@";
	// mongo collection
	private final static String mPartInfoColl = "part_infos";
	private final static String mPartColl = "parts";
	private final static String mStaffColl = "staffs";
	private final static String mEntityColl = "entities";
	private final static String mAttendColl = "attendances";
	private final static String mProductColl = "products";
	private final static String mInspectColl = "inspects";
	private final static String mOperatingStateColl = "operating_states";
	private final static String mMapperItemColl = "mapper_items";
	private final static String mTarget = "targets";

	 
	static {
		if (property == null) {
			synchronized (MongoConf.class) {
				if (property == null)
					property = new Properties();
				try {
					property.load(MongoConf.class.getClassLoader()
							.getResourceAsStream("config.properties"));
				
					// mongo conf
					mHost = getProperty("MHost");
					mPort = Integer.parseInt(getProperty("MPort"));
					mConPerHost = Integer
							.parseInt(getProperty("MConnectionsPerHost"));
					mConMutiplier = Integer
							.parseInt(getProperty("MConnectionMultiplier"));
					mDb = getProperty("MDb");
					mUser = getProperty("MUsername");
					mPwd = getProperty("MPwd"); 
				 
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}

	private static String getProperty(String key) {
		return property.getProperty(key);
	}

 

	public static String getmHost() {
		return mHost;
	}

	public static int getmPort() {
		return mPort;
	}

	public static int getmConPerHost() {
		return mConPerHost;
	}

	public static int getmConMutiplier() {
		return mConMutiplier;
	}

	public static String getmDb() {
		return mDb;
	}

	public static String getmUser() {
		return mUser;
	}

	public static String getmPwd() {
		return mPwd;
	}

	public static String getMpartinfocoll() {
		return mPartInfoColl;
	}

	public static String getMpartcoll() {
		return mPartColl;
	}

	public static String getMattendcoll() {
		return mAttendColl;
	}

	public static String getMproductcoll() {
		return mProductColl;
	}

	public static String getMproductinspectcoll() {
		return mInspectColl;
	}

	public static String getMstaffcoll() {
		return mStaffColl;
	}

	public static String getMentitycoll() {
		return mEntityColl;
	}

	public static String getMoperatingstatecoll() {
		return mOperatingStateColl;
	}

	public static String getMmapperitemcoll() {
		return mMapperItemColl;
	}

	public static String getMtarget() {
		return mTarget;
	}

}

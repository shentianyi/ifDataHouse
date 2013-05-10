package org.cz.epm.thrift.server;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.Map.Entry;

import org.apache.thrift.transport.TTransportException;
import org.bson.types.ObjectId;
import org.cz.epm.data.manager.MongoManager;
import org.cz.epm.data.manager.RedisManager;
import org.cz.epm.resource.Conf;
import org.cz.epm.util.ServerUtil;

import com.mongodb.BasicDBObject;
import com.mongodb.DBCollection;
import com.mongodb.DBCursor;
import com.mongodb.DBObject;

public class EpmServerRunner {
	public static void main(String[] args) throws TTransportException {
		int retcode = 0;
		try {
			if (ServerUtil.PortAvailable(Conf.gettPort())) {
				EpmThriftServer.startServer();
				EpmQuartzServer.startServer();
			} else {
				System.out.println(Integer.toString(Conf.gettPort())
						+ " is not avaiable,please choose another port");
			}
			System.out.println("\n############## Press <Enter> to terminate. ###################");
			System.in.read();
			EpmThriftServer.stopServer();
			EpmQuartzServer.stopServer();
		} catch (Exception e) {
			e.printStackTrace();
			retcode = 1;
		}
		System.exit(retcode);
	}

	private static void testRedis() {
		for (int i = 0; i < 1000; i++) {
			System.out.print(i);
			RedisManager.ZAdd(
					"epm:redis-cache:entityId:E001:productinspecttime:zset:",
					(new Date()).getTime(), UUID.randomUUID().toString());
		}
	}
}

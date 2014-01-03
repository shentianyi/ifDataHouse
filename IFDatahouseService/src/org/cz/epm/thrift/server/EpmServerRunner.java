package org.cz.epm.thrift.server;

import org.apache.thrift.transport.TTransportException;
import org.cz.epm.conf.Conf;
import org.cz.epm.resource.ApiConf;
import org.cz.epm.util.ServerUtil;

public class EpmServerRunner {
	public static void main(String[] args) throws TTransportException {
//		System.out.print(ApiConf.getEntity());
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
}

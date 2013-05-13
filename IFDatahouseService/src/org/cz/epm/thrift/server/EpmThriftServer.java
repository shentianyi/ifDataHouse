package org.cz.epm.thrift.server;

import org.apache.thrift.TProcessorFactory;
import org.apache.thrift.protocol.TBinaryProtocol;
import org.apache.thrift.protocol.TCompactProtocol;
import org.apache.thrift.protocol.TProtocolFactory;
import org.apache.thrift.server.TNonblockingServer;
import org.apache.thrift.server.TServer;
import org.apache.thrift.transport.TFramedTransport;
import org.apache.thrift.transport.TNonblockingServerSocket;
import org.apache.thrift.transport.TNonblockingServerTransport;
import org.apache.thrift.transport.TTransportException;
import org.cz.epm.resource.Conf;
import org.cz.epm.thrift.generated.Datahouse;
import org.cz.epm.thrift.generated.Datahouse.Iface;
import org.cz.epm.thrift.service.EpmDatahouseImpl;

public class EpmThriftServer {
	private static final int PORT = Conf.gettPort();
	private static TServer server;

	public static void startServer() throws TTransportException {
		try {

			boolean compact = true;
			final Datahouse.Processor<Iface> processor = new Datahouse.Processor<Iface>(
					new EpmDatahouseImpl());
			final TNonblockingServerTransport serverTransport = new TNonblockingServerSocket(
					PORT, 2000);
			final TProcessorFactory processorFactory = new TProcessorFactory(
					processor);
			final TNonblockingServer.Args args = new TNonblockingServer.Args(
					serverTransport);

			final TFramedTransport.Factory outputTransportFactory = new TFramedTransport.Factory();
			final TProtocolFactory inputProtocolFactory = new TBinaryProtocol.Factory();
			final TProtocolFactory outputProtocolFactory = new TBinaryProtocol.Factory();
			args.processorFactory(processorFactory);
			args.outputTransportFactory(outputTransportFactory);
			args.inputProtocolFactory(inputProtocolFactory);
			args.outputProtocolFactory(outputProtocolFactory);
			args.transportFactory(outputTransportFactory);
			args.maxReadBufferBytes = Long.MAX_VALUE;
			
			server = new TNonblockingServer(args);
			System.out
					.println("**********  EPM Hbase Thrift Server if Firing up on <"
							+ PORT + "> *****************");
			final Runnable runnable = new Runnable() {
				@Override
				public void run() {
					server.serve();
				}
			};
			Thread t = new Thread(runnable);

			t.start();
		} catch (TTransportException e) {
			e.printStackTrace();
			throw e;
		} catch (RuntimeException e) {
			e.printStackTrace();
			throw e;
		} catch (Exception e) {
			e.printStackTrace();
			throw e;
		} finally {
			System.out.println("Exiting Thrift server.");
		}
	}

	public static void stopServer() {
		try {
			if (server != null) {
				System.out.println("stop thrift server");
				server.stop();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}

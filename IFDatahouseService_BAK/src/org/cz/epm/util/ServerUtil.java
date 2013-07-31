package org.cz.epm.util;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class ServerUtil {
	public static boolean PortAvailable(int port) {
		if (port < 0 || port > 65535) {
			throw new IllegalArgumentException("Invalid start port: " + port);
		}
		boolean portAva = true;
		ServerSocket socket = null;
		try {
			socket = new ServerSocket(port);
		} catch (IOException e) {
			portAva = false;
		} finally {
			if (socket != null)
				try {
					socket.close();
				} catch (IOException e) {
					throw new RuntimeException("You should handle this error.",
							e);
				}
		}
		return portAva;
	}
}

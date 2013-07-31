package org.cz.epm.util;

import org.apache.log4j.Logger;
import org.apache.log4j.xml.DOMConfigurator;

public class LogUtil {
	private final static Logger logger = Logger.getLogger(LogUtil.class);

	public static Logger getLogger() {
		return logger;
	}

	static {
		DOMConfigurator.configure("log4j.xml");		
	}
}

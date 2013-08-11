package org.cz.epm.util;
import java.io.IOException;
import java.util.Properties;

import org.apache.log4j.*;

public class DataTransportLogger {
  public  Logger logger;
  private static DataTransportLogger log;
  	private DataTransportLogger(){
  		logger=Logger.getLogger(DataTransportLogger.class.getName());
  	}
  	public static DataTransportLogger getLogger(){
  		if(log!=null)
  			return log;
  		else
  			return new DataTransportLogger();
  	}
}

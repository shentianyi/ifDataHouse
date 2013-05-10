package org.cz.epm.util;

import java.text.SimpleDateFormat;
import java.util.Date;

public class BaseUtil {
  private final static char rowSplitor='-';
  
  public static String GetCombainRow(String... args){
	  StringBuilder row=new StringBuilder();
	  for(String arg : args)
		  row.append(arg).append(rowSplitor);
	return row.substring(0,row.length()-1);	  
  }
  
  public static String GetDateToDayByMiSec(long millisecond){
	  SimpleDateFormat sforamt=new SimpleDateFormat("yyyyMMdd");
	  return sforamt.format(new Date(millisecond));
  }
}

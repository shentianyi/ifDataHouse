package org.cz.epm.util;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Map;
import java.util.Map.Entry;

public class HttpRequestUtil {
	private final String USER_AGENT = "Mozilla/5.0";

	private void doRequest(String url, String method, Map<String,String> params)
			throws Exception {
		URL obj = new URL(url);
		HttpURLConnection con=(HttpURLConnection)obj.openConnection();
		con.setRequestMethod(method);
		con.setRequestProperty("User-Agent", USER_AGENT);
		// send request
		con.setDoOutput(true);
		DataOutputStream wr=new DataOutputStream(con.getOutputStream());
		String paramStr=FormatParams(params);
		wr.writeBytes(paramStr );
		wr.flush();
		wr.close();
		
		int responseCode = con.getResponseCode();
		System.out.println("\nSending 'POST' request to URL : " + url);
		System.out.println("Post parameters : " + paramStr);
		System.out.println("Response Code : " + responseCode);
		
		BufferedReader in = new BufferedReader(
		        new InputStreamReader(con.getInputStream()));
		String inputLine;
		StringBuffer response = new StringBuffer();
 
		while ((inputLine = in.readLine()) != null) {
			response.append(inputLine);
		}
		in.close();
 
		//print result
		System.out.println(response.toString());
		
	}
	
	private String FormatParams(Map<String,String> params){
		String paramStr="?";
		for(Entry<String, String> param : params.entrySet()){
			paramStr+=(param.getKey()+"="+param.getValue()+"&");
		}
		return paramStr.substring(0, paramStr.length()-1);
	}
}

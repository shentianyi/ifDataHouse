package org.cz.epm.conf;

import java.util.HashMap;
import java.util.Map;
import org.apache.commons.configuration.Configuration;
import org.apache.commons.configuration.ConfigurationException;
import org.apache.commons.configuration.XMLConfiguration;

public class ApiConf {
	private static Configuration config;
	private static Map<String, String> entity;
	static {
		if (config == null) {
			synchronized (ApiConf.class) {
				if (config == null)
					try {
						entity=new HashMap();
						config = new XMLConfiguration("entity.xml");
						String[] nrs = config
								.getStringArray("entities.entity.nr");
						String[] ids = config
								.getStringArray("entities.entity.id");
						for (int i = 0; i < nrs.length; i++) {
							entity.put(nrs[i], ids[i]);
						}
					} catch (ConfigurationException e) {
						e.printStackTrace();
					}
			}
		}
	}

	public static Map<String, String> getEntity() {
		return entity;
	}
}

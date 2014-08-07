package org.cz.epm.util;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

import org.apache.commons.lang.StringUtils;

public class CsvUtil {
	public void GenCsv(String filePath, List<String> header,
			List<List<String>> values, String spliter) {
		try {

			File file = new File(filePath);

			// if file doesnt exists, then create it
			if (!file.exists()) {
				file.createNewFile();
			}

			// true = append file
			FileWriter fileWritter = new FileWriter(file);
			BufferedWriter bufferWritter = new BufferedWriter(fileWritter);

			//bufferWritter.newLine();
			// write header
			bufferWritter.write(StringUtils.join(header.toArray(), spliter));

			// write body
			for (final List<String> value : values) {
				bufferWritter.newLine();
				bufferWritter.write(StringUtils.join(value.toArray(), spliter));
			}
			bufferWritter.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}

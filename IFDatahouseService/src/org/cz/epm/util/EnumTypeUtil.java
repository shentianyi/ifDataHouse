package org.cz.epm.util;

import org.cz.epm.thrift.generated.ProductInspectHandledType;
import org.cz.epm.thrift.generated.ProductInspectType;

public class EnumTypeUtil
 {
	public static ProductInspectHandledType GetProductInspectHandledType(
			Double current, ProductInspectType changer) {
		ProductInspectHandledType returnT = null;
		if (changer == ProductInspectType.FAIL) {
			returnT = ProductInspectHandledType.FAIL;
		} else if (changer == ProductInspectType.PASS) {
			if (current == null
					|| current == ProductInspectHandledType.FIRSTPASS
							.getValue()) {
				returnT = ProductInspectHandledType.FIRSTPASS;
			} else if (current == ProductInspectHandledType.PASS.getValue()
					|| current == ProductInspectHandledType.FAIL.getValue()) {
				returnT = ProductInspectHandledType.PASS;
			}
		} else {
			returnT = ProductInspectHandledType.FAIL;
		}
		return returnT;
	}
}

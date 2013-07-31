package org.cz.epm.resource;

public enum MapperType {
	KeyValue("key:value", 0, "map_key", "map_value"), ValueKey("value:key", 1,
			"map_value", "map_key");
	private String identifier;
	private int index;
	private String kField;
	private String vField;

	private MapperType(String identifier, int index, String kField,
			String vField) {
		this.identifier = identifier;
		this.index = index;
		this.kField = kField;
		this.vField = vField;
	}

	public static String getMapperType(int index) {
		for (MapperType type : MapperType.values()) {
			if (type.getIndex() == index)
				return type.getIdentifier();
		} 
		return null;
	}

	public String getIdentifier() {
		return this.identifier;
	}

	public int getIndex() {
		return this.index;
	}

	public String getkField() {
		return kField;
	}

	public void setkField(String kField) {
		this.kField = kField;
	}

	public String getvField() {
		return vField;
	}

	public void setvField(String vField) {
		this.vField = vField;
	}

}

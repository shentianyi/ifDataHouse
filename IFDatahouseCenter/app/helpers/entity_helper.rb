module EntityHelper
  def get_entity_level_desc value
    EntityLevel.get_desc_by_value value
  end
    def get_entity_type_desc value
    EntityType.get_desc_by_value value
  end
end

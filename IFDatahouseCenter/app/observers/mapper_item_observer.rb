require 'mapper'

class MapperItemObserver < Mongoid::Observer
  observe :staff,:part,:entity
  
  def after_update record
    if record.respond_to?(:map_field)
      if  record.send("#{record.map_field}_changed?")
        MapperItem.where(:map_key=>record.id.to_s).update_all(:map_field_value=>record.send("#{record.map_field}"))
      end
    end
  end
  
  def after_destroy record
    MapperItem.destroy_all(:map_key=>record.id.to_s)
  end
end
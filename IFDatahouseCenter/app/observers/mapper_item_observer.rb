require 'mapper'

class MapperItemObserver < Mongoid::Observer
  observe :staff,:part,:entity,:part_info
  def after_update record
    puts record.to_json
    if record.respond_to?(:map_field)
      if  record.send("#{record.map_field}_changed?")
        # MapperItem.where(:map_key=>record.id.to_s).update_all(:map_field_value=>record.send("#{record.map_field}"))
        MapperItem.where(:map_key=>record.id.to_s).all.each do |mi|
          v=record.send("#{record.map_field}")
          if mi.map_field_value==record.send("#{record.map_field}_was") and mi.map_field_value==mi.map_value
            mi.update_attributes(:map_field_value=>v,:map_value=>v)
          else
            mi.update_attributes(:map_field_value=>v)
          end
        end
      end
    end
  end

  def after_destroy record
    MapperItem.destroy_all(:map_key=>record.id.to_s)
  end

  def after_create record
    mapper_model= record.class.name.downcase
    if Mapper::MHModels.has_value?(mapper_model)
        map_value=record.send(record.map_field)
        map_key=record.id.to_s
        Mapper.generate_mapper_items mapper_model,map_key,map_value,map_value
    end
  end
end
module BlockHelper
  def self.get_entity_down_block
    Proc.new { |line, item|
      line<<item.entityNr
      line<<item.name
      line<<item.contactStaff
      line<<(item.entity.nil? ? '' :item.entity.entityNr)
      line<<item.level
      line<<item.type
    }
  end

  def self.get_part_info_down_block
    Proc.new { |line, item|
      line<<item.partNr
      line<<item.name
      line<<item.orderNr
      line<<item.unitTime
    }
  end

  def self.get_part_down_block
    Proc.new { |line, item|
      line<<item.partNr
      line<<item.name
      line<<item.clientPartNr
      line<<item.orderNr
      line<<item.unitTime
      line<<item.entity.entityNr if item.entity
    }
  end

  def self.get_staff_down_block
    Proc.new { |line, item|
      line<<item.staffNr
      line<<item.name
      line<<item.title
      line<<item.email
      line<<item.contact
    }
  end

  def self.get_inspect_original_down_block
    Proc.new { |line, item|
      line<<item.partNr
      line<<item.productNr
      line<<item.entityId
      line<<item.type
      line<< Time.at(item.inspectTime.to_i/1000)
    }
  end
end
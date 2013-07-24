module BlockHelper
  def self.get_entity_down_block
    Proc.new{|line,item|
      line<<item.entityNr
      line<<item.name
      line<<item.contactStaff
      line<<(item.entity.nil? ? '':item.entity.entityNr)
      line<<item.level
      line<<item.type
    }
  end

  def self.get_part_down_block
    Proc.new {|line,item|
      line<<item.partNr
      line<<item.name
      line<<item.clientPartNr
      line<<item.orderNr
      line<<item.unitTime
      line<<item.entity.entityNr if item.entity
    }
  end

  def self.get_staff_down_block
    Proc.new{|line,item|
      line<<item.staffNr
      line<<item.name
      line<<item.title
      line<<item.email
      line<<item.contact
    }
  end
end
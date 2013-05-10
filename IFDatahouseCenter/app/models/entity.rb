
#encoding: utf-8
class Entity
  include Mongoid::Document
     include Mongoid::Timestamps
     field :entityNr
     
  def self.uniq
    ['entityNr']
  end

  def self.notNil
    uniq+['parentEntity','contactStaff',$UPMARKER]
  end

  def map_field
    "entityNr"
  end
end
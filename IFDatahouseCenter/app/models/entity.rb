#encoding: utf-8
class Entity
  include Mongoid::Document
  include Mongoid::Timestamps
  field :entityNr
  field :name
  field :contactStaff
  field :parent
  field :level, type:Integer
  field :type, type:Integer
  
  def self.uniq
    ['entityNr']
  end

  def self.notNil
    uniq+['parent','contactStaff','level','type',$UPMARKER]
  end

  def map_field
    "entityNr"
  end
end

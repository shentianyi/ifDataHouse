#encoding: utf-8
class Part
  include Mongoid::Document
  include Mongoid::Timestamps
  field :partNr
  field :name
  field :clientPartNr
  field :orderNr
  field :unitTime, type: Float
  def self.uniq
    ['partNr']
  end

  def self.notNil
    uniq+['unitTime',$UPMARKER]
  end

  def map_field
    'partNr'
  end
end
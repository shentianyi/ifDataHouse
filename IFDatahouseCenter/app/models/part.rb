#encoding: utf-8
class Part
  include Mongoid::Document
  include Mongoid::Timestamps
  field :partNr
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
#encoding: utf-8
class Part
  include Mongoid::Document
  include Mongoid::Timestamps
  field :partNr
  field :name
  field :clientPartNr
  
  belongs_to :entity
  def self.uniq
    ['partNr']
  end

  def self.csv_headers
    ['PartNr','Name','ClientPartNr','EntityNr',$UPMARKER]
  end

  def map_field
    'partNr'
  end
# 
  # def self.attr_filter
    # ["_id","created_at","updated_at"]
  # end
end
#encoding: utf-8
class PartInfo
  include Mongoid::Document
  include Mongoid::Timestamps
  field :partNr
  field :name
  field :orderNr
  field :unitTime, type: Float
  
  belongs_to :entity
  def self.uniq
    ['partNr']
  end

  def self.csv_headers
    ['PartNr','Name','OrderNr','UnitTime',$UPMARKER]
  end

  def map_field
    'partNr'
  end
# 
  # def self.attr_filter
    # ["_id","created_at","updated_at"]
  # end
end
#encoding: utf-8
class Entity
  include Mongoid::Document
  include Mongoid::Timestamps
  field :entityNr
  field :name
  field :contactStaff
  # field :parent
  field :level, type:Integer
  field :type, type:Integer
  field :entity_id
  # field :staff_id

  has_many :entities,dependent: :destroy
  belongs_to :entity
  def self.uniq
    ['entityNr']
  end

  def self.csv_headers
    ['EntityNr','Name','ContactStaff','Parent','Level','Type',$UPMARKER]
  end

  def map_field
    "entityNr"
  end

  # def self.attr_filter
    # ["_id","created_at","updated_at","entity_id"]
  # end
end

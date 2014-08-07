#encoding: utf-8
class Inspect
  include Mongoid::Document
  include Mongoid::Timestamps
  field :productNr
  field :partNr
  field :type, type:Integer
  field :entityId
  field :inspectTime
  field :created_at,type:Float

  # belongs_to :entity, :foreign_key=>'entityId'
 
end

#encoding: utf-8
class InspectOriginal
  include Mongoid::Document
  include Mongoid::Timestamps
  field :productNr
  field :partNr
  field :type
  field :entityId
  field :inspectTime
  field :created_at,type:Float

  # belongs_to :entity, :foreign_key=>'entityId'
 
end

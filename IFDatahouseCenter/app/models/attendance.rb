#encoding: utf-8
class Attendance
  include Mongoid::Document
  include Mongoid::Timestamps 
  field :entityId
  field :staffId 
  field :type, type:Integer
  field :attendTime
  field :created_at,type:Float

  # belongs_to :entity, :foreign_key=>:entityId
   
end

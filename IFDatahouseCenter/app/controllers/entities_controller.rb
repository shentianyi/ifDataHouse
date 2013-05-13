#encoding: utf-8
class EntitiesController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
  def updata
    block=Proc.new{|data|
      if entity=Entity.find_by(:entityNr=>data["parentEntity"])
        data["parentEntity"]=entity.id.to_s
        puts "**************************************#{entity.id.to_s}"
      end
    }
    super &block
  end
end

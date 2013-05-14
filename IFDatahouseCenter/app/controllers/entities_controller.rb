#encoding: utf-8
class EntitiesController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
  def updata
    block=Proc.new{|data|
      if entity=Entity.find_by(:entityNr=>data["parentEntity"])
        data["parentEntity"]=entity.id.to_s
      end
    }
    super &block
  end

  def new
    @levels=EntityLevel.all.collect{|m| [m.desc,m.value]}
    @types=EntityType.all.collect{|m| [m.desc,m.value]}
    super
  end

  def edit
    @levels=EntityLevel.all.collect{|m| [m.desc,m.value]}
    @types=EntityType.all.collect{|m| [m.desc,m.value]}
    # @level=@item.level
    # @type=@item.type
    super
  end
end

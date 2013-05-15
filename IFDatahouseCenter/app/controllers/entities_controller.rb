#encoding: utf-8
class EntitiesController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
  def updata
    super {|data,query,row,row_line|
      raise(ArgumentError,"行:#{row_line}, EnityNr/ Level/ Type 不能为空值") if row["EntityNr"].nil? or row["Level"].nil? or row["Type"].nil?
      data["entityNr"]=row["EntityNr"]
      data["name"]=row["Name"] if row["Name"]
      data["level"]=row["Level"]
      data["type"]=row["Type"]
      if  row["Parent"] and parent=Entity.find_by(:entityNr=>row["Parent"])
        data["entity_id"]=parent.id
      end
      if  row["ContactStaff"] and staff=Staff.find_by(:staffNr=>row["ContactStaff"])
        data["contactStaff"]=row["ContactStaff"]
      end
      if query
        query["entityNr"]=row["EntityNr"]
      end
    }
  end

  def new
    get_select
    super
  end

  def edit
    get_select
    super
  end

  def update
    @item= model.find(params[:id])
    params[@model]["entity_id"]=if parent=  Entity.find_by(:entityNr=>params[@model]["entity"])
    parent.id
    else
      ""
    end
    params[@model].delete("entity")
    respond_to do |format|
      if @item.update_attributes(params[@model])
        format.html { redirect_to @item, notice: 'item was successfully updated.' }
      else
        format.html { render action: "edit" }
      end
    end
  end

  private

  def get_select
    @levels=EntityLevel.all.collect{|m| [m.desc,m.value]}
    @types=EntityType.all.collect{|m| [m.desc,m.value]}
  end
end

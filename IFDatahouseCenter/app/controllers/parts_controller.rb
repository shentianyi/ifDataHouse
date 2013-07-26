#encoding: utf-8
class PartsController < ApplicationController
  before_filter :get_entities,:only=>[:new,:edit]
  def updata
    super {|data,query,row,row_line|
      raise(ArgumentError,"行:#{row_line}, PartNr/ UnitTime 不能为空值") if row["PartNr"].nil? or row["UnitTime"].nil?
      data["partNr"]=row["PartNr"]
      data["name"]=row["Name"] if row["Name"]
      data["clientPartNr"]=row["ClientPartNr"] if row["ClientPartNr"]
      data["orderNr"]=row["OrderNr"] if row["OrderNr"]
      data["unitTime"]=row["UnitTime"] if row["UnitTime"]
      if row["EntityNr"]
        if entity=Entity.find_by(:entityNr=>row["EntityNr"])
          data["entity_id"]=entity.id
        end
      end
      query["partNr"]=row["PartNr"]   if query
    }
  end

  private

  def get_entities
    @entities=Entity.where(:type=>EntityType::ProLine).all
  end
end

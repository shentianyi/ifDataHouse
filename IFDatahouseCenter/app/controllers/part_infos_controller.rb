#encoding: utf-8
class PartInfosController < ApplicationController
 

  def updata
    super {|data,query,row,row_line|
      raise(ArgumentError,"行:#{row_line}, PartNr/ UnitTime 不能为空值") if row["PartNr"].nil? or row["UnitTime"].nil?
      data["partNr"]=row["PartNr"]
      data["name"]=row["Name"] if row["Name"]
      data["orderNr"]=row["OrderNr"] if row["OrderNr"]
      data["unitTime"]=row["UnitTime"] if row["UnitTime"]
    
      if query
        query["partNr"]=row["PartNr"]
      end
    }
  end
 
end

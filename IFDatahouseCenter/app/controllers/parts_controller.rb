#encoding: utf-8
class PartsController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
  def updata
    super {|data,query,row,row_line|
      raise(ArgumentError,"行:#{row_line}, PartNr/ UnitTime 不能为空值") if row["PartNr"].nil? or row["UnitTime"].nil?
      data["partNr"]=row["PartNr"]
      data["name"]=row["Name"] if row["Name"]
      data["clientPartNr"]=row["ClientPartNr"] if row["ClientPartNr"]
      data["orderNr"]=row["OrderNr"] if row["OrderNr"]
      data["unitTime"]=row["UnitTime"] if row["UnitTime"]
      if query
        query["partNr"]=row["PartNr"]
      end
    }
  end

  def download
    super{|line,item|
      line<<item.partNr
      line<<item.name
      line<<item.clientPartNr
      line<<item.orderNr
      line<<item.unitTime
    }
  end
end

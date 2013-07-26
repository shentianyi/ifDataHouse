#encoding: utf-8
class StaffsController < ApplicationController

  def updata
    super {|data,query,row,row_line|
      raise(ArgumentError,"行:#{row_line}, StaffNr 不能为空值") if row["StaffNr"].nil?
      data["staffNr"]=row["StaffNr"]
      data["name"]=row["Name"] if row["Name"]
      data["title"]=row["Title"] if row["Title"]
      data["email"]=row["Email"] if row["Email"]
      data["contact"]=row["Contact"] if row["Contact"]
      if query
        query["staffNr"]=row["StaffNr"]
      end
    }
  end
 
end

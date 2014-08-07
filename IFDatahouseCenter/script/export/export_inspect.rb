path=File.join($DOWNLOADPATH,'inspect.csv')
File.open(path,'wb') do |f|
  f.puts ['productNr','partNr','type','time','created_at'].join($CSVSP)
  Inspect.all.each do |item|
    line=[]
    line<<item.productNr
    line<< item.partNr
    line<< item.type
    # line<< item.entity.nil? ? '' :  item.entity.entityNr
    line<<Time.at(item.inspectTime.to_i/1000)
    line<<Time.at(item.created_at.to_i/1000)
    f.puts line.join($CSVSP)
  end
end 
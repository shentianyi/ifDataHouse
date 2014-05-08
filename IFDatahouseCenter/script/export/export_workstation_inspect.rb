# rails runner script/export_workstation_inspect.rb entityNr
# entityNr eg. TSK051
path=File.join($DOWNLOADPATH, 'workstation_inspect.csv')
File.open(path, 'wb') do |f|
  f.puts ['productNr', 'partNr', 'type', 'time', 'created_at'].join($CSVSP)
  if entity=Entity.where(entityNr: ARGV[0]).first
    Inspect.where(entityId: "5199a4b88de3e84db600045d").all.each do |item|
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
end 
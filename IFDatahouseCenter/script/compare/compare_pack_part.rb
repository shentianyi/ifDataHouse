require 'csv'

i=1
j=0
dir = Rails.root.join('script','compare')
y=[]
n=[]
CSV.foreach(File.join(dir,'compare_pack_part.csv'),:headers=>true,:col_sep=>$CSVSP) do |row|
  if !part=Part.find_by(:partNr=>row["partNr"])
    j+=1
    n<< "#{i};#{row['partNr']};#{row['project']}"
    puts "#{i}-#{j}-#{row['partNr']} not exsits"
    
  else
       y<< "#{i};#{row['partNr']};#{row['project']};#{part.entity.entityNr}"
  end
  i+=1
end

File.open(File.join(dir,'compare_pack_part_lack.csv'),'wb') do |f|
  f.puts 'line;partNr;project'
  n.each do |item|
    f.puts item
  end
end


File.open(File.join(dir,'compare_pack_part_exsits.csv'),'wb') do |f|
  f.puts 'line;partNr;project;datahouse entity'
  y.each do |item|
    f.puts item
  end
end
puts "total:#{j}"


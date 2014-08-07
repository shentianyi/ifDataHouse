require 'csv'

 
dir = Rails.root.join('script','make')
t={}
CSV.foreach(File.join(dir,'time.csv'),:headers=>true,:col_sep=>$CSVSP) do |row|
  t[row['ParNr']]= (row["UnitTime"].to_f)*60000 if row["UnitTime"] and row["UnitTime"].length>0
end

File.open(File.join(dir,'compare_pack_part_lack.csv'),'wb') do |f| 
   f.puts "PartNr;Name;ClientPartNr;OrderNr;UnitTime;EntityNr;DOUPDATE"
CSV.foreach(File.join(dir,'part.csv'),:headers=>true,:col_sep=>$CSVSP) do |row|

  partNr=(row['PartNr'].to_s)[0,(row['PartNr'].to_s).length-2]
  puts partNr
  if t[partNr]
    f.puts [row['PartNr'],"",row['ClientPartNr'],"",t[partNr],row['EntityNr'],0].join($CSVSP)
  end
end
end
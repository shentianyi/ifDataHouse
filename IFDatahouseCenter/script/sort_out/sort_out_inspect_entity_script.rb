# require 'csv'
# 
# dir = Rails.root.join('script','sort_out')
# Dir.foreach(dir) do |file|
  # unless ['.','..'].include?(file)
    # if File.extname(file).downcase=='.csv'
      # csv_file=File.join(dir,file)
      # puts csv_file
      # CSV.foreach(csv_file,:headers=>true,:col_sep=>$CSVSP) do |row|
        # puts "> reset part:#{row["LeoniPN"][0..-3]}"
        # if part=Part.find_by(:partNr=>row["LeoniPN"][0..-3])
          # puts ">> reset part:#{row["LeoniPN"][0..-3]}"
          # if entity=Entity.find_by(:entityNr=>row["Harness"])
            # puts ">>> reset part entity :#{row["Harness"]}"
            # part.update_attributes(:entity_id=>entity.id)
          # end
        # end
      # end
    # end
  # end
# end

Inspect.all.each do |inspect|
  if entity=Entity.find_by(:id=>inspect.entityId)
    puts ">> #{inspect.entityId}:#{entity.entityNr}"
    inspect.update_attributes(:entity_id=>entity.id)
  end
end

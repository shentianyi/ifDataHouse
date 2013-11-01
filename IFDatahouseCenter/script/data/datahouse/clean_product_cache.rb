Entity.all.each_with_index do |entity,i|
 puts "#{i}.#{entity.entityNr}" 
 # clean entity product output cache
 puts $redis.del "redis-cache:product:output:zet:entityId:#{entity.id}:" 
end

Entity.all.each_with_index do |entity,i|
 puts "#{i}.#{entity.entityNr}" 
 # clean entity staff count cache (incr)
 puts $redis.del "redis-cache:attend:incr:entityId:#{entity.id}:"
 # clean entity staff locus cache (set)
 puts $redis.del "redis-cache:attend:staff:locus:set:entityId:#{entity.id}:"
end

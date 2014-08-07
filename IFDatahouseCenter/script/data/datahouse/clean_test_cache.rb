Entity.all.each_with_index do |entity,i|
 puts "#{i}.#{entity.entityNr}" 
 # clean entity inspect time cache
 puts $redis.del "redis-cache:productinspecttime:zet:entityId:#{entity.id}:"
 # clean entity inspect type cache
 puts $redis.del "redis-cache:productinspecttype:zet:entityId:#{entity.id}:"
 # clean entity inspect original cache
 puts $redis.del "redis-cache:product:ori:output:zet:entityId:#{entity.id}:" 
end

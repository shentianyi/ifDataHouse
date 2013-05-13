require 'redis'

r= Redis.new(:host => 'localhost',:port => '6379',:db=>5)
$redis=Redis::Namespace.new('ifdatahouse', :redis => r)
if defined?(PhusionPassenger)
  PhusionPassenger.on_event(:starting_worker_process) do |forked|
    if forked
      require 'redis'
      $redis.client.disconnect
      $redis=Redis::Namespace.new('ifdatahouse', :redis => r)
    end
  end
end
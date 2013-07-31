require 'thrift'
require 'thrift/transport/socket'
require 'thrift/protocol/binary_protocol'
require 'uuid'
require 'json'

class Time
  def to_ms
    (self.to_f * 1000.0).to_i
  end
end

Dir['data/*.rb','service/*'].each do |f|
  require_relative f
end

$access_key="diyeleS3oXBHTQohr5sgmA"
class Test
  def initialize
    @transport = ::Thrift::FramedTransport.new(::Thrift::Socket.new('localhost', '9001'))
    protocol=::Thrift::BinaryProtocol.new(@transport)
    @client= CZ::Epm::Thrift::Datahouse::Client.new(protocol)
    @transport.open
    # data
    @staffs=Staff.all
    @entities=Entity.all
    @parts=Part.all
  end

  def pick
    puts %Q{
   *********select method***********
   1. test_add_attendance() && test_get_current_worker_num()
   2. test_add_product_inspect() && test_add_product_pack()
   3. test_add_operating_state()
   4. test_get_origin_product_output_num() && test_get_product_output_num() && test_get_product_output_num_by_part()
   5. test_get_ftr_product_num() && test_get_fail_product_num()
   6. test_on_job_total_time()
   7. test_product_output_num_time()
   8. test_add_plan_target && test_get_plan_target
   }
    choose=gets.chomp
    case choose
    when "1"
      test_add_attendance_current_num
    when "2"
      test_add_product_inspect_pack
    when "3"
      test_add_operating_state
    when "4"
      test_get_ori_product_output_num
    when "5"
      test_get_ftr_fail_product_num
    when "6"
      test_on_job_total_time
    when "7"
      test_product_output_num_time
    when "8"
      test_add_get_plan_target
    end
  end

  #
  # test addAttendance
  #
  def test_add_attendance_current_num
    ids=@entities.map{|e| e.entityId}
    while true
      @entities.each do |entity|
        [1,-1].each do |type|
          @staffs.each do |staff|
            sleep(10)
            puts "staff:#{staff.staffNr}-entity:#{entity.entityId}-type:#{type}"
            @client.addAttendance($access_key,{"entityId"=>entity.entityId,"attendTime"=>(Time.now.to_ms).to_s,"staffId"=>staff.staffNr,"type"=>type.to_s})
            puts "current worker num:#{@client.getCurrentOnJobWorkerNums($access_key,ids)}"
          end
          sleep(120)
        end
      end
    end
  end

  #
  # test addProductInspect
  #
  def test_add_product_inspect_pack
    # while true
      @productNrs=[]
      10.times {@productNrs<<UUID.generate}
      @productNrs.each_with_index do |value,index|
        entity=@entities[rand(@entities.count)]
        type=rand(index+1)%2
      #  @client.addProductInspect($access_key,{"entityId"=>entity.entityId,"inspectTime"=>(Time.now.to_ms).to_s,"productNr"=>value,"type"=>type.to_s})
      #  puts "INSPECT:#{index}:productNr:#{value}-entity:#{entity.entityId}-type:#{(index%2).to_s}"
       sleep(1)
        if type==1
          partNr=@parts[rand(@parts.count)].partNr
           @client.addProductPack($access_key,{"entityId"=>entity.entityId,"packTime"=>(Time.now.to_ms).to_s,"productNr"=>value,"partId"=>partNr})
          puts "***PACK:entity:#{entity.entityId}-#{index}:productNr:#{value}-:partNr#{partNr}"
        end
      end
      puts "*****************************************************************"
    # end
  end

  #
  # test addOperatingState
  #
  def test_add_operating_state
    while true
      @entities.each_with_index do |entity,index|
        [100,200,300,400].each do |state|
          @client.addOperatingState($access_key,{"entityId"=>entity.entityId,"operateTime"=>(Time.now.to_ms).to_s,"state"=>state.to_s})
          puts "#{index}:targetId:#{entity.entityId}-state:#{state}"
          sleep(3)
        end
      end
    end
  end

  #
  # test getOriProductOutputNums && getProductOutputNums
  #
  def test_get_ori_product_output_num
    ids=@entities.map{|e| e.entityId}
    while true
      now=Time.now
      endT=now.to_ms
      startT=endT-10*1000
      puts "ori_output_num:#{@client.getOriProductOutputNums($access_key,ids,startT,endT)}"
      puts "*output_num:#{@client.getProductOutputNums($access_key,ids,startT,endT)}"
       puts "*output_num:#{@client.getProductOutputNumsByPartId($access_key,@entities[rand(@entities.count)].entityId, @parts[rand(@parts.count)].partNr,startT.to_s,endT.to_s)}"
      puts "*****************************************************************"
      sleep(2)
    end
  end

  #
  # test  getFTRProductNums && getFailProductInspectNums
  #
  def test_get_ftr_fail_product_num
    ids=@entities.map{|e| e.entityId}
    while true
      now=Time.now
      endT=now.to_ms
      startT=endT-10*1000
      puts "ftr_product_num:#{@client.getFTRProductNums($access_key,ids,startT,endT)}"
      puts "*fail_product_num:#{@client.getFailProductInspectNums($access_key,ids,startT,endT)}"
      puts "*****************************************************************"
      sleep(10)
    end
  end

  #
  # test getOnJobTotalTimes
  #
  def test_on_job_total_time
    begin
      ids=@entities.map{|e| e.entityId}
      while true
        now=Time.now
        endT=now.to_ms
        startT=endT-20*60*1000
        puts "on_job_total_time:#{@client.getOnJobTotalTimes($access_key,ids,startT,endT)}"
        sleep(1)
      end
    rescue Exception=>e
      puts e.message
    end
  end

  #
  # test
  #
  def test_product_output_num_time
    ids=@entities.map{|e| e.entityId}
    while true
      ids.each do |id|
        now=Time.now
        endT=now.to_ms
        startT=endT-20*60*1000
        pros=@client.getProductOutputNumAndTime($access_key,id,startT,endT)
        pros.each do |pro|
          puts "product num& time:#{pro}"
        end
        puts "-----------------------------------------------------------------------------------------"
      end
      puts "*****************************************************************"
      sleep(10)
    end
  end

  #
  # test_add_get_plan_target
  #
  def test_add_get_plan_target
    begin
      ids=@entities.map{|e| e.entityId}
      ids.each do |id|
        ['D','W','M',"Y"].each do |type|
          part=@parts[rand(@parts.count)]
          data={"entityId"=>id,"type"=>type,"partId"=>part.partNr,"partAmount"=>rand(1000).to_s,"staffNum"=>rand(100).to_s}
          @client.addPlanTarget($access_key,data)
          puts ">>>>>>>#{data}"
          sleep(1)
          puts ">>>>>>>>>>#{@client.getPlanTarget($access_key,{"entityId"=>id,"type"=>type})}"
        end
      end
    rescue Exception=>e
      puts e.message
    end
  end

  def test_finish
    @transport.close
  end
end

test=Test.new
test.pick
test.test_finish

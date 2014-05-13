#encoding: utf-8
class InspectOriginalsController < ApplicationController
  def index
  end

  def search
    puts '_____________________'
    puts params
    puts '*****************************'
    @entity_nr=params[:entity_nr]
    @start_time=params[:start_time]
    @end_time=params[:end_time]
    if params[:entity_nr].blank? || params[:start_time].blank? || params[:end_time].blank?
      flash[:notice]="请填写完整的查询条件.(测试台号,开始时间,结束时间)"
    else
      s=(Time.parse(params[:start_time]).to_f*1000).to_i.to_s
      e=((Time.parse(params[:end_time])+1.day).to_f*1000).to_i.to_s
      if params[:export]
        @items=InspectOriginal.where(entityId:params[:entity_nr]).between(Hash[:inspectTime,[s,e]]).asc('inspectTime')
        else
      @items=InspectOriginal.where(entityId:params[:entity_nr]).between(Hash[:inspectTime,[s,e]]).asc('inspectTime').paginate(:page => params[:page], :per_page => 20)
      end
    end
    if params[:export] && @items
        file_name=Time.now.strftime('%Y-%m-%d')+'_'+@model+".csv"
        path=File.join($DOWNLOADPATH,file_name)
        File.open(path,'wb') do |f|
          f.puts ['零件号',	'产品号',	'测试台号',	'测试结果',	'测试时间'].join($CSVSP)
          @items.each do |item|
            line=[]
            proc=BlockHelper.send "get_#{@model}_down_block"
            proc.call(line,item)
            f.puts line.join($CSVSP)
          end
        end
        send_file path,:type => 'application/csv', :filename =>file_name
    else
    render :index
    end
  end

end

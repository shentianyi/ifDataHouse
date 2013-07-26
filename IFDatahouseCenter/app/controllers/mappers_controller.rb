#encoding: utf-8
class MappersController < ApplicationController
 
  before_filter :check_access_key,:only=>[:cancel,:map,:domap]
  def index
    clean_session
    super
  end

  def new
    @access_key=session[:mapper_access_key]= session[:mapper_access_key]||SecureRandom.urlsafe_base64
    @item=session[:mapper].nil? ? Mapper.new : ((m=Mapper.find(session[:mapper])).nil? ? Mapper.new : m)
    # if params[:mapper] and m=Mapper.find(params[:mapper])
     # @item=m
     # @access_key=@item.access_key
    # end
  end

  def create
    @models=Mapper::MObjects
    if params[:mapper]
      if  params[:mapper][:access_key]==session[:mapper_access_key]
        if mapper=Mapper.find_by(:mapperNr=>params[:mapper][:mapperNr])
           mapper.update_attributes(:name=>params[:mapper][:name]) unless params[:mapper][:name].blank?
        else
          mapper=Mapper.new(params[:mapper])
          mapper.save
        end
        session[:mapper]=mapper.id.to_s
      else
        redirect_to new_mapper_path
      end
    end
  end

  def cancel
    clean_session
    redirect_to mappers_path
  end

  def map
    if @mapper_model=Mapper.get_model(params[:m].to_i)
      session[:mapper_model]=@mapper_model
      @items=@mapper_model.classify.constantize.paginate(:page=>params[:page],:per_page=>20)
      @access_key=session[:mapper_access_key]
    else
      redirect_to new_mapper_path
    end
  end

  def domap
    msg=Message.new
    begin
      mapper_model=session[:mapper_model]
      type=params[:type].to_i
      if mapper_model=session[:mapper_model] and  session[:mapper]
        if type==0
          params[:data].each_pair do |map_key,map_value|
            if  mii=mapper_model.classify.constantize.find(map_key)
              map_value=map_value.length==0 ? mii.send(mii.map_field) : map_value
              generate_mapper_item mapper_model,map_key,map_value,mii.send(mii.map_field)
            end
          end
        else
          mapper_model.classify.constantize.all.each do |m|
            map_value=m.send(m.map_field)
            map_key=m.id.to_s
            generate_mapper_item mapper_model,map_key,map_value,map_value
          end
        end
        msg.content='finish'
      end
    rescue Exception=>e
    msg.content=e.message
    end
    render :json=>msg
  end

  def destory
    @item = model.find(params[:id])
    @item.destroy
    cancel
  end

  def show
    @item= model.find(params[:id])
  end

  def select
    @item=mapper_path
    @action=item_mapper_path
    @models=Mapper::MObjects
  end

  def item
    @mapper_model=Mapper.get_model(params[:m].to_i)
    redirect_to   :controller=>:mapper_items,:action=>:index,:mapper_model=>@mapper_model,:id=>params[:id]
  end

  private

  def check_access_key
    unless session[:mapper_access_key]
      redirect_to   mappers_path
    end
  end

  def clean_session
    session[:mapper]=session[:mapper_model]=session[:mapper_access_key]=nil
  end

  def generate_mapper_item mapper_model,map_key,map_value,map_field_value
    mapper=Mapper.find(session[:mapper])
    if mi=MapperItem.find_by(:map_key=>map_key,:mapper_id=>mapper.id)
      mi.update_attributes(:map_value=>map_value) if mi.map_value!=map_value
    else
      mi=MapperItem.new(:model=>mapper_model, :map_value=>map_value,:map_key=>map_key,:map_field_value=>map_field_value,:access_key=>mapper.access_key)
    mapper.mapper_items<<mi
    end
  end
end

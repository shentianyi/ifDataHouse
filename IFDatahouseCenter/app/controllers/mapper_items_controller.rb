#encoding: utf-8
class MapperItemsController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
  def index
    @items=if @mapper_model=params[:mapper_model] and @mapper_id=params[:id]
          MapperItem.where(:model=>@mapper_model,:mapper_id=>@mapper_id).paginate(:page=>params[:page],:per_page=>20)
    else
      MapperItem.paginate(:page=>params[:page],:per_page=>20)
    end
  end

  def destroy
    @item = model.find(params[:id])
    @item.destroy

    respond_to do |format|
      format.html { redirect_to send("#{@model.pluralize}_path") }
      format.json { head :no_content }
    end
  end
end

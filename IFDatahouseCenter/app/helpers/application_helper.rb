#encoding: utf-8
module ApplicationHelper
  def index
    @items=model.paginate(:page=>params[:page],:per_page=>15)
  end

  def show
    @item= model.find(params[:id])
  end

  def new
    @item=model.new
    render 'share/new'
  end

  def edit
    @item= model.find(params[:id])
    render 'share/edit'
  end

  def import
    render 'share/import'
  end

  def update
    @item= model.find(params[:id])
    respond_to do |format|
      if @item.update_attributes(params[@model])
        format.html { redirect_to @item, notice: 'item was successfully updated.' }
      else
        format.html { render action: "edit" }
      end
    end
  end

  def create
    m=model
    @item = m.new(params[@model])
    if m.respond_to?(:uniq)
      query={}
      m.uniq.each do |u|
        query[u]=params[@model][u]
      end
    end
    respond_to do |format|
      if   item=m.find_by(query)
        @item=item
        format.html { redirect_to @item, notice: '数据已存在,新建失败.' }
        format.json { render json: @item, status: :created, location: @item }
      else
        if @item.save
          format.html { redirect_to @item, notice: '新建成功.' }
          format.json { render json: @item, status: :created, location: @item }
        else
          format.html { render action: "new" }
          format.json { render json: @item.errors, status: :unprocessable_entity }
        end
      end
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

  def search
    @items=model.where(params[@model].clone.delete_if{|k,v| v.length==0}).paginate(:page=>params[:page],:per_page=>20)
    render :index
  end
end

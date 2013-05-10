#encoding: utf-8
module ApplicationHelper
  def index
    @items=model.paginate(:page=>params[:page],:per_page=>20)
    render 'share/index'
  end

  def show
    @item= model.find(params[:id])
    render 'share/show'
  end

  def new
    @item=model.new
    render 'share/new'
  end

  def edit
    @item= model.find(params[:id])
    render 'share/edit'
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

  def updata
    msg=Message.new(:content=>'')
    begin
      files=params[:files]
      if files.count==1
        file=files[0]
        csv=FileData.new(:data=>file,:oriName=>file.original_filename,:path=>$UPDATAPATH)
        csv.saveFile
        hfile=File.join($UPDATAPATH,csv.pathName)
        CSV.foreach(hfile,:headers=>true,:col_sep=>$CSVSP) do |row|
          m=model
          uniquery=nil
          if m.respond_to?(:notNil)
            rheader=m.notNil-row.headers
            raise( ArgumentError, "#{rheader}为必需包含列！" )  unless rheader.empty?
          end
          if m.respond_to?(:uniq)
            uniquery={}
            m.uniq.each do |u|
              uniquery[u]=row[u]
            end
          end
          data={}
          row.each do |k,v|
            data[k]=v
          end
          data.delete($UPMARKER)

          if uniquery
            if item=m.find_by(uniquery)
              if row[$UPMARKER].to_i==1
              item.update_attributes(data)
              end
            else
            m.create(data)
            end
          else
          m.create(data)
          end
        end
        msg.result=true
        msg.content="导入数据成功"
      else
        msg.content='未选择文件或只能上传一个文件'
      end
    rescue Exception=>e
    msg.content=e.message
    end
    render :json=>msg
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

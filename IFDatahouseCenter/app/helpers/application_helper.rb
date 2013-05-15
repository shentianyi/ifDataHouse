#encoding: utf-8
module ApplicationHelper
  def index
    @items=model.paginate(:page=>params[:page],:per_page=>20)
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

  def updata
    msg=Message.new(:content=>'')
    begin
      files=params[:files]
      if files.count==1
        file=files[0]
        csv=FileData.new(:data=>file,:oriName=>file.original_filename,:path=>$UPDATAPATH,:pathName=>SecureRandom.uuid+file.original_filename)
        csv.saveFile
        hfile=File.join($UPDATAPATH,csv.pathName)
        row_line=0
        CSV.foreach(hfile,:headers=>true,:col_sep=>$CSVSP) do |row|
          row_line+=1
          m=model
          uniquery=nil
          if m.respond_to?(:csv_headers)
            rheader=m.csv_headers-row.headers
            raise( ArgumentError, "#{rheader}为必需包含列！" )  unless rheader.empty?
          end
          data={}
          query=nil
          if block_given?
            if m.respond_to?(:uniq)
              query={}
            end
          yield(data,query,row,row_line)
          end
          data.delete($UPMARKER)
          if query
            if item=m.find_by(query)
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
        msg.content="新建/更新成功！"
      else
        msg.content='未选择文件或只能上传一个文件'
      end
    rescue Exception=>e
    msg.content=e.message
    end
    render :json=>msg
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

  def download
    file_name=SecureRandom.uuid+".csv"
    path=File.join($DOWNLOADPATH,file_name)
    File.open(path,'wb') do |f|
      m=model
      # attrs=m.fields.collect { |field| field[0] }
      # if m.respond_to?(:attr_filter)
      # attrs=attrs-m.attr_filter
      # end
      f.puts m.csv_headers.join($CSVSP)
      m.all.each do |item|
        line=[]
        if block_given?
        yield(line,item)
        end
        f.puts line.join($CSVSP)
      end
    end
    send_file path,:type => 'application/csv', :filename =>file_name
  end

  def search
    params[@model].each do |k,v|
      params[@model].delete(k) if v.length==0
    end
    @items=model.where(params[@model]).paginate(:page=>params[:page],:per_page=>20)
    render :index
  end
end

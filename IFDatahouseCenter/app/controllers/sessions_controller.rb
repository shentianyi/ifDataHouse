#encoding: utf-8
class SessionsController < ApplicationController

  layout "login"
  def new
  end

  def create
    if staff = Staff.authenticate( params[:staffNr], params[:password] )
      session[:staff_id] = staff.id
      # render :json=>{ :status=>1 }
      redirect_to index_url
    else
      redirect_to login_url
    end
  end

  def destroy
    session[:staff_id] = nil
    session[:org_id] = nil
    session[:orgOpeType] = nil
    redirect_to login_url, :notice => "已注销"
  end
  
  def index
   render  :layout =>"application"
  end

end

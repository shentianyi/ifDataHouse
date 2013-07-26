#encoding: utf-8
class SessionsController < ApplicationController
  skip_before_filter  :authorize
  skip_before_filter :set_model

  layout "login"
  def new
  end

  def create
    if staff = Staff.authenticate( params[:staffNr], params[:password] )
      session[:staff_id] = staff.id
      # render :json=>{ :status=>1 }
      redirect_to index_path
    else
      redirect_to login_path
    end
  end

  def destroy
    session[:staff_id] = nil
    redirect_to login_path, :notice => "已注销"
  end

  def index
    render  :layout =>"application"
  end

end

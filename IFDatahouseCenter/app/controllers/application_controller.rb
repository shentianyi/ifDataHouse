#encoding: utf-8
require 'csv'

class ApplicationController < ActionController::Base
  protect_from_forgery
  include ApplicationHelper
  include FileHelper
  protected
  def authorize
    unless Staff.where(:id=>session[:staff_id]).exists?
      redirect_to login_path,flash[:notice] => "请填写正确登录信息"
    end
  end

  def set_model
    @model=self.class.name.gsub(/Controller/,'').tableize.singularize.downcase
  end

  def model
    self.class.name.gsub(/Controller/,'').classify.constantize
  end

end

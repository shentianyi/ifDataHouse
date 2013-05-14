#encoding: utf-8
class StaffsController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
  
end

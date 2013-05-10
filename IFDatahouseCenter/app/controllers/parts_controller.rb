#encoding: utf-8
class PartsController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
  
end

#encoding: utf-8
class EntitiesController < ApplicationController
  before_filter  :authorize
  before_filter :set_model
end

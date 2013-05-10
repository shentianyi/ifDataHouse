#encoding: utf-8
require 'base_class'
class Message<CZ::BaseClass
  attr_accessor :result,:object,:content

  def default
    {:result=>false}
  end
end
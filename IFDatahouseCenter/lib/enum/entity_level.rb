#encoding=utf-8
require_relative 'enum'

class EntityLevel
  include Enum
  EntityLevel.define :UnProEnitity,100,"非生产单元"
  EntityLevel.define :ProEntity,200,"生产单元"
end

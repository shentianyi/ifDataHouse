#encoding=utf-8
require_relative 'enum'
class EntityType
  include Enum
  EntityType.define :NorEntity,101,"普通组织"
  EntityType.define :ProLine,201,"生产线"
  EntityType.define :ProWorkbench,202,"工作台"
end

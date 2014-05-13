#encoding: utf-8
class Mapper
  include Mongoid::Document
  include Mongoid::Timestamps
  has_many :mapper_items,dependent: :destroy
  field :mapperNr
  field :name
  field :access_key
  MObjects=[['员工',0],['组织',1],['零件',2]]

  MHModels={0=>'staff',1=>'entity',2=>'part'}

  def self.get_model i
    MHModels[i]
  end

  def self.generate_mapper_items mapper_model,map_key,map_value,map_field_value,mapper_id=nil
    mappers=mapper_id.nil? ? Mapper.all : [Mapper.find(mapper_id)]
    mappers.each do |mapper|
    if mi=MapperItem.find_by(:map_key=>map_key,:mapper_id=>mapper.id)
      mi.update_attributes(:map_value=>map_value) if mi.map_value!=map_value
      mi.g_update_in_redis
    else
      mi=MapperItem.new(:model=>mapper_model, :map_value=>map_value,:map_key=>map_key,:map_field_value=>map_field_value,:access_key=>mapper.access_key)
    mapper.mapper_items<<mi
    end
    end
  end
end

class MapperItem
  include Mongoid::Document
  belongs_to :mapper

  field :model
  field :map_key
  field :map_value
  field :map_field_value
  field :access_key

  after_create :create_in_redis
  after_update :update_in_redis
  after_destroy :delete_from_redis
  def create_in_redis
    value_key_hash=generate_key "value:key"
    $redis.hset value_key_hash,self.map_value,self.map_key
    key_value_hash=generate_key "key:value"
    $redis.hset key_value_hash,self.map_key,self.map_value
  end

  def update_in_redis
    if self.map_value_changed?
      g_update_in_redis
    end
  end

  def g_update_in_redis
    value_key_hash=generate_key "value:key"
    $redis.hset value_key_hash,self.map_value,self.map_key
    $redis.hdel value_key_hash,self.map_value_was
    key_value_hash=generate_key "key:value"
    $redis.hset key_value_hash,self.map_key,self.map_value
  end

  def delete_from_redis
    value_key_hash=generate_key "value:key"
    $redis.hdel value_key_hash,self.map_value
    key_value_hash=generate_key "key:value"
    $redis.hdel key_value_hash,self.map_key
  end

  def generate_key key
    "#{self.mapper.access_key}-#{self.model}-#{key}"
  end
end
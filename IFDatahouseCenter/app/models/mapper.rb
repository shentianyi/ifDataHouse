#encoding: utf-8
class Mapper
  include Mongoid::Document
  include Mongoid::Timestamps
  has_many :mapper_items,dependent: :destroy
  field :mapperNr
  field :name
  field :access_key
  MObjects=[['staff',0],['entity',1],['part',2]]

  MHModels={0=>'staff',1=>'entity',2=>'part'}

  def self.get_model i
    return MHModels[i]
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

  after_save :save_in_redis
  after_destroy :delete_from_redis
  
  def save_in_redis
    value_key_hash=generate_key "value:key"
    $redis.hset value_key_hash,self.map_value,self.map_key
    key_value_hash=generate_key "key:value"
    $redis.hset key_value_hash,self.map_key,self.map_value
  end

  def delete_from_redis
    value_key_hash=generate_key "value:key"
    $redis.hdel value_key_hash,self.map_value
    key_value_hash=generate_key "key:value"
    $redis.hdel value_key_hash,self.map_key
  end

  def generate_key key
    "#{self.mapper.access_key}-#{self.model}-#{key}"
  end
end
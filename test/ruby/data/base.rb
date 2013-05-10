require 'yaml'
class Base
 def self.class
   self
 end
 def self.yml
   return YAML.load(File.open("data/#{self.class.to_s.downcase}.yml"))
 end
 def self.all
    items=[]
   yml.each do |k,v|
     item=eval("#{self.class}.new")
      v.each do |kk,vv|
       eval("item.#{kk}=vv")
      end
      items<<item
   end
   return items
  end
end
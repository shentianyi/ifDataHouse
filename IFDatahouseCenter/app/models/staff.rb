#encoding: utf-8
require 'digest/sha2'

class Staff
  include Mongoid::Document
  include Mongoid::Timestamps
  field :pwd
  field :salt
  attr_accessor :password, :password_confirmation
  field :staffNr
  field :name
  field :email
  field :contact
  field :title
  def password
    @password
  end

  def password=(password)
    @password = password
    if password.present?
      generate_salt
      self.pwd = self.class.encrypt_password(password, salt)
    end
  end

  class << self
    def authenticate( staffNr, password)
      if staff = Staff.find_by(:staffNr=>staffNr)
        if staff.pwd == encrypt_password(password, staff.salt)
        staff
        end
      end
    end

    def encrypt_password(password, salt)
      Digest::SHA2.hexdigest(password + "wibble" + salt)
    end
  end

  def map_field
    'staffNr'
  end
  private

  def generate_salt
    self.salt = self.object_id.to_s + rand.to_s
  end

  def self.uniq
    ['staffNr']
  end

  def self.csv_headers
    ['StaffNr','Name','Title','Email','Contact',$UPMARKER]
  end
  
  # def self.attr_filter
    # ["_id","created_at","updated_at","pwd","salt"]
  # end
end
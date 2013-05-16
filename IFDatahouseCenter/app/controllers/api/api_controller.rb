#encoding: utf-8
module Api
  class ApiController<ActionController::Base
    skip_before_filter :verify_authenticity_token
    def auth
      if params[:access_key].nil? or params[:access_key]!=$AKEY
        render :json=>{:state=>0}
      end
    end
  end
end
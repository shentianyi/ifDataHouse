#encoding: utf-8
module Api
  class FileController<ApiController
    before_filter :auth
    include FileHelper
    def part
      download nil,Part,&BlockHelper.get_part_down_block
    end

    def staff
      download nil,Staff,&BlockHelper.get_staff_down_block
    end

    #
    # http://localhost:3000/api/file/entity?access_key=h-esV6wUMFgRotSc5WpjXQ&type=201
    #
    def entity
      query={:type=>params[:type]}  if params[:type]
      download query,Entity,&BlockHelper.get_entity_down_block
    end
  end
end
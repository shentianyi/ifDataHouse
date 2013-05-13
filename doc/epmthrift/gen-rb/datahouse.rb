#
# Autogenerated by Thrift Compiler (0.9.0)
#
# DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
#

require 'thrift'
require 'epm_service_types'

module CZ
  module Epm
    module Thrift
      module Datahouse
        class Client
          include ::Thrift::Client

          def addAttendance(accessKey, dataMap)
            send_addAttendance(accessKey, dataMap)
            recv_addAttendance()
          end

          def send_addAttendance(accessKey, dataMap)
            send_message('addAttendance', AddAttendance_args, :accessKey => accessKey, :dataMap => dataMap)
          end

          def recv_addAttendance()
            result = receive_message(AddAttendance_result)
            return
          end

          def addProductInspect(accessKey, dataMap)
            send_addProductInspect(accessKey, dataMap)
            recv_addProductInspect()
          end

          def send_addProductInspect(accessKey, dataMap)
            send_message('addProductInspect', AddProductInspect_args, :accessKey => accessKey, :dataMap => dataMap)
          end

          def recv_addProductInspect()
            result = receive_message(AddProductInspect_result)
            return
          end

          def addProductPack(accessKey, dataMap)
            send_addProductPack(accessKey, dataMap)
            recv_addProductPack()
          end

          def send_addProductPack(accessKey, dataMap)
            send_message('addProductPack', AddProductPack_args, :accessKey => accessKey, :dataMap => dataMap)
          end

          def recv_addProductPack()
            result = receive_message(AddProductPack_result)
            return
          end

          def setProductInspectState(accessKey, dataMap)
            send_setProductInspectState(accessKey, dataMap)
            recv_setProductInspectState()
          end

          def send_setProductInspectState(accessKey, dataMap)
            send_message('setProductInspectState', SetProductInspectState_args, :accessKey => accessKey, :dataMap => dataMap)
          end

          def recv_setProductInspectState()
            result = receive_message(SetProductInspectState_result)
            return
          end

          def addOperatingState(accessKey, dataMap)
            send_addOperatingState(accessKey, dataMap)
            recv_addOperatingState()
          end

          def send_addOperatingState(accessKey, dataMap)
            send_message('addOperatingState', AddOperatingState_args, :accessKey => accessKey, :dataMap => dataMap)
          end

          def recv_addOperatingState()
            result = receive_message(AddOperatingState_result)
            return
          end

          def addPlanTarget(accessKey, dataMap)
            send_addPlanTarget(accessKey, dataMap)
            recv_addPlanTarget()
          end

          def send_addPlanTarget(accessKey, dataMap)
            send_message('addPlanTarget', AddPlanTarget_args, :accessKey => accessKey, :dataMap => dataMap)
          end

          def recv_addPlanTarget()
            result = receive_message(AddPlanTarget_result)
            return
          end

          def getPlanTarget(accessKey, dataMap)
            send_getPlanTarget(accessKey, dataMap)
            return recv_getPlanTarget()
          end

          def send_getPlanTarget(accessKey, dataMap)
            send_message('getPlanTarget', GetPlanTarget_args, :accessKey => accessKey, :dataMap => dataMap)
          end

          def recv_getPlanTarget()
            result = receive_message(GetPlanTarget_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getPlanTarget failed: unknown result')
          end

          def getCurrentOnJobWorkerNums(accessKey, entityIds)
            send_getCurrentOnJobWorkerNums(accessKey, entityIds)
            return recv_getCurrentOnJobWorkerNums()
          end

          def send_getCurrentOnJobWorkerNums(accessKey, entityIds)
            send_message('getCurrentOnJobWorkerNums', GetCurrentOnJobWorkerNums_args, :accessKey => accessKey, :entityIds => entityIds)
          end

          def recv_getCurrentOnJobWorkerNums()
            result = receive_message(GetCurrentOnJobWorkerNums_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getCurrentOnJobWorkerNums failed: unknown result')
          end

          def getOriProductOutputNums(accessKey, entityIds, startTime, endTime)
            send_getOriProductOutputNums(accessKey, entityIds, startTime, endTime)
            return recv_getOriProductOutputNums()
          end

          def send_getOriProductOutputNums(accessKey, entityIds, startTime, endTime)
            send_message('getOriProductOutputNums', GetOriProductOutputNums_args, :accessKey => accessKey, :entityIds => entityIds, :startTime => startTime, :endTime => endTime)
          end

          def recv_getOriProductOutputNums()
            result = receive_message(GetOriProductOutputNums_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getOriProductOutputNums failed: unknown result')
          end

          def getFTRProductNums(accessKey, entityIds, startTime, endTime)
            send_getFTRProductNums(accessKey, entityIds, startTime, endTime)
            return recv_getFTRProductNums()
          end

          def send_getFTRProductNums(accessKey, entityIds, startTime, endTime)
            send_message('getFTRProductNums', GetFTRProductNums_args, :accessKey => accessKey, :entityIds => entityIds, :startTime => startTime, :endTime => endTime)
          end

          def recv_getFTRProductNums()
            result = receive_message(GetFTRProductNums_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getFTRProductNums failed: unknown result')
          end

          def getFailProductInspectNums(accessKey, entityIds, startTime, endTime)
            send_getFailProductInspectNums(accessKey, entityIds, startTime, endTime)
            return recv_getFailProductInspectNums()
          end

          def send_getFailProductInspectNums(accessKey, entityIds, startTime, endTime)
            send_message('getFailProductInspectNums', GetFailProductInspectNums_args, :accessKey => accessKey, :entityIds => entityIds, :startTime => startTime, :endTime => endTime)
          end

          def recv_getFailProductInspectNums()
            result = receive_message(GetFailProductInspectNums_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getFailProductInspectNums failed: unknown result')
          end

          def getProductOutputNums(accessKey, entityIds, startTime, endTime)
            send_getProductOutputNums(accessKey, entityIds, startTime, endTime)
            return recv_getProductOutputNums()
          end

          def send_getProductOutputNums(accessKey, entityIds, startTime, endTime)
            send_message('getProductOutputNums', GetProductOutputNums_args, :accessKey => accessKey, :entityIds => entityIds, :startTime => startTime, :endTime => endTime)
          end

          def recv_getProductOutputNums()
            result = receive_message(GetProductOutputNums_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getProductOutputNums failed: unknown result')
          end

          def getOnJobTotalTimes(accessKey, entityIds, startTime, endTime)
            send_getOnJobTotalTimes(accessKey, entityIds, startTime, endTime)
            return recv_getOnJobTotalTimes()
          end

          def send_getOnJobTotalTimes(accessKey, entityIds, startTime, endTime)
            send_message('getOnJobTotalTimes', GetOnJobTotalTimes_args, :accessKey => accessKey, :entityIds => entityIds, :startTime => startTime, :endTime => endTime)
          end

          def recv_getOnJobTotalTimes()
            result = receive_message(GetOnJobTotalTimes_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getOnJobTotalTimes failed: unknown result')
          end

          def getProductOutputNumAndTime(accessKey, entityId, startTime, endTime)
            send_getProductOutputNumAndTime(accessKey, entityId, startTime, endTime)
            return recv_getProductOutputNumAndTime()
          end

          def send_getProductOutputNumAndTime(accessKey, entityId, startTime, endTime)
            send_message('getProductOutputNumAndTime', GetProductOutputNumAndTime_args, :accessKey => accessKey, :entityId => entityId, :startTime => startTime, :endTime => endTime)
          end

          def recv_getProductOutputNumAndTime()
            result = receive_message(GetProductOutputNumAndTime_result)
            return result.success unless result.success.nil?
            raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'getProductOutputNumAndTime failed: unknown result')
          end

        end

        class Processor
          include ::Thrift::Processor

          def process_addAttendance(seqid, iprot, oprot)
            args = read_args(iprot, AddAttendance_args)
            result = AddAttendance_result.new()
            @handler.addAttendance(args.accessKey, args.dataMap)
            write_result(result, oprot, 'addAttendance', seqid)
          end

          def process_addProductInspect(seqid, iprot, oprot)
            args = read_args(iprot, AddProductInspect_args)
            result = AddProductInspect_result.new()
            @handler.addProductInspect(args.accessKey, args.dataMap)
            write_result(result, oprot, 'addProductInspect', seqid)
          end

          def process_addProductPack(seqid, iprot, oprot)
            args = read_args(iprot, AddProductPack_args)
            result = AddProductPack_result.new()
            @handler.addProductPack(args.accessKey, args.dataMap)
            write_result(result, oprot, 'addProductPack', seqid)
          end

          def process_setProductInspectState(seqid, iprot, oprot)
            args = read_args(iprot, SetProductInspectState_args)
            result = SetProductInspectState_result.new()
            @handler.setProductInspectState(args.accessKey, args.dataMap)
            write_result(result, oprot, 'setProductInspectState', seqid)
          end

          def process_addOperatingState(seqid, iprot, oprot)
            args = read_args(iprot, AddOperatingState_args)
            result = AddOperatingState_result.new()
            @handler.addOperatingState(args.accessKey, args.dataMap)
            write_result(result, oprot, 'addOperatingState', seqid)
          end

          def process_addPlanTarget(seqid, iprot, oprot)
            args = read_args(iprot, AddPlanTarget_args)
            result = AddPlanTarget_result.new()
            @handler.addPlanTarget(args.accessKey, args.dataMap)
            write_result(result, oprot, 'addPlanTarget', seqid)
          end

          def process_getPlanTarget(seqid, iprot, oprot)
            args = read_args(iprot, GetPlanTarget_args)
            result = GetPlanTarget_result.new()
            result.success = @handler.getPlanTarget(args.accessKey, args.dataMap)
            write_result(result, oprot, 'getPlanTarget', seqid)
          end

          def process_getCurrentOnJobWorkerNums(seqid, iprot, oprot)
            args = read_args(iprot, GetCurrentOnJobWorkerNums_args)
            result = GetCurrentOnJobWorkerNums_result.new()
            result.success = @handler.getCurrentOnJobWorkerNums(args.accessKey, args.entityIds)
            write_result(result, oprot, 'getCurrentOnJobWorkerNums', seqid)
          end

          def process_getOriProductOutputNums(seqid, iprot, oprot)
            args = read_args(iprot, GetOriProductOutputNums_args)
            result = GetOriProductOutputNums_result.new()
            result.success = @handler.getOriProductOutputNums(args.accessKey, args.entityIds, args.startTime, args.endTime)
            write_result(result, oprot, 'getOriProductOutputNums', seqid)
          end

          def process_getFTRProductNums(seqid, iprot, oprot)
            args = read_args(iprot, GetFTRProductNums_args)
            result = GetFTRProductNums_result.new()
            result.success = @handler.getFTRProductNums(args.accessKey, args.entityIds, args.startTime, args.endTime)
            write_result(result, oprot, 'getFTRProductNums', seqid)
          end

          def process_getFailProductInspectNums(seqid, iprot, oprot)
            args = read_args(iprot, GetFailProductInspectNums_args)
            result = GetFailProductInspectNums_result.new()
            result.success = @handler.getFailProductInspectNums(args.accessKey, args.entityIds, args.startTime, args.endTime)
            write_result(result, oprot, 'getFailProductInspectNums', seqid)
          end

          def process_getProductOutputNums(seqid, iprot, oprot)
            args = read_args(iprot, GetProductOutputNums_args)
            result = GetProductOutputNums_result.new()
            result.success = @handler.getProductOutputNums(args.accessKey, args.entityIds, args.startTime, args.endTime)
            write_result(result, oprot, 'getProductOutputNums', seqid)
          end

          def process_getOnJobTotalTimes(seqid, iprot, oprot)
            args = read_args(iprot, GetOnJobTotalTimes_args)
            result = GetOnJobTotalTimes_result.new()
            result.success = @handler.getOnJobTotalTimes(args.accessKey, args.entityIds, args.startTime, args.endTime)
            write_result(result, oprot, 'getOnJobTotalTimes', seqid)
          end

          def process_getProductOutputNumAndTime(seqid, iprot, oprot)
            args = read_args(iprot, GetProductOutputNumAndTime_args)
            result = GetProductOutputNumAndTime_result.new()
            result.success = @handler.getProductOutputNumAndTime(args.accessKey, args.entityId, args.startTime, args.endTime)
            write_result(result, oprot, 'getProductOutputNumAndTime', seqid)
          end

        end

        # HELPER FUNCTIONS AND STRUCTURES

        class AddAttendance_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          DATAMAP = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            DATAMAP => {:type => ::Thrift::Types::MAP, :name => 'dataMap', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddAttendance_result
          include ::Thrift::Struct, ::Thrift::Struct_Union

          FIELDS = {

          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddProductInspect_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          DATAMAP = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            DATAMAP => {:type => ::Thrift::Types::MAP, :name => 'dataMap', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddProductInspect_result
          include ::Thrift::Struct, ::Thrift::Struct_Union

          FIELDS = {

          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddProductPack_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          DATAMAP = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            DATAMAP => {:type => ::Thrift::Types::MAP, :name => 'dataMap', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddProductPack_result
          include ::Thrift::Struct, ::Thrift::Struct_Union

          FIELDS = {

          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class SetProductInspectState_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          DATAMAP = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            DATAMAP => {:type => ::Thrift::Types::MAP, :name => 'dataMap', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class SetProductInspectState_result
          include ::Thrift::Struct, ::Thrift::Struct_Union

          FIELDS = {

          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddOperatingState_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          DATAMAP = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            DATAMAP => {:type => ::Thrift::Types::MAP, :name => 'dataMap', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddOperatingState_result
          include ::Thrift::Struct, ::Thrift::Struct_Union

          FIELDS = {

          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddPlanTarget_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          DATAMAP = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            DATAMAP => {:type => ::Thrift::Types::MAP, :name => 'dataMap', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class AddPlanTarget_result
          include ::Thrift::Struct, ::Thrift::Struct_Union

          FIELDS = {

          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetPlanTarget_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          DATAMAP = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            DATAMAP => {:type => ::Thrift::Types::MAP, :name => 'dataMap', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetPlanTarget_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::MAP, :name => 'success', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetCurrentOnJobWorkerNums_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          ENTITYIDS = 2

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            ENTITYIDS => {:type => ::Thrift::Types::SET, :name => 'entityIds', :element => {:type => ::Thrift::Types::STRING}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetCurrentOnJobWorkerNums_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::MAP, :name => 'success', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::I64}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetOriProductOutputNums_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          ENTITYIDS = 2
          STARTTIME = 3
          ENDTIME = 4

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            ENTITYIDS => {:type => ::Thrift::Types::SET, :name => 'entityIds', :element => {:type => ::Thrift::Types::STRING}},
            STARTTIME => {:type => ::Thrift::Types::I64, :name => 'startTime'},
            ENDTIME => {:type => ::Thrift::Types::I64, :name => 'endTime'}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetOriProductOutputNums_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::MAP, :name => 'success', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::I64}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetFTRProductNums_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          ENTITYIDS = 2
          STARTTIME = 3
          ENDTIME = 4

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            ENTITYIDS => {:type => ::Thrift::Types::SET, :name => 'entityIds', :element => {:type => ::Thrift::Types::STRING}},
            STARTTIME => {:type => ::Thrift::Types::I64, :name => 'startTime'},
            ENDTIME => {:type => ::Thrift::Types::I64, :name => 'endTime'}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetFTRProductNums_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::MAP, :name => 'success', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::I64}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetFailProductInspectNums_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          ENTITYIDS = 2
          STARTTIME = 3
          ENDTIME = 4

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            ENTITYIDS => {:type => ::Thrift::Types::SET, :name => 'entityIds', :element => {:type => ::Thrift::Types::STRING}},
            STARTTIME => {:type => ::Thrift::Types::I64, :name => 'startTime'},
            ENDTIME => {:type => ::Thrift::Types::I64, :name => 'endTime'}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetFailProductInspectNums_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::MAP, :name => 'success', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::I64}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetProductOutputNums_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          ENTITYIDS = 2
          STARTTIME = 3
          ENDTIME = 4

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            ENTITYIDS => {:type => ::Thrift::Types::SET, :name => 'entityIds', :element => {:type => ::Thrift::Types::STRING}},
            STARTTIME => {:type => ::Thrift::Types::I64, :name => 'startTime'},
            ENDTIME => {:type => ::Thrift::Types::I64, :name => 'endTime'}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetProductOutputNums_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::MAP, :name => 'success', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::I64}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetOnJobTotalTimes_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          ENTITYIDS = 2
          STARTTIME = 3
          ENDTIME = 4

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            ENTITYIDS => {:type => ::Thrift::Types::SET, :name => 'entityIds', :element => {:type => ::Thrift::Types::STRING}},
            STARTTIME => {:type => ::Thrift::Types::I64, :name => 'startTime'},
            ENDTIME => {:type => ::Thrift::Types::I64, :name => 'endTime'}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetOnJobTotalTimes_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::MAP, :name => 'success', :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::I64}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetProductOutputNumAndTime_args
          include ::Thrift::Struct, ::Thrift::Struct_Union
          ACCESSKEY = 1
          ENTITYID = 2
          STARTTIME = 3
          ENDTIME = 4

          FIELDS = {
            ACCESSKEY => {:type => ::Thrift::Types::STRING, :name => 'accessKey'},
            ENTITYID => {:type => ::Thrift::Types::STRING, :name => 'entityId'},
            STARTTIME => {:type => ::Thrift::Types::I64, :name => 'startTime'},
            ENDTIME => {:type => ::Thrift::Types::I64, :name => 'endTime'}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

        class GetProductOutputNumAndTime_result
          include ::Thrift::Struct, ::Thrift::Struct_Union
          SUCCESS = 0

          FIELDS = {
            SUCCESS => {:type => ::Thrift::Types::SET, :name => 'success', :element => {:type => ::Thrift::Types::MAP, :key => {:type => ::Thrift::Types::STRING}, :value => {:type => ::Thrift::Types::STRING}}}
          }

          def struct_fields; FIELDS; end

          def validate
          end

          ::Thrift::Struct.generate_accessors self
        end

      end

    end
  end
end

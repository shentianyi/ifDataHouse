var db = "ifdatadb";
var drop = true;
var colls = ["parts", "staffs", "entities", "attendances", "products", "inspects", "operating_states", "mappers","mapper_items","targets"];

// collection indexs
var indexs = {
     "parts" : [{
          partNr : 1
     }],
     "staffs" : [{
          staffNr : 1
     }],
     "entities" : [{
          entityNr : 1
     }],
     "attendances" : [{
          attendTime : -1,
          staffNr : 1
     }, {
          attendTime : -1,
          entityId : 1
     }],
     "products" : [{
          productNr : 1
     }],
     "inspects" : [{
          inspectTime : -1,
          entityId : 1
     }, {
          productNr : 1
     }, {
          entityId : 1
     }],
     "operating_states" : [{
          operateTime : -1,
          targetId : 1
     }, {
          targetId : 1,
          state : 1
     }],
     "mappers" : [{
          access_key : 1
     }],
     "mapper_items":[{
      key_value:1
     },{
      mapper_id:1
     }],
     "targets":[{
      access_key:1
     }]
}

var user = {
     name : "ifdataer",
     password : "ifdataer@"
}

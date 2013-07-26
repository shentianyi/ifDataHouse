//
// script for create part_infos mongodb collection
//

load("base.js")
var db = "ifdatadb";
var drop = false;
var colls = ["part_infos"];
var indexs = {
     "part_infos" : [{
          partNr : 1
     }]
};

if(createColl(db,colls)){
     p("colls created");
     if(ensureCollIndex(db,indexs,colls)){
          p("indexs created");
     }
}

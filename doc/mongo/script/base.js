var conn = new Mongo();

function dbExists(_db) {
     var dbs = conn.getDBNames();
     for(var i = 0; i < dbs.length; i++) {
          print(dbs[i]);
          if(dbs[i] == _db)
               return true;
     }
     return false;
}


function getDb(_db) {
     return conn.getDB(_db);
}

function getColl(_db,_coll) {
     return getDb(_db).getCollection(_coll);
}

function p(msg) {
     print("----------------------- " + msg + " --------------------------")
}

function hashToString(h) {
     var str = "{";
     for(var hh in h) {
          str += hh + ":" + h[hh] + ","
     }
     str = str.substring(0, str.length - 1);
     str += "}";
     return str;
}


function createColl(_db,_colls) {
     try {
          p(">>create db collections");
          var db = getDb(_db);
          var ecolls = db.getCollectionNames();
          for(var i = 0; i < _colls.length; i++) {
               if(ecolls.indexOf(_colls[i]) < 0) {
                    db.createCollection(_colls[i]);
                    p("<" + _colls[i] + "> created!");
               } else {
                    p(_colls[i] + " exists!");
               }
               sleep(100);
          }
          return true;
     } catch(e) {
          p("???" + e);
     }
     return false;
}

function ensureCollIndex(_db,_indexs,_colls) {
     try {
          p(">>ensure collection indexs");
          for(var i = 0; i < _colls.length; i++) {
               p("**insure " + _colls[i] + " indexs");
               var ins = _indexs[_colls[i]];
               var coll = getColl(_db,_colls[i]);
               if(ins != null)
                    for(var j = 0; j < ins.length; j++) {
                         p("****" + (j + 1) + ":" + hashToString(ins[j]));
                         coll.ensureIndex(ins[j]);
                    }
               sleep(100);
          }
          return true;
     } catch(e) {
          p("???" + e);
     }
     return false;
}

function insertData (_db,_coll,datas) {
     var coll=getColl(_db,_coll);
  for(var i=0;i<datas.length;i++){       
       coll.insert(datas[i]);
       p("insert:>"+hashToString(datas[i]));
  }
}



//
//  script for creating if data center mongo db&collections
//
load("base.js");
load("setting.js");


doInit();

function doInit() {
     p("$ start init script $");
     if(dbExists(db)) {
          p("database: " + db + " exists!");
          if(drop) {
               p("drop db");
               getDb(db).dropDatabase();
          } else {
               return;
          }
     }
     if(createColl(db,colls)) {
          if(ensureCollIndex(db,indexs,colls)) {
               p("# init success!");
          }
     }
}




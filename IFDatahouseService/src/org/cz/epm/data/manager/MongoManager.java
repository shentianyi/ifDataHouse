package org.cz.epm.data.manager;

import java.util.Date;
import java.util.List;
import java.util.Map;

import org.cz.epm.resource.Conf;

import com.mongodb.BasicDBObject;
import com.mongodb.DB;
import com.mongodb.DBCollection;
import com.mongodb.DBObject;
import com.mongodb.Mongo;
import com.mongodb.MongoOptions;

public class MongoManager {

	private static Mongo mongo = null;
	private static String db = Conf.getmDb();

	private MongoManager() {

	}

	public static DB getDB() {
		if (mongo == null) {
			synchronized (MongoManager.class) {
				if (mongo == null)
					try {
						mongo = new Mongo(Conf.getmHost(), Conf.getmPort());
						MongoOptions option = mongo.getMongoOptions();
						option.connectionsPerHost = Conf.getmConPerHost();
						option.threadsAllowedToBlockForConnectionMultiplier = Conf
								.getmConMutiplier();
					} catch (Exception e) {
						e.printStackTrace();
					}
			}
		}
		return mongo.getDB(db);
	}

	public static void InitDB() {

	}

	public static void InsertData(String collName, BasicDBObject doc) {
		coll(collName).insert(doc);
	}

	public static void InsertData(String collName, DBObject[] docs) {
		coll(collName).insert(docs);
	}

	public static void UpdateData(String collName, BasicDBObject query,
			BasicDBObject fields, boolean upsert) {
		if (upsert)
			coll(collName).update(query, fields, upsert, true);
		else
			coll(collName).update(query, fields);
	}

	//find
	public static List<DBObject> Find(String collName, BasicDBObject query,
			BasicDBObject fields) {
		if (fields != null)
			return coll(collName).find(query, fields).toArray();
		else
			return coll(collName).find(query).toArray();
	}
	
	// find one
	public static Map FineOne(String collName, BasicDBObject query,
			BasicDBObject fields) {
		if (fields != null){
			DBObject r= coll(collName).findOne(query, fields);
			return r==null? null:r.toMap();
		}
		else{
			DBObject r= coll(collName).findOne(query);
			return r==null? null:r.toMap();
		}
	}

	// order find
	public static List<DBObject> Find(String collName, BasicDBObject query,
			BasicDBObject orderBy, BasicDBObject fields) {
		if (fields != null)
			return coll(collName).find(query, fields).sort(orderBy).toArray();
		else
			return coll(collName).find(query).sort(orderBy).toArray();
	}
    // order find limit
	public static List<DBObject> Find(String collName, BasicDBObject query,
			BasicDBObject orderBy, int limit, BasicDBObject fields) {
		if (fields != null)
			return coll(collName).find(query, fields).sort(orderBy).limit(limit)
					.toArray();
		else
			return coll(collName).find(query).sort(orderBy).limit(limit)
					.toArray();
	}
	// update
	public static void Update(String collName,BasicDBObject q,BasicDBObject o){
		coll(collName).update(q, o);
	}
	
   // count 
	public static long Count(String collName,BasicDBObject query){
		return coll(collName).count(query);
	}

	public static DBCollection coll(String collName) {
		return getDB().getCollection(collName);
	}
}

//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class SiteCrud {
		///<summary>Gets one Site object from the database using the primary key.  Returns null if not found.</summary>
		public static Site SelectOne(long siteNum){
			string command="SELECT * FROM site "
				+"WHERE SiteNum = "+POut.Long(siteNum);
			List<Site> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Site object from the database using a query.</summary>
		public static Site SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Site> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Site objects from the database using a query.</summary>
		public static List<Site> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Site> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Site> TableToList(DataTable table){
			List<Site> retVal=new List<Site>();
			Site site;
			foreach(DataRow row in table.Rows) {
				site=new Site();
				site.SiteNum    = PIn.Long  (row["SiteNum"].ToString());
				site.Description= PIn.String(row["Description"].ToString());
				site.Note       = PIn.String(row["Note"].ToString());
				retVal.Add(site);
			}
			return retVal;
		}

		///<summary>Converts a list of Site into a DataTable.</summary>
		public static DataTable ListToTable(List<Site> listSites,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Site";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("SiteNum");
			table.Columns.Add("Description");
			table.Columns.Add("Note");
			foreach(Site site in listSites) {
				table.Rows.Add(new object[] {
					POut.Long  (site.SiteNum),
					            site.Description,
					            site.Note,
				});
			}
			return table;
		}

		///<summary>Inserts one Site into the database.  Returns the new priKey.</summary>
		public static long Insert(Site site){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				site.SiteNum=DbHelper.GetNextOracleKey("site","SiteNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(site,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							site.SiteNum++;
							loopcount++;
						}
						else{
							throw ex;
						}
					}
				}
				throw new ApplicationException("Insert failed.  Could not generate primary key.");
			}
			else {
				return Insert(site,false);
			}
		}

		///<summary>Inserts one Site into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Site site,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				site.SiteNum=ReplicationServers.GetKey("site","SiteNum");
			}
			string command="INSERT INTO site (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="SiteNum,";
			}
			command+="Description,Note) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(site.SiteNum)+",";
			}
			command+=
				 "'"+POut.String(site.Description)+"',"
				+"'"+POut.String(site.Note)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				site.SiteNum=Db.NonQ(command,true);
			}
			return site.SiteNum;
		}

		///<summary>Inserts one Site into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Site site){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(site,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					site.SiteNum=DbHelper.GetNextOracleKey("site","SiteNum"); //Cacheless method
				}
				return InsertNoCache(site,true);
			}
		}

		///<summary>Inserts one Site into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Site site,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO site (";
			if(!useExistingPK && isRandomKeys) {
				site.SiteNum=ReplicationServers.GetKeyNoCache("site","SiteNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="SiteNum,";
			}
			command+="Description,Note) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(site.SiteNum)+",";
			}
			command+=
				 "'"+POut.String(site.Description)+"',"
				+"'"+POut.String(site.Note)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				site.SiteNum=Db.NonQ(command,true);
			}
			return site.SiteNum;
		}

		///<summary>Updates one Site in the database.</summary>
		public static void Update(Site site){
			string command="UPDATE site SET "
				+"Description= '"+POut.String(site.Description)+"', "
				+"Note       = '"+POut.String(site.Note)+"' "
				+"WHERE SiteNum = "+POut.Long(site.SiteNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Site in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Site site,Site oldSite){
			string command="";
			if(site.Description != oldSite.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(site.Description)+"'";
			}
			if(site.Note != oldSite.Note) {
				if(command!=""){ command+=",";}
				command+="Note = '"+POut.String(site.Note)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE site SET "+command
				+" WHERE SiteNum = "+POut.Long(site.SiteNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Site,Site) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Site site,Site oldSite) {
			if(site.Description != oldSite.Description) {
				return true;
			}
			if(site.Note != oldSite.Note) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Site from the database.</summary>
		public static void Delete(long siteNum){
			string command="DELETE FROM site "
				+"WHERE SiteNum = "+POut.Long(siteNum);
			Db.NonQ(command);
		}

	}
}
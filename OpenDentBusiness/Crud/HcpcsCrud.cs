//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class HcpcsCrud {
		///<summary>Gets one Hcpcs object from the database using the primary key.  Returns null if not found.</summary>
		public static Hcpcs SelectOne(long hcpcsNum){
			string command="SELECT * FROM hcpcs "
				+"WHERE HcpcsNum = "+POut.Long(hcpcsNum);
			List<Hcpcs> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Hcpcs object from the database using a query.</summary>
		public static Hcpcs SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Hcpcs> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Hcpcs objects from the database using a query.</summary>
		public static List<Hcpcs> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Hcpcs> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Hcpcs> TableToList(DataTable table){
			List<Hcpcs> retVal=new List<Hcpcs>();
			Hcpcs hcpcs;
			foreach(DataRow row in table.Rows) {
				hcpcs=new Hcpcs();
				hcpcs.HcpcsNum        = PIn.Long  (row["HcpcsNum"].ToString());
				hcpcs.HcpcsCode       = PIn.String(row["HcpcsCode"].ToString());
				hcpcs.DescriptionShort= PIn.String(row["DescriptionShort"].ToString());
				retVal.Add(hcpcs);
			}
			return retVal;
		}

		///<summary>Converts a list of Hcpcs into a DataTable.</summary>
		public static DataTable ListToTable(List<Hcpcs> listHcpcss,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Hcpcs";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("HcpcsNum");
			table.Columns.Add("HcpcsCode");
			table.Columns.Add("DescriptionShort");
			foreach(Hcpcs hcpcs in listHcpcss) {
				table.Rows.Add(new object[] {
					POut.Long  (hcpcs.HcpcsNum),
					            hcpcs.HcpcsCode,
					            hcpcs.DescriptionShort,
				});
			}
			return table;
		}

		///<summary>Inserts one Hcpcs into the database.  Returns the new priKey.</summary>
		public static long Insert(Hcpcs hcpcs){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				hcpcs.HcpcsNum=DbHelper.GetNextOracleKey("hcpcs","HcpcsNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(hcpcs,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							hcpcs.HcpcsNum++;
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
				return Insert(hcpcs,false);
			}
		}

		///<summary>Inserts one Hcpcs into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Hcpcs hcpcs,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				hcpcs.HcpcsNum=ReplicationServers.GetKey("hcpcs","HcpcsNum");
			}
			string command="INSERT INTO hcpcs (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="HcpcsNum,";
			}
			command+="HcpcsCode,DescriptionShort) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(hcpcs.HcpcsNum)+",";
			}
			command+=
				 "'"+POut.String(hcpcs.HcpcsCode)+"',"
				+"'"+POut.String(hcpcs.DescriptionShort)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				hcpcs.HcpcsNum=Db.NonQ(command,true);
			}
			return hcpcs.HcpcsNum;
		}

		///<summary>Inserts one Hcpcs into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Hcpcs hcpcs){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(hcpcs,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					hcpcs.HcpcsNum=DbHelper.GetNextOracleKey("hcpcs","HcpcsNum"); //Cacheless method
				}
				return InsertNoCache(hcpcs,true);
			}
		}

		///<summary>Inserts one Hcpcs into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Hcpcs hcpcs,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO hcpcs (";
			if(!useExistingPK && isRandomKeys) {
				hcpcs.HcpcsNum=ReplicationServers.GetKeyNoCache("hcpcs","HcpcsNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="HcpcsNum,";
			}
			command+="HcpcsCode,DescriptionShort) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(hcpcs.HcpcsNum)+",";
			}
			command+=
				 "'"+POut.String(hcpcs.HcpcsCode)+"',"
				+"'"+POut.String(hcpcs.DescriptionShort)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				hcpcs.HcpcsNum=Db.NonQ(command,true);
			}
			return hcpcs.HcpcsNum;
		}

		///<summary>Updates one Hcpcs in the database.</summary>
		public static void Update(Hcpcs hcpcs){
			string command="UPDATE hcpcs SET "
				+"HcpcsCode       = '"+POut.String(hcpcs.HcpcsCode)+"', "
				+"DescriptionShort= '"+POut.String(hcpcs.DescriptionShort)+"' "
				+"WHERE HcpcsNum = "+POut.Long(hcpcs.HcpcsNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Hcpcs in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Hcpcs hcpcs,Hcpcs oldHcpcs){
			string command="";
			if(hcpcs.HcpcsCode != oldHcpcs.HcpcsCode) {
				if(command!=""){ command+=",";}
				command+="HcpcsCode = '"+POut.String(hcpcs.HcpcsCode)+"'";
			}
			if(hcpcs.DescriptionShort != oldHcpcs.DescriptionShort) {
				if(command!=""){ command+=",";}
				command+="DescriptionShort = '"+POut.String(hcpcs.DescriptionShort)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE hcpcs SET "+command
				+" WHERE HcpcsNum = "+POut.Long(hcpcs.HcpcsNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Hcpcs,Hcpcs) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Hcpcs hcpcs,Hcpcs oldHcpcs) {
			if(hcpcs.HcpcsCode != oldHcpcs.HcpcsCode) {
				return true;
			}
			if(hcpcs.DescriptionShort != oldHcpcs.DescriptionShort) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Hcpcs from the database.</summary>
		public static void Delete(long hcpcsNum){
			string command="DELETE FROM hcpcs "
				+"WHERE HcpcsNum = "+POut.Long(hcpcsNum);
			Db.NonQ(command);
		}

	}
}
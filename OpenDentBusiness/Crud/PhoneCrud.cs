//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class PhoneCrud {
		///<summary>Gets one Phone object from the database using the primary key.  Returns null if not found.</summary>
		public static Phone SelectOne(long phoneNum){
			string command="SELECT * FROM phone "
				+"WHERE PhoneNum = "+POut.Long(phoneNum);
			List<Phone> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Phone object from the database using a query.</summary>
		public static Phone SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Phone> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Phone objects from the database using a query.</summary>
		public static List<Phone> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Phone> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Phone> TableToList(DataTable table){
			List<Phone> retVal=new List<Phone>();
			Phone phone;
			foreach(DataRow row in table.Rows) {
				phone=new Phone();
				phone.PhoneNum         = PIn.Long  (row["PhoneNum"].ToString());
				phone.Extension        = PIn.Int   (row["Extension"].ToString());
				phone.EmployeeName     = PIn.String(row["EmployeeName"].ToString());
				string clockStatus=row["ClockStatus"].ToString();
				if(clockStatus==""){
					phone.ClockStatus    =(ClockStatusEnum)0;
				}
				else try{
					phone.ClockStatus    =(ClockStatusEnum)Enum.Parse(typeof(ClockStatusEnum),clockStatus);
				}
				catch{
					phone.ClockStatus    =(ClockStatusEnum)0;
				}
				phone.Description      = PIn.String(row["Description"].ToString());
				phone.ColorBar         = Color.FromArgb(PIn.Int(row["ColorBar"].ToString()));
				phone.ColorText        = Color.FromArgb(PIn.Int(row["ColorText"].ToString()));
				phone.EmployeeNum      = PIn.Long  (row["EmployeeNum"].ToString());
				phone.CustomerNumber   = PIn.String(row["CustomerNumber"].ToString());
				phone.InOrOut          = PIn.String(row["InOrOut"].ToString());
				phone.PatNum           = PIn.Long  (row["PatNum"].ToString());
				phone.DateTimeStart    = PIn.DateT (row["DateTimeStart"].ToString());
				phone.WebCamImage      = PIn.String(row["WebCamImage"].ToString());
				phone.ScreenshotPath   = PIn.String(row["ScreenshotPath"].ToString());
				phone.ScreenshotImage  = PIn.String(row["ScreenshotImage"].ToString());
				phone.CustomerNumberRaw= PIn.String(row["CustomerNumberRaw"].ToString());
				phone.LastCallTimeStart= PIn.DateT (row["LastCallTimeStart"].ToString());
				retVal.Add(phone);
			}
			return retVal;
		}

		///<summary>Converts a list of Phone into a DataTable.</summary>
		public static DataTable ListToTable(List<Phone> listPhones,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Phone";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("PhoneNum");
			table.Columns.Add("Extension");
			table.Columns.Add("EmployeeName");
			table.Columns.Add("ClockStatus");
			table.Columns.Add("Description");
			table.Columns.Add("ColorBar");
			table.Columns.Add("ColorText");
			table.Columns.Add("EmployeeNum");
			table.Columns.Add("CustomerNumber");
			table.Columns.Add("InOrOut");
			table.Columns.Add("PatNum");
			table.Columns.Add("DateTimeStart");
			table.Columns.Add("WebCamImage");
			table.Columns.Add("ScreenshotPath");
			table.Columns.Add("ScreenshotImage");
			table.Columns.Add("CustomerNumberRaw");
			table.Columns.Add("LastCallTimeStart");
			foreach(Phone phone in listPhones) {
				table.Rows.Add(new object[] {
					POut.Long  (phone.PhoneNum),
					POut.Int   (phone.Extension),
					            phone.EmployeeName,
					POut.Int   ((int)phone.ClockStatus),
					            phone.Description,
					POut.Int   (phone.ColorBar.ToArgb()),
					POut.Int   (phone.ColorText.ToArgb()),
					POut.Long  (phone.EmployeeNum),
					            phone.CustomerNumber,
					            phone.InOrOut,
					POut.Long  (phone.PatNum),
					POut.DateT (phone.DateTimeStart,false),
					            phone.WebCamImage,
					            phone.ScreenshotPath,
					            phone.ScreenshotImage,
					            phone.CustomerNumberRaw,
					POut.DateT (phone.LastCallTimeStart,false),
				});
			}
			return table;
		}

		///<summary>Inserts one Phone into the database.  Returns the new priKey.</summary>
		public static long Insert(Phone phone){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				phone.PhoneNum=DbHelper.GetNextOracleKey("phone","PhoneNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(phone,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							phone.PhoneNum++;
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
				return Insert(phone,false);
			}
		}

		///<summary>Inserts one Phone into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Phone phone,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				phone.PhoneNum=ReplicationServers.GetKey("phone","PhoneNum");
			}
			string command="INSERT INTO phone (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PhoneNum,";
			}
			command+="Extension,EmployeeName,ClockStatus,Description,ColorBar,ColorText,EmployeeNum,CustomerNumber,InOrOut,PatNum,DateTimeStart,WebCamImage,ScreenshotPath,ScreenshotImage,CustomerNumberRaw,LastCallTimeStart) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(phone.PhoneNum)+",";
			}
			command+=
				     POut.Int   (phone.Extension)+","
				+"'"+POut.String(phone.EmployeeName)+"',"
				+"'"+POut.String(phone.ClockStatus.ToString())+"',"
				+"'"+POut.String(phone.Description)+"',"
				+    POut.Int   (phone.ColorBar.ToArgb())+","
				+    POut.Int   (phone.ColorText.ToArgb())+","
				+    POut.Long  (phone.EmployeeNum)+","
				+"'"+POut.String(phone.CustomerNumber)+"',"
				+"'"+POut.String(phone.InOrOut)+"',"
				+    POut.Long  (phone.PatNum)+","
				+    POut.DateT (phone.DateTimeStart)+","
				+"'"+POut.String(phone.WebCamImage)+"',"
				+"'"+POut.String(phone.ScreenshotPath)+"',"
				+"'"+POut.String(phone.ScreenshotImage)+"',"
				+"'"+POut.String(phone.CustomerNumberRaw)+"',"
				+    POut.DateT (phone.LastCallTimeStart)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				phone.PhoneNum=Db.NonQ(command,true);
			}
			return phone.PhoneNum;
		}

		///<summary>Inserts one Phone into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Phone phone){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(phone,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					phone.PhoneNum=DbHelper.GetNextOracleKey("phone","PhoneNum"); //Cacheless method
				}
				return InsertNoCache(phone,true);
			}
		}

		///<summary>Inserts one Phone into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Phone phone,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO phone (";
			if(!useExistingPK && isRandomKeys) {
				phone.PhoneNum=ReplicationServers.GetKeyNoCache("phone","PhoneNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="PhoneNum,";
			}
			command+="Extension,EmployeeName,ClockStatus,Description,ColorBar,ColorText,EmployeeNum,CustomerNumber,InOrOut,PatNum,DateTimeStart,WebCamImage,ScreenshotPath,ScreenshotImage,CustomerNumberRaw,LastCallTimeStart) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(phone.PhoneNum)+",";
			}
			command+=
				     POut.Int   (phone.Extension)+","
				+"'"+POut.String(phone.EmployeeName)+"',"
				+"'"+POut.String(phone.ClockStatus.ToString())+"',"
				+"'"+POut.String(phone.Description)+"',"
				+    POut.Int   (phone.ColorBar.ToArgb())+","
				+    POut.Int   (phone.ColorText.ToArgb())+","
				+    POut.Long  (phone.EmployeeNum)+","
				+"'"+POut.String(phone.CustomerNumber)+"',"
				+"'"+POut.String(phone.InOrOut)+"',"
				+    POut.Long  (phone.PatNum)+","
				+    POut.DateT (phone.DateTimeStart)+","
				+"'"+POut.String(phone.WebCamImage)+"',"
				+"'"+POut.String(phone.ScreenshotPath)+"',"
				+"'"+POut.String(phone.ScreenshotImage)+"',"
				+"'"+POut.String(phone.CustomerNumberRaw)+"',"
				+    POut.DateT (phone.LastCallTimeStart)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				phone.PhoneNum=Db.NonQ(command,true);
			}
			return phone.PhoneNum;
		}

		///<summary>Updates one Phone in the database.</summary>
		public static void Update(Phone phone){
			string command="UPDATE phone SET "
				+"Extension        =  "+POut.Int   (phone.Extension)+", "
				+"EmployeeName     = '"+POut.String(phone.EmployeeName)+"', "
				+"ClockStatus      = '"+POut.String(phone.ClockStatus.ToString())+"', "
				+"Description      = '"+POut.String(phone.Description)+"', "
				+"ColorBar         =  "+POut.Int   (phone.ColorBar.ToArgb())+", "
				+"ColorText        =  "+POut.Int   (phone.ColorText.ToArgb())+", "
				+"EmployeeNum      =  "+POut.Long  (phone.EmployeeNum)+", "
				+"CustomerNumber   = '"+POut.String(phone.CustomerNumber)+"', "
				+"InOrOut          = '"+POut.String(phone.InOrOut)+"', "
				+"PatNum           =  "+POut.Long  (phone.PatNum)+", "
				+"DateTimeStart    =  "+POut.DateT (phone.DateTimeStart)+", "
				+"WebCamImage      = '"+POut.String(phone.WebCamImage)+"', "
				+"ScreenshotPath   = '"+POut.String(phone.ScreenshotPath)+"', "
				+"ScreenshotImage  = '"+POut.String(phone.ScreenshotImage)+"', "
				+"CustomerNumberRaw= '"+POut.String(phone.CustomerNumberRaw)+"', "
				+"LastCallTimeStart=  "+POut.DateT (phone.LastCallTimeStart)+" "
				+"WHERE PhoneNum = "+POut.Long(phone.PhoneNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Phone in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Phone phone,Phone oldPhone){
			string command="";
			if(phone.Extension != oldPhone.Extension) {
				if(command!=""){ command+=",";}
				command+="Extension = "+POut.Int(phone.Extension)+"";
			}
			if(phone.EmployeeName != oldPhone.EmployeeName) {
				if(command!=""){ command+=",";}
				command+="EmployeeName = '"+POut.String(phone.EmployeeName)+"'";
			}
			if(phone.ClockStatus != oldPhone.ClockStatus) {
				if(command!=""){ command+=",";}
				command+="ClockStatus = '"+POut.String(phone.ClockStatus.ToString())+"'";
			}
			if(phone.Description != oldPhone.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(phone.Description)+"'";
			}
			if(phone.ColorBar != oldPhone.ColorBar) {
				if(command!=""){ command+=",";}
				command+="ColorBar = "+POut.Int(phone.ColorBar.ToArgb())+"";
			}
			if(phone.ColorText != oldPhone.ColorText) {
				if(command!=""){ command+=",";}
				command+="ColorText = "+POut.Int(phone.ColorText.ToArgb())+"";
			}
			if(phone.EmployeeNum != oldPhone.EmployeeNum) {
				if(command!=""){ command+=",";}
				command+="EmployeeNum = "+POut.Long(phone.EmployeeNum)+"";
			}
			if(phone.CustomerNumber != oldPhone.CustomerNumber) {
				if(command!=""){ command+=",";}
				command+="CustomerNumber = '"+POut.String(phone.CustomerNumber)+"'";
			}
			if(phone.InOrOut != oldPhone.InOrOut) {
				if(command!=""){ command+=",";}
				command+="InOrOut = '"+POut.String(phone.InOrOut)+"'";
			}
			if(phone.PatNum != oldPhone.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(phone.PatNum)+"";
			}
			if(phone.DateTimeStart != oldPhone.DateTimeStart) {
				if(command!=""){ command+=",";}
				command+="DateTimeStart = "+POut.DateT(phone.DateTimeStart)+"";
			}
			if(phone.WebCamImage != oldPhone.WebCamImage) {
				if(command!=""){ command+=",";}
				command+="WebCamImage = '"+POut.String(phone.WebCamImage)+"'";
			}
			if(phone.ScreenshotPath != oldPhone.ScreenshotPath) {
				if(command!=""){ command+=",";}
				command+="ScreenshotPath = '"+POut.String(phone.ScreenshotPath)+"'";
			}
			if(phone.ScreenshotImage != oldPhone.ScreenshotImage) {
				if(command!=""){ command+=",";}
				command+="ScreenshotImage = '"+POut.String(phone.ScreenshotImage)+"'";
			}
			if(phone.CustomerNumberRaw != oldPhone.CustomerNumberRaw) {
				if(command!=""){ command+=",";}
				command+="CustomerNumberRaw = '"+POut.String(phone.CustomerNumberRaw)+"'";
			}
			if(phone.LastCallTimeStart != oldPhone.LastCallTimeStart) {
				if(command!=""){ command+=",";}
				command+="LastCallTimeStart = "+POut.DateT(phone.LastCallTimeStart)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE phone SET "+command
				+" WHERE PhoneNum = "+POut.Long(phone.PhoneNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Phone,Phone) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Phone phone,Phone oldPhone) {
			if(phone.Extension != oldPhone.Extension) {
				return true;
			}
			if(phone.EmployeeName != oldPhone.EmployeeName) {
				return true;
			}
			if(phone.ClockStatus != oldPhone.ClockStatus) {
				return true;
			}
			if(phone.Description != oldPhone.Description) {
				return true;
			}
			if(phone.ColorBar != oldPhone.ColorBar) {
				return true;
			}
			if(phone.ColorText != oldPhone.ColorText) {
				return true;
			}
			if(phone.EmployeeNum != oldPhone.EmployeeNum) {
				return true;
			}
			if(phone.CustomerNumber != oldPhone.CustomerNumber) {
				return true;
			}
			if(phone.InOrOut != oldPhone.InOrOut) {
				return true;
			}
			if(phone.PatNum != oldPhone.PatNum) {
				return true;
			}
			if(phone.DateTimeStart != oldPhone.DateTimeStart) {
				return true;
			}
			if(phone.WebCamImage != oldPhone.WebCamImage) {
				return true;
			}
			if(phone.ScreenshotPath != oldPhone.ScreenshotPath) {
				return true;
			}
			if(phone.ScreenshotImage != oldPhone.ScreenshotImage) {
				return true;
			}
			if(phone.CustomerNumberRaw != oldPhone.CustomerNumberRaw) {
				return true;
			}
			if(phone.LastCallTimeStart != oldPhone.LastCallTimeStart) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Phone from the database.</summary>
		public static void Delete(long phoneNum){
			string command="DELETE FROM phone "
				+"WHERE PhoneNum = "+POut.Long(phoneNum);
			Db.NonQ(command);
		}

	}
}
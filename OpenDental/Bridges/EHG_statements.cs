﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	public class EHG_statements {
		//these are temporary:
		//private static string vendorID="68";
		//private static string vendorPMScode="144";
		//private static string clientAccountNumber="8011";//the dental office number set by EHG
		//private static string creditCardChoices="MC,D,V,A";//MasterCard,Discover,Visa,AmericanExpress
		//private static string userName="";
		//private static string password="";

		///<summary>Returns empty list if no errors.  Otherwise returns a list with error messages.</summary>
		public static List <string> Validate(long clinicNum) {
			List <string> listErrors=new List<string>();
			Clinic clinic=Clinics.GetClinic(clinicNum);
			Ebill eBillClinic=Ebills.GetForClinic(clinicNum);
			Ebill eBillDefault=Ebills.GetForClinic(0);
			EHG_Address addressRemit=null;
			if(eBillClinic==null) {
				addressRemit=GetAddress(eBillDefault.RemitAddress,clinic);
			}
			else {
				addressRemit=GetAddress(eBillClinic.RemitAddress,clinic);
			}
			if(addressRemit.Address1.Trim().Length==0 || addressRemit.City.Trim().Length==0
				|| addressRemit.State.Trim().Length==0 || addressRemit.Zip.Trim().Length==0)
			{
				listErrors.Add(Lan.g("EHG_Statements","invalid")+" "+Lan.g("EHG_Statements",addressRemit.Source));
			}
			return listErrors;
		}

		///<summary>Generates all the xml up to the point where the first statement would go.</summary>
		public static void GeneratePracticeInfo(XmlWriter writer,long clinicNum) {
			Clinic clinic=Clinics.GetClinic(clinicNum);
			Ebill eBillClinic=Ebills.GetForClinic(clinicNum);
			Ebill eBillDefault=Ebills.GetForClinic(0);
			writer.WriteProcessingInstruction("xml","version = \"1.0\" standalone=\"yes\"");
			writer.WriteStartElement("EISStatementFile");
			writer.WriteAttributeString("VendorID",PrefC.GetString(PrefName.BillingElectVendorId));
			writer.WriteAttributeString("OutputFormat","StmOut_Blue6Col");
			writer.WriteAttributeString("Version","2");
			writer.WriteElementString("SubmitDate",DateTime.Today.ToString("yyyy-MM-dd"));
			writer.WriteElementString("PrimarySubmitter",PrefC.GetString(PrefName.BillingElectVendorPMSCode));
			writer.WriteElementString("Transmitter","EHG");
			writer.WriteStartElement("Practice");
			string billingClientAccountNumber=eBillDefault.ClientAcctNumber;
			if(eBillClinic!=null && eBillClinic.ClientAcctNumber!="") {//clinic eBill entry exists, check the fields for overrides
				billingClientAccountNumber=eBillClinic.ClientAcctNumber;
			}
			writer.WriteAttributeString("AccountNumber",billingClientAccountNumber);
			//sender address----------------------------------------------------------
			writer.WriteStartElement("SenderAddress");
			if(clinic==null) {
				writer.WriteElementString("Name",PrefC.GetString(PrefName.PracticeTitle));
			}
			else {
				writer.WriteElementString("Name",clinic.Description);
			}
			if(eBillClinic==null) {
				WriteAddress(writer,eBillDefault.PracticeAddress,clinic);
			}
			else {
				WriteAddress(writer,eBillClinic.PracticeAddress,clinic);
			}
			writer.WriteEndElement();//senderAddress
			//remit address----------------------------------------------------------
			writer.WriteStartElement("RemitAddress");
			if(clinic==null) {
				writer.WriteElementString("Name",PrefC.GetString(PrefName.PracticeTitle));
			}
			else {
				writer.WriteElementString("Name",clinic.Description);
			}
			if(eBillClinic==null) {
				WriteAddress(writer,eBillDefault.RemitAddress,clinic);				
			}
			else {
				WriteAddress(writer,eBillClinic.RemitAddress,clinic);
			}
			writer.WriteEndElement();//remitAddress
			//Rendering provider------------------------------------------------------
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			writer.WriteStartElement("RenderingProvider");
			writer.WriteElementString("Name",prov.GetFormalName());
			writer.WriteElementString("LicenseNumber",prov.StateLicense);
			writer.WriteElementString("State",PrefC.GetString(PrefName.PracticeST));
			writer.WriteEndElement();//Rendering provider
		}

		private static void WriteAddress(XmlWriter writer,EbillAddress eBillAddress,Clinic clinic) {
			EHG_Address address=GetAddress(eBillAddress,clinic);
			writer.WriteElementString("Address1",address.Address1);
			writer.WriteElementString("Address2",address.Address2);
			writer.WriteElementString("City",address.City);
			writer.WriteElementString("State",address.State);
			writer.WriteElementString("Zip",address.Zip);
			writer.WriteElementString("Phone",address.Phone);
		}

		///<summary>The clinic variable can be null.</summary>
		public static EHG_Address GetAddress(EbillAddress eBillAddress,Clinic clinic) {
			EHG_Address address=new EHG_Address();
			//If using practice information or using the default (no clinic) Ebill and a clinic enum is specified, use the practice level information.
			if(eBillAddress==EbillAddress.PracticePhysical || (clinic==null && eBillAddress==EbillAddress.ClinicPhysical)) {
				address.Address1=PrefC.GetString(PrefName.PracticeAddress);
				address.Address2=PrefC.GetString(PrefName.PracticeAddress2);
				address.City=PrefC.GetString(PrefName.PracticeCity);
				address.State=PrefC.GetString(PrefName.PracticeST);
				address.Zip=PrefC.GetString(PrefName.PracticeZip);
				address.Phone=PrefC.GetString(PrefName.PracticePhone);//enforced to be 10 digit fairly rigidly by the UI
				address.Source="Practice Physical Treating Address";
			}
			else if(eBillAddress==EbillAddress.PracticePayTo || (clinic==null && eBillAddress==EbillAddress.ClinicPayTo)) {
				address.Address1=PrefC.GetString(PrefName.PracticePayToAddress);
				address.Address2=PrefC.GetString(PrefName.PracticePayToAddress2);
				address.City=PrefC.GetString(PrefName.PracticePayToCity);
				address.State=PrefC.GetString(PrefName.PracticePayToST);
				address.Zip=PrefC.GetString(PrefName.PracticePayToZip);
				address.Phone=PrefC.GetString(PrefName.PracticePhone);//enforced to be 10 digit fairly rigidly by the UI
				address.Source="Practice Pay To Address";
			}
			else if(eBillAddress==EbillAddress.PracticeBilling || (clinic==null && eBillAddress==EbillAddress.ClinicBilling)) {
				address.Address1=PrefC.GetString(PrefName.PracticeBillingAddress);
				address.Address2=PrefC.GetString(PrefName.PracticeBillingAddress2);
				address.City=PrefC.GetString(PrefName.PracticeBillingCity);
				address.State=PrefC.GetString(PrefName.PracticeBillingST);
				address.Zip=PrefC.GetString(PrefName.PracticeBillingZip);
				address.Phone=PrefC.GetString(PrefName.PracticePhone);//enforced to be 10 digit fairly rigidly by the UI
				address.Source="Practice Billing Address";
			}
			else if(eBillAddress==EbillAddress.ClinicPhysical) {
				address.Address1=clinic.Address;
				address.Address2=clinic.Address2;
				address.City=clinic.City;
				address.State=clinic.State;
				address.Zip=clinic.Zip;
				address.Phone=clinic.Phone;//enforced to be 10 digit fairly rigidly by the UI
				address.Source="Clinic Physical Treating Address";
			}
			else if(eBillAddress==EbillAddress.ClinicPayTo) {
				address.Address1=clinic.PayToAddress;
				address.Address2=clinic.PayToAddress2;
				address.City=clinic.PayToCity;
				address.State=clinic.PayToState;
				address.Zip=clinic.PayToZip;
				address.Phone=clinic.Phone;//enforced to be 10 digit fairly rigidly by the UI
				address.Source="Clinic Pay To Address";
			}
			else if(eBillAddress==EbillAddress.ClinicBilling) {
				address.Address1=clinic.BillingAddress;
				address.Address2=clinic.BillingAddress2;
				address.City=clinic.BillingCity;
				address.State=clinic.BillingState;
				address.Zip=clinic.BillingZip;
				address.Phone=clinic.Phone;//enforced to be 10 digit fairly rigidly by the UI
				address.Source="Clinic Billing Address";
			}
			return address;
		}

		///<summary>Adds the xml for one statement. Validation is performed here. Throws an exception if there is a validation failure.</summary>
		public static void GenerateOneStatement(XmlWriter writer,Statement stmt,Patient pat,Family fam,DataSet dataSet){
			Patient guar=fam.ListPats[0];
			if(!Regex.IsMatch(guar.State,"^[A-Z]{2}$")) {
				throw new ApplicationException(Lan.g("EHG_Statements","Guarantor state must be two uppercase characters.")+" "+guar.FName+" "+guar.LName+" #"+guar.PatNum);
			}
			writer.WriteStartElement("EisStatement");
			writer.WriteAttributeString("OutputFormat","StmOut_Blue6Col");
			writer.WriteAttributeString("CreditCardChoice",PrefC.GetString(PrefName.BillingElectCreditCardChoices));
			writer.WriteStartElement("Patient");
			writer.WriteElementString("Name",guar.GetNameFLFormal());
			writer.WriteElementString("Account",guar.PatNum.ToString());
			writer.WriteElementString("Address1",guar.Address);
			writer.WriteElementString("Address2",guar.Address2);
			writer.WriteElementString("City",guar.City);
			writer.WriteElementString("State",guar.State);
			writer.WriteElementString("Zip",guar.Zip);
			string email="";
			Def billingDef=DefC.GetDef(DefCat.BillingTypes,guar.BillingType);
			if(billingDef.ItemValue=="E") {
				email=guar.Email;
			}
			writer.WriteElementString("EMail",email);
			//Account summary-----------------------------------------------------------------------
			writer.WriteStartElement("AccountSummary");
			if(stmt.DateRangeFrom.Year<1880) {//make up a statement date.
				writer.WriteElementString("PriorStatementDate",DateTime.Today.AddMonths(-1).ToString("MM/dd/yyyy"));
			}
			else {
				writer.WriteElementString("PriorStatementDate",stmt.DateRangeFrom.AddDays(-1).ToString("MM/dd/yyyy"));
			}
			DateTime dueDate;
			if(PrefC.GetLong(PrefName.StatementsCalcDueDate)==-1){
				dueDate=DateTime.Today.AddDays(10);
			}
			else{
				dueDate=DateTime.Today.AddDays(PrefC.GetLong(PrefName.StatementsCalcDueDate));
			}
			writer.WriteElementString("DueDate",dueDate.ToString("MM/dd/yyyy"));
			writer.WriteElementString("StatementDate",stmt.DateSent.ToString("MM/dd/yyyy"));
			double balanceForward=0;
			for(int r=0;r<dataSet.Tables["misc"].Rows.Count;r++){
				if(dataSet.Tables["misc"].Rows[r]["descript"].ToString()=="balanceForward"){
					balanceForward=PIn.Double(dataSet.Tables["misc"].Rows[r]["value"].ToString());
				}
			}
			writer.WriteElementString("PriorBalance",balanceForward.ToString("F2"));
			writer.WriteElementString("RunningBalance","");//for future use
			writer.WriteElementString("PerPayAdj","");//optional
			writer.WriteElementString("InsPayAdj","");//optional
			writer.WriteElementString("Adjustments","");//for future use
			writer.WriteElementString("NewCharges","");//optional
			writer.WriteElementString("FinanceCharges","");//for future use
			DataTable tableAccount=null;
			for(int i=0;i<dataSet.Tables.Count;i++) {
				if(dataSet.Tables[i].TableName.StartsWith("account")) {
					tableAccount=dataSet.Tables[i];
				}
			}
			double credits=0;
			for(int i=0;i<tableAccount.Rows.Count;i++) {
				credits+=PIn.Double(tableAccount.Rows[i]["creditsDouble"].ToString());
			}
			writer.WriteElementString("Credits",credits.ToString("F2"));
			//on a regular printed statement, the amount due at the top might be different from the balance at the middle right.
			//This is because of payment plan balances.
			//But in e-bills, there is only one amount due.
			//Insurance estimate is already subtracted, and payment plan balance is already added.
			double amountDue=guar.BalTotal;
			//add payplan due amt:
			for(int m=0;m<dataSet.Tables["misc"].Rows.Count;m++) {
				if(dataSet.Tables["misc"].Rows[m]["descript"].ToString()=="payPlanDue") {
					amountDue+=PIn.Double(dataSet.Tables["misc"].Rows[m]["value"].ToString());
				}
			}
			if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
				writer.WriteElementString("EstInsPayments","");//optional.
			}
			else {//this is typical
				writer.WriteElementString("EstInsPayments",guar.InsEst.ToString("F2"));//optional.
				amountDue-=guar.InsEst;
			}
			InstallmentPlan installPlan=InstallmentPlans.GetOneForFam(guar.PatNum);
			if(installPlan!=null){
				//show lesser of normal total balance or the monthly payment amount.
				if(installPlan.MonthlyPayment < amountDue) {
					amountDue=installPlan.MonthlyPayment;
				}
			}
			writer.WriteElementString("PatientShare",amountDue.ToString("F2"));
			writer.WriteElementString("CurrentBalance",amountDue.ToString("F2"));//this is ambiguous.  It seems to be AmountDue, but it could possibly be 0-30 days aging
			writer.WriteElementString("PastDue30",guar.Bal_31_60.ToString("F2"));//optional
			writer.WriteElementString("PastDue60",guar.Bal_61_90.ToString("F2"));//optional
			writer.WriteElementString("PastDue90",guar.BalOver90.ToString("F2"));//optional
			writer.WriteElementString("PastDue120","");//optional
			writer.WriteEndElement();//AccountSummary
			//Notes-----------------------------------------------------------------------------------
			writer.WriteStartElement("Notes");
			if(stmt.NoteBold!="") {
				writer.WriteStartElement("Note");
				writer.WriteAttributeString("FgColor","Red");//ColorToHexString(Color.DarkRed));
				//writer.WriteAttributeString("BgColor",ColorToHexString(Color.White));
				writer.WriteString(Tidy(stmt.NoteBold,500));//Limit of 500 char on notes.
				writer.WriteEndElement();//Note
			}
			if(stmt.Note!="") {
				writer.WriteStartElement("Note");
				//writer.WriteAttributeString("FgColor",ColorToHexString(Color.Black));
				//writer.WriteAttributeString("BgColor",ColorToHexString(Color.White));
				writer.WriteString(Tidy(stmt.Note,500));//Limit of 500 char on notes.
				writer.WriteEndElement();//Note
			}
			writer.WriteEndElement();//Notes
			//Detail items------------------------------------------------------------------------------
			writer.WriteStartElement("DetailItems");
			//string note;
			string descript;
			string fulldesc;
			string procCode;
			string tth;
			//string linedesc;
			string[] lineArray;
			List<string> lines;
			DateTime date;
			int seq=0;
			for(int i=0;i<tableAccount.Rows.Count;i++) {
				procCode=tableAccount.Rows[i]["ProcCode"].ToString();
				tth=tableAccount.Rows[i]["tth"].ToString();
				descript=tableAccount.Rows[i]["description"].ToString();
				fulldesc=procCode+" "+tth+" "+descript;//There are frequently CRs within a procedure description for things like ins est.
				lineArray=fulldesc.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
				lines=new List<string>(lineArray);
				//Jessica at DentalXchange says limit is 120.  Specs say limit is 30.
				const int lineMaxLen=120;
				if(lines[0].Length > lineMaxLen) {
					string newline=lines[0].Substring(lineMaxLen);
					lines[0]=lines[0].Substring(0,lineMaxLen);//first half
					lines.Insert(1,newline);//second half
				}
				for(int li=0;li<lines.Count;li++) {
					writer.WriteStartElement("DetailItem");//has a child item. We won't add optional child note
					writer.WriteAttributeString("sequence",seq.ToString());
					writer.WriteStartElement("Item");
					if(li==0) {
						date=(DateTime)tableAccount.Rows[i]["DateTime"];
						writer.WriteElementString("Date",date.ToString("MM/dd/yyyy"));
						writer.WriteElementString("PatientName",tableAccount.Rows[i]["patient"].ToString());
					}
					else {
						writer.WriteElementString("Date","");
						writer.WriteElementString("PatientName","");
					}
					writer.WriteStartElement("Description");
					writer.WriteCData(Tidy(lines[li],lineMaxLen));//CData to allow any string, including punctuation, syntax characters and special characters.
					writer.WriteEndElement();//Description
					if(li==0) {
						writer.WriteElementString("Charges",tableAccount.Rows[i]["charges"].ToString());
						writer.WriteElementString("Credits",tableAccount.Rows[i]["credits"].ToString());
						writer.WriteElementString("Balance",tableAccount.Rows[i]["balance"].ToString());
					}
					else {
						writer.WriteElementString("Charges","");
						writer.WriteElementString("Credits","");
						writer.WriteElementString("Balance","");
					}
					writer.WriteEndElement();//Item
					writer.WriteEndElement();//DetailItem
					seq++;
				}
				/*The code below just didn't work because notes don't get displayed on the statement.
				linedesc=lines[0];
				note="";
				if(linedesc.Length>30) {
					note=linedesc.Substring(30);
					linedesc=linedesc.Substring(0,30);
				}
				for(int l=1;l<lines.Length;l++) {
					if(note!="") {
						note+="\r\n";
					}
					note+=lines[l];
				}
				
				if(note!="") {
					writer.WriteStartElement("Note");
					//we're not going to specify colors here since they're optional
					writer.WriteCData(note);
					writer.WriteEndElement();//Note
				}*/
			}
			writer.WriteEndElement();//DetailItems
			writer.WriteEndElement();//Patient
			writer.WriteEndElement();//EisStatement
		}

		///<summary>Converts a .net color to a hex string.  Includes the #.</summary>
		private static string ColorToHexString(Color color) {
			char[] hexDigits={'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};
			byte[] bytes = new byte[3];
			bytes[0] = color.R;
			bytes[1] = color.G;
			bytes[2] = color.B;
			char[] chars=new char[bytes.Length * 2];
			for(int i=0;i<bytes.Length;i++){
				int b=bytes[i];
				chars[i*2]=hexDigits[b >> 4];
				chars[i*2+1]=hexDigits[b & 0xF];
			}
			string retVal=new string(chars);
			retVal="#"+retVal;
			return retVal;
		}

		///<summary>After statements are added, this adds the necessary closing xml elements.</summary>
		public static void GenerateWrapUp(XmlWriter writer) {
			writer.WriteEndElement();//Practice
			writer.WriteEndElement();//EISStatementFile
		}

		private static string Tidy(string str,int len) {
			if(str.Length>len) {
				return str.Substring(0,len);
			}
			return str;
		}

		///<summary>Surround with try catch.  The "data" is the previously constructed xml.  If the internet connection is lost or unavailable, then the exception thrown will be a 404 error similar to the following: "The remote server returned an error: (404) Not Found"</summary>
		public static void Send(string data,long clinicNum) {
			//Validate the structure of the XML before sending.
			StringReader sr=new StringReader(data);
			try {
				XmlReader xmlr=XmlReader.Create(sr);
				while(xmlr.Read()) { //Read every node an ensure that there are no exceptions thrown.
				}
			}
			catch(Exception ex) {
				throw new ApplicationException("Invalid XML in statement batch: "+ex.Message);
			}
			finally {
				sr.Dispose();
			}
			string strHistoryFile="";
			if(PrefC.GetBool(PrefName.BillingElectSaveHistory)) {
				string strHistoryDir=CodeBase.ODFileUtils.CombinePaths(ImageStore.GetPreferredAtoZpath(),"EHG_History");
				if(!Directory.Exists(strHistoryDir)) {
					Directory.CreateDirectory(strHistoryDir);
				}
				strHistoryFile=CodeBase.ODFileUtils.CreateRandomFile(strHistoryDir,".txt");
				File.WriteAllText(strHistoryFile,data);
			}
			//Step 1: Post authentication request:
			Version myVersion=new Version(Application.ProductVersion);
			HttpWebRequest webReq;
			WebResponse response;
			StreamReader readStream;
			string str;
			string[] responseParams;
			string status="";
			string group="";
			string userid="";
			string authid="";
			string errormsg="";
			string alertmsg="";
			string curParam="";
			string serverName=//"https://prelive.dentalxchange.com/dci/upload.svl";
				"https://claimconnect.dentalxchange.com/dci/upload.svl";
			webReq=(HttpWebRequest)WebRequest.Create(serverName);
			Ebill ebillDefault=Ebills.GetForClinic(0);
			string billingUserName=ebillDefault.ElectUserName;
			string billingPassword=ebillDefault.ElectPassword;
			if(PrefC.HasClinicsEnabled && clinicNum!=0) {
				Ebill eBill=Ebills.GetForClinic(clinicNum);
				if(eBill!=null) {//eBill entry exists, check the fields for overrides.
					if(eBill.ElectUserName!="") {
						billingUserName=eBill.ElectUserName;
					}
					if(eBill.ElectPassword!="") {
						billingPassword=eBill.ElectPassword;
					}
				}
			}
			string postData=
				"Function=Auth"//CONSTANT; signifies that this is an authentication request
				+"&Source=STM"//CONSTANT; file format
				+"&UploaderName=OpenDental"//CONSTANT
				+"&UploaderVersion="+myVersion.Major.ToString()+"."+myVersion.Minor.ToString()+"."+myVersion.Build.ToString()//eg 12.3.24			
				+"&Username="+billingUserName
				+"&Password="+billingPassword;
			webReq.KeepAlive=false;
			webReq.Method="POST";
			webReq.ContentType="application/x-www-form-urlencoded";
			webReq.ContentLength=postData.Length;
			ASCIIEncoding encoding=new ASCIIEncoding();
			byte[] bytes=encoding.GetBytes(postData);
			Stream streamOut=webReq.GetRequestStream();
			streamOut.Write(bytes,0,bytes.Length);
			streamOut.Close();
			response=webReq.GetResponse();
			//Process the authentication response:
			readStream=new StreamReader(response.GetResponseStream(),Encoding.ASCII);
			str=readStream.ReadToEnd();
			readStream.Close();
			if(strHistoryFile!="") {//Tack the response onto the end of the saved history file if one was created above.
				File.AppendAllText(strHistoryFile,"\r\n\r\nCONNECTION REQUEST: postData.Length="+postData.Length+" bytes.Length="+bytes.Length+"==============\r\n"
					+" RESPONSE TO CONNECTION REQUEST================================================================\r\n"+str);
			}
			//Debug.WriteLine(str);
			//MessageBox.Show(str);
			responseParams=str.Split('&');
			for(int i=0;i<responseParams.Length;i++) {
				curParam=GetParam(responseParams[i]);
				switch(curParam) {
					case "Status":
						status=GetParamValue(responseParams[i]);
						break;
					case "GROUP":
						group=GetParamValue(responseParams[i]);
						break;
					case "UserID":
						userid=GetParamValue(responseParams[i]);
						break;
					case "AuthenticationID":
						authid=GetParamValue(responseParams[i]);
						break;
					case "ErrorMessage":
						errormsg=GetParamValue(responseParams[i]);
						break;
					case "AlertMessage":
						alertmsg=GetParamValue(responseParams[i]);
						break;
					default:
						throw new Exception("Unexpected parameter: "+curParam);
				}
			}
			//Process response for errors:
			if(alertmsg!="") {
				MessageBox.Show(alertmsg);
			}
			switch(status) {
				case "0":
					//MessageBox.Show("Authentication successful.");
					break;
				case "1":
					throw new Exception("Authentication failed. "+errormsg);
				case "2":
					throw new Exception("Cannot authenticate at this time. "+errormsg);
				case "3":
					throw new Exception("Invalid authentication request. "+errormsg);
				case "4":
					throw new Exception("Invalid program version. "+errormsg);
				case "5":
					throw new Exception("No customer contract. "+errormsg);
				default://some as-yet-undefined error
					throw new Exception("Error "+status+". "+errormsg);
			}
			//Step 2: Post upload request:
			//string fileName=Directory.GetFiles(clearhouse.ExportPath)[0];
			string boundary="------------7d13e425b00d0";
			postData=
				"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"Function\"\r\n"
				+"\r\n"
				+"Upload\r\n"
				+"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"Source\"\r\n"
				+"\r\n"
				+"STM\r\n"
				+"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"AuthenticationID\"\r\n"
				+"\r\n"
				+authid+"\r\n"
				+"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"File\"; filename=\""+"stmt.xml"+"\"\r\n"
				+"Content-Type: text/plain\r\n"
				+"\r\n"
			//using(StreamReader sr=new StreamReader(fileName)) {
			//	postData+=sr.ReadToEnd()+"\r\n"
				+data+"\r\n"
				+"--"+boundary+"--";
			//}
			//Debug.WriteLine(postData);
			//MessageBox.Show(postData);
			webReq=(HttpWebRequest)WebRequest.Create(serverName);
			//Timeout documentation: https://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.timeout(v=vs.110).aspx.
			//Timeout: "Gets or sets the time-out value in milliseconds for the GetResponse and GetRequestStream methods."
			//Timeout default is 100 seconds, which should be sufficient in waiting for a reply from dentalxchange, since the reply is small.
			//ReadWriteTimeout documentation: https://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.readwritetimeout%28v=vs.110%29.aspx
			//ReadWriteTimeout: "Gets or sets a time-out in milliseconds when writing to or reading from a stream."
			//ReadWriteTimeout default is 300 seconds (5 minutes).
			//Our message box that tells the user to wait up to 10 minutes for bills to send, therefore we need at least a 10 minute ReadWriteTimeout.
			//The user sees progress in the UI when sending.  We can increase timeouts as much as we want without making the program look like it crashed.
			webReq.ReadWriteTimeout=600000;//10 minutes = 10*60 seconds = 600 seconds = 600*1000 milliseconds = 600,000 milliseconds.
			webReq.KeepAlive=false;
			webReq.Method="POST";
			webReq.ContentType="multipart/form-data; boundary="+boundary;
			webReq.ContentLength=postData.Length;
			bytes=encoding.GetBytes(postData);
			streamOut=webReq.GetRequestStream();
			streamOut.Write(bytes,0,bytes.Length);
			streamOut.Close();
			response=webReq.GetResponse();
			//Process the response
			readStream=new StreamReader(response.GetResponseStream(),Encoding.ASCII);
			str=readStream.ReadToEnd();
			readStream.Close();
			if(strHistoryFile!="") {//Tack the response onto the end of the saved history file if one was created above.
				File.AppendAllText(strHistoryFile,"\r\n\r\nUPLOAD REQUEST: postData.Length="+postData.Length+" bytes.Length="+bytes.Length+"==============\r\n"
					+" RESPONSE TO DATA UPLOAD================================================================\r\n"+str);
			}
			errormsg="";
			status="";
			str=str.Replace("\r\n","");
			//Debug.Write(str);
			if(str.Length>300) {
				throw new Exception("Unknown lengthy error message received.");
			}
			responseParams=str.Split('&');
			for(int i=0;i<responseParams.Length;i++){
				curParam=GetParam(responseParams[i]);
				switch(curParam){
					case "Status":
						status=GetParamValue(responseParams[i]);
						break;
					case "Error Message":
					case "ErrorMessage":
						errormsg=GetParamValue(responseParams[i]);
						break;
					case "Filename":
					case "Timestamp":
						break;
					case ""://errorMessage blank
						break;
					default:
						throw new Exception(str);//"Unexpected parameter: "+str);//curParam+"*");
				}
			}
			switch(status){
				case "0":
					//MessageBox.Show("Upload successful.");
					break;
				case "1":
					throw new Exception("Authentication failed. "+errormsg);
				case "2":
					throw new Exception("Cannot upload at this time. "+errormsg);
			}
		}

		private static string GetParam(string paramAndValue) {
			if(paramAndValue=="") {
				return "";
			}
			string[] pair=paramAndValue.Split('=');
			//if(pair.Length!=2){
			//	throw new Exception("Unexpected parameter from server: "+paramAndValue);
			return pair[0];
		}

		private static string GetParamValue(string paramAndValue) {
			if(paramAndValue=="") {
				return "";
			}
			string[] pair=paramAndValue.Split('=');
			//if(pair.Length!=2){
			//	throw new Exception("Unexpected parameter from server: "+paramAndValue);
			//}
			if(pair.Length==1) {
				return "";
			}
			return pair[1];
		}


	}

	public class EHG_Address {
		public string Address1;
		public string Address2;
		public string City;
		public string State;
		public string Zip;
		public string Phone;
		public string Source;
	}

}

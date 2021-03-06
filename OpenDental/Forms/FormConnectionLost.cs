using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormConnectionLost:Form {

		///<summary>Optionally set errorMessage to override the label text that is displayed to the user.</summary>
		public FormConnectionLost(string errorMessage="") {
			InitializeComponent();
			labelErrMsg.Text=errorMessage;
		}

		private void TestConnection() {
			Cursor=Cursors.WaitCursor;
			DataConnection dconn=new DataConnection();
			bool isConnected=true;
			try {
				dconn.SetDb(DataConnection.GetCurrentConnectionString(),"",DataConnection.DBtype);
			}
			catch { 
				isConnected=false;
			}
			Cursor=Cursors.Default;
			if(isConnected) {
				DialogResult=DialogResult.OK;
			}
		}

		private void timerConnTest_Tick(object sender,EventArgs e) {
			TestConnection();
		}

		private void butRetry_Click(object sender,EventArgs e) {
			TestConnection();
		}

		private void butExit_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormConnectionLost_FormClosing(object sender,FormClosingEventArgs e) {
			timerConnTest.Stop();
		}

	}
}
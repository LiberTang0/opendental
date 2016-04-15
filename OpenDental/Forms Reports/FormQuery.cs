//using Excel;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Threading;
using OpenDentBusiness;

namespace OpenDental{
///<summary>This is getting very outdated.  I realize it is difficult to use and will be phased out soon. The report displayed will be based on report.TableQ and report.</summary>
	public class FormQuery : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.DataGrid grid2;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butSubmit;
		private System.Windows.Forms.RadioButton radioRaw;
		///<summary></summary>
		public System.Windows.Forms.RadioButton radioHuman;
		private OpenDental.UI.Button butFavorite;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private OpenDental.UI.Button butAdd;
		private DataGridTableStyle myGridTS;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog2;
		private System.Drawing.Printing.PrintDocument pd2;
		private bool totalsPrinted;
		private bool summaryPrinted;
		private int linesPrinted;
		private int pagesPrinted;
		///<summary></summary>
		public bool IsReport;
		private bool headerPrinted;
		private System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private bool tablePrinted;
		private System.Drawing.Font titleFont = new System.Drawing.Font("Arial",14,FontStyle.Bold);
		private System.Drawing.Font subtitleFont=new System.Drawing.Font("Arial",8,FontStyle.Bold);
		private System.Drawing.Font colCaptFont=new System.Drawing.Font("Arial",8,FontStyle.Bold);
		private System.Drawing.Font bodyFont = new System.Drawing.Font("Arial", 8);
		private OpenDental.UI.Button butFullPage;
		private System.Windows.Forms.Panel panelZoom;
		private System.Windows.Forms.Label labelTotPages;
		private System.Windows.Forms.Label label1;
		///<summary></summary>
		public System.Windows.Forms.TextBox textTitle;
		private System.Windows.Forms.SaveFileDialog saveFileDialog2;
		private OpenDental.UI.Button butCopy;
		private OpenDental.UI.Button butPaste;
		private OpenDental.UI.Button butZoomIn;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butExport;
		private OpenDental.UI.Button butQView;
		private OpenDental.UI.Button butPrintPreview;
		private OpenDental.UI.Button butBack;
		private OpenDental.UI.Button butFwd;
		private OpenDental.UI.Button butExportExcel;
		///<summary></summary>
		public OpenDental.ODtextBox textQuery;
		private int totalPages=0;
		private static Hashtable hListPlans;
		private UserQuery UserQueryCur;//never gets used.  It's a holdover.
		private static Dictionary<long,string> patientNames;
		private SplitContainer splitContainerQuery;
		private Panel panel1;
		///<summary>Will be null if a query has not been ran.</summary>
		private ReportSimpleGrid report;

		///<summary>Can pass in null if not a report.</summary>
		public FormQuery(ReportSimpleGrid report){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this,new System.Windows.Forms.Control[] {
				//exclude:
				labelTotPages,
			});
			this.report=report;
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuery));
			this.butClose = new OpenDental.UI.Button();
			this.grid2 = new System.Windows.Forms.DataGrid();
			this.textQuery = new OpenDental.ODtextBox();
			this.butExportExcel = new OpenDental.UI.Button();
			this.butPaste = new OpenDental.UI.Button();
			this.butCopy = new OpenDental.UI.Button();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.butFavorite = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioHuman = new System.Windows.Forms.RadioButton();
			this.radioRaw = new System.Windows.Forms.RadioButton();
			this.butSubmit = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printPreviewDialog2 = new System.Windows.Forms.PrintPreviewDialog();
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.butFullPage = new OpenDental.UI.Button();
			this.panelZoom = new System.Windows.Forms.Panel();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butZoomIn = new OpenDental.UI.Button();
			this.butBack = new OpenDental.UI.Button();
			this.butFwd = new OpenDental.UI.Button();
			this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
			this.butPrint = new OpenDental.UI.Button();
			this.butExport = new OpenDental.UI.Button();
			this.butQView = new OpenDental.UI.Button();
			this.butPrintPreview = new OpenDental.UI.Button();
			this.splitContainerQuery = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.panelZoom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerQuery)).BeginInit();
			this.splitContainerQuery.Panel1.SuspendLayout();
			this.splitContainerQuery.Panel2.SuspendLayout();
			this.splitContainerQuery.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(879, 753);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 27);
			this.butClose.TabIndex = 5;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// grid2
			// 
			this.grid2.DataMember = "";
			this.grid2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grid2.Location = new System.Drawing.Point(3, 25);
			this.grid2.Name = "grid2";
			this.grid2.ReadOnly = true;
			this.grid2.Size = new System.Drawing.Size(957, 599);
			this.grid2.TabIndex = 1;
			// 
			// textQuery
			// 
			this.textQuery.AcceptsTab = true;
			this.textQuery.BackColor = System.Drawing.SystemColors.Window;
			this.textQuery.DetectUrls = false;
			this.textQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textQuery.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textQuery.Location = new System.Drawing.Point(0, 0);
			this.textQuery.Margin = new System.Windows.Forms.Padding(3, 3, 3, 25);
			this.textQuery.MaximumSize = new System.Drawing.Size(557, 900);
			this.textQuery.Name = "textQuery";
			this.textQuery.QuickPasteType = OpenDentBusiness.QuickPasteType.Query;
			this.textQuery.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textQuery.Size = new System.Drawing.Size(557, 103);
			this.textQuery.SpellCheckIsEnabled = false;
			this.textQuery.TabIndex = 16;
			this.textQuery.Text = "";
			// 
			// butExportExcel
			// 
			this.butExportExcel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExportExcel.Autosize = true;
			this.butExportExcel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExportExcel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExportExcel.CornerRadius = 4F;
			this.butExportExcel.Image = global::OpenDental.Properties.Resources.butExportExcel;
			this.butExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butExportExcel.Location = new System.Drawing.Point(159, 66);
			this.butExportExcel.Name = "butExportExcel";
			this.butExportExcel.Size = new System.Drawing.Size(79, 26);
			this.butExportExcel.TabIndex = 15;
			this.butExportExcel.Text = "Excel";
			this.butExportExcel.Visible = false;
			this.butExportExcel.Click += new System.EventHandler(this.butExportExcel_Click);
			// 
			// butPaste
			// 
			this.butPaste.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPaste.Autosize = true;
			this.butPaste.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPaste.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPaste.CornerRadius = 4F;
			this.butPaste.Image = global::OpenDental.Properties.Resources.butPaste;
			this.butPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPaste.Location = new System.Drawing.Point(78, 51);
			this.butPaste.Name = "butPaste";
			this.butPaste.Size = new System.Drawing.Size(65, 23);
			this.butPaste.TabIndex = 11;
			this.butPaste.Text = "Paste";
			this.butPaste.Click += new System.EventHandler(this.butPaste_Click);
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCopy.Autosize = true;
			this.butCopy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopy.CornerRadius = 4F;
			this.butCopy.Image = global::OpenDental.Properties.Resources.butCopy;
			this.butCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCopy.Location = new System.Drawing.Point(3, 51);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(72, 23);
			this.butCopy.TabIndex = 10;
			this.butCopy.Text = "Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
			// 
			// textTitle
			// 
			this.textTitle.Location = new System.Drawing.Point(63, 3);
			this.textTitle.Name = "textTitle";
			this.textTitle.Size = new System.Drawing.Size(219, 20);
			this.textTitle.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Title";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Location = new System.Drawing.Point(3, 27);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(140, 23);
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "Add To Favorites";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butFavorite
			// 
			this.butFavorite.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFavorite.Autosize = true;
			this.butFavorite.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFavorite.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFavorite.CornerRadius = 4F;
			this.butFavorite.Location = new System.Drawing.Point(3, 3);
			this.butFavorite.Name = "butFavorite";
			this.butFavorite.Size = new System.Drawing.Size(140, 23);
			this.butFavorite.TabIndex = 2;
			this.butFavorite.Text = "Favorites";
			this.butFavorite.Click += new System.EventHandler(this.butFavorites_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioHuman);
			this.groupBox1.Controls.Add(this.radioRaw);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(162, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(122, 58);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Format";
			// 
			// radioHuman
			// 
			this.radioHuman.Checked = true;
			this.radioHuman.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioHuman.Location = new System.Drawing.Point(10, 16);
			this.radioHuman.Name = "radioHuman";
			this.radioHuman.Size = new System.Drawing.Size(108, 16);
			this.radioHuman.TabIndex = 0;
			this.radioHuman.TabStop = true;
			this.radioHuman.Text = "Human-readable";
			this.radioHuman.Click += new System.EventHandler(this.radioHuman_Click);
			// 
			// radioRaw
			// 
			this.radioRaw.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioRaw.Location = new System.Drawing.Point(10, 34);
			this.radioRaw.Name = "radioRaw";
			this.radioRaw.Size = new System.Drawing.Size(104, 16);
			this.radioRaw.TabIndex = 1;
			this.radioRaw.Text = "Raw";
			this.radioRaw.Click += new System.EventHandler(this.radioRaw_Click);
			// 
			// butSubmit
			// 
			this.butSubmit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSubmit.Autosize = true;
			this.butSubmit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSubmit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSubmit.CornerRadius = 4F;
			this.butSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butSubmit.Location = new System.Drawing.Point(3, 75);
			this.butSubmit.Name = "butSubmit";
			this.butSubmit.Size = new System.Drawing.Size(102, 23);
			this.butSubmit.TabIndex = 6;
			this.butSubmit.Text = "&Submit Query";
			this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
			// 
			// printPreviewDialog2
			// 
			this.printPreviewDialog2.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog2.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog2.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog2.Enabled = true;
			this.printPreviewDialog2.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog2.Icon")));
			this.printPreviewDialog2.Name = "printPreviewDialog2";
			this.printPreviewDialog2.Visible = false;
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Location = new System.Drawing.Point(6, 136);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(313, 636);
			this.printPreviewControl2.TabIndex = 5;
			this.printPreviewControl2.Zoom = 1D;
			// 
			// butFullPage
			// 
			this.butFullPage.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFullPage.Autosize = true;
			this.butFullPage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullPage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFullPage.CornerRadius = 4F;
			this.butFullPage.Location = new System.Drawing.Point(9, 5);
			this.butFullPage.Name = "butFullPage";
			this.butFullPage.Size = new System.Drawing.Size(75, 27);
			this.butFullPage.TabIndex = 9;
			this.butFullPage.Text = "&Full Page";
			this.butFullPage.Visible = false;
			this.butFullPage.Click += new System.EventHandler(this.butFullPage_Click);
			// 
			// panelZoom
			// 
			this.panelZoom.Controls.Add(this.labelTotPages);
			this.panelZoom.Controls.Add(this.butFullPage);
			this.panelZoom.Controls.Add(this.butZoomIn);
			this.panelZoom.Controls.Add(this.butBack);
			this.panelZoom.Controls.Add(this.butFwd);
			this.panelZoom.Location = new System.Drawing.Point(337, 744);
			this.panelZoom.Name = "panelZoom";
			this.panelZoom.Size = new System.Drawing.Size(229, 37);
			this.panelZoom.TabIndex = 0;
			this.panelZoom.Visible = false;
			// 
			// labelTotPages
			// 
			this.labelTotPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTotPages.Location = new System.Drawing.Point(143, 10);
			this.labelTotPages.Name = "labelTotPages";
			this.labelTotPages.Size = new System.Drawing.Size(52, 18);
			this.labelTotPages.TabIndex = 11;
			this.labelTotPages.Text = "25 / 26";
			this.labelTotPages.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butZoomIn
			// 
			this.butZoomIn.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butZoomIn.Autosize = true;
			this.butZoomIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butZoomIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butZoomIn.CornerRadius = 4F;
			this.butZoomIn.Image = global::OpenDental.Properties.Resources.butZoomIn;
			this.butZoomIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Location = new System.Drawing.Point(9, 5);
			this.butZoomIn.Name = "butZoomIn";
			this.butZoomIn.Size = new System.Drawing.Size(91, 26);
			this.butZoomIn.TabIndex = 12;
			this.butZoomIn.Text = "Zoom In";
			this.butZoomIn.Click += new System.EventHandler(this.butZoomIn_Click);
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBack.Autosize = true;
			this.butBack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBack.CornerRadius = 4F;
			this.butBack.Image = global::OpenDental.Properties.Resources.Left;
			this.butBack.Location = new System.Drawing.Point(123, 7);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(18, 23);
			this.butBack.TabIndex = 17;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(1, 0);
			this.butFwd.Autosize = true;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwd.CornerRadius = 4F;
			this.butFwd.Image = global::OpenDental.Properties.Resources.Right;
			this.butFwd.Location = new System.Drawing.Point(195, 7);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18, 23);
			this.butFwd.TabIndex = 18;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(785, 753);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(79, 26);
			this.butPrint.TabIndex = 13;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butExport
			// 
			this.butExport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExport.Autosize = true;
			this.butExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExport.CornerRadius = 4F;
			this.butExport.Image = global::OpenDental.Properties.Resources.butExport;
			this.butExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butExport.Location = new System.Drawing.Point(690, 753);
			this.butExport.Name = "butExport";
			this.butExport.Size = new System.Drawing.Size(79, 26);
			this.butExport.TabIndex = 14;
			this.butExport.Text = "&Export";
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// butQView
			// 
			this.butQView.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butQView.Autosize = true;
			this.butQView.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butQView.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butQView.CornerRadius = 4F;
			this.butQView.Image = global::OpenDental.Properties.Resources.butQView;
			this.butQView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butQView.Location = new System.Drawing.Point(574, 739);
			this.butQView.Name = "butQView";
			this.butQView.Size = new System.Drawing.Size(104, 26);
			this.butQView.TabIndex = 15;
			this.butQView.Text = "&Query View";
			this.butQView.Click += new System.EventHandler(this.butQView_Click);
			// 
			// butPrintPreview
			// 
			this.butPrintPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrintPreview.Autosize = true;
			this.butPrintPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintPreview.CornerRadius = 4F;
			this.butPrintPreview.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintPreview.Location = new System.Drawing.Point(573, 753);
			this.butPrintPreview.Name = "butPrintPreview";
			this.butPrintPreview.Size = new System.Drawing.Size(113, 26);
			this.butPrintPreview.TabIndex = 16;
			this.butPrintPreview.Text = "P&rint Preview";
			this.butPrintPreview.Click += new System.EventHandler(this.butPrintPreview_Click);
			// 
			// splitContainerQuery
			// 
			this.splitContainerQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainerQuery.Location = new System.Drawing.Point(0, 0);
			this.splitContainerQuery.Name = "splitContainerQuery";
			this.splitContainerQuery.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerQuery.Panel1
			// 
			this.splitContainerQuery.Panel1.Controls.Add(this.panel1);
			this.splitContainerQuery.Panel1.Controls.Add(this.textQuery);
			this.splitContainerQuery.Panel1MinSize = 105;
			// 
			// splitContainerQuery.Panel2
			// 
			this.splitContainerQuery.Panel2.Controls.Add(this.grid2);
			this.splitContainerQuery.Panel2.Controls.Add(this.label1);
			this.splitContainerQuery.Panel2.Controls.Add(this.textTitle);
			this.splitContainerQuery.Panel2MinSize = 200;
			this.splitContainerQuery.Size = new System.Drawing.Size(963, 733);
			this.splitContainerQuery.SplitterDistance = 105;
			this.splitContainerQuery.TabIndex = 17;
			this.splitContainerQuery.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.butFavorite);
			this.panel1.Controls.Add(this.butExportExcel);
			this.panel1.Controls.Add(this.butAdd);
			this.panel1.Controls.Add(this.butPaste);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.butSubmit);
			this.panel1.Controls.Add(this.butCopy);
			this.panel1.Location = new System.Drawing.Point(560, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(400, 105);
			this.panel1.TabIndex = 17;
			// 
			// FormQuery
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(963, 788);
			this.Controls.Add(this.splitContainerQuery);
			this.Controls.Add(this.butPrintPreview);
			this.Controls.Add(this.printPreviewControl2);
			this.Controls.Add(this.panelZoom);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butExport);
			this.Controls.Add(this.butQView);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(894, 486);
			this.Name = "FormQuery";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Query";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormQuery_Closing);
			this.Load += new System.EventHandler(this.FormQuery_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormQuery_Layout);
			((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.panelZoom.ResumeLayout(false);
			this.splitContainerQuery.Panel1.ResumeLayout(false);
			this.splitContainerQuery.Panel2.ResumeLayout(false);
			this.splitContainerQuery.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerQuery)).EndInit();
			this.splitContainerQuery.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormQuery_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			printPreviewControl2.Location=new System.Drawing.Point(0,0);
			printPreviewControl2.Height=ClientSize.Height-39;
			printPreviewControl2.Width=ClientSize.Width;	
			grid2.Height=ClientSize.Height-grid2.Location.Y-150;
			grid2.Width=ClientSize.Width-2;
			butClose.Location=new System.Drawing.Point(ClientSize.Width-90,ClientSize.Height-34);
			butExport.Location=new System.Drawing.Point(ClientSize.Width-180,ClientSize.Height-34);
			butPrint.Location=new System.Drawing.Point(ClientSize.Width-270,ClientSize.Height-34);
			butPrintPreview.Location=new System.Drawing.Point(ClientSize.Width-385,ClientSize.Height-34);
			butQView.Location=new System.Drawing.Point(ClientSize.Width-385,ClientSize.Height-34);
			panelZoom.Location=new System.Drawing.Point(ClientSize.Width-620,ClientSize.Height-38);
			splitContainerQuery.Height=ClientSize.Height-39;
			splitContainerQuery.Width=ClientSize.Width-2;
		}

		private void FormQuery_Load(object sender, System.EventArgs e) {
			//report.TableQ=null;//this will crash the program
			grid2.Font=bodyFont;
			splitContainerQuery.SplitterDistance=105;
			if(IsReport){
				printPreviewControl2.Visible=true;
				splitContainerQuery.Visible=false;
				Text="Report";
				butPrintPreview.Visible=false;
				panelZoom.Visible=true;
				PrintReport(true);
				labelTotPages.Text="/ "+totalPages.ToString();
				if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
					butFullPage.Visible = true;
					butZoomIn.Visible = false;
					printPreviewControl2.Zoom = 1;
				}
				else {
					printPreviewControl2.Zoom = ((double)printPreviewControl2.ClientSize.Height
					/ (double)pd2.DefaultPageSettings.PaperSize.Height);
				}
            }
			else{
				printPreviewControl2.Visible=false;
				Text=Lan.g(this,"Query");
			}
		}

		private void butSubmit_Click(object sender, System.EventArgs e) {
			if(!IsSafeSql()) {
				return;
			}
			bool isCommand;
			try {
				isCommand=IsCommandSql(textQuery.Text);
			}
			catch {
				MsgBox.Show(this,"Validation failed. Please remove mid-query comments and try again.");
				return;
			}
			if(isCommand && !Security.IsAuthorized(Permissions.UserQueryAdmin)) {
				return;
			}
			if(isCommand) {
				SecurityLogs.MakeLogEntry(Permissions.UserQueryAdmin,0,"Command query run.");
			}
			report=new ReportSimpleGrid();
			report.Query=textQuery.Text;
			SubmitQuery();
		}

		///<summary>This is used internally instead of SubmitReportQuery.  Can also be called externally if we want to automate a userquery.  Column names will be handled automatically.</summary>
		public void SubmitQuery(){
			Cursor=Cursors.WaitCursor;
			patientNames=Patients.GetAllPatientNames();
      //hListPlans=InsPlans.GetHListAll();
			try {
				report.SubmitQuery();
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(Lan.g(this,"Invalid query")+": "+ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			/* (for later if more complex queries with loops:)
			//SubmitQueryThread();
			//Thread Thread2 = new Thread(new ThreadStart(SubmitQueryThread));
			//Thread2.Start();
      //FormProcessWaiting fpw = new FormProcessWaiting();
			//while(Thread2.ThreadState!=ThreadState.Stopped)				//{
			//	;
			//			if(!fpw.Created)  {
			//		    fpw.ShowDialog();
			//			}
			//			if(fpw.DialogResult==DialogResult.Abort)  {
			//				Thread2.Suspend();
			//		  break;
			//		}
			//		}
		  //  fpw.Close();
			//Thread2.Abort();
			//ThreadState.
			//if(MessageBox.Show("Waiting for Server","",MessageBoxButtons.
			//Wait for dialog result
			//If abort, then skip the rest of this.*/
			grid2.TableStyles.Clear();
			grid2.SetDataBinding(report.TableQ,"");
			myGridTS = new DataGridTableStyle();
			grid2.TableStyles.Add(myGridTS);
			if(radioHuman.Checked){
				report.TableQ=MakeReadable(report.TableQ,report);
				grid2.TableStyles.Clear();
				grid2.SetDataBinding(report.TableQ,"");
				grid2.TableStyles.Add(myGridTS);
			}
			//if(!IsReport){
				AutoSizeColumns();
				/*for(int i=0;i<doubleCount;i++){
					int colTotal=0;
					for(int iRow=0;iRow<report.TableQ.Rows.Count;i++){
						
					}
					report.Summary[i]="TOTAL :"+;
				}*/
				report.Title=textTitle.Text;
				report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
				for(int iCol=0;iCol<report.TableQ.Columns.Count;iCol++){
					report.ColCaption[iCol]=report.TableQ.Columns[iCol].Caption;//myGridTS.GridColumnStyles[iCol].HeaderText;
					//again, I don't know why this would fail, so here's a check:
					if(myGridTS.GridColumnStyles.Count >= iCol+1) {
						myGridTS.GridColumnStyles[iCol].Alignment=report.ColAlign[iCol];
					}
				}
			//}		
		}

		//private void SubmitQueryThread(){
			//Queries.SubmitCur();
		//}

		///<summary>When used as a report, this is called externally. Used instead of SubmitQuery(). Column names can be handled manually by the external form calling this report.</summary>
		public void SubmitReportQuery() {
			Cursor=Cursors.WaitCursor;
			report.SubmitQuery();
			Cursor=Cursors.Default;
			grid2.TableStyles.Clear();
			grid2.SetDataBinding(report.TableQ,"");
			myGridTS = new DataGridTableStyle();
			grid2.TableStyles.Add(myGridTS);
			report.TableQ=MakeReadable(report.TableQ,report);//?
			grid2.SetDataBinding(report.TableQ,"");//because MakeReadable trashes the TableQ
		}

		/*
		///<summary>When used as a report, this is called externally. Used instead of SubmitQuery(). Column names will be handled manually by the external form calling this report.</summary>
		public void SubmitReportQuery(DataTable table) {
			report.TableQ=table;
			report.ColWidth=new int[report.TableQ.Columns.Count];
			report.ColPos=new int[report.TableQ.Columns.Count+1];
			report.ColPos[0]=0;
			report.ColCaption=new string[report.TableQ.Columns.Count];
			report.ColAlign=new HorizontalAlignment[report.TableQ.Columns.Count];
			report.ColTotal=new double[report.TableQ.Columns.Count];
			grid2.TableStyles.Clear();
			grid2.SetDataBinding(report.TableQ,"");
			myGridTS = new DataGridTableStyle();
			grid2.TableStyles.Add(myGridTS);
			report.TableQ=MakeReadable(report.TableQ);//?
			grid2.SetDataBinding(report.TableQ,"");//because MakeReadable trashes the TableQ
		}*/

		///<summary></summary>
		public void ResetGrid(){
			grid2.TableStyles.Clear();
			grid2.SetDataBinding(report.TableQ,"");
			myGridTS = new DataGridTableStyle();
			grid2.TableStyles.Add(myGridTS);
		}

		private void AutoSizeColumns(){
			Graphics grfx=this.CreateGraphics();
			//int colWidth;
			int tempWidth;
			//for(int i=0;i<myGridTS.GridColumnStyles.Count;i++){
			for(int i=0;i<report.ColWidth.Length;i++){
				report.ColWidth[i]
					=(int)grfx.MeasureString(report.TableQ.Columns[i].Caption,grid2.Font).Width;
					//myGridTS.GridColumnStyles[i].HeaderText,grid2.Font).Width;
				for(int j=0;j<report.TableQ.Rows.Count;j++){
					tempWidth=(int)grfx.MeasureString(report.TableQ.Rows[j][i].ToString(),grid2.Font).Width;
					if(tempWidth>report.ColWidth[i])
						report.ColWidth[i]=tempWidth;
				}
				if(report.ColWidth[i]>400) {
					report.ColWidth[i]=400;
				}
				//I have no idea why this is failing, so we'll just do a check:
				if(myGridTS.GridColumnStyles.Count >= i+1) {
					myGridTS.GridColumnStyles[i].Width=report.ColWidth[i]+12;
				}
				report.ColWidth[i]+=6;//so the columns don't touch
				report.ColPos[i+1]=report.ColPos[i]+report.ColWidth[i];
			}
		}

		///<summary>Starting to use this externally as well.  OK to pass in a null report.</summary>
		public static DataTable MakeReadable(DataTable tableIn,ReportSimpleGrid reportIn){
			//this can probably be improved upon later for speed
			if(hListPlans==null){
				hListPlans=InsPlans.GetHListAll();
			}
			DataTable tableOut=tableIn.Clone();//copies just the structure
			for(int j=0;j<tableOut.Columns.Count;j++){
				tableOut.Columns[j].DataType=typeof(string);
			}
			DataRow thisRow;
			//copy data from tableInput to tableOutput while converting to strings
			//string str;
			//Type t;
			for(int i=0;i<tableIn.Rows.Count;i++){
				thisRow=tableOut.NewRow();//new row with new schema
				for(int j=0;j<tableIn.Columns.Count;j++){
					thisRow[j]=PIn.ByteArray(tableIn.Rows[i][j]);
					//str=tableIn.Rows[i][j].ToString();
					//t=tableIn.Rows[i][j].GetType();
					//thisRow[j]=str;
				}
				tableOut.Rows.Add(thisRow);
			}
			DateTime date;
			decimal[] colTotals=new decimal[tableOut.Columns.Count];
			for(int j=0;j<tableOut.Columns.Count;j++){
				for(int i=0;i<tableOut.Rows.Count;i++){
					try{
					if(tableOut.Columns[j].Caption.Substring(0,1)=="$"){
						tableOut.Rows[i][j]=PIn.Double(tableOut.Rows[i][j].ToString()).ToString("F");
						if(reportIn!=null) {
							reportIn.ColAlign[j]=HorizontalAlignment.Right;
							colTotals[j]+=PIn.Decimal(tableOut.Rows[i][j].ToString());
						}
					}
					else if(tableOut.Columns[j].Caption.ToLower().StartsWith("date")){
						date=PIn.Date(tableOut.Rows[i][j].ToString());
						if(date.Year<1880){
							tableOut.Rows[i][j]="";
						}
						else{
							tableOut.Rows[i][j]=date.ToString("d");
						}
					}
					else switch(tableOut.Columns[j].Caption.ToLower())
					{
						//bool
						case "isprosthesis":
						case "ispreventive":
						case "ishidden":
						case "isrecall":
						case "usedefaultfee":
						case "usedefaultcov":
						case "isdiscount":
						case "removetooth":
						case "setrecall":
						case "nobillins":
						case "isprosth":
						case "ishygiene":
						case "issecondary":
						case "orpribool":
						case "orsecbool":
						case "issplit":
  					case "ispreauth":
 					  case "isortho":
            case "releaseinfo":
            case "assignben":
            case "enabled":
            case "issystem":
            case "usingtin":
            case "sigonfile": 
            case "notperson":
            case "isfrom":
							tableOut.Rows[i][j]=PIn.Bool(tableOut.Rows[i][j].ToString()).ToString();
							break;
						//date. Some of these are actually handled further up.
						case "adjdate":
						case "baldate":
						case "dateservice":
						case "datesent":
						case "datereceived":
						case "priordate":
						case "date":
						case "dateviewing":
						case "datecreated":
						case "dateeffective":
						case "dateterm":
						case "paydate":
						case "procdate":
						case "rxdate":
						case "birthdate":
						case "monthyear":
            case "accidentdate":
						case "orthodate":
            case "checkdate":
						case "screendate":
						case "datedue":
						case "dateduecalc":
						case "datefirstvisit":
						case "mydate"://this is a workaround for the daily payment report
							tableOut.Rows[i][j]=PIn.Date(tableOut.Rows[i][j].ToString()).ToString("d");
							break;
						//age
						case "birthdateforage":
							tableOut.Rows[i][j]=PatientLogic.DateToAgeString(PIn.Date(tableOut.Rows[i][j].ToString()));
							break;
						//time 
						case "aptdatetime":
						case "nextschedappt":
						case "starttime":
						case "stoptime":
							tableOut.Rows[i][j]=PIn.DateT(tableOut.Rows[i][j].ToString()).ToString("t")+"   "
								+PIn.DateT(tableOut.Rows[i][j].ToString()).ToString("d");
							break;
						//TimeCardManage
						case "adjevent":
						case "adjreg":
						case "adjotime":
						case "breaktime":
						case "temptotaltime":
						case "tempreghrs":
						case "tempovertime":
							if(PrefC.GetBool(PrefName.TimeCardsUseDecimalInsteadOfColon)) {
								tableOut.Rows[i][j]=PIn.Time(tableOut.Rows[i][j].ToString()).TotalHours.ToString("n");
							}
							else if(PrefC.GetBool(PrefName.TimeCardShowSeconds)) {//Colon format with seconds
								tableOut.Rows[i][j]=PIn.Time(tableOut.Rows[i][j].ToString()).ToStringHmmss();
							}
							else {//Colon format without seconds
								tableOut.Rows[i][j]=PIn.Time(tableOut.Rows[i][j].ToString()).ToStringHmm();
							}
							break;
  					//double
						case "adjamt":
						case "monthbalance":
						case "claimfee":
						case "inspayest":
						case "inspayamt":
						case "dedapplied":
						case "amount":
						case "payamt":
						case "splitamt":
						case "balance":
						case "procfee":
						case "overridepri":
						case "overridesec":
						case "priestim":
						case "secestim":
						case "procfees":
						case "claimpays":
						case "insest":
						case "paysplits":
						case "adjustments":
						case "bal_0_30":
						case "bal_31_60":
						case "bal_61_90":
						case "balover90":
						case "baltotal":
							tableOut.Rows[i][j]=PIn.Double(tableOut.Rows[i][j].ToString()).ToString("F");
							if(reportIn!=null) {
								reportIn.ColAlign[j]=HorizontalAlignment.Right;
								colTotals[j]+=PIn.Decimal(tableOut.Rows[i][j].ToString());
							}
							break;
						case "toothnum":
							tableOut.Rows[i][j]=Tooth.ToInternat(tableOut.Rows[i][j].ToString());
							break;
						//definitions:
						case "adjtype":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.AdjTypes,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "confirmed":
							tableOut.Rows[i][j]
								=DefC.GetValue(DefCat.ApptConfirmed,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "dx":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.Diagnosis,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "discounttype":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.DiscountTypes,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "doccategory":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.ImageCats,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "op":
							tableOut.Rows[i][j]
								=Operatories.GetAbbrev(PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "paytype":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.PaymentTypes,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "proccat":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.ProcCodeCats,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "unschedstatus":
						case "recallstatus":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.RecallUnschedStatus,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "billingtype":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.BillingTypes,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						//patnums:
						case "patnum":
						case "guarantor":
						case "pripatnum":
						case "secpatnum":
						case "subscriber":
            case "withpat":
							if(patientNames.ContainsKey(PIn.Long(tableOut.Rows[i][j].ToString()))) {
								//MessageBox.Show((string)Patients.HList[PIn.PInt(tableOut.Rows[i][j].ToString())]);
								tableOut.Rows[i][j]=patientNames[PIn.Long(tableOut.Rows[i][j].ToString())];
							}
							else
								tableOut.Rows[i][j]="";
							break;
            //plannums:        
            case "plannum":
            case "priplannum":
            case "secplannum": 
							if(hListPlans.ContainsKey(PIn.Long(tableOut.Rows[i][j].ToString())))
								tableOut.Rows[i][j]=hListPlans[PIn.Long(tableOut.Rows[i][j].ToString())];
							else
								tableOut.Rows[i][j]="";
							break;
            //referralnum             
            case "referralnum":
							if(PIn.Long(tableOut.Rows[i][j].ToString())!=0){
								Referral referral=Referrals.GetReferral
									(PIn.Long(tableOut.Rows[i][j].ToString()));
								tableOut.Rows[i][j]
									=referral.LName+", "+referral.FName+" "+referral.MName;
							}
							else
								tableOut.Rows[i][j]="";
							break; 
						//enumerations:
						case "aptstatus":
							tableOut.Rows[i][j]
								=((ApptStatus)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "category":
							tableOut.Rows[i][j]=((DefCat)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "renewmonth":
							tableOut.Rows[i][j]=((Month)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "patstatus":
							tableOut.Rows[i][j]
								=((PatientStatus)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "gender":
							tableOut.Rows[i][j]
								=((PatientGender)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						//case "lab":
						//	tableOut.Rows[i][j]
						//		=((LabCaseOld)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
						//  break;
						case "position":
							tableOut.Rows[i][j]
								=((PatientPosition)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "deductwaivprev":
						case "flocovered":
						case "misstoothexcl":
						case "procstatus":
							tableOut.Rows[i][j]=((ProcStat)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "majorwait":
						case "hascaries":
						case "needssealants":
						case "cariesexperience":
						case "earlychildcaries":
						case "existingsealants":
						case "missingallteeth":
							tableOut.Rows[i][j]=((YN)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "prirelationship":
						case "secrelationship":
							tableOut.Rows[i][j]=((Relat)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "treatarea":
							tableOut.Rows[i][j]
								=((TreatmentArea)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "specialty":
							tableOut.Rows[i][j]
								=DefC.GetName(DefCat.ProviderSpecialties,PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
						case "placeservice":
							tableOut.Rows[i][j]
								=((PlaceOfService)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
            case "employrelated": 
							tableOut.Rows[i][j]
								=((YN)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
            case "schedtype": 
							tableOut.Rows[i][j]
								=((ScheduleType)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
            case "dayofweek": 
							tableOut.Rows[i][j]
								=((DayOfWeek)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "race":
							//We expect a comma separated list of integers representing the patient races.  When designing custom queries, the query writer must use GROUP_CONCAT() and should leave the race values in their raw state.
							string[] arrayRaceDescripts=tableOut.Rows[i][j].ToString().Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
							for(int r=0;r<arrayRaceDescripts.Length;r++) {
								arrayRaceDescripts[r]=((PatRace)PIn.Long(arrayRaceDescripts[r].Trim())).ToString();
							}
							tableOut.Rows[i][j]=string.Join(", ",arrayRaceDescripts);
							continue;
						case "raceOld":
							tableOut.Rows[i][j]
								=((PatientRaceOld)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "gradelevel":
							tableOut.Rows[i][j]
								=((PatientGrade)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "urgency":
							tableOut.Rows[i][j]
								=((TreatmentUrgency)PIn.Long(tableOut.Rows[i][j].ToString())).ToString();
							break;
						//miscellaneous:
						case "provnum":
						case "provhyg":
						case "priprov":
						case "secprov":
            case "provtreat":
            case "provbill":   
							tableOut.Rows[i][j]=Providers.GetAbbr(PIn.Long(tableOut.Rows[i][j].ToString()));
							break;

						case "covcatnum":
							tableOut.Rows[i][j]=CovCats.GetDesc(PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
            case "referringprov": 
	//					  tableOut.Rows[i][j]=CovCats.GetDesc(PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;			
            case "addtime":
							if(tableOut.Rows[i][j].ToString()!="0")
								tableOut.Rows[i][j]+="0";
							break;
						case "feesched":
						case "feeschednum":
							tableOut.Rows[i][j]=FeeScheds.GetDescription(PIn.Long(tableOut.Rows[i][j].ToString()));
							break;
					}//end switch column caption
					}//end try
					catch{
						//return tableOut;
					}
				}//end for i rows
			}//end for j cols
			if(reportIn!=null){
				for(int k=0;k<colTotals.Length;k++){
					reportIn.ColTotal[k]=PIn.Decimal(colTotals[k].ToString("n"));
				}
			}
			return tableOut;
		}

		///<summary>Checks to see if the computer is allowed to use create table or drop table syntax queries.  Will return false if using replication and the computer OD is running on is not the ReplicationUserQueryServer set in replication setup.  Otherwise true.</summary>
		private bool IsSafeSql() {
			if(!PrefC.RandomKeys) {//If replication is disabled, then any command is safe.
				return true;
			}
			bool isSafe=true;
			if(Regex.IsMatch(textQuery.Text,".*CREATE[\\s]+TABLE.*",RegexOptions.IgnoreCase)) {
				isSafe=false;
			}
			if(Regex.IsMatch(textQuery.Text,".*CREATE[\\s]+TEMPORARY[\\s]+TABLE.*",RegexOptions.IgnoreCase)) {
				isSafe=false;
			}
			if(Regex.IsMatch(textQuery.Text,".*DROP[\\s]+TABLE.*",RegexOptions.IgnoreCase)) {
				isSafe=false;
			}
			if(isSafe) {
				return true;
			}
			//At this point we know that replication is enabled and the command is potentially unsafe.
			if(PrefC.GetLong(PrefName.ReplicationUserQueryServer)==0) {//if no allowed ReplicationUserQueryServer set in replication setup
				MsgBox.Show(this,"This query contains unsafe syntax that can crash replication.  There is currently no computer set that is allowed to run these types of queries.  This can be set in the replication setup window.");
				return false;
			}
			else if(!ReplicationServers.IsConnectedReportServer()) {//if not running query from the ReplicationUserQueryServer set in replication setup 
				MsgBox.Show(this,"This query contains unsafe syntax that can crash replication.  Only computers connected to the report server are allowed to run these queries.  The current report server can be found in the replication setup window.");
				return false;
			}
			return true;
		}

		private void butFavorites_Click(object sender, System.EventArgs e) {
			FormQueryFavorites FormQF=new FormQueryFavorites();
			FormQF.UserQueryCur=UserQueryCur;
			FormQF.ShowDialog();
			if(FormQF.DialogResult==DialogResult.OK){
				textQuery.Text=FormQF.UserQueryCur.QueryText;
				//grid2.CaptionText=UserQueries.Cur.Description;
				textTitle.Text=FormQF.UserQueryCur.Description;
				UserQueryCur=FormQF.UserQueryCur;
				if(!IsSafeSql()) {
					return;
				}
				report=new ReportSimpleGrid();
				report.Query=textQuery.Text;
				SubmitQuery();
				//this.butSaveChanges.Enabled=true;
			}
			else{
				//butSaveChanges.Enabled=false;
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormQueryEdit FormQE=new FormQueryEdit();
			FormQE.UserQueryCur=new UserQuery();
			FormQE.UserQueryCur.QueryText=textQuery.Text;
			FormQE.IsNew=true;
			FormQE.ShowDialog();
			if(FormQE.DialogResult==DialogResult.OK){
				textQuery.Text=FormQE.UserQueryCur.QueryText;
				grid2.CaptionText=FormQE.UserQueryCur.Description;
			}
		}

		private void radioRaw_Click(object sender, System.EventArgs e) {
			SubmitQuery();
		}

		private void radioHuman_Click(object sender, System.EventArgs e) {
			SubmitQuery();
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(report==null || report.TableQ==null) {
				MessageBox.Show(Lan.g(this,"Please run query first"));
				return;
			}
			PrintReport(false);
			if(IsReport){
				DialogResult=DialogResult.Cancel;
			}
		}

		private void butPrintPreview_Click(object sender, System.EventArgs e) {
			if(report==null || report.TableQ==null) {
				MessageBox.Show(Lan.g(this,"Please run query first"));
				return;
			}
			butFullPage.Visible=false;
			butZoomIn.Visible=true;
			printPreviewControl2.Visible=true;
			butPrintPreview.Visible=false;
			butQView.Visible=true;
			panelZoom.Visible=true;
			splitContainerQuery.Visible=false;
			totalPages=0;
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);
			PrintReport(true);
			labelTotPages.Text="/ "+totalPages.ToString();
		}

		private void butQView_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.UserQuery)) {
				return;
			}
			printPreviewControl2.Visible=false;
			panelZoom.Visible=false;
			butPrintPreview.Visible=true;
			butQView.Visible=false;
			splitContainerQuery.Visible=true;
		}

		///<summary></summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(25,50,50,60);
			if(report.IsLandscape) {
				pd2.DefaultPageSettings.Landscape=true;
				pd2.DefaultPageSettings.Margins=new Margins(25,120,50,60);
			}
			if(pd2.DefaultPageSettings.PrintableArea.Height==0){
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			pagesPrinted=0;
			linesPrinted=0;
			try{
				if(justPreview){
					printPreviewControl2.Document=pd2;
				}
				else if(PrinterL.SetPrinter(pd2,PrintSituation.Default,0,"Query printed")){
					pd2.Print();
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}
		
		///<summary>raised for each page to be printed.</summary>
		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){
			Rectangle bounds=ev.MarginBounds;
			float yPos = bounds.Top;
			if(!headerPrinted){
				ev.Graphics.DrawString(report.Title
					,titleFont,Brushes.Black
					,bounds.Width/2
					-ev.Graphics.MeasureString(report.Title,titleFont).Width/2,yPos);
				yPos+=titleFont.GetHeight(ev.Graphics);
				for(int i=0;i<report.SubTitle.Count;i++){
					ev.Graphics.DrawString(report.SubTitle[i]
						,subtitleFont,Brushes.Black
						,bounds.Width/2
						-ev.Graphics.MeasureString(report.SubTitle[i],subtitleFont).Width/2,yPos);
					yPos+=subtitleFont.GetHeight(ev.Graphics)+2;
				}
				headerPrinted=true;
			}
			yPos+=10;
			ev.Graphics.DrawString(Lan.g(this,"Date:")+" "+DateTime.Today.ToString("d")
				,bodyFont,Brushes.Black,bounds.Left,yPos);
			//if(totalPages==0){
			ev.Graphics.DrawString(Lan.g(this,"Page:")+" "+(pagesPrinted+1).ToString()
				,bodyFont,Brushes.Black
				,bounds.Right
				-ev.Graphics.MeasureString(Lan.g(this,"Page:")+" "+(pagesPrinted+1).ToString()
				,bodyFont).Width,yPos);
			/*}
			else{//maybe work on this later.  Need totalPages on first pass
				ev.Graphics.DrawString("Page: "+(pagesPrinted+1).ToString()+" / "+totalPages.ToString()
					,bodyFont,Brushes.Black
					,bounds.Right
					-ev.Graphics.MeasureString("Page: "+(pagesPrinted+1).ToString()+" / "
					+totalPages.ToString(),bodyFont).Width
					,yPos);
			}*/
			yPos+=bodyFont.GetHeight(ev.Graphics)+10;
			ev.Graphics.DrawLine(new Pen(Color.Black),bounds.Left,yPos-5,bounds.Right,yPos-5);
			//column captions:
			for(int i=0;i<report.ColCaption.Length;i++){
				if(report.ColAlign[i]==HorizontalAlignment.Right){
					ev.Graphics.DrawString(report.ColCaption[i]
						,colCaptFont,Brushes.Black,new RectangleF(
						bounds.Left+report.ColPos[i+1]
						-ev.Graphics.MeasureString(report.ColCaption[i],colCaptFont).Width,yPos
						,report.ColWidth[i],colCaptFont.GetHeight(ev.Graphics)));
				}
				else{
					ev.Graphics.DrawString(report.ColCaption[i]
						,colCaptFont,Brushes.Black,bounds.Left+report.ColPos[i],yPos);
				}
			}
			yPos+=bodyFont.GetHeight(ev.Graphics)+5;
			float fontHeight=bodyFont.GetHeight(ev.Graphics);
			float yPosTableTop=yPos;
			//table: each loop iteration prints one row in the grid.
			while(yPos<bounds.Top+bounds.Height-18//The 18 is minimum allowance for the line about to print. 
				&& linesPrinted < report.TableQ.Rows.Count)//Page might finish early on the last page.
			{
				bool isColWrap=PrefC.GetBool(PrefName.ReportsWrapColumns);
				if(isColWrap && yPos > yPosTableTop) {//First row always prints.  Otherwise the row might be pushed to next page if too tall.
					int cellWidth;//Width to be adjusted and used to calculate row height.
					bool isRowTooTall=false;//Bool to indicate if a row we are about to print is too tall for the avaible space on page.
					for(int iCol2=0;iCol2<report.TableQ.Columns.Count;iCol2++){
						if(report.ColAlign[iCol2]==HorizontalAlignment.Right) {
							cellWidth=report.ColWidth[iCol2];
						}
						else {
							cellWidth=report.ColPos[iCol2+1]-report.ColPos[iCol2]+6;
						}
						//Current height of the string with given width.
						string cellText=grid2[linesPrinted,iCol2].ToString();
						float rectHeight=ev.Graphics.MeasureString(cellText,bodyFont,cellWidth).Height;
						if(yPos+rectHeight > bounds.Bottom) {//Check for if we have enough height to print on current page.
							isRowTooTall=true;
							break;
						}
					}
					if(isRowTooTall) {
						break;//Break so current row goes to next page.
					}
				}
				float rowHeight=fontHeight;//When wrapping, we get the hight of the tallest cell in the row and increase yPos by it.
				for(int iCol=0;iCol<report.TableQ.Columns.Count;iCol++){//For each cell in the row, print the cell contents.
					float cellHeight=rowHeight;
					if(isColWrap) {
						cellHeight=0;//Infinate height.
					}
					int cellWidth=0;
					string cellText=grid2[linesPrinted,iCol].ToString();
					if(report.ColAlign[iCol]==HorizontalAlignment.Right){
						cellWidth=report.ColWidth[iCol];
						ev.Graphics.DrawString(cellText
							,bodyFont,Brushes.Black,new RectangleF(
							bounds.Left+report.ColPos[iCol+1]
							-ev.Graphics.MeasureString(cellText,bodyFont).Width-1,yPos
							,cellWidth,cellHeight));
					}
					else{
						cellWidth=report.ColPos[iCol+1]-report.ColPos[iCol]+6;
						ev.Graphics.DrawString(cellText
							,bodyFont,Brushes.Black,new RectangleF(
							bounds.Left+report.ColPos[iCol],yPos
							,cellWidth
							,cellHeight));
					}
					if(isColWrap) {
						rowHeight=Math.Max(rowHeight,ev.Graphics.MeasureString(cellText,bodyFont,cellWidth).Height);
					}
				}
				yPos+=rowHeight;
				linesPrinted++;
				if(linesPrinted==report.TableQ.Rows.Count){
					tablePrinted=true;
				}
			}
			if(report.TableQ.Rows.Count==0){
				tablePrinted=true;
			}
			//totals:
			if(tablePrinted){
				if(yPos<bounds.Bottom){
					ev.Graphics.DrawLine(new Pen(Color.Black),bounds.Left,yPos+3,bounds.Right,yPos+3);
					yPos+=4;
					for(int iCol=0;iCol<report.TableQ.Columns.Count;iCol++){
						if(report.ColAlign[iCol]==HorizontalAlignment.Right){
							if(report.TableQ.Columns[iCol].Caption.ToLower().StartsWith("count(")) {//"=="count(*)") {
								continue;
							}
							float textWidth=(float)(ev.Graphics.MeasureString
								(report.ColTotal[iCol].ToString("n"),subtitleFont).Width);
							ev.Graphics.DrawString(report.ColTotal[iCol].ToString("n")
								,subtitleFont,Brushes.Black,new RectangleF(
								bounds.Left+report.ColPos[iCol+1]-textWidth+3,yPos//the 3 is arbitrary
								,textWidth,subtitleFont.GetHeight(ev.Graphics)));
						}
						//else{
						//	ev.Graphics.DrawString(grid2[linesPrinted,iCol].ToString()
						//		,bodyFont,Brushes.Black,new RectangleF(
						//		bounds.Left+report.ColPos[iCol],yPos
						//		,report.ColPos[iCol+1]-report.ColPos[iCol]
						//,bodyFont.GetHeight(ev.Graphics)));
						//}
					}
					totalsPrinted=true;
					yPos+=subtitleFont.GetHeight(ev.Graphics);
				}
			}
			//Summary
			if(totalsPrinted){
				if(yPos+report.Summary.Count*subtitleFont.GetHeight(ev.Graphics)< bounds.Top+bounds.Height){
					ev.Graphics.DrawLine(new Pen(Color.Black),bounds.Left,yPos+2,bounds.Right,yPos+2);
					yPos+=bodyFont.GetHeight(ev.Graphics);
					for(int i=0;i<report.Summary.Count;i++){
						if(report.SummaryFont!=null && report.SummaryFont!=""){
							Font acctnumFont=new Font(report.SummaryFont,12);
							ev.Graphics.DrawString(report.Summary[i],acctnumFont,Brushes.Black,bounds.Left,yPos);
							yPos+=acctnumFont.GetHeight(ev.Graphics);
						}
						else{
							ev.Graphics.DrawString(report.Summary[i],subtitleFont,Brushes.Black,bounds.Left,yPos);
							yPos+=subtitleFont.GetHeight(ev.Graphics);
						}
					}
					summaryPrinted=true;
				}
			}
			if(!summaryPrinted){//linesPrinted < report.TableQ.Rows.Count){
				ev.HasMorePages = true;
				pagesPrinted++;
			}
			else{
				ev.HasMorePages = false;
				//UpDownPage.Maximum=pagesPrinted+1;
				totalPages=pagesPrinted+1;
				labelTotPages.Text="1 / "+totalPages.ToString();
				pagesPrinted=0;
				linesPrinted=0;
				headerPrinted=false;
				tablePrinted=false;
				totalsPrinted=false;
				summaryPrinted=false;
			}
		}

		private void splitContainer1_SplitterMoved(object sender,SplitterEventArgs e) {
			//Dynamically set the height of the grid so that the bottom scrollbar does not disappear when the user resizes the splitter.
			//Subtract 5 so that the grid's horizontal scroll bar does not get pushed off the screen.
			grid2.Height=splitContainerQuery.Height-grid2.Location.Y-splitContainerQuery.SplitterDistance-5;
		}

		private void butZoomIn_Click(object sender, System.EventArgs e){
			butFullPage.Visible=true;
			butZoomIn.Visible=false;
			printPreviewControl2.Zoom=1;
		}

		private void butFullPage_Click(object sender, System.EventArgs e){
			butFullPage.Visible=false;
			butZoomIn.Visible=true;
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);
		}

		private void butBack_Click(object sender, System.EventArgs e){
			if(printPreviewControl2.StartPage==0) return;
			printPreviewControl2.StartPage--;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		private void butFwd_Click(object sender, System.EventArgs e){
			if(printPreviewControl2.StartPage==totalPages-1) return;
			printPreviewControl2.StartPage++;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		private void butExportExcel_Click(object sender, System.EventArgs e) {
			/*
			saveFileDialog2=new SaveFileDialog();
      saveFileDialog2.AddExtension=true;
			saveFileDialog2.Title=Lan.g(this,"Select Folder to Save File To");
		  if(IsReport){
				saveFileDialog2.FileName=report.Title;
			}
      else{
        saveFileDialog2.FileName=UserQueries.Cur.FileName;
			}
			if(!Directory.Exists( ((Pref)PrefC.HList["ExportPath"]).ValueString )){
				try{
					Directory.CreateDirectory( ((Pref)PrefC.HList["ExportPath"]).ValueString );
					saveFileDialog2.InitialDirectory=((Pref)PrefC.HList["ExportPath"]).ValueString;
				}
				catch{
					//initialDirectory will be blank
				}
			}
			else saveFileDialog2.InitialDirectory=((Pref)PrefC.HList["ExportPath"]).ValueString;
			//saveFileDialog2.DefaultExt="xls";
			//saveFileDialog2.Filter="txt files(*.txt)|*.txt|All files(*.*)|*.*";
      //saveFileDialog2.FilterIndex=1;
		  if(saveFileDialog2.ShowDialog()!=DialogResult.OK){
	   	  return;
			}
			Excel.Application excel=new Excel.ApplicationClass();
			excel.Workbooks.Add(Missing.Value);
			Worksheet worksheet = (Worksheet) excel.ActiveSheet;
			Range range=(Excel.Range)excel.Cells[1,1];
			range.Value2="test";
			range.Font.Bold=true;
			range=(Excel.Range)excel.Cells[1,2];
			range.ColumnWidth=30;
			range.FormulaR1C1="12345";
			excel.Save(saveFileDialog2.FileName);
	//this test case worked, so now it is just a matter of finishing this off, and Excel export will be done.
			MessageBox.Show(Lan.g(this,"File created successfully"));
			*/
		}

		private void butExport_Click(object sender, System.EventArgs e){
			if(report==null || report.TableQ==null){
				MessageBox.Show(Lan.g(this,"Please run query first"));
				return;
			}
			saveFileDialog2=new SaveFileDialog();
      saveFileDialog2.AddExtension=true;
			//saveFileDialog2.Title=Lan.g(this,"Select Folder to Save File To");
		  if(IsReport){
				saveFileDialog2.FileName=report.Title;
			}
      else{
				if(UserQueryCur==null || UserQueryCur.FileName==null || UserQueryCur.FileName=="")//.FileName==null)
					saveFileDialog2.FileName=report.Title;
				else
					saveFileDialog2.FileName=UserQueryCur.FileName;
			}
			if(!Directory.Exists(PrefC.GetString(PrefName.ExportPath) )){
				try{
					Directory.CreateDirectory(PrefC.GetString(PrefName.ExportPath) );
					saveFileDialog2.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
				}
				catch{
					//initialDirectory will be blank
				}
			}
			else saveFileDialog2.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
			//saveFileDialog2.DefaultExt="txt";
			saveFileDialog2.Filter="Text files(*.txt)|*.txt|Excel Files(*.xls)|*.xls|All files(*.*)|*.*";
      saveFileDialog2.FilterIndex=0;
		  if(saveFileDialog2.ShowDialog()!=DialogResult.OK){
	   	  return;
			}
			try{
			  using(StreamWriter sw=new StreamWriter(saveFileDialog2.FileName,false))
					//new FileStream(,FileMode.Create,FileAccess.Write,FileShare.Read)))
				{
					String line="";
					for(int i=0;i<report.ColCaption.Length;i++) {
						string columnCaption=report.ColCaption[i];
						//Check for columns that start with special characters that spreadsheet programs treat differently than simply displaying them.
						if(columnCaption.StartsWith("-") || columnCaption.StartsWith("=")) {
							//Adding a space to the beginning of the cell will trick the spreadsheet program into not treating it uniquely.
							columnCaption=" "+columnCaption;
						}
						line+=columnCaption;
						if(i<report.TableQ.Columns.Count-1){
							line+="\t";
						}
					}
					sw.WriteLine(line);
					string cell;
					for(int i=0;i<report.TableQ.Rows.Count;i++){
						line="";
						for(int j=0;j<report.TableQ.Columns.Count;j++){
							cell=report.TableQ.Rows[i][j].ToString();
							cell=cell.Replace("\r","");
							cell=cell.Replace("\n","");
							cell=cell.Replace("\t","");
							cell=cell.Replace("\"","");
							line+=cell;
							if(j<report.TableQ.Columns.Count-1){
								line+="\t";
							}
						}
						sw.WriteLine(line);
					}
				}
      }
      catch{
        MessageBox.Show(Lan.g(this,"File in use by another program.  Close and try again."));
				return;
			}
			MessageBox.Show(Lan.g(this,"File created successfully"));
		}

		private void butCopy_Click(object sender, System.EventArgs e){
			Clipboard.SetDataObject(textQuery.Text);
		}

		private void butPaste_Click(object sender, System.EventArgs e){
			IDataObject iData=Clipboard.GetDataObject();
			if(iData.GetDataPresent(DataFormats.Text)){
				textQuery.Text=(String)iData.GetData(DataFormats.Text); 
			}
			else{
				MessageBox.Show(Lan.g(this,"Could not retrieve data off the clipboard."));
			}

		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
			Close();
		}

		private void FormQuery_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//SecurityLogs.MakeLogEntry("User Query","");
		}

		///<summary>Returns true if the given SQL script in strSql contains any commands (INSERT, UPDATE, DELETE, etc.). Surround with a try/catch.</summary>
		private bool IsCommandSql(string strSql) {
			string trimmedSql=strSql.Trim();//If a line is completely a comment it may have only a trailing \n to make a subquery on. We need to keep it there.
			string[] arraySqlExpressions=trimmedSql.ToUpper().Split(';');
			//Because of the complexities of parsing through MySQL and the fact that we don't want to take the time to create a fully functional parser
			//for our simple query runner we elected to err on the side of caution.  If there are comments in the middle of the query this section of
			//code will fire a UE.  This is due to the fact that without massive work we cannot intelligently discern if a comment is in the middle of
			//a string being used or if it is a legitimate comment.  Since we cannot know this we want to block more often than may be absolutely 
			//necessary to catch people doing anything that could potentially lead to SQL injection attacks.  We thus want to inform the user that simply
			//removing intra-query comments is the necessary fix for their problem.
			for(int i=0;i<arraySqlExpressions.Length;i++) {
				//Clean out any leading comments before we do anything else
				while(arraySqlExpressions[i].Trim().StartsWith("#") || arraySqlExpressions[i].Trim().StartsWith("--") || arraySqlExpressions[i].Trim().StartsWith("/*")) {
					if(arraySqlExpressions[i].Trim().StartsWith("/*")) {
						arraySqlExpressions[i]=arraySqlExpressions[i].Remove(0,arraySqlExpressions[i].IndexOf("*/")+3).Trim();
					}
					else {//Comment starting with # or starting with --
						int endIndex=arraySqlExpressions[i].IndexOf("\n");
						if(endIndex!=-1) {//This is so it doesn't break if the last line of a command is a comment
							arraySqlExpressions[i]=arraySqlExpressions[i].Remove(0,arraySqlExpressions[i].IndexOf("\n")).Trim();
						}
						else {
							arraySqlExpressions[i]=arraySqlExpressions[i].Remove(0,arraySqlExpressions[i].Length).Trim();
						}
					}
				}
				if(String.IsNullOrWhiteSpace(arraySqlExpressions[i])) {
					continue;//Ignore empty SQL statements.
				}
				if(arraySqlExpressions[i].Trim().StartsWith("SELECT")) {//We don't care about select queries
					continue;
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("SET")) {
					//We need to allow SET statements because we use them to set variables in our query examples.
					continue;
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("UPDATE")) {//These next we allow if they are on temp tables
					if(HasNonTempTable("UPDATE",arraySqlExpressions[i])) {
						return true;
					}
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("ALTER")) {
					if(HasNonTempTable("TABLE",arraySqlExpressions[i])) {
						return true;
					}
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("CREATE")) {//CREATE INDEX or CREATE TABLE or CREATE TEMPORARY TABLE
					int a=arraySqlExpressions[i].Trim().IndexOf("INDEX");
					int b=arraySqlExpressions[i].Trim().IndexOf("TABLE");
					string keyword="";
					if(a==-1 && b==-1) {
						//Invalid command.  Ignore.
					}
					else if(a!=-1 && b==-1) {
						keyword="INDEX";
					}
					else if(a==-1 && b!=-1) {
						keyword="TABLE";
					}
					else if(a!=-1 && b!=-1) {
						keyword=arraySqlExpressions[i].Trim().Substring(Math.Min(a,b),5);//Get the keyword that is closest to the front of the string.
					}
					if(keyword!="" && HasNonTempTable(keyword,arraySqlExpressions[i])) {
						return true;
					}
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("DROP")) { //DROP [TEMPORARY] TABLE [IF EXISTS]
					int a=arraySqlExpressions[i].Trim().IndexOf("TABLE");
					//We require exactly one space between these two keywords, because there are all sorts of technically valid ways to write the IF EXISTS which would create a lot of work for us.
					//Examples "DROP TABLE x IF    EXISTS ...", "DROP TABLE x IF /*comment IF EXISTS*/  EXISTS ...", "DROP TABLE ifexists IF EXISTS /*IF EXISTS*/"
					int b=arraySqlExpressions[i].Trim().IndexOf("IF EXISTS");
					string keyword="";
					if(a==-1 && b==-1) {
						//Invalid command.  Ignore.
					}
					else if(b==-1) {
						keyword="TABLE";//Must have TABLE if it's not invalid
					}
					else {
						keyword="IF EXISTS";//It has the IF EXISTS statement
					}
					if(keyword!="" && HasNonTempTable(keyword,arraySqlExpressions[i])) {
						return true;
					}
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("RENAME")) {
					if(HasNonTempTable("TABLE",arraySqlExpressions[i])) {
						return true;
					}
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("TRUNCATE")) {
					if(HasNonTempTable("TABLE",arraySqlExpressions[i])) {
						return true;
					}
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("DELETE")) {
					if(HasNonTempTable("DELETE",arraySqlExpressions[i])) {
						return true;
					}
				}
				else if(arraySqlExpressions[i].Trim().StartsWith("INSERT")) {
					if(HasNonTempTable("INTO",arraySqlExpressions[i])) {
						return true;
					}
				}
				else {//All the rest of the commands that we won't allow, even with temp tables, also includes if there are any additional comments embedded.
					return true;
				}
			}
			return false;
		}

		///<summary>The keywords must be listed in the order they are required to appear within the query.</summary>
		private static bool HasNonTempTable(string keyword,string command) {
			int keywordEndIndex=command.IndexOf(keyword)+keyword.Length;
			command=command.Remove(0,keywordEndIndex).Trim();//Everything left will be the table/s or nested queries.
			//Match one or more table names with optional alias for each table name, separated by commas.
			//A word in this contenxt is any string of non-space characters which also does not include ',' or '(' or ')'.
			Match m=Regex.Match(command,@"^([^\s,\(\)]+(\s+[^\s,\(\)]+)?(\s*,\s*[^\s,\(\)]+(\s+[^\s,\(\)]+)?)*)");
			string[] arrayTableNames=m.Result("$1").Split(',');
			for(int i=0;i<arrayTableNames.Length;i++) {//Adding matched strings to list
				string tableName=arrayTableNames[i].Trim().Split(' ')[0];
				if(!tableName.StartsWith("TEMP") && !tableName.StartsWith("TMP")) {//A table name that doesn't start with temp nor tmp (non temp table).
					return true;
				}
			}			
			return false;
		}

		

		

	}
}
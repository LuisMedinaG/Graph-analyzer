/*
 * User: Beto
 * Date: 28/05/2019
 * Time: 12:51 a. m. 
 */
namespace GraphAnalyser
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBoxImg;
		private System.Windows.Forms.Button bttnScanImg;
		private System.Windows.Forms.Button bttnLoadImg;
		private System.Windows.Forms.TreeView treeViewCircles;
		private System.Windows.Forms.Button bttnAddAge;
		private System.Windows.Forms.Label labelAgents;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label labelNumVer;
		private System.Windows.Forms.TrackBar tbNumVer;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxImg = new System.Windows.Forms.PictureBox();
            this.treeViewCircles = new System.Windows.Forms.TreeView();
            this.bttnLoadImg = new System.Windows.Forms.Button();
            this.bttnScanImg = new System.Windows.Forms.Button();
            this.bttnAddAge = new System.Windows.Forms.Button();
            this.labelAgents = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelNumVer = new System.Windows.Forms.Label();
            this.tbNumVer = new System.Windows.Forms.TrackBar();
            this.treeViewCaMaLa = new System.Windows.Forms.TreeView();
            this.bttnCaMaLaAg = new System.Windows.Forms.Button();
            this.cbAgents = new System.Windows.Forms.ComboBox();
            this.bttnCaMaLaVer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bttnPrim = new System.Windows.Forms.Button();
            this.bttnKruskal = new System.Windows.Forms.Button();
            this.cbInitialVertex = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TreeViewPrim = new System.Windows.Forms.TreeView();
            this.bttnAnimatePrim = new System.Windows.Forms.Button();
            this.bttnAnimateKruskal = new System.Windows.Forms.Button();
            this.bttnBoth = new System.Windows.Forms.Button();
            this.bttnAddPrey = new System.Windows.Forms.Button();
            this.TreeViewKruskal = new System.Windows.Forms.TreeView();
            this.cbDestinationVertex = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelHunters = new System.Windows.Forms.Label();
            this.tbHunters = new System.Windows.Forms.TrackBar();
            this.bttnAddHunter = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelOrgDes = new System.Windows.Forms.Label();
            this.bttnFinBFS = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bttnBruFor = new System.Windows.Forms.Button();
            this.bttnDivCon = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.labelTDC = new System.Windows.Forms.Label();
            this.labelTBF = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFindBFS = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbNumVer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHunters)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxImg
            // 
            this.pictureBoxImg.Location = new System.Drawing.Point(177, 12);
            this.pictureBoxImg.Name = "pictureBoxImg";
            this.pictureBoxImg.Size = new System.Drawing.Size(752, 638);
            this.pictureBoxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImg.TabIndex = 0;
            this.pictureBoxImg.TabStop = false;
            // 
            // treeViewCircles
            // 
            this.treeViewCircles.Location = new System.Drawing.Point(6, 147);
            this.treeViewCircles.Name = "treeViewCircles";
            this.treeViewCircles.Size = new System.Drawing.Size(164, 219);
            this.treeViewCircles.TabIndex = 0;
            this.treeViewCircles.DoubleClick += new System.EventHandler(this.TreeViewCircles_DoubleClick);
            // 
            // bttnLoadImg
            // 
            this.bttnLoadImg.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnLoadImg.Location = new System.Drawing.Point(6, 12);
            this.bttnLoadImg.Name = "bttnLoadImg";
            this.bttnLoadImg.Size = new System.Drawing.Size(164, 47);
            this.bttnLoadImg.TabIndex = 0;
            this.bttnLoadImg.Text = "Load image";
            this.bttnLoadImg.UseVisualStyleBackColor = true;
            this.bttnLoadImg.Click += new System.EventHandler(this.BttnLoadImgClick);
            // 
            // bttnScanImg
            // 
            this.bttnScanImg.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnScanImg.Location = new System.Drawing.Point(6, 65);
            this.bttnScanImg.Name = "bttnScanImg";
            this.bttnScanImg.Size = new System.Drawing.Size(165, 48);
            this.bttnScanImg.TabIndex = 1;
            this.bttnScanImg.Text = "Analyse";
            this.bttnScanImg.UseVisualStyleBackColor = true;
            this.bttnScanImg.Click += new System.EventHandler(this.BttnScanImgClick);
            // 
            // bttnAddAge
            // 
            this.bttnAddAge.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnAddAge.Location = new System.Drawing.Point(9, 463);
            this.bttnAddAge.Name = "bttnAddAge";
            this.bttnAddAge.Size = new System.Drawing.Size(159, 48);
            this.bttnAddAge.TabIndex = 3;
            this.bttnAddAge.Text = "Add";
            this.bttnAddAge.UseVisualStyleBackColor = true;
            this.bttnAddAge.Click += new System.EventHandler(this.BttnAddAgentClick);
            // 
            // labelAgents
            // 
            this.labelAgents.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAgents.Location = new System.Drawing.Point(11, 371);
            this.labelAgents.Name = "labelAgents";
            this.labelAgents.Size = new System.Drawing.Size(157, 23);
            this.labelAgents.TabIndex = 4;
            this.labelAgents.Text = "Number of agents";
            this.labelAgents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelNumVer
            // 
            this.labelNumVer.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumVer.Location = new System.Drawing.Point(144, 403);
            this.labelNumVer.Name = "labelNumVer";
            this.labelNumVer.Size = new System.Drawing.Size(24, 48);
            this.labelNumVer.TabIndex = 5;
            this.labelNumVer.Text = "0";
            this.labelNumVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbNumVer
            // 
            this.tbNumVer.LargeChange = 1;
            this.tbNumVer.Location = new System.Drawing.Point(11, 404);
            this.tbNumVer.Name = "tbNumVer";
            this.tbNumVer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbNumVer.Size = new System.Drawing.Size(127, 45);
            this.tbNumVer.TabIndex = 2;
            this.tbNumVer.Scroll += new System.EventHandler(this.TrackBarNumVer_Scroll);
            // 
            // treeViewCaMaLa
            // 
            this.treeViewCaMaLa.Location = new System.Drawing.Point(9, 597);
            this.treeViewCaMaLa.Name = "treeViewCaMaLa";
            this.treeViewCaMaLa.Size = new System.Drawing.Size(160, 52);
            this.treeViewCaMaLa.TabIndex = 6;
            // 
            // bttnCaMaLaAg
            // 
            this.bttnCaMaLaAg.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnCaMaLaAg.Location = new System.Drawing.Point(10, 543);
            this.bttnCaMaLaAg.Name = "bttnCaMaLaAg";
            this.bttnCaMaLaAg.Size = new System.Drawing.Size(76, 48);
            this.bttnCaMaLaAg.TabIndex = 7;
            this.bttnCaMaLaAg.Text = "Agent path";
            this.bttnCaMaLaAg.UseVisualStyleBackColor = true;
            this.bttnCaMaLaAg.Click += new System.EventHandler(this.BttnCaMaLa_Click);
            // 
            // cbAgents
            // 
            this.cbAgents.FormattingEnabled = true;
            this.cbAgents.Location = new System.Drawing.Point(10, 514);
            this.cbAgents.Name = "cbAgents";
            this.cbAgents.Size = new System.Drawing.Size(158, 21);
            this.cbAgents.Sorted = true;
            this.cbAgents.TabIndex = 4;
            // 
            // bttnCaMaLaVer
            // 
            this.bttnCaMaLaVer.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnCaMaLaVer.Location = new System.Drawing.Point(92, 543);
            this.bttnCaMaLaVer.Name = "bttnCaMaLaVer";
            this.bttnCaMaLaVer.Size = new System.Drawing.Size(76, 48);
            this.bttnCaMaLaVer.TabIndex = 10;
            this.bttnCaMaLaVer.Text = "Longest path";
            this.bttnCaMaLaVer.UseVisualStyleBackColor = true;
            this.bttnCaMaLaVer.Click += new System.EventHandler(this.BttnCaMaLaVer_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Vertex and edges";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bttnPrim
            // 
            this.bttnPrim.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnPrim.Location = new System.Drawing.Point(936, 91);
            this.bttnPrim.Name = "bttnPrim";
            this.bttnPrim.Size = new System.Drawing.Size(93, 39);
            this.bttnPrim.TabIndex = 12;
            this.bttnPrim.Text = "Prim";
            this.bttnPrim.UseVisualStyleBackColor = true;
            this.bttnPrim.Click += new System.EventHandler(this.BttnPrim_Click);
            // 
            // bttnKruskal
            // 
            this.bttnKruskal.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnKruskal.Location = new System.Drawing.Point(1114, 91);
            this.bttnKruskal.Name = "bttnKruskal";
            this.bttnKruskal.Size = new System.Drawing.Size(90, 38);
            this.bttnKruskal.TabIndex = 13;
            this.bttnKruskal.Text = "Kruskal";
            this.bttnKruskal.UseVisualStyleBackColor = true;
            this.bttnKruskal.Click += new System.EventHandler(this.BttnKruskal_Click);
            // 
            // cbInitialVertex
            // 
            this.cbInitialVertex.FormattingEnabled = true;
            this.cbInitialVertex.Location = new System.Drawing.Point(937, 38);
            this.cbInitialVertex.MaxDropDownItems = 10;
            this.cbInitialVertex.Name = "cbInitialVertex";
            this.cbInitialVertex.Size = new System.Drawing.Size(127, 21);
            this.cbInitialVertex.TabIndex = 15;
            this.cbInitialVertex.Click += new System.EventHandler(this.CbInitialVertex_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(936, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "Origin";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TreeViewPrim
            // 
            this.TreeViewPrim.Location = new System.Drawing.Point(937, 181);
            this.TreeViewPrim.Name = "TreeViewPrim";
            this.TreeViewPrim.Size = new System.Drawing.Size(132, 136);
            this.TreeViewPrim.TabIndex = 16;
            this.TreeViewPrim.DoubleClick += new System.EventHandler(this.TreeViewARM_DoubleClick);
            // 
            // bttnAnimatePrim
            // 
            this.bttnAnimatePrim.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnAnimatePrim.Location = new System.Drawing.Point(937, 136);
            this.bttnAnimatePrim.Name = "bttnAnimatePrim";
            this.bttnAnimatePrim.Size = new System.Drawing.Size(132, 39);
            this.bttnAnimatePrim.TabIndex = 17;
            this.bttnAnimatePrim.Text = "Animate Prim";
            this.bttnAnimatePrim.UseVisualStyleBackColor = true;
            this.bttnAnimatePrim.Click += new System.EventHandler(this.BttnAnimatePrim_Click);
            // 
            // bttnAnimateKruskal
            // 
            this.bttnAnimateKruskal.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnAnimateKruskal.Location = new System.Drawing.Point(1076, 136);
            this.bttnAnimateKruskal.Name = "bttnAnimateKruskal";
            this.bttnAnimateKruskal.Size = new System.Drawing.Size(129, 38);
            this.bttnAnimateKruskal.TabIndex = 18;
            this.bttnAnimateKruskal.Text = "Animate Kruskal";
            this.bttnAnimateKruskal.UseVisualStyleBackColor = true;
            this.bttnAnimateKruskal.Click += new System.EventHandler(this.BttnAnimateKruskal_Click);
            // 
            // bttnBoth
            // 
            this.bttnBoth.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnBoth.Location = new System.Drawing.Point(1035, 92);
            this.bttnBoth.Name = "bttnBoth";
            this.bttnBoth.Size = new System.Drawing.Size(73, 38);
            this.bttnBoth.TabIndex = 19;
            this.bttnBoth.Text = "Both";
            this.bttnBoth.UseVisualStyleBackColor = true;
            this.bttnBoth.Click += new System.EventHandler(this.BttnBoth_Click);
            // 
            // bttnAddPrey
            // 
            this.bttnAddPrey.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnAddPrey.Location = new System.Drawing.Point(1076, 346);
            this.bttnAddPrey.Name = "bttnAddPrey";
            this.bttnAddPrey.Size = new System.Drawing.Size(129, 48);
            this.bttnAddPrey.TabIndex = 20;
            this.bttnAddPrey.Text = "Add prey";
            this.bttnAddPrey.UseVisualStyleBackColor = true;
            this.bttnAddPrey.Click += new System.EventHandler(this.BttnAddPrey_Click);
            // 
            // TreeViewKruskal
            // 
            this.TreeViewKruskal.Location = new System.Drawing.Point(1077, 181);
            this.TreeViewKruskal.Name = "TreeViewKruskal";
            this.TreeViewKruskal.Size = new System.Drawing.Size(127, 136);
            this.TreeViewKruskal.TabIndex = 21;
            // 
            // cbDestinationVertex
            // 
            this.cbDestinationVertex.FormattingEnabled = true;
            this.cbDestinationVertex.Location = new System.Drawing.Point(1074, 38);
            this.cbDestinationVertex.MaxDropDownItems = 10;
            this.cbDestinationVertex.Name = "cbDestinationVertex";
            this.cbDestinationVertex.Size = new System.Drawing.Size(130, 21);
            this.cbDestinationVertex.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1071, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 23);
            this.label5.TabIndex = 23;
            this.label5.Text = "Destiny";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHunters
            // 
            this.labelHunters.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHunters.Location = new System.Drawing.Point(1046, 403);
            this.labelHunters.Name = "labelHunters";
            this.labelHunters.Size = new System.Drawing.Size(22, 34);
            this.labelHunters.TabIndex = 29;
            this.labelHunters.Text = "0";
            this.labelHunters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbHunters
            // 
            this.tbHunters.LargeChange = 1;
            this.tbHunters.Location = new System.Drawing.Point(940, 403);
            this.tbHunters.Name = "tbHunters";
            this.tbHunters.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbHunters.Size = new System.Drawing.Size(100, 45);
            this.tbHunters.TabIndex = 28;
            this.tbHunters.Scroll += new System.EventHandler(this.TbHunters_Scroll);
            // 
            // bttnAddHunter
            // 
            this.bttnAddHunter.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnAddHunter.Location = new System.Drawing.Point(1076, 400);
            this.bttnAddHunter.Name = "bttnAddHunter";
            this.bttnAddHunter.Size = new System.Drawing.Size(129, 48);
            this.bttnAddHunter.TabIndex = 27;
            this.bttnAddHunter.Text = "Add hunter";
            this.bttnAddHunter.UseVisualStyleBackColor = true;
            this.bttnAddHunter.Click += new System.EventHandler(this.BttnAddHunter_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(937, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(268, 23);
            this.label6.TabIndex = 30;
            this.label6.Text = "Preys and Hunters";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(937, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(268, 23);
            this.label7.TabIndex = 31;
            this.label7.Text = "Prim and Kruskal";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOrgDes
            // 
            this.labelOrgDes.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrgDes.Location = new System.Drawing.Point(941, 365);
            this.labelOrgDes.Name = "labelOrgDes";
            this.labelOrgDes.Size = new System.Drawing.Size(128, 29);
            this.labelOrgDes.TabIndex = 32;
            this.labelOrgDes.Text = "Orig:      Des:  ";
            this.labelOrgDes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bttnFinBFS
            // 
            this.bttnFinBFS.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnFinBFS.Location = new System.Drawing.Point(940, 570);
            this.bttnFinBFS.Name = "bttnFinBFS";
            this.bttnFinBFS.Size = new System.Drawing.Size(265, 45);
            this.bttnFinBFS.TabIndex = 33;
            this.bttnFinBFS.Text = "Find";
            this.bttnFinBFS.UseVisualStyleBackColor = true;
            this.bttnFinBFS.Click += new System.EventHandler(this.BttnFinBFS_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(941, 544);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 23);
            this.label4.TabIndex = 34;
            this.label4.Text = "Same heigth breadth-first tree";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bttnBruFor
            // 
            this.bttnBruFor.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnBruFor.Location = new System.Drawing.Point(940, 477);
            this.bttnBruFor.Name = "bttnBruFor";
            this.bttnBruFor.Size = new System.Drawing.Size(129, 45);
            this.bttnBruFor.TabIndex = 35;
            this.bttnBruFor.Text = "Brute Force";
            this.bttnBruFor.UseVisualStyleBackColor = true;
            this.bttnBruFor.Click += new System.EventHandler(this.BttnBruFor_Click);
            // 
            // bttnDivCon
            // 
            this.bttnDivCon.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnDivCon.Location = new System.Drawing.Point(1077, 477);
            this.bttnDivCon.Name = "bttnDivCon";
            this.bttnDivCon.Size = new System.Drawing.Size(128, 45);
            this.bttnDivCon.TabIndex = 36;
            this.bttnDivCon.Text = "Divide and Conquer";
            this.bttnDivCon.UseVisualStyleBackColor = true;
            this.bttnDivCon.Click += new System.EventHandler(this.BttnDivCon_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(941, 451);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(263, 23);
            this.label8.TabIndex = 37;
            this.label8.Text = "Closest pair of points";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTDC
            // 
            this.labelTDC.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTDC.Location = new System.Drawing.Point(1077, 525);
            this.labelTDC.Name = "labelTDC";
            this.labelTDC.Size = new System.Drawing.Size(124, 19);
            this.labelTDC.TabIndex = 38;
            this.labelTDC.Text = "0";
            this.labelTDC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTBF
            // 
            this.labelTBF.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTBF.Location = new System.Drawing.Point(940, 525);
            this.labelTBF.Name = "labelTBF";
            this.labelTBF.Size = new System.Drawing.Size(128, 19);
            this.labelTBF.TabIndex = 39;
            this.labelTBF.Text = "0";
            this.labelTBF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(941, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 23);
            this.label1.TabIndex = 40;
            this.label1.Text = "New";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFindBFS
            // 
            this.labelFindBFS.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFindBFS.Location = new System.Drawing.Point(940, 618);
            this.labelFindBFS.Name = "labelFindBFS";
            this.labelFindBFS.Size = new System.Drawing.Size(264, 31);
            this.labelFindBFS.TabIndex = 41;
            this.labelFindBFS.Text = "Verice origen: ";
            this.labelFindBFS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1216, 662);
            this.Controls.Add(this.labelFindBFS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTBF);
            this.Controls.Add(this.labelTDC);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bttnDivCon);
            this.Controls.Add(this.bttnBruFor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bttnFinBFS);
            this.Controls.Add(this.labelOrgDes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelHunters);
            this.Controls.Add(this.tbHunters);
            this.Controls.Add(this.bttnAddHunter);
            this.Controls.Add(this.cbDestinationVertex);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TreeViewKruskal);
            this.Controls.Add(this.bttnAddPrey);
            this.Controls.Add(this.bttnBoth);
            this.Controls.Add(this.bttnAnimateKruskal);
            this.Controls.Add(this.bttnAnimatePrim);
            this.Controls.Add(this.TreeViewPrim);
            this.Controls.Add(this.cbInitialVertex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bttnKruskal);
            this.Controls.Add(this.bttnPrim);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bttnCaMaLaVer);
            this.Controls.Add(this.cbAgents);
            this.Controls.Add(this.bttnCaMaLaAg);
            this.Controls.Add(this.treeViewCaMaLa);
            this.Controls.Add(this.tbNumVer);
            this.Controls.Add(this.labelNumVer);
            this.Controls.Add(this.labelAgents);
            this.Controls.Add(this.bttnAddAge);
            this.Controls.Add(this.bttnScanImg);
            this.Controls.Add(this.treeViewCircles);
            this.Controls.Add(this.bttnLoadImg);
            this.Controls.Add(this.pictureBoxImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proyecto Integrador";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbNumVer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHunters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.TreeView treeViewCaMaLa;
        private System.Windows.Forms.Button bttnCaMaLaAg;
        private System.Windows.Forms.ComboBox cbAgents;
        private System.Windows.Forms.Button bttnCaMaLaVer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bttnPrim;
        private System.Windows.Forms.Button bttnKruskal;
        private System.Windows.Forms.ComboBox cbInitialVertex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView TreeViewPrim;
        private System.Windows.Forms.Button bttnAnimatePrim;
        private System.Windows.Forms.Button bttnAnimateKruskal;
        private System.Windows.Forms.Button bttnBoth;
        private System.Windows.Forms.Button bttnAddPrey;
        private System.Windows.Forms.TreeView TreeViewKruskal;
        private System.Windows.Forms.ComboBox cbDestinationVertex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelHunters;
        private System.Windows.Forms.TrackBar tbHunters;
        private System.Windows.Forms.Button bttnAddHunter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelOrgDes;
        private System.Windows.Forms.Button bttnFinBFS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bttnBruFor;
        private System.Windows.Forms.Button bttnDivCon;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelTDC;
        private System.Windows.Forms.Label labelTBF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFindBFS;
    }
}

namespace RedCarpet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bymlViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeCurrentLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleDemo00StageMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSaveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cpath = new System.Windows.Forms.TextBox();
            this.objectsList = new System.Windows.Forms.ListBox();
            this.glControl1 = new OpenTK.GLControl();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SectionSelect = new System.Windows.Forms.ComboBox();
            this.btn_openBymlView = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.stageToolStripMenuItem,
            this.createToolStripMenuItem,
            this.testSaveLevelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bymlViewerToolStripMenuItem,
            this.openLevelToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeCurrentLevelToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // bymlViewerToolStripMenuItem
            // 
            this.bymlViewerToolStripMenuItem.Name = "bymlViewerToolStripMenuItem";
            this.bymlViewerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.bymlViewerToolStripMenuItem.Text = "Byml viewer";
            this.bymlViewerToolStripMenuItem.Click += new System.EventHandler(this.bymlViewerToolStripMenuItem_Click);
            // 
            // openLevelToolStripMenuItem
            // 
            this.openLevelToolStripMenuItem.Name = "openLevelToolStripMenuItem";
            this.openLevelToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openLevelToolStripMenuItem.Text = "Open level";
            this.openLevelToolStripMenuItem.Click += new System.EventHandler(this.openLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // closeCurrentLevelToolStripMenuItem
            // 
            this.closeCurrentLevelToolStripMenuItem.Name = "closeCurrentLevelToolStripMenuItem";
            this.closeCurrentLevelToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.closeCurrentLevelToolStripMenuItem.Text = "Close current level";
            this.closeCurrentLevelToolStripMenuItem.Click += new System.EventHandler(this.closeCurrentLevelToolStripMenuItem_Click);
            // 
            // stageToolStripMenuItem
            // 
            this.stageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titleToolStripMenuItem});
            this.stageToolStripMenuItem.Name = "stageToolStripMenuItem";
            this.stageToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.stageToolStripMenuItem.Text = "Stage";
            // 
            // titleToolStripMenuItem
            // 
            this.titleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titleDemo00StageMapToolStripMenuItem});
            this.titleToolStripMenuItem.Name = "titleToolStripMenuItem";
            this.titleToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.titleToolStripMenuItem.Text = "Title";
            // 
            // titleDemo00StageMapToolStripMenuItem
            // 
            this.titleDemo00StageMapToolStripMenuItem.Name = "titleDemo00StageMapToolStripMenuItem";
            this.titleDemo00StageMapToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.titleDemo00StageMapToolStripMenuItem.Text = "EnterCatMarioStageMap1";
            this.titleDemo00StageMapToolStripMenuItem.Click += new System.EventHandler(this.titleDemo00StageDesign1ToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actorToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // actorToolStripMenuItem
            // 
            this.actorToolStripMenuItem.Name = "actorToolStripMenuItem";
            this.actorToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.actorToolStripMenuItem.Text = "Actor";
            this.actorToolStripMenuItem.Click += new System.EventHandler(this.actorToolStripMenuItem_Click);
            // 
            // testSaveLevelToolStripMenuItem
            // 
            this.testSaveLevelToolStripMenuItem.Name = "testSaveLevelToolStripMenuItem";
            this.testSaveLevelToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.testSaveLevelToolStripMenuItem.Text = "Test save level";
            this.testSaveLevelToolStripMenuItem.Click += new System.EventHandler(this.testSaveLevelToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Compile Path";
            // 
            // cpath
            // 
            this.cpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cpath.Enabled = false;
            this.cpath.Location = new System.Drawing.Point(3, 23);
            this.cpath.Name = "cpath";
            this.cpath.Size = new System.Drawing.Size(156, 20);
            this.cpath.TabIndex = 3;
            // 
            // objectsList
            // 
            this.objectsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectsList.FormattingEnabled = true;
            this.objectsList.Location = new System.Drawing.Point(5, 29);
            this.objectsList.Name = "objectsList";
            this.objectsList.Size = new System.Drawing.Size(136, 329);
            this.objectsList.TabIndex = 11;
            this.objectsList.SelectedIndexChanged += new System.EventHandler(this.objectsList_SelectedIndexChanged);
            this.objectsList.DoubleClick += new System.EventHandler(this.objectsList_doubleClick);
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(3, 3);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(610, 423);
            this.glControl1.TabIndex = 10;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseWheel);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_resize);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 49);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(156, 367);
            this.propertyGrid1.TabIndex = 12;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SectionSelect);
            this.splitContainer1.Panel1.Controls.Add(this.btn_openBymlView);
            this.splitContainer1.Panel1.Controls.Add(this.objectsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cpath);
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(310, 423);
            this.splitContainer1.SplitterDistance = 144;
            this.splitContainer1.TabIndex = 13;
            // 
            // SectionSelect
            // 
            this.SectionSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SectionSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionSelect.FormattingEnabled = true;
            this.SectionSelect.Location = new System.Drawing.Point(5, 3);
            this.SectionSelect.Name = "SectionSelect";
            this.SectionSelect.Size = new System.Drawing.Size(136, 21);
            this.SectionSelect.TabIndex = 13;
            this.SectionSelect.SelectedIndexChanged += new System.EventHandler(this.SectionSelect_SelectedIndexChanged);
            // 
            // btn_openBymlView
            // 
            this.btn_openBymlView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_openBymlView.Enabled = false;
            this.btn_openBymlView.Location = new System.Drawing.Point(5, 389);
            this.btn_openBymlView.Name = "btn_openBymlView";
            this.btn_openBymlView.Size = new System.Drawing.Size(136, 27);
            this.btn_openBymlView.TabIndex = 12;
            this.btn_openBymlView.Text = "Open byml viewer";
            this.btn_openBymlView.UseVisualStyleBackColor = true;
            this.btn_openBymlView.Click += new System.EventHandler(this.btn_openBymlView_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(12, 27);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.glControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(936, 429);
            this.splitContainer2.SplitterDistance = 616;
            this.splitContainer2.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 468);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RedCarpet";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem titleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem titleDemo00StageMapToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cpath;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ListBox objectsList;
        private System.Windows.Forms.ToolStripMenuItem bymlViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentLevelToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_openBymlView;
        private System.Windows.Forms.ToolStripMenuItem testSaveLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actorToolStripMenuItem;
        private System.Windows.Forms.ComboBox SectionSelect;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}


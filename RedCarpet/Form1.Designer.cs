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
            this.selectStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bymlViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCurrentLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.changeGameFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectFileManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SectionSelect = new System.Windows.Forms.ComboBox();
            this.objectsList = new System.Windows.Forms.ListBox();
            this.cpath = new System.Windows.Forms.TextBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.glControl1 = new OpenTK.GLControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.DelAllWithoutOne = new System.Windows.Forms.Button();
            this.DelAllEx = new System.Windows.Forms.TextBox();
            this.btn_duplicate = new System.Windows.Forms.Button();
            this.DelAllObj = new System.Windows.Forms.Button();
            this.btn_openBymlView = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.BymlImp = new System.Windows.Forms.Button();
            this.ExpByml = new System.Windows.Forms.Button();
            this.EditLinks = new System.Windows.Forms.Button();
            this.Undobut = new System.Windows.Forms.Button();
            this.Redobut = new System.Windows.Forms.Button();
            this.AddObj = new System.Windows.Forms.Button();
            this.FindObj = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.Prev = new System.Windows.Forms.Button();
            this.ToolContainer = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.ToolContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.createToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.objectFilesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1032, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectStageToolStripMenuItem,
            this.openLevelToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.bymlViewerToolStripMenuItem,
            this.closeCurrentLevelToolStripMenuItem,
            this.toolStripSeparator2,
            this.changeGameFolderToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // selectStageToolStripMenuItem
            // 
            this.selectStageToolStripMenuItem.Name = "selectStageToolStripMenuItem";
            this.selectStageToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.selectStageToolStripMenuItem.Text = "Select stage";
            this.selectStageToolStripMenuItem.Click += new System.EventHandler(this.selectStageToolStripMenuItem_Click);
            // 
            // openLevelToolStripMenuItem
            // 
            this.openLevelToolStripMenuItem.Name = "openLevelToolStripMenuItem";
            this.openLevelToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openLevelToolStripMenuItem.Text = "Open external level";
            this.openLevelToolStripMenuItem.Click += new System.EventHandler(this.openLevelToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveToolStripMenuItem.Text = "Save level";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // bymlViewerToolStripMenuItem
            // 
            this.bymlViewerToolStripMenuItem.Name = "bymlViewerToolStripMenuItem";
            this.bymlViewerToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.bymlViewerToolStripMenuItem.Text = "Byml viewer";
            this.bymlViewerToolStripMenuItem.Click += new System.EventHandler(this.bymlViewerToolStripMenuItem_Click);
            // 
            // closeCurrentLevelToolStripMenuItem
            // 
            this.closeCurrentLevelToolStripMenuItem.Name = "closeCurrentLevelToolStripMenuItem";
            this.closeCurrentLevelToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.closeCurrentLevelToolStripMenuItem.Text = "Close current level";
            this.closeCurrentLevelToolStripMenuItem.Click += new System.EventHandler(this.closeCurrentLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // changeGameFolderToolStripMenuItem
            // 
            this.changeGameFolderToolStripMenuItem.Name = "changeGameFolderToolStripMenuItem";
            this.changeGameFolderToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.changeGameFolderToolStripMenuItem.Text = "Change game folder";
            this.changeGameFolderToolStripMenuItem.Click += new System.EventHandler(this.changeGameFolderToolStripMenuItem_Click);
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
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // objectFilesToolStripMenuItem
            // 
            this.objectFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectFileManagerToolStripMenuItem});
            this.objectFilesToolStripMenuItem.Name = "objectFilesToolStripMenuItem";
            this.objectFilesToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.objectFilesToolStripMenuItem.Text = "Object Files";
            this.objectFilesToolStripMenuItem.Click += new System.EventHandler(this.objectFilesToolStripMenuItem_Click);
            // 
            // objectFileManagerToolStripMenuItem
            // 
            this.objectFileManagerToolStripMenuItem.Name = "objectFileManagerToolStripMenuItem";
            this.objectFileManagerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.objectFileManagerToolStripMenuItem.Text = "Object File Manager";
            this.objectFileManagerToolStripMenuItem.Click += new System.EventHandler(this.objectFileManagerToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.SectionSelect);
            this.splitContainer1.Panel1.Controls.Add(this.objectsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.cpath);
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(295, 440);
            this.splitContainer1.SplitterDistance = 137;
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
            this.SectionSelect.Size = new System.Drawing.Size(121, 21);
            this.SectionSelect.TabIndex = 13;
            this.SectionSelect.SelectedIndexChanged += new System.EventHandler(this.SectionSelect_SelectedIndexChanged);
            // 
            // objectsList
            // 
            this.objectsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectsList.FormattingEnabled = true;
            this.objectsList.Location = new System.Drawing.Point(5, 29);
            this.objectsList.Name = "objectsList";
            this.objectsList.Size = new System.Drawing.Size(121, 394);
            this.objectsList.TabIndex = 11;
            this.objectsList.SelectedIndexChanged += new System.EventHandler(this.objectsList_SelectedIndexChanged);
            this.objectsList.DoubleClick += new System.EventHandler(this.objectsList_doubleClick);
            this.objectsList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.objectsList_KeyDown);
            // 
            // cpath
            // 
            this.cpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cpath.Enabled = false;
            this.cpath.Location = new System.Drawing.Point(3, 23);
            this.cpath.Name = "cpath";
            this.cpath.Size = new System.Drawing.Size(139, 20);
            this.cpath.TabIndex = 3;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 49);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(139, 388);
            this.propertyGrid1.TabIndex = 12;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyValueChanged);
            this.propertyGrid1.Click += new System.EventHandler(this.propertyGrid1_Click);
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
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1 (Normal)",
            "2 (Double)",
            "3 (Triple)",
            "4 (Quadruple)",
            "5 (Fiveduple)",
            "10 (Double-Fiveduple)",
            "25 (Fiveduple-Fiveduple)",
            "50 (Fiveduple-Tenduple)",
            "75 (Triple-Fiveduple-Fiveduple)",
            "100 (Hundruple)",
            "200 (Double-Hundruple)",
            "300 (Triple-Hundruple)",
            "500 (Fivedruple-Hundruple)",
            "1000 (Thousandruple)",
            "5000 (FiveThousandDruple)",
            "10000 (Ten-Thousandruple)"});
            this.comboBox1.Location = new System.Drawing.Point(784, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(242, 21);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.Text = "1 (Normal)";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(3, 10);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(573, 437);
            this.glControl1.TabIndex = 10;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseWheel);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_resize);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer2.Location = new System.Drawing.Point(12, 27);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.glControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(884, 443);
            this.splitContainer2.SplitterDistance = 562;
            this.splitContainer2.TabIndex = 14;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // DelAllWithoutOne
            // 
            this.DelAllWithoutOne.Location = new System.Drawing.Point(6, 19);
            this.DelAllWithoutOne.Name = "DelAllWithoutOne";
            this.DelAllWithoutOne.Size = new System.Drawing.Size(118, 36);
            this.DelAllWithoutOne.TabIndex = 0;
            this.DelAllWithoutOne.Text = "Delete All Objects Except One";
            this.DelAllWithoutOne.UseVisualStyleBackColor = true;
            this.DelAllWithoutOne.Click += new System.EventHandler(this.DelAllWithoutOne_Click);
            // 
            // DelAllEx
            // 
            this.DelAllEx.Location = new System.Drawing.Point(6, 60);
            this.DelAllEx.Name = "DelAllEx";
            this.DelAllEx.Size = new System.Drawing.Size(118, 20);
            this.DelAllEx.TabIndex = 1;
            this.DelAllEx.Text = "     Object To Leave";
            // 
            // btn_duplicate
            // 
            this.btn_duplicate.Location = new System.Drawing.Point(6, 173);
            this.btn_duplicate.Name = "btn_duplicate";
            this.btn_duplicate.Size = new System.Drawing.Size(118, 23);
            this.btn_duplicate.TabIndex = 13;
            this.btn_duplicate.Text = "Duplicate object";
            this.btn_duplicate.UseVisualStyleBackColor = true;
            this.btn_duplicate.Click += new System.EventHandler(this.btn_duplicate_Click);
            // 
            // DelAllObj
            // 
            this.DelAllObj.Location = new System.Drawing.Point(6, 86);
            this.DelAllObj.Name = "DelAllObj";
            this.DelAllObj.Size = new System.Drawing.Size(118, 23);
            this.DelAllObj.TabIndex = 2;
            this.DelAllObj.Text = "Delete All Objects";
            this.DelAllObj.UseVisualStyleBackColor = true;
            this.DelAllObj.Click += new System.EventHandler(this.DelAllObj_Click);
            // 
            // btn_openBymlView
            // 
            this.btn_openBymlView.Enabled = false;
            this.btn_openBymlView.Location = new System.Drawing.Point(6, 231);
            this.btn_openBymlView.Name = "btn_openBymlView";
            this.btn_openBymlView.Size = new System.Drawing.Size(118, 27);
            this.btn_openBymlView.TabIndex = 12;
            this.btn_openBymlView.Text = "Open byml viewer";
            this.btn_openBymlView.UseVisualStyleBackColor = true;
            this.btn_openBymlView.Click += new System.EventHandler(this.btn_openBymlView_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(6, 202);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(118, 23);
            this.btn_del.TabIndex = 14;
            this.btn_del.Text = "Delete object";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // BymlImp
            // 
            this.BymlImp.Location = new System.Drawing.Point(6, 144);
            this.BymlImp.Name = "BymlImp";
            this.BymlImp.Size = new System.Drawing.Size(118, 23);
            this.BymlImp.TabIndex = 15;
            this.BymlImp.Text = "Import Byml From Xml";
            this.BymlImp.UseVisualStyleBackColor = true;
            this.BymlImp.Click += new System.EventHandler(this.button3_Click);
            // 
            // ExpByml
            // 
            this.ExpByml.Location = new System.Drawing.Point(6, 115);
            this.ExpByml.Name = "ExpByml";
            this.ExpByml.Size = new System.Drawing.Size(118, 23);
            this.ExpByml.TabIndex = 14;
            this.ExpByml.Text = "Export Byml To Xml";
            this.ExpByml.UseVisualStyleBackColor = true;
            this.ExpByml.Click += new System.EventHandler(this.button2_Click);
            // 
            // EditLinks
            // 
            this.EditLinks.Location = new System.Drawing.Point(6, 264);
            this.EditLinks.Name = "EditLinks";
            this.EditLinks.Size = new System.Drawing.Size(118, 23);
            this.EditLinks.TabIndex = 13;
            this.EditLinks.Text = "Edit links (TODO)";
            this.EditLinks.UseVisualStyleBackColor = true;
            this.EditLinks.Click += new System.EventHandler(this.EditLinks_Click);
            // 
            // Undobut
            // 
            this.Undobut.Location = new System.Drawing.Point(6, 293);
            this.Undobut.Name = "Undobut";
            this.Undobut.Size = new System.Drawing.Size(118, 23);
            this.Undobut.TabIndex = 3;
            this.Undobut.Text = "Undo";
            this.Undobut.UseVisualStyleBackColor = true;
            this.Undobut.Click += new System.EventHandler(this.Undobut_Click);
            // 
            // Redobut
            // 
            this.Redobut.Location = new System.Drawing.Point(6, 322);
            this.Redobut.Name = "Redobut";
            this.Redobut.Size = new System.Drawing.Size(118, 23);
            this.Redobut.TabIndex = 16;
            this.Redobut.Text = "Redo";
            this.Redobut.UseVisualStyleBackColor = true;
            this.Redobut.Click += new System.EventHandler(this.Redobut_Click);
            // 
            // AddObj
            // 
            this.AddObj.Location = new System.Drawing.Point(6, 351);
            this.AddObj.Name = "AddObj";
            this.AddObj.Size = new System.Drawing.Size(118, 23);
            this.AddObj.TabIndex = 17;
            this.AddObj.Text = "Add Object";
            this.AddObj.UseVisualStyleBackColor = true;
            this.AddObj.Click += new System.EventHandler(this.AddObj_Click);
            // 
            // FindObj
            // 
            this.FindObj.Location = new System.Drawing.Point(6, 380);
            this.FindObj.Name = "FindObj";
            this.FindObj.Size = new System.Drawing.Size(118, 23);
            this.FindObj.TabIndex = 18;
            this.FindObj.Text = "FindObject";
            this.FindObj.UseVisualStyleBackColor = true;
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(6, 429);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(58, 23);
            this.Next.TabIndex = 19;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            // 
            // Prev
            // 
            this.Prev.Location = new System.Drawing.Point(64, 429);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(60, 23);
            this.Prev.TabIndex = 20;
            this.Prev.Text = "Prev";
            this.Prev.UseVisualStyleBackColor = true;
            // 
            // ToolContainer
            // 
            this.ToolContainer.Controls.Add(this.textBox1);
            this.ToolContainer.Controls.Add(this.Prev);
            this.ToolContainer.Controls.Add(this.Next);
            this.ToolContainer.Controls.Add(this.FindObj);
            this.ToolContainer.Controls.Add(this.AddObj);
            this.ToolContainer.Controls.Add(this.Redobut);
            this.ToolContainer.Controls.Add(this.Undobut);
            this.ToolContainer.Controls.Add(this.EditLinks);
            this.ToolContainer.Controls.Add(this.ExpByml);
            this.ToolContainer.Controls.Add(this.BymlImp);
            this.ToolContainer.Controls.Add(this.btn_del);
            this.ToolContainer.Controls.Add(this.btn_openBymlView);
            this.ToolContainer.Controls.Add(this.DelAllObj);
            this.ToolContainer.Controls.Add(this.btn_duplicate);
            this.ToolContainer.Controls.Add(this.DelAllEx);
            this.ToolContainer.Controls.Add(this.DelAllWithoutOne);
            this.ToolContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.ToolContainer.Location = new System.Drawing.Point(902, 24);
            this.ToolContainer.Name = "ToolContainer";
            this.ToolContainer.Size = new System.Drawing.Size(130, 458);
            this.ToolContainer.TabIndex = 15;
            this.ToolContainer.TabStop = false;
            this.ToolContainer.Text = "Tools";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 405);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 20);
            this.textBox1.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(667, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Mouse Scroll Speed ::";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 482);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ToolContainer);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RedCarpet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
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
            this.ToolContainer.ResumeLayout(false);
            this.ToolContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bymlViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectStageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem changeGameFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.ComboBox SectionSelect;
        public System.Windows.Forms.ListBox objectsList;
        private System.Windows.Forms.TextBox cpath;
        private System.Windows.Forms.Label label1;
        public OpenTK.GLControl glControl1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Button DelAllWithoutOne;
        private System.Windows.Forms.TextBox DelAllEx;
        private System.Windows.Forms.Button btn_duplicate;
        private System.Windows.Forms.Button DelAllObj;
        private System.Windows.Forms.Button btn_openBymlView;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button BymlImp;
        private System.Windows.Forms.Button ExpByml;
        private System.Windows.Forms.Button EditLinks;
        private System.Windows.Forms.Button Undobut;
        private System.Windows.Forms.Button Redobut;
        private System.Windows.Forms.Button AddObj;
        private System.Windows.Forms.Button FindObj;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Prev;
        private System.Windows.Forms.GroupBox ToolContainer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem objectFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectFileManagerToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}


namespace RedCarpet
{
    partial class StageSelectForm
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
            this.StageSelectListBox = new System.Windows.Forms.ListBox();
            this.TextSearch = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // StageSelectListBox
            // 
            this.StageSelectListBox.FormattingEnabled = true;
            this.StageSelectListBox.Location = new System.Drawing.Point(12, 30);
            this.StageSelectListBox.Name = "StageSelectListBox";
            this.StageSelectListBox.Size = new System.Drawing.Size(282, 433);
            this.StageSelectListBox.TabIndex = 0;
            this.StageSelectListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.StageSelectListBox_MouseDoubleClick);
            // 
            // TextSearch
            // 
            this.TextSearch.Location = new System.Drawing.Point(12, 4);
            this.TextSearch.Name = "TextSearch";
            this.TextSearch.Size = new System.Drawing.Size(282, 20);
            this.TextSearch.TabIndex = 1;
            this.TextSearch.Text = "Search";
            this.TextSearch.Click += new System.EventHandler(this.TextSearch_click);
            this.TextSearch.TextChanged += new System.EventHandler(this.TextSearch_TextChanged);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.Black;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalExtent = 1000;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(300, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(450, 407);
            this.listBox1.TabIndex = 2;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "WORLD MAP",
            "WORLD - 1",
            "WORLD - 2 ",
            "WORLD - 3",
            "WORLD - 4",
            "WORLD - 5",
            "WORLD - 6",
            "WORLD - \'",
            "WORLD - :",
            "WORLD - \"",
            "WORLD - $",
            "WORLD - %",
            "WORLD - &",
            "MISCELLANEOUS"});
            this.comboBox1.Location = new System.Drawing.Point(300, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(450, 26);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // StageSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 478);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.TextSearch);
            this.Controls.Add(this.StageSelectListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StageSelectForm";
            this.Text = "Stage Select";
            this.Load += new System.EventHandler(this.StageSelectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox StageSelectListBox;
        private System.Windows.Forms.TextBox TextSearch;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
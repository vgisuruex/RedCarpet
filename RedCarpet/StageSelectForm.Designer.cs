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
            // StageSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 478);
            this.Controls.Add(this.TextSearch);
            this.Controls.Add(this.StageSelectListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StageSelectForm";
            this.Text = "Stage Select";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox StageSelectListBox;
        private System.Windows.Forms.TextBox TextSearch;
    }
}
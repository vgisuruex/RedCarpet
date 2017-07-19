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
            this.SuspendLayout();
            // 
            // StageSelectListBox
            // 
            this.StageSelectListBox.FormattingEnabled = true;
            this.StageSelectListBox.Location = new System.Drawing.Point(12, 14);
            this.StageSelectListBox.Name = "StageSelectListBox";
            this.StageSelectListBox.Size = new System.Drawing.Size(282, 446);
            this.StageSelectListBox.TabIndex = 0;
            this.StageSelectListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.StageSelectListBox_MouseDoubleClick);
            // 
            // StageSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 478);
            this.Controls.Add(this.StageSelectListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StageSelectForm";
            this.Text = "Stage Select";
            this.Load += new System.EventHandler(this.StageSelectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox StageSelectListBox;
    }
}
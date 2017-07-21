using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedCarpet
{
    public partial class StageSelectForm : Form
    {
        string[] LevelsList;
        public StageSelectForm()
        {
            InitializeComponent();

            string[] FileList = Directory.GetFiles(Properties.Settings.Default.GamePath + "StageData/", "*.szs"); // get all files from /StageData that ends with szs
            List<string> levels = new List<string>();
            foreach (string path in FileList) // getting filePaths and do the following for every file
            {
                if (path.Contains("Map")) // like it says, if it contains "Map" in it
                {
                    string filename = Path.GetFileNameWithoutExtension(path); // create variable "filename" with the filename
                    levels.Add(filename);
                    StageSelectListBox.Items.Add(filename);
                }
            }
            LevelsList = levels.ToArray();
        }

        private void StageSelectListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form1 form = (Form1)this.Owner; // get the owner of this form

            // check if there is an item under the mouse pointer
            int index = this.StageSelectListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                form.LoadLevel(Properties.Settings.Default.GamePath + "StageData/" + StageSelectListBox.Items[index] + ".szs"); // load the name from /StageData/ and add .szs file extension
                this.Close(); // let the window disappear
            }
        }

        private void StageSelectForm_Load(object sender, EventArgs e)
        {

        }

        private void TextSearch_TextChanged(object sender, EventArgs e)
        {
            if (TextSearch.Text.Trim() == "" && StageSelectListBox.Items.Count == LevelsList.Length) return;
            StageSelectListBox.Items.Clear();
            if (TextSearch.Text.Trim() == "") StageSelectListBox.Items.AddRange(LevelsList);
            foreach (string s in LevelsList) if (s.IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0) StageSelectListBox.Items.Add(s);
        }

        bool SeachPlaceholder = true;
        private void TextSearch_click(object sender, EventArgs e)
        {
            if (!SeachPlaceholder) return;
            TextSearch.Text = "";
            SeachPlaceholder = false;
        }
    }
}

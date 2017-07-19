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
        public StageSelectForm()
        {
            InitializeComponent();

            string[] filePaths = Directory.GetFiles(Form1.BASEPATH + "StageData/", "*.szs"); // get all files from /StageData that ends with szs

            foreach (string path in filePaths) // getting filePaths and do the following for every file
            {
                if (path.Contains("Map")) // like it says, if it contains "Map" in it
                {
                    string filename = Path.GetFileNameWithoutExtension(path); // create variable "filename" with the filename

                    StageSelectListBox.Items.Add(filename);
                }
            }
        }

        private void StageSelectListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form1 form = (Form1)this.Owner; // get the owner of this form

            // check if there is an item under the mouse pointer
            int index = this.StageSelectListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                form.LoadLevel(Form1.BASEPATH + "StageData/" + StageSelectListBox.Items[index] + ".szs"); // load the name from /StageData/ and add .szs file extension
                this.Close(); // let the window disappear
            }
        }

        private void StageSelectForm_Load(object sender, EventArgs e)
        {

        }
    }
}

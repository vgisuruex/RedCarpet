using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static RedCarpet.Object;
using ICSharpCode.SharpZipLib.Zip;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RedCarpet
{
    public partial class Form2 : Form
    {
        public MapObject testobject;
        public int CycleNumber = 0;
        public ListBox ObjectList;
        bool objectfileloaded = false;
        bool IsCorrectRco = false;
        bool ImageInside = false;
        bool ImageLoaded = false;
        int lastselected = 0;
        int number = 1;
        public bool ObjectReadyToSave;
        public MapObject Item;
        public List<MapObject> ObjectsInScene;
        public string SelectedSectionName;

        private int SelectedIndex

        {
            get { return listBox2.SelectedIndex; }
            set { listBox2.SelectedIndex = value; }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CheckLibrary();
            if (Directory.Exists("Library/Unpacked")) Directory.Delete("Library/Unpacked", true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Png Images |*png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)Bitmap.FromFile(ofd.FileName);
                if (bmp.Size.Height != 200 && bmp.Size.Width != 200)
                {
                    Bitmap Image = ResizeBitmap(bmp, 200, 200);
                    pictureBox1.Image = Image;
                    ImageLoaded = true;
                    Directory.CreateDirectory("FilesToExport");
                    Image.Save("FilesToExport/image.png");
                }
                else
                {
                    pictureBox1.Image = bmp;
                    ImageLoaded = true;
                }
            }
        }
        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (ObjectReadyToSave == false)
                {
                    MessageBox.Show("Please load a level first", "No object to export", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }
                else if (listBox1.Items.Count == 0)
                {
                    MessageBox.Show("Not object added", "No objects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Red Carpet Object Files |*.rco";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        FastZip zip = new FastZip();
                        zip.CreateZip(sfd.FileName, "FilesToExport/", false, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please type a name for the object file", "No Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
                if (e.Handled == true) ;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                if (!listBox1.Items.Contains(ObjectsInScene[SelectedIndex].unitConfigName))
                {
                    listBox1.Items.Add(ObjectsInScene[SelectedIndex].unitConfigName);
                    Directory.CreateDirectory("FilesToExport");
                    MemoryStream stream = new MemoryStream();
                    stream.Position = 0;
                    IFormatter formatter = new BinaryFormatter();
                    stream.Position = 0;
                    formatter.Serialize(stream, ObjectsInScene[SelectedIndex]);
                    stream.Position = 0;
                    using (FileStream file = new FileStream("FilesToExport/" + ObjectsInScene[SelectedIndex].unitConfigName + ".Itm", FileMode.Create, System.IO.FileAccess.Write))
                        stream.CopyTo(file);
                }
                else
                {
                    SameObjectExist();
                }
            }
        }
        private void SameObjectExist()
        {
            if (!listBox1.Items.Contains(ObjectsInScene[SelectedIndex].unitConfigName + number))
            {
                listBox1.Items.Add(ObjectsInScene[SelectedIndex].unitConfigName + number);
                ObjectsInScene.Add(ObjectsInScene[SelectedIndex]);
                Directory.CreateDirectory("FilesToExport");
                MemoryStream stream = new MemoryStream();
                stream.Position = 0;
                IFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                formatter.Serialize(stream, ObjectsInScene[SelectedIndex]);
                using (FileStream file = new FileStream("FilesToExport/" + ObjectsInScene[SelectedIndex].unitConfigName + number + ".Itm", FileMode.Create, System.IO.FileAccess.Write))
                    stream.CopyTo(file);
            }
            else
            {
                number += 1;
                if (number == 101)
                {
                    MessageBox.Show("WHAT THE HELL?! THE OBJECT YOU'RE TRYING TO ADD IS ALREADY ADDED 100 TIMES. COME ON MAN", "SERIOULSY?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (number == 201)
                {
                    MessageBox.Show("Really man?", "SERIOULSY?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (number == 501)
                {
                    MessageBox.Show("Oh come on.", "oh.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (number == 701)
                {
                    MessageBox.Show("I know that you're doing it to see what i'm gonna say. i can read your mind. MWHAHAHAHAHA", "are you even reading this?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (number == 1001)
                {
                    MessageBox.Show("Ok no more. you win. there's a super secret file named the 'debugmode.txt'. it will tell you how to get access to the debug tools.", "okay then.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    File.WriteAllText("debugmode.txt", "You FOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOL.");
                }
                SameObjectExist();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (textBox2.Text != "New Object Name")
                {
                    if (!listBox1.Items.Contains(textBox2.Text))
                    {
                        string FileContent = File.ReadAllText("FilesToExport/" + listBox1.GetItemText(listBox1.SelectedItem) + ".Itm");
                        File.Delete("FilesToExport/" + listBox1.GetItemText(listBox1.SelectedItem) + ".Itm");
                        File.WriteAllText("FilesToExport/" + textBox2.Text + ".Itm", FileContent);
                        listBox1.Items[listBox1.SelectedIndex] = textBox2.Text;
                    }
                    else
                    {
                        MessageBox.Show("Two or more objects should not have the same name.", "A object with the same name exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Please put a unique name. -_-", "You didn't enter any text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                File.Delete("FilesToExport/" + listBox1.GetItemText(listBox1.SelectedItem) + ".Itm");
                lastselected = listBox1.SelectedIndex;
                listBox1.Items.Remove(listBox1.SelectedItem);

                if (listBox1.Items.Count > lastselected + 1)
                {
                    listBox1.SelectedIndex = lastselected += 1;
                }
                else
                {
                    listBox1.SelectedIndex = lastselected -= 1;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            number = 1;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists("FilesToExport"))
            {
                Directory.Delete("FilesToExport", true);
            }
            if (Directory.Exists("FilesInView"))
            {
                Directory.Delete("FilesInView", true);
            }
            if (Directory.Exists("Library/Unpacked")) Directory.Delete("Library/Unpacked", true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ImageLoaded == true)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the picture?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    pictureBox1.Image = null;
                    ImageLoaded = false;
                    File.Delete("FilesToExport/Image.png");
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Directory.CreateDirectory("FilesToExport");
            File.WriteAllText("FilesToExport/Name.txt", textBox1.Text);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Directory.CreateDirectory("FilesToExport");
            File.WriteAllText("FilesToExport/Details.txt", richTextBox1.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Red Carpet Object Files|*.rco";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (objectfileloaded == true)
                {
                    Directory.Delete("FilesInView", true);
                }
                FastZip zip = new FastZip();
                objectfileloaded = true;
                zip.ExtractZip(ofd.FileName, "FilesInView", null);
                if (File.Exists("FilesInView/Name.txt"))
                {
                    IsCorrectRco = true;
                    if (File.Exists("FilesInView/Image.png"))
                    {
                        ImageInside = true;
                    }
                    else
                    {
                        ImageInside = false;
                    }
                }
                else
                {
                    IsCorrectRco = false;
                }
            }
            if (IsCorrectRco == true)
            {
                textBox3.Text = File.ReadAllText("FilesInView/Name.txt");
                if (ImageInside == true)
                {
                    using (var fs = new System.IO.FileStream("FilesInView/Image.png", System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        pictureBox2.Image = (Bitmap)bmp.Clone();
                    }
                }
                if (File.Exists("FilesInView/Details.txt")) richTextBox2.Text = File.ReadAllText("FilesInView/Details.txt");
                DirectoryInfo d = new DirectoryInfo("FilesInView");
                FileInfo[] Files = d.GetFiles("*.Itm");
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = file.Name;
                    string item = str.Remove(str.Length - 4);
                    listBox3.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Not a correct red carpet object file", "Not a correct file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GetAllDataFromFiles();
        }
        public void GetAllDataFromFiles()
        {
            var file = Directory.GetFiles("FilesInView", "*.Itm")
            .First();
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            using (FileStream FileToSave = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                FileToSave.Position = 0;
                ms.Position = 0;
                FileToSave.CopyTo(ms);
            }
            ms.Position = 0;
            MapObject ItemToAdd = (MapObject)formatter.Deserialize(ms);
            if (checkBox1.Checked == true)
            {
                OpenTK.Vector3 vector3 = new OpenTK.Vector3((float)numericUpDown1.Value, (float)numericUpDown2.Value, (float)numericUpDown3.Value);
                ItemToAdd.position = vector3;
            }
            if (checkBox2.Checked == true)
            {
                OpenTK.Vector3 vector3 = new OpenTK.Vector3((float)numericUpDown9.Value, (float)numericUpDown8.Value, (float)numericUpDown7.Value);
                ItemToAdd.rotation = vector3;
            }
            if (checkBox3.Checked == true)
            {
                OpenTK.Vector3 vector3 = new OpenTK.Vector3((float)numericUpDown6.Value, (float)numericUpDown5.Value, (float)numericUpDown4.Value);
                ItemToAdd.scale = vector3;
            }
            if (checkBox4.Checked == true)
            {
                OpenTK.Vector3 vector3 = new OpenTK.Vector3((float)numericUpDown1.Value, (float)numericUpDown2.Value, (float)numericUpDown3.Value);
                ItemToAdd.position += vector3;
            }
            if (checkBox5.Checked == true)
            {
                OpenTK.Vector3 vector3 = new OpenTK.Vector3((float)numericUpDown9.Value, (float)numericUpDown8.Value, (float)numericUpDown7.Value);
                ItemToAdd.rotation += vector3;
            }
            if (checkBox6.Checked == true)
            {
                OpenTK.Vector3 vector3 = new OpenTK.Vector3((float)numericUpDown6.Value, (float)numericUpDown5.Value, (float)numericUpDown4.Value);
                ItemToAdd.scale += vector3;
            }
            ((Form1)Owner).AddObject(ItemToAdd, SelectedSectionName);
            File.Delete(file);
            if (Directory.GetFiles("FilesInView", "*.Itm").Length != 0)
            {
                GetAllDataFromFiles();
            }
            else
            {
                if (Directory.Exists("FilesToExport"))
                {
                    Directory.Delete("FilesToExport", true);
                }
                if (Directory.Exists("FilesInView"))
                {
                    Directory.Delete("FilesInView", true);
                }
                Close();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox4.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox5.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox6.Checked = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            File.Delete("FilesInView/" + listBox3.SelectedItem.ToString() + ".Itm");
            listBox3.Items.Remove(listBox3.SelectedItem);
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox5.Items.Count != 0) listBox5.Items.Clear();
            if (Directory.Exists("Library/Unpacked")) Directory.Delete("Library/Unpacked", true);
            if (listBox4.SelectedIndex != -1)
            {
                FastZip zip = new FastZip();
                zip.ExtractZip("Library/" + listBox4.SelectedItem + ".RCO", "Library/Unpacked", null);
                if (File.Exists("Library/Unpacked/Image.png"))
                {
                    using (var fs = new System.IO.FileStream("Library/Unpacked/Image.png", System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        pictureBox3.Image = (Bitmap)bmp.Clone();
                    }
                }
                if (File.Exists("Library/Unpacked/Details.txt")) richTextBox3.Text = File.ReadAllText("Library/Unpacked/Details.txt");
                if (File.Exists("Library/Unpacked/Name.txt")) textBox4.Text = File.ReadAllText("Library/Unpacked/Name.txt");
                DirectoryInfo d = new DirectoryInfo("Library/Unpacked");
                FileInfo[] Files = d.GetFiles("*.Itm");
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = file.Name;
                    string item = str.Remove(str.Length - 4);
                    listBox5.Items.Add(item);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Red Carpet Object Files|*.rco";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.Copy("Library/" + listBox4.SelectedItem + ".RCO", sfd.FileName, true);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to replace this rco in the library? you can't recover it again", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                if (listBox4.SelectedIndex != -1)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Red Carpet Object Files|*.rco";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(ofd.FileName, "Library/" + listBox4.SelectedItem + ".RCO", true);
                        CheckLibrary();
                    }
                }
            }
            else
            {
                return;
            }
        }
        public void CheckLibrary()
        {
            if (listBox4.Items.Count != 0)
            {
                listBox4.Items.Clear();
            }
            if (Directory.Exists("Library"))
            {
                DirectoryInfo d = new DirectoryInfo("Library");
                FileInfo[] Files = d.GetFiles("*.RCO");
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = file.Name;
                    string File = str.Remove(str.Length - 4);
                    listBox4.Items.Add(File);
                }
            }
            else
            {
                Directory.CreateDirectory("Library");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
            {
                if (MessageBox.Show("Are you sure you want to remove this from you're library? you cannot recover it again.", "Are you really sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    File.Delete("Library/" + listBox4.SelectedItem + ".rco");
                    CheckLibrary();
                }
                else
                {
                    return;
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Red Carpet Object Files|*.rco";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                File.Copy(ofd.FileName, "Library/" + ofd.SafeFileName,true);
                CheckLibrary();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            CheckLibrary();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
            {
                if (textBox4.Text != "")
                {
                    File.Copy("Library/" + listBox4.SelectedItem + ".RCO", "Library/CopyFile.RCO", true);
                    File.Delete("Library/" + listBox4.SelectedItem + ".RCO");
                    File.Copy("Library/CopyFile.RCO", "Library/" + RemoveSpecialCharacters(textBox4.Text) + ".RCO", true);
                    File.Delete("Library/CopyFile.RCO");
                    listBox4.SelectedIndex = -1;
                    CheckLibrary();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("FilesInView")) Directory.Delete("FilesInView",true);
            Directory.Move("Library/Unpacked", "FilesInView");
            listBox4.SelectedIndex = -1;
            if (File.Exists("FilesInView/Name.txt"))
            {
                IsCorrectRco = true;
                if (File.Exists("FilesInView/Image.png"))
                {
                    ImageInside = true;
                }
                else
                {
                    ImageInside = false;
                }
            }
            else
            {
                IsCorrectRco = false;
            }
            if (IsCorrectRco == true)
            {
                textBox3.Text = File.ReadAllText("FilesInView/Name.txt");
                if (ImageInside == true)
                {
                    using (var fs = new System.IO.FileStream("FilesInView/Image.png", System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        pictureBox2.Image = (Bitmap)bmp.Clone();
                    }
                }
                if(File.Exists("FilesInView/Details.txt")) richTextBox2.Text = File.ReadAllText("FilesInView/Details.txt");
                DirectoryInfo d = new DirectoryInfo("FilesInView");
                FileInfo[] Files = d.GetFiles("*.Itm");
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = file.Name;
                    string item = str.Remove(str.Length - 4);
                    listBox3.Items.Add(item);
                }
                tabControl1.SelectedIndex = 2;
            }
            else
            {
                MessageBox.Show("Not a correct red carpet object file", "Not a correct file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

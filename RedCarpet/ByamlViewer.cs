using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedCarpet
{
    public partial class ByamlViewer : Form
    {
        Dictionary<string,dynamic> byml;
        public ByamlViewer(Dictionary<string, dynamic> by)
        {
            InitializeComponent();
            byml = by;
            //the first node should always be a dictionary node
            parseDictNode(byml, treeView1.Nodes);
        }

        /*nodes aren't recursively added to the tree view since that would cause a stack overflow
          and a non recursive way will freeze in a probably endless loop, sub nodes are loaded,
          when the parent is expanded, check BeforeExpand*/
        void parseDictNode(Dictionary<string, dynamic> node, TreeNodeCollection addto)
        {
            foreach (string k in node.Keys)
            {
                TreeNode current = addto.Add(k);
                if (node[k] is Dictionary<string, dynamic>)
                {
                    current.Text += " : <Dictionary>";
                    current.Tag = node[k]; 
                    current.Nodes.Add("✯✯dummy✯✯"); //a text that can't be in a byml
                }
                else if (node[k] is List<dynamic>)
                {
                    current.Text += " : <Array>";
                    current.Tag = node[k];
                    current.Nodes.Add("✯✯dummy✯✯");
                }
                else
                {
                    current.Text = current.Text + " : " + (node[k] == null  ? "<NULL>" : node[k].ToString());
                }
            }
        }

        void parseArrayNode(List<dynamic> list, TreeNodeCollection addto)
        {
            foreach (dynamic k in list)
            {
                if (k is Dictionary<string, dynamic>)
                {
                    TreeNode current = addto.Add("<Dictionary>");
                    current.Tag = k;
                    current.Nodes.Add("✯✯dummy✯✯");
                }
                else if (k is List<dynamic>)
                {
                    TreeNode current = addto.Add("<Array>");
                    current.Tag = k;
                    current.Nodes.Add("✯✯dummy✯✯");
                }
                else
                {
                    addto.Add(k == null ? "<NULL>" : k.ToString());
                }
            }
        }

        private void BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag != null && e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "✯✯dummy✯✯")
            {
                e.Node.Nodes.Clear();
                if (((dynamic)e.Node.Tag).Count == 0)
                {
                    e.Node.Nodes.Add("<Empty>");
                    return;
                }
                if (e.Node.Tag is List<dynamic>) parseArrayNode((List<dynamic>)e.Node.Tag, e.Node.Nodes);
                else if (e.Node.Tag is Dictionary<string, dynamic>) parseDictNode((Dictionary<string, dynamic>)e.Node.Tag, e.Node.Nodes);
                else throw new Exception("WTF");
            }
        }

        private void ContextMenuOpening(object sender, CancelEventArgs e)
        {
            CopyNode.Enabled = treeView1.SelectedNode != null;
        }

        private void CopyNode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(treeView1.SelectedNode.Text);
        }

        private void ByamlViewer_Load(object sender, EventArgs e)
        {

        }
    }
}

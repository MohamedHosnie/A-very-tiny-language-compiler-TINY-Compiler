using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_Compiler
{
    public partial class ParserForm : Form
    {
        Parser.treeNode tree;
        public ParserForm()
        {
            InitializeComponent();
        }
        private void ParserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.parserForm = null;
        }
        private void ParserForm_Load(object sender, EventArgs e)
        {
            this.Parse();
        }
        public void Parse()
        {
            tree = Program.compiler.Parse();

            treeView1.Nodes.Clear();

            this.fillTree(treeView1.Nodes, tree);
            treeView1.ExpandAll();

        }
        private void fillTree(TreeNodeCollection collection, Parser.treeNode treenode)
        {
            Parser.treeNode temp = treenode;
            while (temp != null)
            {
                string text = "";
                switch (temp.nodekind)
                {
                    case Parser.nodeKind.Stmt:
                        text += temp.stmtkind;
                        break;
                    case Parser.nodeKind.Exp:
                        text += temp.expkind;
                        break;
                }
                if (temp.name != "")
                    text += " (" + temp.name + ")";
                if (temp.val != null)
                    text += " (" + temp.val + ")";
                if (temp.expkind == Parser.expKind.Op)
                    text += " (" + temp.tokentype.Value.token_Type + ")";

                TreeNode last_node = collection.Add(text);

                foreach (Parser.treeNode child in temp.child)
                {
                    if (child == null) break;
                    fillTree(last_node.Nodes, child);
                }

                temp = temp.sibling;
            }

        }

    }
}

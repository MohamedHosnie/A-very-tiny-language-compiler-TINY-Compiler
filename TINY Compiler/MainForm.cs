using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_Compiler
{
    public partial class MainForm : Form
    {
        public Scanner scanner;
        public Stack<string> history_redo;
        public MainForm()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            history_redo = new Stack<string>();
            //AllocConsole();
        }
        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();
        private void toolStripButtonScan_Click(object sender, EventArgs e)
        {
            Program.compiler.Scan((this.ScanText.Text).ToString());
            if (Program.scanForm == null)
            {
                Program.scanForm = new ScanForm();
                Program.scanForm.Show();
            }
            else
            {
                Program.scanForm.Scan();
            }

        }
        private void toolStripButtonParse_Click(object sender, EventArgs e)
        {
            Program.compiler.Scan((this.ScanText.Text).ToString());
            if (Program.parserForm == null)
            {
                Program.parserForm = new ParserForm();
                Program.parserForm.Show();
            }
            else
            {
                Program.parserForm.Parse();
            }

            
        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if(open.ShowDialog() == DialogResult.OK)
            {
                string file;
                using(StreamReader sr = new StreamReader(open.FileName))
                {
                    file = sr.ReadToEnd();
                    sr.Close();
                }

                ScanText.Text = file;
            }

        }


        /// <summary>
        /// these are event handlers for the tool strip
        /// </summary>
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            ScanText.Cut();
        }
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            ScanText.Copy();
        }
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                ScanText.Paste();
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ScanText.CanUndo == true)
            {
                history_redo.Push(ScanText.Text);
                ScanText.Undo();
                ScanText.ClearUndo();
            }
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(history_redo.Count > 0)
            {
                ScanText.Text = history_redo.Pop();
            }
            
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanText.SelectAll();
        }





        internal void error(string _error)
        {
            MessageBox.Show(_error);
        }
    }
}

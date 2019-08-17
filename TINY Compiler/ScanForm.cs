using System;
using System.Collections;
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
    public partial class ScanForm : Form
    {
        public ScanForm()
        {
            InitializeComponent();
        }
        private void ScanForm_Load(object sender, EventArgs e)
        {
            this.Scan();
        }
        public void Scan()
        {
            var tokens = Program.compiler.getTokens();
            var symbol = Program.compiler.getSymbolTable();
            var comments = Program.compiler.getComments();
            var errors = Program.compiler.getErrors();

            listComments.Clear();
            listError.Clear();


            dataGridTokens.RowCount = 0;
            dataGridTokens.ColumnCount = 2;

            Program.mainForm.lableStatus.Text = "None";
            Program.mainForm.lableStatus.ForeColor = Color.Black;

            for (int i = 0; i < tokens.Count; i++)
            {
                dataGridTokens.RowCount += 1;
                dataGridTokens.Rows[dataGridTokens.RowCount - 1].Cells[0].Value = tokens[i].token_Type;
                dataGridTokens.Rows[dataGridTokens.RowCount - 1].Cells[1].Value = tokens[i].token_Name;

                Program.mainForm.lableStatus.Text = "Success";
                Program.mainForm.lableStatus.ForeColor = Color.Green;
            }


            for (int i = 0; i < comments.Count; i++)
            {
                int line_num = comments[i].lineNumber;
                string comment = comments[i].Text.Replace("{", "\"");
                comment = comment.Replace("}", "\"");
                listComments.Items.Add("Comment on line " + line_num + ": " + comment);
            }
            for (int i = 0; i < errors.Count; i++)
            {
                int line_num = errors[i].lineNumber;
                listError.Items.Add("Error: \"" + errors[i].Text + "\" on line: " + line_num);

                Program.mainForm.lableStatus.Text = "Error";
                Program.mainForm.lableStatus.ForeColor = Color.Red;
            }


            dataSymbol.RowCount = symbol.Count;
            dataSymbol.ColumnCount = 2;
            int j = 0;
            foreach (DictionaryEntry item in symbol)
            {
                dataSymbol.Rows[j].Cells[0].Value = item.Key;
                dataSymbol.Rows[j].Cells[1].Value = item.Value;
                j++;
            }
        }
        private void ScanForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.scanForm = null;
        }
        

    }
}

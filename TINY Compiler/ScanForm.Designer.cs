namespace TINY_Compiler
{
    partial class ScanForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.listComments = new System.Windows.Forms.ListView();
            this.listError = new System.Windows.Forms.ListView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridTokens = new System.Windows.Forms.DataGridView();
            this.colmnLexeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colomnTokencategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSymbol = new System.Windows.Forms.DataGridView();
            this.columnSymbolname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTokens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSymbol)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(684, 461);
            this.splitContainer1.SplitterDistance = 406;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.listComments);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.listError);
            this.splitContainer3.Size = new System.Drawing.Size(406, 461);
            this.splitContainer3.SplitterDistance = 279;
            this.splitContainer3.TabIndex = 0;
            // 
            // listComments
            // 
            this.listComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listComments.GridLines = true;
            this.listComments.LabelWrap = false;
            this.listComments.Location = new System.Drawing.Point(0, 0);
            this.listComments.Name = "listComments";
            this.listComments.Size = new System.Drawing.Size(406, 279);
            this.listComments.TabIndex = 21;
            this.listComments.UseCompatibleStateImageBehavior = false;
            this.listComments.View = System.Windows.Forms.View.List;
            // 
            // listError
            // 
            this.listError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listError.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listError.ForeColor = System.Drawing.Color.Maroon;
            this.listError.GridLines = true;
            this.listError.Location = new System.Drawing.Point(0, 0);
            this.listError.Name = "listError";
            this.listError.Size = new System.Drawing.Size(406, 178);
            this.listError.TabIndex = 20;
            this.listError.UseCompatibleStateImageBehavior = false;
            this.listError.View = System.Windows.Forms.View.List;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridTokens);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataSymbol);
            this.splitContainer2.Size = new System.Drawing.Size(274, 461);
            this.splitContainer2.SplitterDistance = 279;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridTokens
            // 
            this.dataGridTokens.AllowUserToAddRows = false;
            this.dataGridTokens.AllowUserToDeleteRows = false;
            this.dataGridTokens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridTokens.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridTokens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridTokens.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridTokens.ColumnHeadersHeight = 20;
            this.dataGridTokens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colmnLexeme,
            this.colomnTokencategory});
            this.dataGridTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridTokens.Location = new System.Drawing.Point(0, 0);
            this.dataGridTokens.MultiSelect = false;
            this.dataGridTokens.Name = "dataGridTokens";
            this.dataGridTokens.ReadOnly = true;
            this.dataGridTokens.RowHeadersVisible = false;
            this.dataGridTokens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridTokens.Size = new System.Drawing.Size(274, 279);
            this.dataGridTokens.TabIndex = 19;
            // 
            // colmnLexeme
            // 
            this.colmnLexeme.HeaderText = "Token";
            this.colmnLexeme.Name = "colmnLexeme";
            this.colmnLexeme.ReadOnly = true;
            this.colmnLexeme.Width = 61;
            // 
            // colomnTokencategory
            // 
            this.colomnTokencategory.HeaderText = "Value";
            this.colomnTokencategory.Name = "colomnTokencategory";
            this.colomnTokencategory.ReadOnly = true;
            this.colomnTokencategory.Width = 58;
            // 
            // dataSymbol
            // 
            this.dataSymbol.AllowUserToAddRows = false;
            this.dataSymbol.AllowUserToDeleteRows = false;
            this.dataSymbol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataSymbol.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataSymbol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataSymbol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataSymbol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSymbolname,
            this.columnValue});
            this.dataSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataSymbol.Location = new System.Drawing.Point(0, 0);
            this.dataSymbol.MultiSelect = false;
            this.dataSymbol.Name = "dataSymbol";
            this.dataSymbol.ReadOnly = true;
            this.dataSymbol.RowHeadersVisible = false;
            this.dataSymbol.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataSymbol.Size = new System.Drawing.Size(274, 178);
            this.dataSymbol.TabIndex = 22;
            // 
            // columnSymbolname
            // 
            this.columnSymbolname.HeaderText = "Symbol Name";
            this.columnSymbolname.Name = "columnSymbolname";
            this.columnSymbolname.ReadOnly = true;
            this.columnSymbolname.Width = 96;
            // 
            // columnValue
            // 
            this.columnValue.HeaderText = "Value";
            this.columnValue.Name = "columnValue";
            this.columnValue.ReadOnly = true;
            this.columnValue.Width = 58;
            // 
            // ScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanForm";
            this.ShowIcon = false;
            this.Text = "Scanner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScanForm_FormClosed);
            this.Load += new System.EventHandler(this.ScanForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTokens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSymbol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView listComments;
        private System.Windows.Forms.ListView listError;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridTokens;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmnLexeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn colomnTokencategory;
        private System.Windows.Forms.DataGridView dataSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSymbolname;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;

    }
}
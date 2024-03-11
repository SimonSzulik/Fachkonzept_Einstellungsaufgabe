namespace Fachkonzept_Einstellungsaufgabe
{
    partial class UserControl1
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tabControl = new TabControl();
            testPage = new TabPage();
            testGrid = new DataGridView();
            prodPage = new TabPage();
            prodGrid = new DataGridView();
            diffPage = new TabPage();
            diffGrid = new DataGridView();
            filterButton = new Button();
            filterText = new TextBox();
            tabControl.SuspendLayout();
            testPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)testGrid).BeginInit();
            prodPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)prodGrid).BeginInit();
            diffPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)diffGrid).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(testPage);
            tabControl.Controls.Add(prodPage);
            tabControl.Controls.Add(diffPage);
            tabControl.Location = new Point(0, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(712, 350);
            tabControl.TabIndex = 0;
            // 
            // testPage
            // 
            testPage.Controls.Add(testGrid);
            testPage.Location = new Point(4, 24);
            testPage.Name = "testPage";
            testPage.Padding = new Padding(3);
            testPage.Size = new Size(704, 322);
            testPage.TabIndex = 0;
            testPage.Text = "Testfile";
            testPage.UseVisualStyleBackColor = true;
            // 
            // testGrid
            // 
            testGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            testGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.CadetBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            testGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            testGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            testGrid.EnableHeadersVisualStyles = false;
            testGrid.Location = new Point(3, 3);
            testGrid.Name = "testGrid";
            testGrid.Size = new Size(698, 316);
            testGrid.TabIndex = 0;
            testGrid.VirtualMode = true;
            // 
            // prodPage
            // 
            prodPage.Controls.Add(prodGrid);
            prodPage.Location = new Point(4, 24);
            prodPage.Name = "prodPage";
            prodPage.Padding = new Padding(3);
            prodPage.Size = new Size(704, 322);
            prodPage.TabIndex = 1;
            prodPage.Text = "Productive";
            prodPage.UseVisualStyleBackColor = true;
            // 
            // prodGrid
            // 
            prodGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prodGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.CadetBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            prodGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            prodGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            prodGrid.EnableHeadersVisualStyles = false;
            prodGrid.Location = new Point(3, 3);
            prodGrid.Name = "prodGrid";
            prodGrid.Size = new Size(698, 316);
            prodGrid.TabIndex = 1;
            prodGrid.VirtualMode = true;
            // 
            // diffPage
            // 
            diffPage.Controls.Add(diffGrid);
            diffPage.Location = new Point(4, 24);
            diffPage.Name = "diffPage";
            diffPage.Padding = new Padding(3);
            diffPage.Size = new Size(704, 322);
            diffPage.TabIndex = 2;
            diffPage.Text = "Difference";
            diffPage.UseVisualStyleBackColor = true;
            // 
            // diffGrid
            // 
            diffGrid.AllowUserToAddRows = false;
            diffGrid.AllowUserToDeleteRows = false;
            diffGrid.AllowUserToResizeColumns = false;
            diffGrid.AllowUserToResizeRows = false;
            diffGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            diffGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.CadetBlue;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            diffGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            diffGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            diffGrid.EnableHeadersVisualStyles = false;
            diffGrid.Location = new Point(3, 3);
            diffGrid.Name = "diffGrid";
            diffGrid.ReadOnly = true;
            diffGrid.Size = new Size(698, 316);
            diffGrid.TabIndex = 2;
            diffGrid.VirtualMode = true;
            // 
            // filterButton
            // 
            filterButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            filterButton.Location = new Point(620, 3);
            filterButton.Name = "filterButton";
            filterButton.Size = new Size(88, 23);
            filterButton.TabIndex = 1;
            filterButton.Text = "Filter";
            filterButton.UseVisualStyleBackColor = true;
            filterButton.Click += filterButtonClick;
            // 
            // filterText
            // 
            filterText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            filterText.Location = new Point(437, 3);
            filterText.Name = "filterText";
            filterText.Size = new Size(177, 23);
            filterText.TabIndex = 2;
            // 
            // UserControl1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(filterText);
            Controls.Add(filterButton);
            Controls.Add(tabControl);
            Name = "UserControl1";
            Size = new Size(715, 365);
            tabControl.ResumeLayout(false);
            testPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)testGrid).EndInit();
            prodPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)prodGrid).EndInit();
            diffPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)diffGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        internal TabPage prodPage;
        internal TabPage diffPage;
        internal TabControl tabControl;
        internal DataGridView prodGrid;
        internal Button filterButton;
        internal TextBox filterText;
        internal TabPage testPage;
        internal DataGridView testGrid;
        internal DataGridView diffGrid;
    }
}

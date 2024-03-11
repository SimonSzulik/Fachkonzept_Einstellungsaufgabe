namespace Fachkonzept_Einstellungsaufgabe
{
    partial class Screen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataControlPanel = new UserControl1();
            menu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadTestToolStripMenuItem = new ToolStripMenuItem();
            loadProductiveToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            closeAltF4ToolStripMenuItem = new ToolStripMenuItem();
            showDifferencesToolStripMenuItem = new ToolStripMenuItem();
            menu.SuspendLayout();
            SuspendLayout();
            // 
            // dataControlPanel
            // 
            dataControlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataControlPanel.Location = new Point(0, 27);
            dataControlPanel.Name = "dataControlPanel";
            dataControlPanel.Size = new Size(795, 423);
            dataControlPanel.TabIndex = 6;
            // 
            // menu
            // 
            menu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, showDifferencesToolStripMenuItem });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Size = new Size(795, 24);
            menu.TabIndex = 7;
            menu.Text = "menu";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadTestToolStripMenuItem, loadProductiveToolStripMenuItem, exportToolStripMenuItem, closeAltF4ToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadTestToolStripMenuItem
            // 
            loadTestToolStripMenuItem.Name = "loadTestToolStripMenuItem";
            loadTestToolStripMenuItem.Size = new Size(273, 22);
            loadTestToolStripMenuItem.Text = "Load Testversion";
            loadTestToolStripMenuItem.Click += loadTestToolStripMenuItem_Click;
            // 
            // loadProductiveToolStripMenuItem
            // 
            loadProductiveToolStripMenuItem.Name = "loadProductiveToolStripMenuItem";
            loadProductiveToolStripMenuItem.Size = new Size(273, 22);
            loadProductiveToolStripMenuItem.Text = "Load Productive";
            loadProductiveToolStripMenuItem.Click += loadProductiveToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(273, 22);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // closeAltF4ToolStripMenuItem
            // 
            closeAltF4ToolStripMenuItem.Name = "closeAltF4ToolStripMenuItem";
            closeAltF4ToolStripMenuItem.Size = new Size(273, 22);
            closeAltF4ToolStripMenuItem.Text = "Close                                             Alt+F4";
            closeAltF4ToolStripMenuItem.Click += closeAltF4ToolStripMenuItem_Click;
            // 
            // showDifferencesToolStripMenuItem
            // 
            showDifferencesToolStripMenuItem.Name = "showDifferencesToolStripMenuItem";
            showDifferencesToolStripMenuItem.Size = new Size(110, 20);
            showDifferencesToolStripMenuItem.Text = "Show Differences";
            showDifferencesToolStripMenuItem.Click += showDifferencesToolStripMenuItem_Click;
            // 
            // Screen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(795, 451);
            Controls.Add(dataControlPanel);
            Controls.Add(menu);
            MainMenuStrip = menu;
            Name = "Screen";
            Text = "Screen";
            menu.ResumeLayout(false);
            menu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UserControl1 dataControlPanel;
        private MenuStrip menu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadTestToolStripMenuItem;
        private ToolStripMenuItem loadProductiveToolStripMenuItem;
        private ToolStripMenuItem closeAltF4ToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem showDifferencesToolStripMenuItem;
    }
}

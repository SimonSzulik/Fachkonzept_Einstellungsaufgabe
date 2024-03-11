using ClosedXML.Excel;
using System.Data;

namespace Fachkonzept_Einstellungsaufgabe
{
    public partial class Screen : Form
    {
        public Screen()
        {
            InitializeComponent();
            dataControlPanel.testGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
            dataControlPanel.prodGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
            dataControlPanel.diffGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
        }

        private void loadFile(string filePath, DataGridView dataGridView)
        {
            try
            {
                DataTable dtInput = new DataTable();

                using (XLWorkbook workbook = new XLWorkbook(filePath))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);
                    bool bFirstRow = true;
                    foreach (IXLRow row in worksheet.Rows())
                    {
                        if (bFirstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                string columnName = cell.Value.ToString();
                                // case for special snowflakes (multiple columns with same header)
                                if (!dtInput.Columns.Contains(columnName))
                                {
                                    dtInput.Columns.Add(columnName);
                                }
                            }
                            bFirstRow = false;
                        }
                        else
                        {
                            dtInput.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                dtInput.Rows[dtInput.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }
                }
                dataGridView.DataSource = dtInput;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading the Excel file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 1)
            {
                // keep highlighting after adding filter color
                if (e.CellStyle.BackColor == ColorTranslator.FromHtml("#addbe6"))
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#71b0bf");
                }
                else
                {
                    e.CellStyle.BackColor = Color.LightGray;

                }
            }
        }

        // column clarity
        private void dataGridView_Resize(object sender, EventArgs e)
        {
            int iMaxWidth = 0;
            int iMaxVisibleColumns = 7;
            for (int i = 0; i < iMaxVisibleColumns; i++)
            {
                iMaxWidth += dataControlPanel.testGrid.Columns[i].Width;
                iMaxWidth += dataControlPanel.prodGrid.Columns[i].Width;
                iMaxWidth += dataControlPanel.diffGrid.Columns[i].Width;
            }
            dataControlPanel.testGrid.Width = iMaxWidth;
            dataControlPanel.prodGrid.Width = iMaxWidth;
            dataControlPanel.diffGrid.Width = iMaxWidth;
        }

        private void loadTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMenuClick(sender, e, dataControlPanel.testPage);
        }

        private void loadProductiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMenuClick(sender, e, dataControlPanel.prodPage);
        }

        private void loadMenuClick(object sender, EventArgs e, TabPage tabPage)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = openFileDialog.FileName;
                string extension = Path.GetExtension(filepath).ToLower();
                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm")
                {
                    tabPage.Text = Path.GetFileName(filepath.Split(".xls")[0]);
                    loadFile(openFileDialog.FileName, tabPage.Controls.OfType<DataGridView>().First());
                }
                else
                {
                    MessageBox.Show("Please select a valid Excel file.", "Invalid File Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage currentTab = dataControlPanel.tabControl.SelectedTab;
            if (currentTab != null)
            {
                DataGridView currentGrid = currentTab.Controls.OfType<DataGridView>().FirstOrDefault();
                if (currentGrid != null && currentGrid.Columns.Count != 0)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                        sfd.Title = "Save Excel File";
                        sfd.FileName = currentTab.Text + ".xlsx";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            using (var workbook = new XLWorkbook())
                            {
                                var worksheet = workbook.Worksheets.Add("Exported Data");
                                for (int i = 1; i <= currentGrid.Columns.Count; i++)
                                {
                                    worksheet.Cell(1, i).Value = currentGrid.Columns[i - 1].HeaderText;
                                }
                                for (int i = 0; i < currentGrid.Rows.Count; i++)
                                {
                                    for (int j = 0; j < currentGrid.Columns.Count; j++)
                                    {
                                        if (currentGrid.Rows[i].Cells[j].Value != null)
                                        {
                                            worksheet.Cell(i + 2, j + 1).Value = currentGrid.Rows[i].Cells[j].Value.ToString();
                                        }
                                    }
                                }
                                worksheet.Columns().AdjustToContents();
                                workbook.SaveAs(sfd.FileName);
                                MessageBox.Show($"Data successfully exported to: {sfd.FileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No table loaded into the current tab.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void closeAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}

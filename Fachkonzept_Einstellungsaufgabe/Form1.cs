using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace Fachkonzept_Einstellungsaufgabe
{
    public partial class Screen : MaterialForm
    {
        // color vars 
        private static readonly Color ColorNotFound = ColorTranslator.FromHtml("#df7674");
        private static readonly Color ColorDiffers = ColorTranslator.FromHtml("#f5edac");
        private static readonly Color ColorRowNotFound = ColorTranslator.FromHtml("#6e5e5d");

        private static readonly Color ColorNotFoundDarker = ColorTranslator.FromHtml("#d14c49");
        private static readonly Color ColorDiffersDarker = ColorTranslator.FromHtml("#eed279");
        private static readonly Color ColorRowNotFoundDarker = ColorTranslator.FromHtml("#524544");

        public Screen()
        {
            InitializeComponent();

            // material skin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // extra formatting and functions
            dataControlPanel.testGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
            dataControlPanel.prodGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
            dataControlPanel.diffGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
            dataControlPanel.diffGrid.CellDoubleClick += DiffGrid_CellDoubleClick;
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
                    foreach (IXLRow row in worksheet.RowsUsed())
                    {
                        if (bFirstRow)
                        {
                            foreach (IXLCell cell in row.CellsUsed())
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
                            foreach (IXLCell cell in row.CellsUsed())
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
                    // load to prod and diff
                    tabPage.Text = Path.GetFileName(filepath.Split(".xls")[0]);
                    loadFile(openFileDialog.FileName, tabPage.Controls.OfType<DataGridView>().First());
                    if (tabPage.Name == "prodPage")
                    {
                        loadFile(openFileDialog.FileName, dataControlPanel.diffPage.Controls.OfType<DataGridView>().First());
                    }
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

        // keep coloring
        public void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 1)
            {
                switch (e.CellStyle.BackColor.ToArgb())
                {
                    case int color when color == ColorNotFound.ToArgb():
                        e.CellStyle.BackColor = ColorNotFoundDarker;
                        break;
                    case int color when color == ColorDiffers.ToArgb():
                        e.CellStyle.BackColor = ColorDiffersDarker;
                        break;
                    case int color when color == ColorRowNotFound.ToArgb():
                        e.CellStyle.BackColor = ColorRowNotFoundDarker;
                        break;
                    default:
                        e.CellStyle.BackColor = Color.LightGray;
                        break;
                }
            }
        }

        private void showDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable prodTable = dataControlPanel.prodGrid.DataSource as DataTable;
                DataTable testTable = dataControlPanel.testGrid.DataSource as DataTable;

                // find matching rows (prodTableRow, matchingTestTableRow)
                if (prodTable != null && testTable != null)
                {
                    // reduced table
                    testTable = testTable.DefaultView.ToTable();

                    // highlight differences
                    DataTable completeTable = dataControlPanel.diffGrid.DataSource as DataTable;
                    DataTable diffTable = completeTable.DefaultView.ToTable();

                    foreach (DataRow row in diffTable.Rows)
                    {
                        DataRow checkRow = findMatchingRow(row, testTable);

                        // no row found
                        if (checkRow.Equals(row))
                        {
                            foreach (DataColumn diffColumn in diffTable.Columns)
                            {
                                dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#6e5e5d");
                                dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].ToolTipText = "This Row doesn't exist in Testversion";
                            }
                        }
                        else
                        {
                            foreach (DataColumn diffColumn in diffTable.Columns)
                            {
                                if (testTable.Columns.Contains(diffColumn.ColumnName))
                                {
                                    // column exists
                                    if (dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Value.ToString() !=
                                            dataControlPanel.testGrid.Rows[testTable.Rows.IndexOf(checkRow)].Cells[diffColumn.ColumnName].Value.ToString())
                                    {
                                        dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#f5edac");
                                        dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].ToolTipText =
                                            dataControlPanel.testGrid.Rows[testTable.Rows.IndexOf(checkRow)].Cells[diffColumn.ColumnName].Value.ToString();
                                    }
                                    else
                                    {
                                        if (dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].RowIndex % 2 == 1)
                                        {
                                            dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = Color.LightGray;
                                        }
                                        else
                                        {
                                            dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = Color.Empty;
                                        }
                                    }
                                }
                                // column doesn't exist
                                else
                                {
                                    dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#df7674");
                                    dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].ToolTipText = "This Column doesn't exist in Testversion";
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please load both tables before comparing.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Some rows can't be matched. Try reloading the filter.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public DataRow findMatchingRow(DataRow rowToMatch, DataTable dtTableToFindRow)
        {
            if (rowToMatch == null || dtTableToFindRow == null)
            {
                throw new ArgumentNullException();
            }

            var strNameToMatch = rowToMatch["name"] as string;
            var strDisplayNameToMatch = rowToMatch["display_name"] as string;

            foreach (DataRow row in dtTableToFindRow.Rows)
            {
                var strName = row["name"] as string;
                var strDisplayName = row["display_name"] as string;

                if (string.Equals(strName, strNameToMatch, StringComparison.Ordinal) ||
                    string.Equals(strDisplayName, strDisplayNameToMatch, StringComparison.Ordinal))
                {
                    return row;
                }
            }
            // no match found
            return rowToMatch;
        }

        // adapt test version 
        public void DiffGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = dataControlPanel.diffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.ToolTipText != "" && !cell.ToolTipText.Contains(" doesn't exist in Testversion"))
                {
                    cell.Value = cell.ToolTipText;
                    cell.ToolTipText = "";
                    if (cell.RowIndex % 2 == 1)
                    {
                        cell.Style.BackColor = Color.LightGray;
                    }
                    else
                    {
                        cell.Style.BackColor = Color.Empty;
                    }
                }
            }
        }

        private void loadTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMenuClick(sender, e, dataControlPanel.testPage);
        }

        private void loadProductiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMenuClick(sender, e, dataControlPanel.prodPage);
        }

        private void closeAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

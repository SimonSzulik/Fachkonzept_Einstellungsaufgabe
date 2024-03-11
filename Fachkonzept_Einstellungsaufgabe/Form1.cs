using ClosedXML.Excel;
using System.Data;

namespace Fachkonzept_Einstellungsaufgabe
{
    public partial class Screen : Form
    {
        // matching var
        Dictionary<int, int> matches = new Dictionary<int, int>();
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
                // keep highlighting after diff matches and column wasn't found
                else if (e.CellStyle.BackColor == ColorTranslator.FromHtml("#df7674"))
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#d14c49");
                }
                // keep highlighting after diff matches and cell value differs
                else if (e.CellStyle.BackColor == ColorTranslator.FromHtml("#f5edac"))
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#eed279");
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

        private void showDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable prodTable = dataControlPanel.prodGrid.DataSource as DataTable;
            DataTable testTable = dataControlPanel.testGrid.DataSource as DataTable;

            // find matching rows (prodTableRow, matchingTestTableRow)
            if (prodTable != null && testTable != null)
            {
                // highlight differences
                DataTable diffTable = dataControlPanel.diffGrid.DataSource as DataTable;

                foreach (DataRow row in diffTable.Rows)
                {
                    DataRow checkRow = findSpecialRow(row, testTable);

                    // no row found
                    if (checkRow == row)
                    {
                        foreach (DataColumn diffColumn in diffTable.Columns)
                        {
                            dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#df7674");
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
                                        dataControlPanel.testGrid.Rows[testTable.Rows.IndexOf(checkRow)].Cells[diffColumn.ColumnName].Value.ToString() )
                                {
                                    dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#f5edac");
                                }
                            }
                            // column doesn't exist
                            else
                            {
                                dataControlPanel.diffGrid.Rows[diffTable.Rows.IndexOf(row)].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#df7674");
                            }
                        }
                    }
                }


               /* for (int i = 0; i < diffTable.Rows.Count; i++)
                {
                    DataRow diffRow = diffTable.Rows[i];
                    DataRow checkRow = findSpecialRow(diffRow, testTable);
                    
                    // matching row found
                    foreach (DataColumn diffColumn in diffTable.Columns)
                    {
                        if (checkRow.Table.Columns.Contains(diffColumn.ColumnName))
                        {
                            if (!diffRow[diffColumn.ColumnName].Equals(checkRow[diffColumn.ColumnName]))
                            {
                                dataControlPanel.diffGrid.Rows[i].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#f5edac");
                            }
                        }
                        else
                        {
                            dataControlPanel.diffGrid.Rows[i].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#df7674");
                        }
                    }
                }*/
            }
            else
            {
                MessageBox.Show("Please load both tables before comparing.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // find special row
        public DataRow findSpecialRow(DataRow rowToMatch, DataTable dtTableToFindRow)
        {
            // search for matching row
            string strNameToMatch = rowToMatch["name"].ToString();
            string strDisplayNameToMatch = rowToMatch["display_name"].ToString();

            foreach (DataRow row in dtTableToFindRow.Rows)
            {
                string strName = row["name"].ToString();
                string strDisplayName = row["display_name"].ToString();

                if (strName == strNameToMatch || strDisplayName == strDisplayNameToMatch)
                {
                    return row;
                }
            }
            // no row found
            return rowToMatch;
        }

        /*private void showDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> matching = new Dictionary<int, int>();
            DataTable prodTable = dataControlPanel.prodGrid.DataSource as DataTable;
            DataTable testTable = dataControlPanel.testGrid.DataSource as DataTable;

            // find matching rows (prodTableRow, matchingTestTableRow)
            if (prodTable != null && testTable != null)
            {
                // create matching
                if (matches.Count == 0)
                {
                    foreach (DataRow prodRow in prodTable.Rows)
                    {
                        DataRow mostSimilarRow = findMostSimilarRow(prodRow, testTable);
                        matches.Add(prodTable.Rows.IndexOf(prodRow), testTable.Rows.IndexOf(mostSimilarRow));
                    }
                }

                // highlight differences
                DataTable diffTable = dataControlPanel.diffGrid.DataSource as DataTable;

                for (int i = 0; i < diffTable.Rows.Count; i++)
                {
                    DataRow diffRow = diffTable.Rows[i];
                    DataRow checkRow = testTable.Rows[i];

                    foreach (DataColumn diffColumn in diffTable.Columns)
                    {
                        if (testTable.Columns.Contains(diffColumn.ColumnName))
                        {
                            if (dataControlPanel.diffGrid.Rows[i].Cells[diffColumn.ColumnName].Value.ToString() !=
                                dataControlPanel.testGrid.Rows[matches[i]].Cells[diffColumn.ColumnName].Value.ToString())
                            {
                                dataControlPanel.diffGrid.Rows[i].Cells[diffColumn.ColumnName].Value !=
                                    dataControlPanel.testGrid.Rows[matches[i]].Cells[diffColumn.ColumnName].Value.ToString();
                                dataControlPanel.diffGrid.Rows[i].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#f5edac");
                            }
                        }
                        else
                        {
                            dataControlPanel.diffGrid.Rows[i].Cells[diffColumn.ColumnName].Style.BackColor = ColorTranslator.FromHtml("#df7674");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please load both tables before comparing.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // row similarity
        public DataRow findMostSimilarRow(DataRow rowToMatch, DataTable dtTableToFindRow)
        {
            DataRow mostSimilarRow = null;
            int lowestDistance = int.MaxValue;

            // search for similarity in columns "name" and "display_name"
            string strNameToMatch = rowToMatch["name"].ToString();
            string strDisplayNameToMatch = rowToMatch["display_name"].ToString();

            foreach (DataRow row in dtTableToFindRow.Rows)
            {
                string strName = row["name"].ToString();
                string strDisplayName = row["display_name"].ToString();

                int distance = calculateLevenshteinDistance(strNameToMatch, strName)
                               + calculateLevenshteinDistance(strDisplayNameToMatch, strDisplayName);

                if (distance < lowestDistance)
                {
                    lowestDistance = distance;
                    mostSimilarRow = row;
                }
            }
            return mostSimilarRow;
        }

        // word similarity
        public int calculateLevenshteinDistance(string s1, string s2)
        {
            int n = s1.Length;
            int m = s2.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
                return m;
            if (m == 0)
                return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }*/

        private void closeAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

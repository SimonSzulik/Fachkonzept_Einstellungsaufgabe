using System.Data;

namespace Fachkonzept_Einstellungsaufgabe
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void filterButtonClick(object sender, EventArgs e)
        {
            string filterString = filterText.Text;

            TabPage currentTab = tabControl.SelectedTab;
            if (currentTab != null)
            {
                DataGridView currentGrid = currentTab.Controls.OfType<DataGridView>().FirstOrDefault();
                if (currentGrid != null && currentGrid.DataSource is DataTable dataTable)
                {
                    DataView dataView = dataTable.DefaultView;
                    var filterExpressions = dataView.Table.Columns.Cast<DataColumn>().Select(col => $"Convert([{col.ColumnName}], 'System.String') LIKE '%{filterString}%'");

                    string resetString = string.Join(" OR ", filterExpressions);
                    dataView.RowFilter = resetString;

                    if (!string.IsNullOrWhiteSpace(filterString))
                    {
                        higlightFilterCells(currentGrid, filterString);
                    }
                }
            }
        }
        private void higlightFilterCells(DataGridView dataGridView, string filterString)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Contains(filterString))
                    {
                        cell.Style.BackColor = ColorTranslator.FromHtml("#addbe6");
                    }
                    else
                    {
                        cell.Style.BackColor = dataGridView.DefaultCellStyle.BackColor;
                    }
                }
            }
        }

        private void prodGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

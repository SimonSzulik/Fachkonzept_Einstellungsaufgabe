using System.Data;
using System.Windows.Forms;

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
            string filterString = filterText.Text.Trim();

            TabPage currentTab = tabControl.SelectedTab;
            if (currentTab != null)
            {
                DataGridView currentGrid = currentTab.Controls.OfType<DataGridView>().FirstOrDefault();
                if (currentGrid != null && currentGrid.DataSource is DataTable dataTable)
                {
                    DataView dataView = dataTable.DefaultView;

                    if (!string.IsNullOrWhiteSpace(filterString))
                    {
                        try
                        {
                            var pairs = filterString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            var filters = new List<string>();

                            foreach (var pair in pairs)
                            {
                                var parts = pair.Split(new[] { '=' }, 2).Select(part => part.Trim().Trim('\'', '\"')).ToArray();
                                if (parts.Length == 2)
                                {
                                    string columnName = parts[0];
                                    string value = parts[1];

                                    if (!dataTable.Columns.Contains(columnName))
                                    {
                                        // cancel computation if column not found
                                        MessageBox.Show($"Column name '{columnName}' does not exist. Please check for typos.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return; 
                                    }

                                    filters.Add($"{columnName} = '{value}'");
                                }
                                else
                                {
                                    MessageBox.Show("Invalid filter format. Use 'ColumnName = Value'.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            // combining filters
                            string combinedFilter = string.Join(" AND ", filters);
                            dataView.RowFilter = combinedFilter;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error applying filter: {ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // no filter = show whole table
                        dataView.RowFilter = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("No table loaded into the current tab.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

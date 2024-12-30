using System.Data;
using System.Windows.Forms;

namespace LSP_AqillaShahbaniM
{
    public static class DataHelper
    {
        public static void SearchFilter(DataTable dt, string columnName, string searchText, DataGridView dataGridView)
        {
            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("{0} LIKE '%{1}%'", columnName, searchText);
                dataGridView.DataSource = dv;
            }
        }

        public static void SearchFilter(DataTable dt, string filterExpression, DataGridView dataGridView)
        {
            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = filterExpression;
                dataGridView.DataSource = dv;
            }
        }

        public static void fillDataGrid(DataGridView dataGridView)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}
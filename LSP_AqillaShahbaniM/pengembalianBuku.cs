using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LSP_AqillaShahbaniM
{
    public partial class pengembalianBuku : Form
    {
        string sqlQuery;
        DataTable dtPeminjaman = new DataTable();
        private NavigationHelper navigationHelper;

        public pengembalianBuku()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void pengembalianBuku_Load(object sender, EventArgs e)
        {
            sqlQuery = "SELECT P.ID_PEMINJAMAN as 'ID_PEMINJAMAN', C.NAME_CUSTOMER AS 'Nama_Member', P.TANGGAL_PEMINJAMAN AS 'Tanggal Pinjam', P.TANGGAL_PENGEMBALIAN AS 'Tanggal Kembali' FROM PEMINJAMAN P JOIN CUSTOMER C ON P.ID_CUSTOMER = C.ID_CUSTOMER WHERE P.STATUS_PEMINJAMAN = 1;";
            dtPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridPilihPinjaman.DataSource = dtPeminjaman;
            DataHelper.fillDataGrid(dataGridPilihPinjaman);
        }

        private void buttonDetailPeminjaman_Click(object sender, EventArgs e)
        {
            if (dataGridPilihPinjaman.SelectedRows.Count > 0) 
            {
                string idPeminjaman = dataGridPilihPinjaman.SelectedRows[0].Cells["ID_PEMINJAMAN"].Value.ToString();

                navigationHelper.NavigateToDetailPengembalian(this, idPeminjaman);
            }
            else
            {
                MessageBox.Show("Silakan pilih buku dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxSearchPinjamanBuku_TextChanged(object sender, EventArgs e)
        {
            DataHelper.SearchFilter(dtPeminjaman, "Nama_Member", textBoxSearchPinjamanBuku.Text, dataGridPilihPinjaman);
        }

        private void buttonDaftarBuku_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new daftarBukuView());
        }

        private void buttonDaftarAnggota_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new daftarMemberView());
        }

        private void buttonRiwayatPeminjaman_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new historyPeminjaman());
        }

        private void buttonPeminjaman_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new mainMenu());
        }
    }
}

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
    public partial class historyPeminjaman : Form
    {
        string sqlQuery;
        DataTable dtPeminjaman = new DataTable();
        private NavigationHelper navigationHelper;

        public historyPeminjaman()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void historyPeminjaman_Load(object sender, EventArgs e)
        {
            sqlQuery = "SELECT P.ID_PEMINJAMAN as 'ID_PEMINJAMAN', C.NAME_CUSTOMER AS 'Nama_Member', P.TANGGAL_PEMINJAMAN AS 'Tanggal Pinjam', P.TANGGAL_PENGEMBALIAN AS 'Tanggal Kembali', CASE WHEN P.STATUS_PEMINJAMAN = 0 THEN 'Sudah Kembali' WHEN P.STATUS_PEMINJAMAN = 1 THEN 'Belum Kembali' ELSE 'Status Tidak Diketahui' END AS 'Status Peminjaman' FROM PEMINJAMAN P JOIN CUSTOMER C ON P.ID_CUSTOMER = C.ID_CUSTOMER;";
            dtPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridPilihPinjaman.DataSource = dtPeminjaman;
            DataHelper.fillDataGrid(dataGridPilihPinjaman);
        }

        private void textBoxSearchPinjamanBuku_TextChanged(object sender, EventArgs e)
        {
            DataHelper.SearchFilter(dtPeminjaman, "Nama_Member", textBoxSearchPinjamanBuku.Text, dataGridPilihPinjaman);
        
        }

        private void buttonDetailPeminjaman_Click(object sender, EventArgs e)
        {
            if (dataGridPilihPinjaman.SelectedRows.Count > 0)
            {
                string idPeminjaman = dataGridPilihPinjaman.SelectedRows[0].Cells["ID_PEMINJAMAN"].Value.ToString();

                detailHistoryPeminjaman formDetailHistoryPeminjaman = new detailHistoryPeminjaman(idPeminjaman);
                formDetailHistoryPeminjaman.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Silakan pilih id peminjaman dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonPengembalian_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new pengembalianBuku());
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

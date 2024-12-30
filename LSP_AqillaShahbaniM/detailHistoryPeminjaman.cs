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
    public partial class detailHistoryPeminjaman : Form
    {
        private string idPeminjaman;
        string sqlQuery;
        DataTable dtDetailPeminjaman = new DataTable();
        DataTable dtDataDiri = new DataTable();
        DataTable dtPeminjaman = new DataTable();
        private NavigationHelper navigationHelper;

        public detailHistoryPeminjaman(string idPeminjaman)
        {
            InitializeComponent();
            this.idPeminjaman = idPeminjaman;
            navigationHelper = new NavigationHelper();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new historyPeminjaman());
        }

        private void detailHistoryPeminjaman_Load(object sender, EventArgs e)
        {
            sqlQuery = "SELECT C.NAME_CUSTOMER AS Nama, C.ADDRESS AS Alamat, C.PHONENUMBER AS Nomor_Handphone FROM PEMINJAMAN P JOIN CUSTOMER C ON P.ID_CUSTOMER = C.ID_CUSTOMER WHERE P.ID_PEMINJAMAN = '" + idPeminjaman + "';";
            dtDataDiri = DatabaseHelper.ExecuteQuery(sqlQuery);
            string nama = dtDataDiri.Rows[0][0].ToString();
            string alamat = dtDataDiri.Rows[0][1].ToString();
            string nohp = dtDataDiri.Rows[0][2].ToString();

            sqlQuery = "SELECT P.TANGGAL_PEMINJAMAN AS 'Tanggal Pinjam', P.TANGGAL_PENGEMBALIAN AS 'Tanggal Kembali', CASE WHEN P.STATUS_PEMINJAMAN = 0 THEN 'Sudah Kembali' WHEN P.STATUS_PEMINJAMAN = 1 THEN 'Belum Kembali' ELSE 'Status Tidak Diketahui' END AS 'Status Peminjaman' FROM PEMINJAMAN P WHERE P.ID_PEMINJAMAN = '" + idPeminjaman + "';";
            dtPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            string tanggalPinjam = dtPeminjaman.Rows[0][0].ToString();
            string tanggalKembali = dtPeminjaman.Rows[0][1].ToString();
            string statusPeminjaman = dtPeminjaman.Rows[0][2].ToString();

            labelDataDiri.Text = $": {nama}\n: {alamat}\n: {nohp}\n: {tanggalPinjam}\n: {tanggalKembali}\n: {statusPeminjaman}";

            sqlQuery = "SELECT B.TITLE AS 'BUKU YANG DIPINJAM' FROM BOOK_PEMINJAMAN BP JOIN BOOK B ON BP.ID_BOOK = B.ID_BOOK WHERE BP.ID_PEMINJAMAN = '" + idPeminjaman + "';";
            dtDetailPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridDaftarBuku.DataSource = dtDetailPeminjaman;
            DataHelper.fillDataGrid(dataGridDaftarBuku);
        }

        private void buttonSelesai_Click(object sender, EventArgs e)
        {

            navigationHelper.NavigateToFormClose(this, new historyPeminjaman());
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

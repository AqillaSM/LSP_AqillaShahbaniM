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
    public partial class detailPengembalianBuku : Form
    {
        private string idPeminjaman;
        string sqlQuery;
        DataTable dtDetailPeminjaman = new DataTable();
        DataTable dtDataDiri = new DataTable();
        private NavigationHelper navigationHelper;
        public detailPengembalianBuku(string idPeminjaman)
        {
            InitializeComponent();
            this.idPeminjaman = idPeminjaman;
            navigationHelper = new NavigationHelper();
        }

        private void detailPengembalianBuku_Load(object sender, EventArgs e)
        {
            sqlQuery = "SELECT C.NAME_CUSTOMER AS Nama, C.ADDRESS AS Alamat, C.PHONENUMBER AS Nomor_Handphone FROM PEMINJAMAN P JOIN CUSTOMER C ON P.ID_CUSTOMER = C.ID_CUSTOMER WHERE P.ID_PEMINJAMAN = '" + idPeminjaman + "';";
            dtDataDiri = DatabaseHelper.ExecuteQuery(sqlQuery);
            string nama = dtDataDiri.Rows[0][0].ToString();
            string alamat = dtDataDiri.Rows[0][1].ToString();
            string nohp = dtDataDiri.Rows[0][2].ToString();

            labelDataDiri.Text = $": {nama}\n: {alamat}\n: {nohp}";

            sqlQuery = "SELECT B.TITLE AS 'BUKU YANG DIPINJAM' FROM BOOK_PEMINJAMAN BP JOIN BOOK B ON BP.ID_BOOK = B.ID_BOOK WHERE BP.ID_PEMINJAMAN = '" + idPeminjaman + "';";
            dtDetailPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridDaftarBuku.DataSource = dtDetailPeminjaman;
            DataHelper.fillDataGrid(dataGridDaftarBuku);
        }

        private void buttonSelesai_Click(object sender, EventArgs e)
        {
            sqlQuery = "UPDATE PEMINJAMAN SET STATUS_PEMINJAMAN = 0 WHERE ID_PEMINJAMAN = '"+ idPeminjaman +"'; ";
            DatabaseHelper.ExecuteQuery(sqlQuery);

            sqlQuery = "UPDATE BOOK SET STOK = STOK + 1 WHERE ID_BOOK IN(SELECT ID_BOOK FROM BOOK_PEMINJAMAN WHERE ID_PEMINJAMAN = '" + idPeminjaman + "');";
            DatabaseHelper.ExecuteQuery(sqlQuery);

            try
            {
                string filePath = "history_peminjaman.txt";

                string content = $"Nama: {dtDataDiri.Rows[0][0]}\n" +
                                 $"Alamat: {dtDataDiri.Rows[0][1]}\n" +
                                 $"Nomor Handphone: {dtDataDiri.Rows[0][2]}\n" +
                                 $"Status Peminjaman: Sudah dikembalikan";

                System.IO.File.WriteAllText(filePath, content);

                MessageBox.Show("Data berhasil disimpan ke file!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menyimpan data: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string filePath = "history_peminjaman.txt";

                if (System.IO.File.Exists(filePath))
                {
                    string content = System.IO.File.ReadAllText(filePath);
                    MessageBox.Show(content, "Data dari File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("File tidak ditemukan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat membaca data: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            navigationHelper.NavigateToFormClose(this, new pengembalianBuku());
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new pengembalianBuku());
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

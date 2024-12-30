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
    public partial class pinjamBukuPilihBuku : Form
    {
        string sqlQuery;
        DataTable dtBukuPerpustakaan = new DataTable();
        DataTable dtBukuPilihan = new DataTable();
        DataTable dtCustomer = new DataTable();
        string idPeminjaman;
        DataTable dtPeminjaman = new DataTable();
        List<string> selectedBooks = new List<string>();
        private NavigationHelper navigationHelper;

        public pinjamBukuPilihBuku()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            clearPeminjaman();

            navigationHelper.NavigateToFormClose(this, new pinjamBukuPilihMember());
        }

        private void buttonSubmitBuku_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new mainMenu());
        }

        private void pinjamBukuPilihBuku_Load(object sender, EventArgs e)
        {
            sqlQuery = "select ID_BOOK, TITLE as 'Judul_Buku', AUTHOR as 'Penulis' from BOOK where STOK > 0;";
            dtBukuPerpustakaan = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridDaftarBukuPerpus.DataSource = dtBukuPerpustakaan;
            DataHelper.fillDataGrid(dataGridDaftarBukuPerpus);

            sqlQuery = "SELECT C.NAME_CUSTOMER FROM PEMINJAMAN P JOIN CUSTOMER C ON P.ID_CUSTOMER = C.ID_CUSTOMER ORDER BY P.ID_PEMINJAMAN DESC LIMIT 1;";
            dtCustomer = DatabaseHelper.ExecuteQuery(sqlQuery);
            labelNamaAnggota.Text = dtCustomer.Rows[0][0].ToString();

            sqlQuery = "SELECT ID_PEMINJAMAN FROM PEMINJAMAN ORDER BY id_peminjaman DESC LIMIT 1;";
            dtPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            idPeminjaman = dtPeminjaman.Rows[0][0].ToString();

            sqlQuery = "SELECT  B.TITLE AS 'Judul_Buku', B.AUTHOR AS 'Penulis' FROM  BOOK_PEMINJAMAN BP JOIN  BOOK B ON BP.ID_BOOK = B.ID_BOOK JOIN PEMINJAMAN P ON BP.ID_PEMINJAMAN = P.ID_PEMINJAMAN WHERE BP.ID_PEMINJAMAN = '" + idPeminjaman + "';";
            dtBukuPilihan = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridDaftarBukuPilihan.DataSource = dtBukuPilihan;
            DataHelper.fillDataGrid(dataGridDaftarBukuPilihan);
        }

        private void textBoxCariBuku_TextChanged(object sender, EventArgs e)
        {
            DataHelper.SearchFilter(dtBukuPerpustakaan, "Judul_Buku", textBoxCariBuku.Text, dataGridDaftarBukuPerpus);
        }

        private void buttonTambahBuku_Click(object sender, EventArgs e)
        {
            sqlQuery = "SELECT ID_PEMINJAMAN FROM PEMINJAMAN ORDER BY id_peminjaman DESC LIMIT 1;";
            dtPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            idPeminjaman = dtPeminjaman.Rows[0][0].ToString();

            if (dataGridDaftarBukuPerpus.SelectedRows.Count > 0)
            {
                string idBook = dataGridDaftarBukuPerpus.SelectedRows[0].Cells["ID_BOOK"].Value.ToString();

                if (selectedBooks.Contains(idBook))
                {
                    MessageBox.Show("Buku ini sudah ditambahkan ke peminjaman!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tambahkan ID buku ke array
                selectedBooks.Add(idBook);

                // Operasi database tetap dijalankan
                sqlQuery = "INSERT INTO BOOK_PEMINJAMAN (ID_BOOK, ID_PEMINJAMAN) VALUES ('" + idBook + "', '" + idPeminjaman + "');";
                DatabaseHelper.ExecuteQuery(sqlQuery);

                sqlQuery = "UPDATE BOOK SET STOK = STOK - 1 WHERE ID_BOOK = '" + idBook + "';";
                DatabaseHelper.ExecuteQuery(sqlQuery);

                // Refresh data
                dtBukuPilihan.Clear();
                sqlQuery = "SELECT B.TITLE AS 'Judul_Buku', B.AUTHOR AS 'Penulis' FROM BOOK_PEMINJAMAN BP JOIN BOOK B ON BP.ID_BOOK = B.ID_BOOK WHERE BP.ID_PEMINJAMAN = '" + idPeminjaman + "';";
                dtBukuPilihan = DatabaseHelper.ExecuteQuery(sqlQuery);
                dataGridDaftarBukuPilihan.DataSource = dtBukuPilihan;
                DataHelper.fillDataGrid(dataGridDaftarBukuPilihan);

                dtBukuPerpustakaan.Clear();
                sqlQuery = "SELECT ID_BOOK, TITLE AS 'Judul_Buku', AUTHOR AS 'Penulis' FROM BOOK WHERE STOK > 0;";
                dtBukuPerpustakaan = DatabaseHelper.ExecuteQuery(sqlQuery);
                dataGridDaftarBukuPerpus.DataSource = dtBukuPerpustakaan;
                DataHelper.fillDataGrid(dataGridDaftarBukuPerpus);
            }
            else
            {
                MessageBox.Show("Silakan pilih buku dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void buttonHapusBuku_Click(object sender, EventArgs e)
        {
            if (dataGridDaftarBukuPilihan.SelectedRows.Count > 0)
            {
                string judulBuku = dataGridDaftarBukuPilihan.SelectedRows[0].Cells["Judul_Buku"].Value.ToString();

                sqlQuery = "DELETE BP FROM BOOK_PEMINJAMAN BP JOIN BOOK B ON BP.ID_BOOK = B.ID_BOOK WHERE B.TITLE = '" + judulBuku + "' AND BP.ID_PEMINJAMAN = '" + idPeminjaman + "';";
                DatabaseHelper.ExecuteQuery(sqlQuery);

                sqlQuery = "UPDATE BOOK SET STOK = STOK + 1 WHERE title = '" + judulBuku + "';";
                DatabaseHelper.ExecuteQuery(sqlQuery);

                dtBukuPilihan.Clear();

                sqlQuery = "SELECT  B.TITLE AS 'Judul_Buku', B.AUTHOR AS 'Penulis' FROM  BOOK_PEMINJAMAN BP JOIN  BOOK B ON BP.ID_BOOK = B.ID_BOOK JOIN PEMINJAMAN P ON BP.ID_PEMINJAMAN = P.ID_PEMINJAMAN WHERE BP.ID_PEMINJAMAN = '" + idPeminjaman + "';";
                dtBukuPilihan = DatabaseHelper.ExecuteQuery(sqlQuery);
                dataGridDaftarBukuPilihan.DataSource = dtBukuPilihan;
                DataHelper.fillDataGrid(dataGridDaftarBukuPilihan);

                dtBukuPerpustakaan.Clear();

                sqlQuery = "select ID_BOOK, TITLE as 'Judul_Buku', AUTHOR as 'Penulis' from BOOK where STOK > 0;";
                dtBukuPerpustakaan = DatabaseHelper.ExecuteQuery(sqlQuery);
                dataGridDaftarBukuPerpus.DataSource = dtBukuPerpustakaan;
                DataHelper.fillDataGrid(dataGridDaftarBukuPerpus);
            }
            else
            {
                MessageBox.Show("Silakan pilih buku dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonPengembalian_Click(object sender, EventArgs e)
        {
            clearPeminjaman();

            navigationHelper.NavigateToFormClose(this, new pengembalianBuku());
        }

        private void buttonDaftarBuku_Click(object sender, EventArgs e)
        {
            clearPeminjaman();

            navigationHelper.NavigateToFormClose(this, new daftarBukuView());
        }

        private void buttonDaftarAnggota_Click(object sender, EventArgs e)
        {
            clearPeminjaman();

            navigationHelper.NavigateToFormClose(this, new daftarMemberView());
        }

        private void buttonRiwayatPeminjaman_Click(object sender, EventArgs e)
        {
            clearPeminjaman();

            navigationHelper.NavigateToFormClose(this, new historyPeminjaman());
        }

        private void clearPeminjaman()
        {
            sqlQuery = "SELECT ID_PEMINJAMAN FROM PEMINJAMAN ORDER BY id_peminjaman DESC LIMIT 1;";
            dtPeminjaman = DatabaseHelper.ExecuteQuery(sqlQuery);
            idPeminjaman = dtPeminjaman.Rows[0][0].ToString();


            sqlQuery = "UPDATE BOOK SET STOK = STOK + 1 WHERE ID_BOOK IN ( SELECT ID_BOOK FROM BOOK_PEMINJAMAN WHERE ID_PEMINJAMAN = '" + idPeminjaman + "' );";
            DatabaseHelper.ExecuteQuery(sqlQuery);

            sqlQuery = "DELETE FROM BOOK_PEMINJAMAN WHERE ID_PEMINJAMAN = '" + idPeminjaman + "';";
            DatabaseHelper.ExecuteQuery(sqlQuery);

            sqlQuery = "DELETE FROM PEMINJAMAN WHERE ID_PEMINJAMAN = '" + idPeminjaman + "'; ";
            DatabaseHelper.ExecuteQuery(sqlQuery);
        }

        private void buttonPeminjaman_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new mainMenu());
        }
    }
}

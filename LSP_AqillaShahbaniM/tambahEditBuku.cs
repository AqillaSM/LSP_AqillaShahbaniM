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
    public partial class tambahEditBuku : Form
    {
        string sqlQuery;
        DataTable dtBukuEdit = new DataTable();
        DataTable dtBukuTambah = new DataTable();
        private string idBuku;
        private NavigationHelper navigationHelper;

        public tambahEditBuku(string idBuku)
        {
            InitializeComponent();
            this.idBuku = idBuku;
            navigationHelper = new NavigationHelper();
        }

        private void tambahEditBuku_Load(object sender, EventArgs e)
        {
            if (idBuku != "")
            {
                sqlQuery = "SELECT TITLE, AUTHOR, STOK FROM BOOK WHERE ID_BOOK = '" + idBuku + "';";
                dtBukuTambah = DatabaseHelper.ExecuteQuery(sqlQuery);
                textBoxJudul.Text = dtBukuTambah.Rows[0][0].ToString();
                textBoxPenulis.Text = dtBukuTambah.Rows[0][1].ToString();
                textBoxStok.Text = dtBukuTambah.Rows[0][2].ToString();

                buttonTambahBuku.Hide();
                buttonEditBuku.Show();
            }
            else
            {
                buttonTambahBuku.Show();
                buttonEditBuku.Hide();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new daftarBukuView());
        }

        private void buttonEditBuku_Click(object sender, EventArgs e)
        {
            string judul = textBoxJudul.Text.Trim();
            string penulis = textBoxPenulis.Text.Trim();
            string stok = textBoxStok.Text.Trim();

            // Validasi input
            if (string.IsNullOrEmpty(judul) || string.IsNullOrEmpty(penulis) || string.IsNullOrEmpty(stok))
            {
                MessageBox.Show("Semua kolom harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            sqlQuery = "UPDATE BOOK SET TITLE = '" + textBoxJudul.Text + "', AUTHOR = '" + textBoxPenulis.Text + "', STOK = '" + textBoxStok.Text + "' WHERE ID_BOOK = '" + idBuku + "';";
            DatabaseHelper.ExecuteQuery(sqlQuery);

            navigationHelper.NavigateToFormClose(this, new daftarBukuView());
        }

        private void buttonTambahBuku_Click(object sender, EventArgs e)
        {
            string judul = textBoxJudul.Text.Trim();
            string penulis = textBoxPenulis.Text.Trim();
            string stok = textBoxStok.Text.Trim();

            if (string.IsNullOrEmpty(judul) || string.IsNullOrEmpty(penulis) || string.IsNullOrEmpty(stok))
            {
                MessageBox.Show("Semua kolom harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sqlQuery = "INSERT INTO BOOK (TITLE, AUTHOR, STOK) VALUES ('" + textBoxJudul.Text + "', '" + textBoxPenulis.Text + "', '" + textBoxStok.Text + "'); ";
            DatabaseHelper.ExecuteQuery(sqlQuery);

            navigationHelper.NavigateToFormClose(this, new daftarBukuView());

        }

        private void textBoxStok_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
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

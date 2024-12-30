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
    public partial class pinjamBukuAddMember : Form
    {
        private NavigationHelper navigationHelper;

        public pinjamBukuAddMember()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new mainMenu());
        }

        private void buttonTambahAnggota_Click(object sender, EventArgs e)
        {
            string nama = textBoxNama.Text.Trim();
            string alamat = textBoxAlamat.Text.Trim();
            string noHp = textBoxNoHP.Text.Trim();

            if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(alamat) || string.IsNullOrEmpty(noHp))
            {
                MessageBox.Show("Semua kolom harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string insertCustomerQuery = $"INSERT INTO CUSTOMER (NAME_CUSTOMER, ADDRESS, PHONENUMBER) VALUES ('{nama}', '{alamat}', '{noHp}');";
            DatabaseHelper.ExecuteNonQuery(insertCustomerQuery);

            string getLastCustomerIdQuery = "SELECT ID_CUSTOMER FROM CUSTOMER ORDER BY ID_CUSTOMER DESC LIMIT 1;";
            DataTable dtCustomer = DatabaseHelper.ExecuteQuery(getLastCustomerIdQuery);
            string idCustomer = dtCustomer.Rows[0][0].ToString();

            string insertPeminjamanQuery = $"INSERT INTO PEMINJAMAN (ID_CUSTOMER) VALUES ('{idCustomer}');";
            DatabaseHelper.ExecuteNonQuery(insertPeminjamanQuery);

            navigationHelper.NavigateToFormClose(this, new pinjamBukuPilihBuku());
        }
        private void pinjamBukuAddMember_Load(object sender, EventArgs e)
        {

        }

        private void textBoxNoHP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            TextBox textBox = sender as TextBox;

            if (textBox != null && textBox.Text.Length >= 13 && !char.IsControl(e.KeyChar))
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

        private void textBoxNoHP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
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
    public partial class tambahEditMember : Form
    {
        string sqlQuery;
        DataTable dtCustomerTambah = new DataTable();
        private string idCustomer;
        private NavigationHelper navigationHelper;

        public tambahEditMember(string idCustomer)
        {
            InitializeComponent();
            this.idCustomer = idCustomer;
            navigationHelper = new NavigationHelper();
        }

        private void tambahEditMember_Load(object sender, EventArgs e)
        {
            if (idCustomer != "")
            {
                

                sqlQuery = "SELECT NAME_CUSTOMER AS Nama, ADDRESS AS Alamat, PHONENUMBER AS Nomor_Handphone FROM CUSTOMER WHERE ID_CUSTOMER = '" + idCustomer + "';";
                dtCustomerTambah = DatabaseHelper.ExecuteQuery(sqlQuery);
                textBoxNama.Text = dtCustomerTambah.Rows[0][0].ToString();
                textBoxAlamat.Text = dtCustomerTambah.Rows[0][1].ToString();
                textBoxNoHP.Text = dtCustomerTambah.Rows[0][2].ToString();

                buttonDaftarAnggota.Hide();
            }
            else
            {
                buttonEditAnggota.Hide();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new daftarMemberView());
        }

        private void buttonTambahAnggota_Click(object sender, EventArgs e)
        {

            string nama = textBoxNama.Text.Trim();
            string alamat = textBoxAlamat.Text.Trim();
            string noHp = textBoxNoHP.Text.Trim();

            // Validasi input
            if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(alamat) || string.IsNullOrEmpty(noHp))
            {
                MessageBox.Show("Semua kolom harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sqlQuery = "INSERT INTO CUSTOMER (NAME_CUSTOMER, ADDRESS, PHONENUMBER) VALUES ('" + textBoxNama.Text + "', '" + textBoxAlamat.Text + "', '" + textBoxNoHP.Text + "'); ";
            DatabaseHelper.ExecuteQuery(sqlQuery);


            navigationHelper.NavigateToFormClose(this, new daftarMemberView());
        }

        private void buttonEditAnggota_Click(object sender, EventArgs e)
        {
            string nama = textBoxNama.Text.Trim();
            string alamat = textBoxAlamat.Text.Trim();
            string noHp = textBoxNoHP.Text.Trim();

            // Validasi input
            if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(alamat) || string.IsNullOrEmpty(noHp))
            {
                MessageBox.Show("Semua kolom harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sqlQuery = "UPDATE CUSTOMER SET NAME_CUSTOMER = '" + textBoxNama.Text + "', ADDRESS = '" + textBoxAlamat.Text + "', PHONENUMBER = '" + textBoxNoHP.Text + "' WHERE ID_CUSTOMER = '" + idCustomer + "';";
            DatabaseHelper.ExecuteQuery(sqlQuery);

            navigationHelper.NavigateToFormClose(this, new daftarMemberView());
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

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
    public partial class pinjamBukuPilihMember : Form
    {
        DataTable dtMember = new DataTable();
        private NavigationHelper navigationHelper;
        public pinjamBukuPilihMember()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void pinjamBukuPilihMember_Load(object sender, EventArgs e)
        {
            string sqlQuery = "select ID_CUSTOMER, NAME_CUSTOMER as 'Nama', ADDRESS as 'Alamat', PHONENUMBER as 'No HP' from CUSTOMER;";
            dtMember= DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridPilihMember.DataSource = dtMember;
            DataHelper.fillDataGrid(dataGridPilihMember);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToFormClose(this, new mainMenu());
        }

        private void buttonPilihAnggota_Click(object sender, EventArgs e)
        {
            if (dataGridPilihMember.SelectedRows.Count > 0) // Pastikan ada baris yang dipilih
            {
                string idCustomer = dataGridPilihMember.SelectedRows[0].Cells["ID_CUSTOMER"].Value.ToString();

                string sqlQuery = $"INSERT INTO PEMINJAMAN (ID_CUSTOMER) VALUES ('{idCustomer}');";
                DatabaseHelper.ExecuteQuery(sqlQuery);

                navigationHelper.NavigateToFormClose(this, new pinjamBukuPilihBuku());
            }
            else
            {
                MessageBox.Show("Silakan pilih anggota dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxSearchAnggota_TextChanged(object sender, EventArgs e)
        {
            DataHelper.SearchFilter(dtMember, "Nama", textBoxSearchAnggota.Text, dataGridPilihMember);
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

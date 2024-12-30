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
    public partial class daftarMemberView : Form
    {
        string sqlQuery;
        DataTable dtMember = new DataTable();
        private NavigationHelper navigationHelper;

        public daftarMemberView()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void daftarMemberView_Load(object sender, EventArgs e)
        {
            sqlQuery = "select ID_CUSTOMER, NAME_CUSTOMER as 'Nama', ADDRESS as 'Alamat', PHONENUMBER as 'No HP' from CUSTOMER where DELETE_CUSTOMER = 0;";
            dtMember = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridPilihMember.DataSource = dtMember;
            DataHelper.fillDataGrid(dataGridPilihMember);
        }

        private void textBoxSearchAnggota_TextChanged(object sender, EventArgs e)
        {
            DataHelper.SearchFilter(dtMember, "Nama", textBoxSearchAnggota.Text, dataGridPilihMember);
        }

        private void buttonHapusAnggota_Click(object sender, EventArgs e)
        {
            if (dataGridPilihMember.SelectedRows.Count > 0) // Pastikan ada baris yang dipilih
            {
                // Ambil nilai ID_CUSTOMER dari baris yang dipilih
                string idCustomer = dataGridPilihMember.SelectedRows[0].Cells["ID_CUSTOMER"].Value.ToString();

                sqlQuery = "UPDATE CUSTOMER SET DELETE_CUSTOMER = 1 WHERE ID_CUSTOMER = '" + idCustomer + "';";
                DatabaseHelper.ExecuteQuery(sqlQuery);

                dtMember.Clear();

                sqlQuery = "select ID_CUSTOMER, NAME_CUSTOMER as 'Nama', ADDRESS as 'Alamat', PHONENUMBER as 'No HP' from CUSTOMER where DELETE_CUSTOMER = 0;";
                dtMember = DatabaseHelper.ExecuteQuery(sqlQuery);
                dataGridPilihMember.DataSource = dtMember;
            }
            else
            {
                MessageBox.Show("Silakan pilih anggota dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEditAnggota_Click(object sender, EventArgs e)
        {
            if (dataGridPilihMember.SelectedRows.Count > 0) // Pastikan ada baris yang dipilih
            {
                // Ambil nilai ID_CUSTOMER dari baris yang dipilih
                string idCustomer = dataGridPilihMember.SelectedRows[0].Cells["ID_CUSTOMER"].Value.ToString();

                navigationHelper.NavigateToTambahEditMember(this, idCustomer);
            }
            else
            {
                MessageBox.Show("Silakan pilih anggota dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonTambahAnggota_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToTambahEditMember(this, "");
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

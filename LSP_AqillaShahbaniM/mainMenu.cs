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
    public partial class mainMenu : Form
    {
        private NavigationHelper navigationHelper;

        public mainMenu()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void buttonAnggotaYes_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToForm(this, new pinjamBukuPilihMember());
        }

        private void buttonAnggotaTidak_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToForm(this, new pinjamBukuAddMember());
        }

        private void mainMenu_Load(object sender, EventArgs e)
        {

        }

        private void buttonPengembalian_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToForm(this, new pengembalianBuku());
        }

        private void buttonRiwayatPeminjaman_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToForm(this, new historyPeminjaman());
        }

        private void buttonDaftarAnggota_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToForm(this, new daftarMemberView());
        }

        private void buttonDaftarBuku_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToForm(this, new daftarBukuView());
        }

        private void buttonPeminjaman_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToForm(this, new mainMenu());
        }
    }
}

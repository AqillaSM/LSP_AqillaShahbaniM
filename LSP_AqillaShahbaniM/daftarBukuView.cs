﻿using System;
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
    public partial class daftarBukuView : Form
    {
        string sqlQuery;
        DataTable dtBuku = new DataTable();
        DataTable dtBukuterpinjam = new DataTable();
        private NavigationHelper navigationHelper;

        public daftarBukuView()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper();
        }

        private void textBoxSearchBuku_TextChanged(object sender, EventArgs e)
        {
            DataHelper.SearchFilter(dtBuku, "Judul_Buku", textBoxSearchBuku.Text, dataGridPilihBuku);
        }

        private void daftarBukuView_Load(object sender, EventArgs e)
        {
            sqlQuery = "select ID_BOOK, TITLE as 'Judul_Buku', AUTHOR as 'Penulis', STOK as 'Stok' from BOOK where DELETE_BOOK = 0 ;";
            dtBuku = DatabaseHelper.ExecuteQuery(sqlQuery);
            dataGridPilihBuku.DataSource = dtBuku;
            DataHelper.fillDataGrid(dataGridPilihBuku);
        }

        private void buttonHapusBuku_Click(object sender, EventArgs e)
        {
           

            if (dataGridPilihBuku.SelectedRows.Count > 0)
            {
                string idBook = dataGridPilihBuku.SelectedRows[0].Cells["ID_BOOK"].Value.ToString();

                sqlQuery = "SELECT B.ID_BOOK, B.TITLE, P.ID_PEMINJAMAN, P.STATUS_PEMINJAMAN " +
                           "FROM BOOK B " +
                           "JOIN BOOK_PEMINJAMAN BP ON B.ID_BOOK = BP.ID_BOOK " +
                           "JOIN PEMINJAMAN P ON BP.ID_PEMINJAMAN = P.ID_PEMINJAMAN " +
                           "WHERE P.STATUS_PEMINJAMAN = 1 AND B.ID_BOOK = '" + idBook + "';";
                dtBukuterpinjam = DatabaseHelper.ExecuteQuery(sqlQuery);

                if (dtBukuterpinjam != null && dtBukuterpinjam.Rows.Count > 0)
                {
                    MessageBox.Show("Buku tidak bisa dihapus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    sqlQuery = "UPDATE BOOK SET DELETE_BOOK = 1 WHERE ID_BOOK = '" + idBook + "';";
                    DatabaseHelper.ExecuteQuery(sqlQuery);

                    dtBuku.Clear();

                    sqlQuery = "select ID_BOOK, TITLE as 'Judul_Buku', AUTHOR as 'Penulis', STOK as 'Stok' from BOOK where DELETE_BOOK = 0 ;";
                    dtBuku = DatabaseHelper.ExecuteQuery(sqlQuery);
                    dataGridPilihBuku.DataSource = dtBuku;
                }





                
            }
            else if 
                (dataGridPilihBuku.SelectedRows.Count > 0)
            {

            }
            else
            {
                MessageBox.Show("Silakan pilih buku dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEditBuku_Click(object sender, EventArgs e)
        {
            if (dataGridPilihBuku.SelectedRows.Count > 0) 
            {
               
                string idBook = dataGridPilihBuku.SelectedRows[0].Cells["ID_BOOK"].Value.ToString();

                navigationHelper.NavigateToTambahEditBuku(this, idBook);
            }
            else
            {
                MessageBox.Show("Silakan pilih buku dari daftar terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonTambahBuku_Click(object sender, EventArgs e)
        {
            navigationHelper.NavigateToTambahEditBuku(this, "");
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

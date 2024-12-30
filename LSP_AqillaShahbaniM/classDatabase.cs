using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP_AqillaShahbaniM
{
    public class Book
    {
        public string IdBook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Stock { get; set; }
        public int DeleteBook { get; set; }
    }

    public class Customer
    {
        public string IdCustomer { get; set; }
        public string NameCustomer { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int DeleteCustomer { get; set; }
    }

    public class Peminjaman
    {
        public string IdPeminjaman { get; set; }
        public string IdCustomer { get; set; }
        public DateTime TanggalPeminjaman { get; set; }
        public DateTime TanggalPengembalian { get; set; }
        public int StatusPeminjaman { get; set; }
    }

    public class BookPeminjaman
    {
        public string IdBook { get; set; }
        public string IdPeminjaman { get; set; }
        public int DeleteBookPeminjaman { get; set; }
    }
}

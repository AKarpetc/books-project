using book_editor.data.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.data.DB
{
    public class BooksContext : DbContext, IDBContext
    {
        public BooksContext() : base("BoocksContext")
        {

        }

        public virtual DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Cover> Covers { get; set; }
    }
}



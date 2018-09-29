using book_editor.data.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.data.DB
{
    internal sealed class DBConfiguaration : DbMigrationsConfiguration<BooksContext>
    {
        public DBConfiguaration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }
        protected override void Seed(BooksContext db)
        {
            db.Books.AddOrUpdate(x => x.Header, new Models.Book { Header = "Книга 1", ISBN = "13216546", PageCount = 300, PublishingOffice = "Москва", PublishYear = 1981, Authors = new List<Author> { new Author { Name = "Автор Имя 1", Surname= "Автор Имя 2 " } } });
            db.Books.AddOrUpdate(x => x.Header, new Models.Book { Header = "Книга 2", ISBN = "13216546", PageCount = 300, PublishingOffice = "Москва", PublishYear = 1981, Authors = new List<Author> { new Author { Name = "Имя 1", Surname = "Фамилия 2 " } } });
            db.Books.AddOrUpdate(x => x.Header, new Models.Book { Header = "Книга 3", ISBN = "13216546", PageCount = 300, PublishingOffice = "Москва", PublishYear = 1981, Authors = new List<Author> { new Author { Name = "Имя 1", Surname = "Фамилия 2 " } } });
            db.Books.AddOrUpdate(x => x.Header, new Models.Book { Header = "Книга 4", ISBN = "13216546", PageCount = 300, PublishingOffice = "Москва", PublishYear = 1981, Authors = new List<Author> { new Author { Name = "Имя 1", Surname = "Фамилия 2 " },new Author { Name = "Имя 5", Surname = "Фамилия 6 " } } });
            db.Books.AddOrUpdate(x => x.Header, new Models.Book { Header = "Книга 5", ISBN = "13216546", PageCount = 300, PublishingOffice = "Москва", PublishYear = 1981, Authors = new List<Author> { new Author { Name = "Имя 1", Surname = "Фамилия 2 " } } });

            db.SaveChanges();
        }
    }
}
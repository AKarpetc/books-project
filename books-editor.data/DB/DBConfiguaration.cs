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
            db.Books.AddOrUpdate(x => x.Header, new Models.Book { Header = "Тайные виды на гору Фудзи", AuditDateTime=DateTime.Now, ISBN = "978-5-8469-2187-4", PageCount = 300, PublishingOffice = "Москва", PublishYear = 2010, Authors = new List<Author> { new Author { Name = "Виктор", Surname= "Пелевин" } } });
            db.Books.AddOrUpdate(x => x.Header, new Models.Book { Header = "CSharp 6.0. Справочник.", ISBN = "978-5-8459-2087-4", PageCount = 300, PublishingOffice = "Вильямс", PublishYear = 2005, Authors = new List<Author> { new Author { Name = "Джозеф", Surname = "Албахари" }, new Author { Name = "Бен", Surname = "Албахари" } }, });

            db.SaveChanges();
        }
    }
}
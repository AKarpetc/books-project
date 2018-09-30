namespace book_editor.data.DB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Surname = c.String(maxLength: 20),
                        BookId = c.Int(nullable: false),
                        AuditDateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(maxLength: 30),
                        PageCount = c.Int(nullable: false),
                        PublishingOffice = c.String(maxLength: 30),
                        PublishYear = c.Int(nullable: false),
                        ISBN = c.String(),
                        AuditDateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Covers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        File = c.Binary(),
                        FileName = c.String(),
                        BookId = c.Int(nullable: false),
                        AuditDateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Covers", "BookId", "dbo.Books");
            DropForeignKey("dbo.Authors", "BookId", "dbo.Books");
            DropIndex("dbo.Covers", new[] { "BookId" });
            DropIndex("dbo.Authors", new[] { "BookId" });
            DropTable("dbo.Covers");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}

namespace WindowsFormsApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            AddForeignKey("dbo.Posts", "CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            AddForeignKey("dbo.Posts", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}

namespace RecipesMVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "Cuisine_CuisineID", "dbo.Cuisines");
            DropIndex("dbo.Recipes", new[] { "Cuisine_CuisineID" });
            RenameColumn(table: "dbo.Recipes", name: "Cuisine_CuisineID", newName: "CuisineID");
            AlterColumn("dbo.Recipes", "CuisineID", c => c.Int(nullable: true));
            CreateIndex("dbo.Recipes", "CuisineID");
            AddForeignKey("dbo.Recipes", "CuisineID", "dbo.Cuisines", "CuisineID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "CuisineID", "dbo.Cuisines");
            DropIndex("dbo.Recipes", new[] { "CuisineID" });
            AlterColumn("dbo.Recipes", "CuisineID", c => c.Int());
            RenameColumn(table: "dbo.Recipes", name: "CuisineID", newName: "Cuisine_CuisineID");
            CreateIndex("dbo.Recipes", "Cuisine_CuisineID");
            AddForeignKey("dbo.Recipes", "Cuisine_CuisineID", "dbo.Cuisines", "CuisineID");
        }
    }
}

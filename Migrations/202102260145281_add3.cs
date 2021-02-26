namespace RecipesMVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "CuisineID", "dbo.Cuisines");
            DropIndex("dbo.Recipes", new[] { "CuisineID" });
            AlterColumn("dbo.Recipes", "CuisineID", c => c.Int());
            CreateIndex("dbo.Recipes", "CuisineID");
            AddForeignKey("dbo.Recipes", "CuisineID", "dbo.Cuisines", "CuisineID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "CuisineID", "dbo.Cuisines");
            DropIndex("dbo.Recipes", new[] { "CuisineID" });
            AlterColumn("dbo.Recipes", "CuisineID", c => c.Int(nullable: false));
            CreateIndex("dbo.Recipes", "CuisineID");
            AddForeignKey("dbo.Recipes", "CuisineID", "dbo.Cuisines", "CuisineID", cascadeDelete: true);
        }
    }
}

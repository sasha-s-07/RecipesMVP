using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RecipesMVP.Models
{
    public class RecipesDataContext : DbContext
    {
        public RecipesDataContext() : base("name=RecipesDataContext")
        {

        }

        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Cuisine> Cuisine { get; set; }

    }
}
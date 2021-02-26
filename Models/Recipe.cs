using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipesMVP.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }

        public string RecipeName { get; set; }

        public string RecipeLink { get; set; }
        public string CookingTime { get; set; }

        [ForeignKey("Cuisine")]
        public int? CuisineID { get; set; }
        public virtual Cuisine Cuisine { get; set; }

    }
    public class RecipeDto
    {
        public int RecipeID { get; set; }

        public string RecipeName { get; set; }

        public string RecipeLink { get; set; }
        public string CookingTime { get; set; }



    }
}
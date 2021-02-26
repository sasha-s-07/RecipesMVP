using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipesMVP.Models
{
    public class Cuisine
    {
        [Key]
        public int CuisineID { get; set; }

        public string CuisineName { get; set; }
        //One Cuisine, Many Recipes
        public ICollection<Recipe> Recipes { get; set; }

    }

    /*public class CuisineDto
    {
        public int CuisineID { get; set; }

        public string CuisineName { get; set; }*/

    
}
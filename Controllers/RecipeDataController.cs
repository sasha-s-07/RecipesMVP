using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RecipesMVP.Models;

namespace RecipesMVP.Controllers
{
    public class RecipeDataController : ApiController
    {
        private RecipesDataContext db = new RecipesDataContext();
        /// <summary>
        /// Gets recipes in the database.
        /// </summary>
        /// <returns>a list of Recipes including their Id, CuisineID, CuisineName, RecipeName, RecipeLink, CookingTime</returns>
        /// <example>
        // GET: api/RecipesData/GetRecipe

        public IQueryable<Recipe> GetRecipe()
        {
            return db.Recipe;
        }
        /// <summary>
        /// Gets recipes in the database.
        /// </summary>
        /// <returns>Recipe's Id, CuisineID, CuisineName, RecipeName, RecipeLink, CookingTime</returns>
        /// <example>
        // GET: api/RecipesData/GetRecipe/{id}


        [ResponseType(typeof(Recipe))]
        public IHttpActionResult GetRecipe(int id)
        {
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }
        /// <summary>
        /// Adds a recipe to the database.
        /// </summary>
        /// <param name="recipe">A recipe object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        // POST: api/RecipesData/AddRecipe/5
        // FORM DATA: Recipe JSON Object
        [ResponseType(typeof(Recipe))]
        [HttpPost]
        public IHttpActionResult AddRecipe([FromBody] Recipe recipe)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recipe.Add(recipe);
            db.SaveChanges();

            return Ok(recipe.RecipeID);
        }
        // PUT: api/Recipe/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecipe(int id, Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.RecipeID)
            {
                return BadRequest();
            }

            db.Entry(recipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Updates a recipe in the database given information about the recipe.
        /// </summary>
        /// <param name="id">The Recipe id</param>
        /// <param name="RecipeName">A recipe object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/RecipeData/UpdateRecipe/5
        /// FORM DATA: Recipe JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRecipe(int id, [FromBody] Recipe recipe)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.RecipeID)
            {
                return BadRequest();
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/Recipe
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult PostRecipe(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recipe.Add(recipe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recipe.RecipeID }, recipe);
        }

        /// <summary>
        /// Deletes a recipe in the database
        /// </summary>
        /// <param name="id">The id of the recipe to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/RecipeData/DeleteRecipe/5
        /// </example>
        [HttpPost]
        public IHttpActionResult DeleteRecipe(int id)
        {
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }
            

            db.Recipe.Remove(recipe);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecipeExists(int id)
        {
            return db.Recipe.Count(e => e.RecipeID == id) > 0;
        }
    }
}
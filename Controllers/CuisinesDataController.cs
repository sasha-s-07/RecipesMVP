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
    public class CuisinesDataController : ApiController
    {
        private RecipesDataContext db = new RecipesDataContext();
        /// <summary>
        /// Gets cuisines in the database.
        /// </summary>
        /// <returns>Cuisines' Ids and CuisineNames</returns>
        /// <example>
        // GET: api/CuisinesData/GetCuisine

        public IQueryable<Cuisine> GetCuisine()
        {
            return db.Cuisine;
        }
        /// <summary>
        /// Gets cuisines in the database.
        /// </summary>
        /// <returns>Cuisine Id, CuisineName</returns>
        /// <example>
        // GET: api/CuisinesData/GetCuisine/{id}
        
        [ResponseType(typeof(Cuisine))]
        public IHttpActionResult GetCuisine(int id)
        {
            Cuisine cuisine = db.Cuisine.Find(id);
            if (cuisine == null)
            {
                return NotFound();
            }

            return Ok(cuisine);
        }
        /// <summary>
        /// Adds a cuisine to the database.
        /// </summary>
        /// <param name="cuisine">A cuisine object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        // POST: api/CusinesData/AddRecipe
        // FORM DATA: Cusine JSON Object
        [ResponseType(typeof(Cuisine))]
        [HttpPost]
        public IHttpActionResult AddCusine([FromBody] Cuisine cuisine)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuisine.Add(cuisine);
            db.SaveChanges();

            return Ok(cuisine.CuisineID);
        }

        /// <summary>
        /// Updates a Cuisine in the database given information about the Cuisine.
        /// </summary>
        /// <param name="id">The Cuisine id</param>
        /// <param name="CuisineName">A Cuisine object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/CuisineData/UpdateCuisine/5
        /// FORM DATA: Player JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRecipe(int id, [FromBody] Cuisine cuisine)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuisine.CuisineID)
            {
                return BadRequest();
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuisineExists(id))
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
        /// Deletes a cuisine in the database
        /// </summary>
        /// <param name="id">The id of the cuisine to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/CuisineData/DeleteCuisine/5
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


        // PUT: api/CuisinesData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCuisine(int id, Cuisine cuisine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuisine.CuisineID)
            {
                return BadRequest();
            }

            db.Entry(cuisine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuisineExists(id))
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

        // POST: api/CuisinesData
        [ResponseType(typeof(Cuisine))]
        public IHttpActionResult PostCuisine(Cuisine cuisine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuisine.Add(cuisine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuisine.CuisineID }, cuisine);
        }

        // DELETE: api/CuisinesData/5
        [ResponseType(typeof(Cuisine))]
        public IHttpActionResult DeleteCuisine(int id)
        {
            Cuisine cuisine = db.Cuisine.Find(id);
            if (cuisine == null)
            {
                return NotFound();
            }

            db.Cuisine.Remove(cuisine);
            db.SaveChanges();

            return Ok(cuisine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuisineExists(int id)
        {
            return db.Cuisine.Count(e => e.CuisineID == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecipesMVP.Models;

namespace RecipesMVP.Controllers
{
    public class RecipesController : Controller
    {
        private RecipesDataContext db = new RecipesDataContext();
        /// <summary>
        /// Displays the names of all the recipes in the database
        /// </summary>
        /// <returns>A list of all Recipes</returns>
        // GET: Recipes
        public async Task<ActionResult> Index()
        {
           
            var recipe = db.Recipe.Include(r => r.Cuisine);
            return View(await recipe.ToListAsync());
        }
        /// <summary>
        /// Shows the details of a selected recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The recipe name, recipe link, cooking time, cuisine name</returns>
        // GET: Recipes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }
        /// <summary>
        /// Displays a form to add a new recipe to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Adds the new recipe and it's info to the database</returns>

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.CuisineID = new SelectList(db.Cuisine, "CuisineID", "CuisineName");
            return View();
        }
        /// <summary>
        /// Sends the request to add the new recipe to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Adds the new recipe and it's info to the database</returns>

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RecipeID,RecipeName,RecipeLink,CookingTime,CuisineID")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipe.Add(recipe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CuisineID = new SelectList(db.Cuisine, "CuisineID", "CuisineName", recipe.CuisineID);
            return View(recipe);
        }
        /// <summary>
        /// Displays a form to edit recipe details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Adds edited recipe information to the database</returns>

        // GET: Recipes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuisineID = new SelectList(db.Cuisine, "CuisineID", "CuisineName", recipe.CuisineID);
            return View(recipe);
        }
        /// <summary>
        /// Sends the request to edit recipe details and add them to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Sends the edited recipe and its info to the database</returns>

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RecipeID,RecipeName,RecipeLink,CookingTime,CuisineID")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CuisineID = new SelectList(db.Cuisine, "CuisineID", "CuisineName", recipe.CuisineID);
            return View(recipe);
        }
        /// <summary>
        /// Deletes a recipe from the database based on its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletes recipe from the database</returns>

        // GET: Recipes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }
        /// <summary>
        /// Sends the request to delete recipe from the database based on Recipe ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletes the recipe from the database</returns>

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recipe recipe = await db.Recipe.FindAsync(id);
            db.Recipe.Remove(recipe);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecipesMVP.Models;

namespace RecipesMVP.Controllers
{
    public class CuisinesController : Controller
    {
        private RecipesDataContext db = new RecipesDataContext();
        /// <summary>
        /// Displays  all the cuisines and their information from the database
        /// </summary>
        /// <returns>A list of all Cuisines and their information</returns>

        // GET: Cuisines
        public ActionResult Index()
        {
            return View(db.Cuisine.ToList());
        }

        /// <summary>
        /// Displays the cuisine name for the specified Cuisine ID
        /// </summary>
        /// <param name="id">takes Cuisine ID</param>
        /// <returns>The Cuisine name</returns>
        // GET: Cuisines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuisine cuisine = db.Cuisine.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }
        /// <summary>
        /// Creates a new cuisine and adds it to the database
        /// </summary>
        /// <returns>Adds new cuisine to the database</returns>
        // GET: Cuisines/Create
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Sends request to add information about new cuisine to the database 
        /// </summary>
        /// <param name="cuisine"></param>
        /// <returns>adds new cuisine info to the database</returns>
        // POST: Cuisines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CuisineID,CuisineName")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                db.Cuisine.Add(cuisine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cuisine);
        }
        /// <summary>
        /// Update's information for a specific cuisine based on the Cuisine ID
        /// </summary>
        /// <param name="id">uses cuisine id</param>
        /// <returns></returns>
        // GET: Cuisines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuisine cuisine = db.Cuisine.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }
        /// <summary>
        /// Sends request to update information about the specified cuisine
        /// </summary>
        /// <param name="cuisine">uses cuisine id</param>
        /// <returns>updates the edited cuisine information in the database</returns>

        // POST: Cuisines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CuisineID,CuisineName")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuisine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cuisine);
        }
        /// <summary>
        /// deletes a cuisine from the database based on the Cuisine ID
        /// </summary>
        /// <param name="id">uses cuisine id</param>
        /// <returns>removes cuisine from the database</returns>

        // GET: Cuisines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuisine cuisine = db.Cuisine.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }
        /// <summary>
        /// Sends request to delete a specified cuisine
        /// </summary>
        /// <param name="cuisine">uses cuisine id</param>
        /// <returns>deletes the cuisine from the database</returns>

        // POST: Cuisines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuisine cuisine = db.Cuisine.Find(id);
            db.Cuisine.Remove(cuisine);
            db.SaveChanges();
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

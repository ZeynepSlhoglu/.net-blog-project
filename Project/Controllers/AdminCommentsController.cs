using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class AdminCommentsController : Controller
    {
        private BLOGEntities db = new BLOGEntities();

        // GET: AdminComments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Authors).Include(c => c.Blogs).Include(c => c.Users);
            return View(comments.ToList());
        }

        // GET: AdminComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: AdminComments/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "name");
            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "title");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "name");
            return View();
        }

        // POST: AdminComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,text,date,BlogID,UserID,AuthorID")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "name", comments.AuthorID);
            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "title", comments.BlogID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "name", comments.UserID);
            return View(comments);
        }

        // GET: AdminComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "name", comments.AuthorID);
            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "title", comments.BlogID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "name", comments.UserID);
            return View(comments);
        }

        // POST: AdminComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,text,date,BlogID,UserID,AuthorID")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "name", comments.AuthorID);
            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "title", comments.BlogID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "name", comments.UserID);
            return View(comments);
        }

        // GET: AdminComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
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

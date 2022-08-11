using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers.Frontend
{
    public class AuthorBlogsController : Controller
    {
        private BLOGEntities db = new BLOGEntities();

        // GET: AuthorBlogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.Authors).Include(b => b.Categories);
            return View(blogs.ToList());
        }

        // GET: AuthorBlogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // GET: AuthorBlogs/Create
        public ActionResult Create()
        {
            ViewBag.category = db.Categories.ToList();

            return View();
        }

        // POST: AuthorBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blogs blog, HttpPostedFileBase image)
        { //ben de bilmiyorum ki çalışıyrdu :( bak hatta admin kısmından kaydedelim 
            try
            {
                if (image != null)
                {
                    WebImage webImage = new WebImage(image.InputStream);
                    FileInfo fileInfo = new FileInfo(image.FileName);
                    string newImage = Guid.NewGuid().ToString() + fileInfo.Extension;
                    webImage.Resize(800, 350);
                    webImage.Save("~/Uploads/" + newImage);
                    blog.image = "/Uploads/" + newImage;
                }
                blog.blogDate = DateTime.Now;
                db.Blogs.Add(blog);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorBlogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "name", blogs.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "title", blogs.CategoryID);
            return View(blogs);
        }

        // POST: AuthorBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogID,title,text,image,CategoryID,AuthorID,blogDate")] Blogs blogs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "name", blogs.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "title", blogs.CategoryID);
            return View(blogs);
        }

        // GET: AuthorBlogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // POST: AuthorBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blogs blogs = db.Blogs.Find(id);
            db.Blogs.Remove(blogs);
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

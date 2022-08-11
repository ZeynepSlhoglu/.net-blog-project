using Project.Models;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Web.Helpers;
using System;
using System.Collections.Generic;

namespace Project.Controllers
{
    public class AdminBlogController : Controller
    {
        BLOGEntities db = new BLOGEntities();
        // GET: AdminBlog
        public ActionResult Index()

        {

            var blog = db.Blogs.ToList();
            return View(blog);
        }

        // GET: AdminBlog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }

            Blogs blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();

            }
            return View(blog);
        }

        
        // GET: AdminBlog/Create
        public ActionResult Create()
        {
            ViewBag.category = db.Categories.ToList();
           
            return View();
        }

        // POST: AdminBlog/Create
        [HttpPost]
        public ActionResult Create(Blogs blog, HttpPostedFileBase image)
        {
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

        // GET: AdminBlog/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.category = db.Categories.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }
            var blog = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: AdminBlog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase image, Blogs blog)
        {
            try
            {
                var blogguncelle = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
                if (image != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(blogguncelle.image)))
                    {
                        System.IO.File.Delete(Server.MapPath(blogguncelle.image));

                    }
                    WebImage webImage = new WebImage(image.InputStream);
                    FileInfo imageinfo = new FileInfo(image.FileName);
                    string newimage = Guid.NewGuid().ToString() + imageinfo.Extension;
                    webImage.Resize(800, 350);
                    webImage.Save("~/Uploads/" + newimage);
                    blogguncelle.image = "/Uploads/" + newimage;
                    blogguncelle.title = blog.title;
                    blogguncelle.text = blog.text;
                    blogguncelle.CategoryID = blog.CategoryID;
                    blogguncelle.AuthorID = blog.AuthorID;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminBlog/Delete/5
        public ActionResult Delete(int id)
        {
            var blog = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
            if(blog == null)
            {
                return HttpNotFound(); 
            }
            return View(blog);
        }

        // POST: AdminBlog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var blog = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();
                if (blog == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(blog.image))){
                    System.IO.File.Exists(Server.MapPath(blog.image));
                };
                db.Blogs.Remove(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using PagedList;
using PagedList.Mvc;
using Project.Models.Class;
using System.Web.UI;
using System.Runtime.Serialization;
namespace Project.Controllers.Frontend
{
    public class BlogController : Controller
    {
        BLOGEntities db = new BLOGEntities();

        // GET: Blog
        public ActionResult Index(int page=1)
        {
            var blog = db.Blogs.OrderByDescending(b => b.BlogID).ToPagedList(page, 4);

            return View(blog);
        }

        BlogComment bc = new BlogComment();

        public ActionResult BlogDetay(int id, int page=1)
        {
            bc.BlogComments = db.Blogs.Where(b =>   b.BlogID == id).ToList();
            bc.CommentComments = db.Comments.Where(b => b.BlogID == id).ToList();
            ViewBag.BlogID = id;

            return View(bc);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult Comment(int id)
        {
            db.Blogs.FirstOrDefault(b => b.BlogID == id);
            ViewBag.BlogID = id;
           return PartialView(id);
        }

        [HttpPost]  
        public PartialViewResult Comment(Comments comment)
        {
            
            comment.date = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();
            return PartialView(comment);
        }

        public ActionResult FavoriteList(int id)
        {
            var favori = db.Favorites.ToList();
            ViewBag.BlogID = id;
            return View(favori);
        }


  
        public PartialViewResult Favorite(Favorites favorites)
        {
                Session["UserID"] = favorites.UserID;
            ViewBag.BlogId = favorites.BlogID;
                db.Favorites.Add(favorites);
                var rt = db.SaveChanges();
                ViewBag.Basarili = "Favorilere eklendi.";
                return PartialView(rt);
        }
    }
}
 


//public ActionResult BlogDetay(int id)
//{ 
//    var blogdetay = db.Blogs.Where(b => b.BlogID == id).SingleOrDefault();

//    if (blogdetay == null)
//    {
//        return HttpNotFound(); Hoşgledn hooopp
//    }
//    return View(blogdetay);

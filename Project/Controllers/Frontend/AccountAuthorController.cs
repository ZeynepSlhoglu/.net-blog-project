using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers.Frontend
{
    public class AccountAuthorController : Controller
    {
        // GET: AccountAuthor
        BLOGEntities db = new BLOGEntities();
        // GET: Account

        [HttpGet]
        public ActionResult Account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Account(Authors author)
        {
            var yzr = db.Authors.Where(u => u.name == author.name && u.email == author.email && u.password == author.password).FirstOrDefault();

            if (yzr == null)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                Session["authorID"] = author.AuthorID;
                Session["name"] = author.name.ToString();
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ViewBag.HataliGiris = "Böyle bir hesap zaten var.";
                return View();
            }
            db.Authors.Add(author);
            db.SaveChanges(); 
            return View();
        }
    }
}
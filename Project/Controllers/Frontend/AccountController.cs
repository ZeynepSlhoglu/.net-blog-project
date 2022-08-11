using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers.Frontend
{
    public class AccountController : Controller
    {
        BLOGEntities db = new BLOGEntities();
        // GET: Account

        [HttpGet]
        public ActionResult Account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Account(Users user)
        {
            var kl  = db.Users.Where(u => u.name == user.name && u.email == user.email && u.password == user.password).FirstOrDefault();

            if (kl == null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                Session["UserID"] = user.UserID;
                Session["name"] = user.name.ToString();
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ViewBag.HataliGiris = "Böyle bir hesap zaten var.";
                return View();
            }
            db.Users.Add(user);
            db.SaveChanges();
            return View();
        }
    }
}

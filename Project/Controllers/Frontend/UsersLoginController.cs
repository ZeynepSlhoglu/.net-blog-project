using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.Models;

namespace Project.Controllers.Frontend
{
    public class UsersLoginController : Controller
    {
        private BLOGEntities db = new BLOGEntities();

        // GET: UsersLogin
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: UsersLogin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: UsersLogin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersLogin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,name,email,password")] Users user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: UsersLogin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UsersLogin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,name,email,password")] Users user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: UsersLogin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UsersLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users user = db.Users.Find(id);
            db.Users.Remove(user);
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user, Authors author)
        {
            var login = db.Users.FirstOrDefault(u => u.email == user.email && u.password == user.password);
            var yzrLogin = db.Authors.FirstOrDefault(u => u.email == author.email && u.password == author.password);
            if (login != null)
            {
                FormsAuthentication.SetAuthCookie(login.UserID.ToString(), false);
                Session["UserID"] = login.UserID;
                Session["name"] = login.name.ToString(); 
                return RedirectToAction("Index", "Blog");
            }
            else if (yzrLogin != null)
            {
                FormsAuthentication.SetAuthCookie(yzrLogin.AuthorID.ToString(), false);
                Session["AuthorID"] = yzrLogin.AuthorID;
                Session["name"] = yzrLogin.name.ToString();
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ViewBag.HataliGiris = "Hatalı Giriş Denemesi Yaptınız.Tekrar Deneyiniz";
                return View();
            }


        }
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["AuthorID"] = null;

            Session.Abandon();

            return RedirectToAction("Index", "Blog");
        }
    }
}


//[HttpGet]
//public ActionResult Login()
//{
//    return View();
//}

//[HttpPost]
//public ActionResult Login(Users user, Authors author)
//{
//    var login = db.Users.Where(u => u.email == user.email && u.password == user.password).FirstOrDefault();
//    var yzrLogin = db.Authors.Where(u => u.email == author.email && u.password == author.password).FirstOrDefault();
//    if (login != null)
//    {
//        Session["UserID"] = login.UserID;
//        Session["name"] = login.name;

//        return RedirectToAction("Index", "Blog");
//    }
//    else if (yzrLogin != null)
//    {
//        Session["AuthorID"] = yzrLogin.AuthorID;
//        Session["name"] = yzrLogin.name;
//        return RedirectToAction("Index", "Blog");
//    }
//    else
//    {
//        ViewBag.HataliGiris = "Hatalı Giriş Denemesi Yaptınız.Tekrar Deneyiniz";
//        return View();
//    }


//}
//public ActionResult Logout()
//{
//    Session["UserID"] = null;
//    Session["AuthorID"] = null;

//    Session.Abandon();

//    return RedirectToAction("Index", "Blog");
//}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS4790A4.Models;
using CS4790A4.ViewModels;

namespace CS4790A4.Controllers
{
    public class UsersController : Controller
    {
        private ARCCproposalsDbContext db = new ARCCproposalsDbContext();

        // GET: Users
        [AuthorizationFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        [AuthorizationFilter]
        [AdminFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "email,password")] User user)
        {
            if (ModelState.IsValid)
            {
               
                if (Repository.CreateUser(user))
                {
                    Session["UserName"] = user.email;
                    return RedirectToAction("Index", "ProposalScores");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
 
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [AdminFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult Edit([Bind(Include = "Id,email,lastName,firstName,password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Login/5
        public ActionResult Login()
        {
            User user = new Models.User();
            return View(user);
        }

        // POST: Users/Login/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Id,email,password")] User user)
        {
            if (ModelState.IsValid)
            {
                if(user.email.Equals("admin@admin.com"))
                {
                    Session["isAdmin"] = true;
                }
                int id = Repository.GetUserId(user.email);
                if (id < 1)
                {
                    return RedirectToAction("Login", "Users");
                }
                Session["UserName"] = user.email;
                return RedirectToAction("Index","ProposalScores");
            }
            return RedirectToAction("Login", "Users");
        }

        // GET: Users/Login/5
        public ActionResult Logout()
        {
            Session["Username"] = null;
            return RedirectToAction("Login");
        }

        // GET: Users/Delete/5
        [AdminFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
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
    }
}

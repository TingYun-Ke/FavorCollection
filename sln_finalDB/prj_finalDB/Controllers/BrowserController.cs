using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using prj_finalDB.Models;
using System.Web.Security;
using PagedList;

namespace prj_finalDB.Controllers
{
    public class BrowserController : Controller
    {
        private dbFinalEntities db = new dbFinalEntities();

        //登入
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string em, string pw)
        {
            var temp = db.TableBrowser2034
                .Where(m => m.email == em && m.password == pw)
                .FirstOrDefault();
            if (temp == null)
            {
                ViewBag.Message = "信箱或密碼錯誤，請重新輸入";
                return View();
            }
            FormsAuthentication.RedirectFromLoginPage(em, true);
            Session["LoginOK"] = "Yes";
            Session["identity"] = temp.bNo;
            Session["name"] = temp.bName;


            return RedirectToAction("Home", "Browser");
        }

        public ActionResult Home()
        {
            if (Session["LoginOK"] != null && Session["LoginOK"].ToString() == "Yes")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Browser");
            }
        }

        //// GET: Browser
        //public ActionResult Index()
        //{
        //    return View(db.TableBrowser2034.ToList());
        //}

        // GET: Browser/Details/5
        public ActionResult Details()
        {
            if (Session["LoginOK"] != null && Session["LoginOK"].ToString() == "Yes")
            {
                string pName = Session["name"].ToString();
                List<TableBrowser2034> temp = db.TableBrowser2034
                .Where(m => m.bName == pName).ToList();
                return View(temp);
            }
            else
            {
                return RedirectToAction("Login", "Browser");
            }
        }

        // GET: Browser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Browser/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bNo,bName,email,password,bPhone")] TableBrowser2034 tableBrowser2034)
        {
            if (ModelState.IsValid)
            {
                db.TableBrowser2034.Add(tableBrowser2034);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tableBrowser2034);
        }

        // GET: Browser/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableBrowser2034 tableBrowser2034 = db.TableBrowser2034.Find(id);
            if (tableBrowser2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableBrowser2034);
        }

        // POST: Browser/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bNo,bName,email,password,bPhone")] TableBrowser2034 tableBrowser2034)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableBrowser2034).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(tableBrowser2034);
        }

        // GET: Browser/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableBrowser2034 tableBrowser2034 = db.TableBrowser2034.Find(id);
            if (tableBrowser2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableBrowser2034);
        }

        // POST: Browser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TableBrowser2034 tableBrowser2034 = db.TableBrowser2034.Find(id);
            db.TableBrowser2034.Remove(tableBrowser2034);
            db.SaveChanges();
            return RedirectToAction("Login");
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

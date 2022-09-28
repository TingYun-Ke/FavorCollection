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
    public class SightController : Controller
    {
        private dbFinalEntities db = new dbFinalEntities();

        // GET: Sight
        int pageSize = 5;
        public ActionResult Index(int page = 1)
        {
            if (Session["LoginOK"] != null && Session["LoginOK"].ToString() == "Yes")
            {
                string pName = Session["name"].ToString();
                List<TableBrowser2034> temp = db.TableBrowser2034
                .Where(m => m.bName == pName).ToList();
                //String user = temp[0].bName; //使用者
                List<TableSight2034> w0 = temp[0].TableSight2034
                    .OrderBy(m => m.sCounty).ToList(); //使用者收藏的飯店

                int currentPage = page < 1 ? 1 : page;
                var children = w0;
                //var children = db.TableAccommodation2034.OrderBy(m => m.aPrice).ToList();
                var result = children.ToPagedList(currentPage, pageSize);
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Browser");
            }
        }

        // GET: Sight/Create
        public ActionResult Create()
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

        // POST: Sight/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sName,ticket,businessHour,sCounty")] TableSight2034 sight)
        {
            if (ModelState.IsValid)
            {
                string pName = Session["name"].ToString();
                List<TableBrowser2034> Slist = db.TableBrowser2034
                .Where(m => m.bName == pName).ToList();
                Slist[0].TableSight2034.Add(sight);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(sight);
            }
        }

        // GET: Sight/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableSight2034 tableSight2034 = db.TableSight2034.Find(id);
            if (tableSight2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableSight2034);
        }

        // GET: Sight/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableSight2034 tableSight2034 = db.TableSight2034.Find(id);
            if (tableSight2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableSight2034);
        }

        // POST: Sight/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sName,ticket,businessHour,sCounty")] TableSight2034 tableSight2034)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableSight2034).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tableSight2034);
        }

        // GET: Sight/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableSight2034 tableSight2034 = db.TableSight2034.Find(id);
            if (tableSight2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableSight2034);
        }

        // POST: Sight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TableSight2034 tableSight2034 = db.TableSight2034.Find(id);
            db.TableSight2034.Remove(tableSight2034);
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

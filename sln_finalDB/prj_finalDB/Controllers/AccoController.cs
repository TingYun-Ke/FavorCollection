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
    public class AccoController : Controller
    {
        private dbFinalEntities db = new dbFinalEntities();

        // GET: Acco
        int pageSize = 5;
        public ActionResult Index(int page = 1)
        {
            if (Session["LoginOK"] != null && Session["LoginOK"].ToString() == "Yes")
            {
                string pName = Session["name"].ToString();
                List<TableBrowser2034> temp = db.TableBrowser2034
                .Where(m => m.bName == pName).ToList();
                //String user = temp[0].bName; //使用者
                List<TableAccommodation2034> w0 = temp[0].TableAccommodation2034
                    .OrderBy(m => m.aCounty).ToList(); //使用者收藏的飯店

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

        // GET: Acco/Create
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

        // POST: Acco/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aName,aPrice,aPhone,address,type,aCounty")] TableAccommodation2034 acco)
        {
            if (ModelState.IsValid)
            {
                string pName = Session["name"].ToString();
                List<TableBrowser2034> temp = db.TableBrowser2034
                .Where(m => m.bName == pName).ToList();
                temp[0].TableAccommodation2034.Add(acco);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(acco);
            }
        }

        // GET: Acco/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableAccommodation2034 tableAccommodation2034 = db.TableAccommodation2034.Find(id);
            if (tableAccommodation2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableAccommodation2034);
        }

        // GET: Acco/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableAccommodation2034 tableAccommodation2034 = db.TableAccommodation2034.Find(id);
            if (tableAccommodation2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableAccommodation2034);
        }

        // POST: Acco/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aName,aPrice,aPhone,address,type,aCounty")] TableAccommodation2034 tableAccommodation2034)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableAccommodation2034).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tableAccommodation2034);
        }

        // GET: Acco/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableAccommodation2034 tableAccommodation2034 = db.TableAccommodation2034.Find(id);
            if (tableAccommodation2034 == null)
            {
                return HttpNotFound();
            }
            return View(tableAccommodation2034);
        }

        // POST: Acco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            string pName = Session["name"].ToString();
            TableAccommodation2034 tableAccommodation2034 = db.TableAccommodation2034.Find(id);
            List<TableBrowser2034> temp = tableAccommodation2034
                .TableBrowser2034.Where(m=>m.bName == pName).ToList();
            db.TableAccommodation2034.Remove(tableAccommodation2034);
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

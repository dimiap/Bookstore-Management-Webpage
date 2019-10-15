using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ergasia.Models;

namespace Ergasia.Controllers
{
    public class discountsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: discounts
        public ActionResult Index()
        {
            var discounts = db.discounts.Include(d => d.stores);
            return View(discounts.ToList());
        }

        // GET: discounts/Details/5
        public ActionResult Details(string id1, string id2)
        {
            if (id1 == null&& id2==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discounts discounts = db.discounts.Single(x => x.discounttype == id1 && x.stor_id == id2);
            if (discounts == null)
            {
                return HttpNotFound();
            }
            return View(discounts);
        }

        // GET: discounts/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            return View();
        }

        // POST: discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "discounttype,stor_id,lowqty,highqty,discount")] discounts discounts)
        {
            if (ModelState.IsValid)
            {
                db.discounts.Add(discounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // GET: discounts/Edit/5
        public ActionResult Edit(string id1, string id2)
        {
            if (id1 == null && id2==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discounts discounts = db.discounts.Single(x=>x.discounttype==id1 && x.stor_id==id2);
            if (discounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // POST: discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "discounttype,stor_id,lowqty,highqty,discount")] discounts discounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // GET: discounts/Delete/5
        public ActionResult Delete(string id1, string id2)
        {
            if (id1 == null&& id2==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discounts discounts = db.discounts.Single(x => x.discounttype == id1 && x.stor_id == id2);
            if (discounts == null)
            {
                return HttpNotFound();
            }
            return View(discounts);
        }

        // POST: discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2)
        {
            discounts discounts = db.discounts.Single(x => x.discounttype == id1 && x.stor_id == id2);
            db.discounts.Remove(discounts);
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

﻿using System;
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
    public class titleauthorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: titleauthors
        public ActionResult Index()
        {
            var titleauthor = db.titleauthor.Include(t => t.authors).Include(t => t.titles);
            return View(titleauthor.ToList());
        }

        // GET: titleauthors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] splitid = id.Split(',');
            titleauthor titleauthor = db.titleauthor.Find(splitid[0], splitid[1]);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            return View(titleauthor);
        }

        // GET: titleauthors/Create
        public ActionResult Create()
        {
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            return View();
        }

        // POST: titleauthors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if (ModelState.IsValid)
            {
                db.titleauthor.Add(titleauthor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: titleauthors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] splitid = id.Split(',');
            titleauthor titleauthor = db.titleauthor.Find(splitid[0], splitid[1]);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // POST: titleauthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titleauthor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: titleauthors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] splitid = id.Split(',');
            titleauthor titleauthor = db.titleauthor.Find(splitid[0], splitid[1]);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            return View(titleauthor);
        }

        // POST: titleauthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            string[] splitid = id.Split(',');
            titleauthor titleauthor = db.titleauthor.Find(splitid[0], splitid[1]);
            db.titleauthor.Remove(titleauthor);
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

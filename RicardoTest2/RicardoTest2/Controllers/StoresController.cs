﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RicardoTest2.Models;

namespace RicardoTest2.Controllers
{
    public class StoresController : Controller
    {
        private DbModel db = new DbModel();

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var stores = db.Stores.Include(s => s.Company);
            return View(stores.ToList());
        }

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id = 0)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        //
        // GET: /Store/Create

        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
            return View();
        }

        //
        // POST: /Store/Create

        [HttpPost]
        public ActionResult Create(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", store.CompanyId);
            return View(store);
        }

        //
        // GET: /Store/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", store.CompanyId);
            return View(store);
        }

        //
        // POST: /Store/Edit/5

        [HttpPost]
        public ActionResult Edit(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", store.CompanyId);
            return View(store);
        }

        //
        // GET: /Store/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        //
        // POST: /Store/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
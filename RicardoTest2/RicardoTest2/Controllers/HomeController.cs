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
    public class HomeController : Controller
    {
        private DbModel db = new DbModel();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Store).Include(t => t.SalesMan).Include(t => t.Customer);
            return View(transactions.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "PhoneNumber1");
            ViewBag.SalesManId = new SelectList(db.Users, "UserId", "FName");
            ViewBag.CustomerId = new SelectList(db.Users, "UserId", "FName");
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "PhoneNumber1", transaction.StoreId);
            ViewBag.SalesManId = new SelectList(db.Users, "UserId", "FName", transaction.SalesManId);
            ViewBag.CustomerId = new SelectList(db.Users, "UserId", "FName", transaction.CustomerId);
            return View(transaction);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "PhoneNumber1", transaction.StoreId);
            ViewBag.SalesManId = new SelectList(db.Users, "UserId", "FName", transaction.SalesManId);
            ViewBag.CustomerId = new SelectList(db.Users, "UserId", "FName", transaction.CustomerId);
            return View(transaction);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "PhoneNumber1", transaction.StoreId);
            ViewBag.SalesManId = new SelectList(db.Users, "UserId", "FName", transaction.SalesManId);
            ViewBag.CustomerId = new SelectList(db.Users, "UserId", "FName", transaction.CustomerId);
            return View(transaction);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RicardoTest2.Models;

namespace RicardoTest2.Controllers
{
    public class RelationshipsController : Controller
    {
        private DbModel db = new DbModel();

        //
        // GET: /Relationship/

        public ActionResult Index()
        {
            var relationships = db.Relationships.Include(r => r.User1).Include(r => r.User2).Include(r => r.RelationshipType);
            return View(relationships.ToList());
        }

        //
        // GET: /Relationship/Details/5

        public ActionResult Details(int id = 0)
        {
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        //
        // GET: /Relationship/Create

        public ActionResult Create()
        {
            ViewBag.UserId1 = new SelectList(db.Users, "UserId", "FName");
            ViewBag.UserId2 = new SelectList(db.Users, "UserId", "FName");
            ViewBag.RelationshipTypeId = new SelectList(db.RelationshipTypes, "RelationshipTypeId", "Description");
            return View();
        }

        //
        // POST: /Relationship/Create

        [HttpPost]
        public ActionResult Create(Relationship relationship)
        {
            if (ModelState.IsValid)
            {
                db.Relationships.Add(relationship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId1 = new SelectList(db.Users, "UserId", "FName", relationship.UserId1);
            ViewBag.UserId2 = new SelectList(db.Users, "UserId", "FName", relationship.UserId2);
            ViewBag.RelationshipTypeId = new SelectList(db.RelationshipTypes, "RelationshipTypeId", "Description", relationship.RelationshipTypeId);
            return View(relationship);
        }

        //
        // GET: /Relationship/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId1 = new SelectList(db.Users, "UserId", "FName", relationship.UserId1);
            ViewBag.UserId2 = new SelectList(db.Users, "UserId", "FName", relationship.UserId2);
            ViewBag.RelationshipTypeId = new SelectList(db.RelationshipTypes, "RelationshipTypeId", "Description", relationship.RelationshipTypeId);
            return View(relationship);
        }

        //
        // POST: /Relationship/Edit/5

        [HttpPost]
        public ActionResult Edit(Relationship relationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId1 = new SelectList(db.Users, "UserId", "FName", relationship.UserId1);
            ViewBag.UserId2 = new SelectList(db.Users, "UserId", "FName", relationship.UserId2);
            ViewBag.RelationshipTypeId = new SelectList(db.RelationshipTypes, "RelationshipTypeId", "Description", relationship.RelationshipTypeId);
            return View(relationship);
        }

        //
        // GET: /Relationship/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        //
        // POST: /Relationship/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Relationship relationship = db.Relationships.Find(id);
            db.Relationships.Remove(relationship);
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
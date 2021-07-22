using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserProfile;

namespace UserProfile.Controllers
{
    public class POCsController : Controller
    {
        private PA_COMMON db = new PA_COMMON();

        // GET: POCs
        public ActionResult Index()
        {
            return View(db.POCS.ToList());
        }

        // GET: POCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POC pOC = db.POCS.Find(id);
            if (pOC == null)
            {
                return HttpNotFound();
            }
            return View(pOC);
        }

        // GET: POCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: POCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "POC_ID,POC_LAST,POC_FIRST,POC_MI,RANK,ORG_LOC,AKO_LOGIN,EMAIL,SEMAIL,ALT_EMAIL,CAC_EDIPI,PHONE_DSN,PHONE_COMM,PHONE_EMERG,LAST_VERIFIED,LAST_UPDATE,LAST_UPDATE_BY,POC_AVATAR,COMPANY_ID,CAC_EDIPI_LAST_UPDATE,OVERRIDE_EMAIL_UPDATE,LAST_LOGIN,DAYS_LAST_LOGIN")] POC pOC)
        {
            if (ModelState.IsValid)
            {
                db.POCS.Add(pOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pOC);
        }

        // GET: POCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POC pOC = db.POCS.Find(id);
            if (pOC == null)
            {
                return HttpNotFound();
            }
            return View(pOC);
        }

        // POST: POCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "POC_ID,POC_LAST,POC_FIRST,POC_MI,RANK,ORG_LOC,AKO_LOGIN,EMAIL,SEMAIL,ALT_EMAIL,CAC_EDIPI,PHONE_DSN,PHONE_COMM,PHONE_EMERG,LAST_VERIFIED,LAST_UPDATE,LAST_UPDATE_BY,POC_AVATAR,COMPANY_ID,CAC_EDIPI_LAST_UPDATE,OVERRIDE_EMAIL_UPDATE,LAST_LOGIN,DAYS_LAST_LOGIN")] POC pOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pOC);
        }

        // GET: POCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POC pOC = db.POCS.Find(id);
            if (pOC == null)
            {
                return HttpNotFound();
            }
            return View(pOC);
        }

        // POST: POCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POC pOC = db.POCS.Find(id);
            db.POCS.Remove(pOC);
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

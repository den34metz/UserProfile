/*using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserProfile;

namespace UserProfile.Controllers
{
    public class TRAININGsController : Controller
    {
        private TrainingTracker db = new TrainingTracker();

        // GET: TRAININGs
        public ActionResult Index()
        {
            *//*Get current user CAC data*/
            /*HttpClientCertificate cs = Request.ClientCertificate;
            string[] subjectArray = cs.Subject.Split(',');


            //Holds the entire contents of the subject line.
            string entireSubjectLine = cs.Subject.ToString();


            //gets the total length of the subject line
            int subjectLineLength = entireSubjectLine.Length;


            //-10 grabs the start of the 10 digit CAC identifer code for the user.
            int startOfCacIdentifierPosition = subjectLineLength - 10;


            string cacIdentifier = entireSubjectLine.Substring(startOfCacIdentifierPosition, 10);


            string[] arr = subjectArray[5].Split(' ');
            string[] user = arr[1].Split('=');
            StringBuilder sb = new StringBuilder();
            foreach (string field in user)
            {
                sb.Append(field);
            }
            sb.Remove(0, 2);


            string str1 = sb.ToString();
            string[] sArr1 = str1.Split('.');
            string lastName = sArr1[0].ToString();
            string firstName = sArr1[1].ToString();
            string MI = sArr1[2].ToString();
            string Id = cacIdentifier; //10 Digit Unique CAC identifier*//*
            //IntPtr accountToken = WindowsIdentity.GetCurrent().Token;
            string username = WindowsIdentity.GetCurrent().Name.ToString();
            Console.WriteLine("Curr User " + " : " + username);
            Console.WriteLine("Current user is: " + WindowsIdentity.GetCurrent().Name);
            *//*  return View(db.TRAININGS.ToList());*//*
            return View();
        }

        // GET: TRAININGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAINING training = db.TRAININGS.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // GET: TRAININGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TRAININGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TRAIN_ID,TRAIN_TITLE,TRAIN_DETAILS,TYPE,FREQUENCY,CLASS_DUR,MIN_ATTEND,MAX_ATTEND,ATCTS,MANDATED_BY,URL")] TRAINING training)
        {
            if (ModelState.IsValid)
            {
                db.TRAININGS.Add(training);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(training);
        }

        // GET: TRAININGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAINING training = db.TRAININGS.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: TRAININGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TRAIN_ID,TRAIN_TITLE,TRAIN_DETAILS,TYPE,FREQUENCY,CLASS_DUR,MIN_ATTEND,MAX_ATTEND,ATCTS,MANDATED_BY,URL")] TRAINING training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(training);
        }

        // GET: TRAININGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAINING training = db.TRAININGS.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: TRAININGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRAINING training = db.TRAININGS.Find(id);
            db.TRAININGS.Remove(training);
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
}*/

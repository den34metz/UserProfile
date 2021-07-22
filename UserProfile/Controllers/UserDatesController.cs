
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserProfile.Models;
using UserProfile.BuinessLogic;
using System.IO;

namespace UserProfile.Controllers
{
    public class UserDatesController : Controller
    {
        private TrainingTracker tdb = new TrainingTracker();
        //UserDatesProcessor dateProc = new UserDatesProcessor();

        // GET: UserDates
        public ActionResult GetSchedDates()
        {
            var data = UserDatesProcessor.GetAllSchedDate();

            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return jsonResult;

           
        }
        [HttpPost]
        public ActionResult InsertSchedDates(SchedDatesModel usermodel)
        {
            if (ModelState.IsValid)
            {

                int createSchedDate = UserDatesProcessor.AddSchedDate(usermodel.TRAIN_DATE_TIME, usermodel.EDIPI, usermodel.TRAIN_ID, usermodel.TRAIN_TITLE, usermodel.DATE_TIME_ID, usermodel.DATEMODIFIED);
                ViewBag.Message = String.Format("Worked! UserDatesController");
            }
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult InsertUserDates(UserDatesModel usermodel, HttpPostedFileBase file, int? id)
        {             

            if (ModelState.IsValid)
            {
                String fileName = "";
                byte[] fileBytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    fileBytes = br.ReadBytes(file.ContentLength);
                    fileName = Path.GetFileName(file.FileName);
                }

                if (id == null)
                {
                    int createDates = UserDatesProcessor.AddDate(usermodel.DATETAKEN, usermodel.DATENEXTDUE, usermodel.EDIPI, usermodel.TRAIN_ID, usermodel.TRAINTITLE, fileBytes, fileName, usermodel.TYPE);
                    ViewBag.Message = String.Format("Worked! UserDatesController");
                    //tdb.SpInsertUserDates(usermodel.DATETAKEN, usermodel.DATENEXTDUE, usermodel.EDIPI, usermodel.TRAIN_ID, usermodel.CERT, usermodel.CERTNAME, usermodel.DATETAKENID);
                }


                /*else
                {
                    tdb.SpInsertUserDates( usermodel.DATETAKEN, usermodel.DATENEXTDUE,id, usermodel.EDIPI, usermodel.TRAIN_ID, usermodel.CERT, usermodel.CERTNAME, id );
                }*/
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Cert
        public ActionResult GetCert(int datetakenid)
        {
            var data = UserDatesProcessor.GetCert(datetakenid);

            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return jsonResult;

        }
    }  
}
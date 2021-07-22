using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UserProfile.Models;
using Common.DataAccess;
using System.Data.Entity.Core.Objects;
using UserProfile.BuinessLogic;
using UserProfile.BuisnessLogic;
using UserProfile.ViewModels;

namespace UserProfile.Controllers
{
    public class HomeController : Controller
    {
        private TrainingTracker db = new TrainingTracker();
        int edipi = 0;
        
        public ActionResult Index()
        {
            /*var userModel = */
            var userEdipi = ConfigurationManager.AppSettings["HTTP_ARMYEDIPI"];
            var intedipi = Int32.Parse(userEdipi);
            edipi = intedipi;
            ViewBag.keyValue1 = "Edipi" + userEdipi;
            var data = PocsProcessor.GetPocTeam(edipi);

            return View(data[0]);

        }

        [HttpPost]
        public ActionResult Index(PocsModel model)
        {
            if(model.CAC_EDIPI != 0)
            {
                var fullname = model.FullName;
                var email = model.EMAIL;
                var semail = model.SEMAIL;
                var edipi = model.CAC_EDIPI;
                return View(model.ToString());
            }
            else
            {
                var userEdipi = ConfigurationManager.AppSettings["HTTP_ARMYEDIPI"];
                var intedipi = Int32.Parse(userEdipi);
                edipi = intedipi;
                var data = PocsProcessor.GetPocTeam(edipi);
                return View(data[0]);
            }
            
        }

        public ActionResult GetCurrEdipi()
        {
            var userEdipi = ConfigurationManager.AppSettings["HTTP_ARMYEDIPI"];
            ViewBag.keyValue1 = "Edipi" + userEdipi;
            var intedipi = Int32.Parse(userEdipi);
            edipi = intedipi;
            return Json(new { data = edipi }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPoc()
        {
            GetCurrEdipi();

            var data = PocsProcessor.GetPocs(edipi);
          
            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetSchedDates()
        {
            GetCurrEdipi();
            var data = UserDatesProcessor.GetSchedDate(edipi);

            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public ActionResult GetSingleSchedDate()
        {
           /* GetCurrEdipi();
            var data = UserDatesProcessor.GetSingleSchedDate(edipi);

            var jsonResult = Json(new { data - data }, JsonRequestBehavior.AllowGet);*/
            return null;
        }

        public ActionResult GetUserDates()
        {

            GetCurrEdipi();

            var data = UserDatesProcessor.GetDates(edipi);
           
            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult DateCertModal()
        {
            ViewBag.ModalState = "show";
            return View("DateCertModal");
        }
        /*[HttpPost]
        public ActionResult DateCertModel()
        {

        }
*/
        public ActionResult GetTrainingData()
        {
            var data = TrainingProcessor.GetAllTrainings();

            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public ActionResult GetSingleTraining()
        {
            var data = TrainingProcessor.GetSingleTraining();

            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
    }
}
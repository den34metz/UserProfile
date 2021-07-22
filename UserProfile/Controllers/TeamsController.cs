using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserProfile.BuisnessLogic;

namespace UserProfile.Controllers
{
    public class TeamsController : Controller
    {
        // GET: Teams
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTeam(int edipi)
        {
            var data = PocsProcessor.GetTeam(edipi);

            var jsonResult = Json(new { data = data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
    }
}
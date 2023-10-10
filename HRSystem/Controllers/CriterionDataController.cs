using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRSystem.Controllers
{
    public class CriterionDataController : Controller
    {
        // GET: CriterionData
        public ActionResult excel_locate()
        {
            return View();
        }
        public ActionResult pdf_locate()
        {
            return View();
        }
        public ActionResult xml_locate()
        {
            return View();
        }
    }
}
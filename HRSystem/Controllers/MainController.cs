using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRSystem.Controllers
{
    public class MainController : Controller
    {
        LeftDAO leftDAO = new LeftDAO();    
        // GET: Main
        public ActionResult Index()
        {
            //var users = TempData.Peek("users");
            return View();
        }
        public  ActionResult left()
        {
            return View();

        }
        public async Task<ActionResult> leftFind()
        {
            Users_XJ users = (Users_XJ)Session["users"];
            List<Jurisdiction_s> list = await leftDAO.GetTress(users.u_id);
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> RUFind()
        {
            Users_XJ users = (Users_XJ)Session["users"];
        UserRolesFenye userRolesFenye = await leftDAO.RUFind(users.u_id);
            return Json(userRolesFenye, JsonRequestBehavior.AllowGet);

        }
        public ActionResult main()
        {
            return View();
        }
        public ActionResult top()
        {
            return View();
        }
    }
}
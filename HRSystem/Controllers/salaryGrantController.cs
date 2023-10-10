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
    public class salaryGrantController : Controller
    {
        // GET: salaryGrant
        salary_grantXJ_DAO sgDAO =new salary_grantXJ_DAO();
        public ActionResult register_locate()
        {
            return View();
        }
        public ActionResult check_list()
        {
            return View();
        }
        public ActionResult query_locate()
        {
            return View();
        }

        public async Task<ActionResult> check(string str)
        {
            IEnumerable<human_file_XJ> list = await sgDAO.HFFind_FH(str);
            Users_XJ users = (Users_XJ)Session["users"];
            ViewBag.us = users;
            ViewBag.s = list;
            return View();
        }
        public async Task<ActionResult> SSDFindByid(string sid)
        {
            IEnumerable<salary_standard_details> list = await sgDAO.SSDFindByid(sid);
            return Json(list, JsonRequestBehavior.AllowGet); ;

        }


        public async Task<ActionResult> query(string str,string sgid)
        {
            IEnumerable<human_file_XJ> list = await sgDAO.HFFind_FH(str);
            salary_grantXJ sg = await sgDAO.SGFindRegister(sgid);
            ViewBag.s = list;
            ViewBag.sg = sg;
            return View();
        }
        

        public async Task<ActionResult> register_list(string str)
        {
            IEnumerable<human_file_XJ> list = await sgDAO.SGFind(str);
            ViewBag.s = list;
            return View();
        }
        public async Task<ActionResult> register_commit(string str)
        {
            Users_XJ users = (Users_XJ)Session["users"];
            ViewBag.us = users;
            IEnumerable<human_file_XJ> list = await sgDAO.HFFind(str);
            ViewBag.s = list;
            return View();
        }

        [HttpPost]
        public async Task<int> SG_Add(salary_grantXJ sg,salary_grant_detailsXJ sgd,int count)
        {
            return await sgDAO.SG_Add(sg,sgd,count);
        }

        public async Task<ActionResult> SGFindFenye(FenYe<salary_grantXJ> fenYe ,string str)
        {
            fenYe = await sgDAO.SGFindFenye(fenYe, str);
          
            return Json(fenYe, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public async Task<int> SG_Update(salary_grantXJ sg, salary_grant_detailsXJ sgd, int count)
        {
            return await sgDAO.SG_Update(sg, sgd, count);
        }


        public ActionResult query_list(string str)
        {
            ViewBag.s = str;
            return View();
        }
    }
}
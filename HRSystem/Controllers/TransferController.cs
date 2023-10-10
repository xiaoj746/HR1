using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRSystem.Controllers
{
    public class TransferController : Controller
    {
        major_change_DAO mcDAO = new major_change_DAO();
        salary_grantXJ_DAO sgDAO =new salary_grantXJ_DAO();

        // GET: Transfer
        public ActionResult register_locate()
        {
            return View();
        }

        public async Task<ActionResult> RLFind()
        {
            IEnumerable<config_file_first_kind_cader> list = await mcDAO.RLFind();
            return Json(list, JsonRequestBehavior.AllowGet); ;

        }
        public ActionResult register_list(string str)
        {
            ViewBag.s=str;
            return View();
        }
        public async Task<ActionResult> HFFindFenye(FenYe<human_file_XJ> fenYe, string str)
        {
            fenYe = await sgDAO.HFFindFenye(fenYe, str);

            return Json(fenYe, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> register_success(string hid)
        {
            ViewBag.s = await mcDAO.HFFindByid(hid);
            ViewBag.y = await mcDAO.CM_FindChidern();
            ViewBag.u = await mcDAO.RLFind();
            ViewBag.o = await mcDAO.MS_SSFind();
            Users_XJ users = (Users_XJ)Session["users"];
            ViewBag.us = users;
            return View();
        }
        public async Task<ActionResult> MC_Add(major_change mc)
        {           
            int res = await mcDAO.MC_Add(mc);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult check_list()
        {
            return View();
        }
        public ActionResult list(string str)
        {
            ViewBag.s = str;
            return View();
        }

        public async Task<ActionResult> detail(int mid)
        {
            ViewBag.s = await mcDAO.MCFindBymid(mid);
            return View();
        }
        

        public async Task<ActionResult> MCFind(FenYe<major_change> fenYe, string str)
        {
            fenYe = await mcDAO.MCFindFenye(fenYe,str);
            return Json(fenYe, JsonRequestBehavior.AllowGet); ;

        }
        public async Task<ActionResult> check(string hid)
        {
            ViewBag.s = await mcDAO.MCFindByid(hid);
            ViewBag.y = await mcDAO.CM_FindChidern();
            ViewBag.u = await mcDAO.RLFind();
            ViewBag.o = await mcDAO.MS_SSFind();
            Users_XJ users = (Users_XJ)Session["users"];
            ViewBag.us = users;
            return View();
        }
        public async Task<ActionResult> MC_Update(major_change mc)
        {
            int res = await mcDAO.MC_Update(mc);
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> locate()
        {
            ViewBag.y = await mcDAO.CM_FindChidern();
            ViewBag.u = await mcDAO.RLFind();
            return View();
        }
    }
}
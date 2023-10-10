using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAO;
using Model;

namespace HRSystem.Controllers
{
    public class SalaryCriterionController : Controller
    {
        // GET: SalaryCriterion
        salary_standard_details_DAO ssdDAO = new salary_standard_details_DAO();
        salary_standard_DAO ssDAO = new salary_standard_DAO();
        Users_XJ_DAO userDAO =new Users_XJ_DAO();
        public async Task<ActionResult> salarystandard_register()
        {
            IEnumerable<Users_XJ> list = await userDAO.Users_Find();
            ViewBag.y = list;
            Users_XJ users = (Users_XJ)Session["users"];
            ViewBag.us = users;

            return View();
        }

        /// <summary>
        /// 薪酬标准登记
        /// </summary>
        /// <param name="ssd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SSD_Add(salary_standard_details_S ssd)
        {
            return  await ssdDAO.SSD_Add(ssd);
        }
        
        public ActionResult salarystandard_check_list()
        {
            return View();
        }

        /// <summary>
        /// 查I薪酬标准登记
        /// </summary>
        /// <param name="fenYe"></param>
        /// <returns></returns>
        public async Task<ActionResult> SS_Find(FenYe<salary_standard> fenYe,string str)
        {
           FenYe<salary_standard> list = await ssDAO.SS_Find(fenYe,str);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        /// <summary>
        /// 查薪酬组成和I薪酬标准登记单个信息
        /// </summary>
        /// <param name="fenYe"></param>
        /// <returns></returns>
        public async Task<ActionResult> salarystandard_check(string sid)
        {
            IEnumerable<salary_standard_details> ssd = await ssDAO.SSD_Find(sid);
            IEnumerable<Users_XJ> list = await userDAO.Users_Find();
            salary_standard ss = await ssDAO.SS_FindByid(sid);
            Users_XJ users = (Users_XJ)Session["users"];
            ViewBag.us = users;
            ViewBag.s = ssd;
            ViewBag.y = ss;
            ViewBag.u = list;
            return View();

        }

        /// <summary>
        /// 薪酬标准登记复核
        /// </summary>
        /// <param name="ssd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SS_Update(salary_standard ss,string salary,string item_id,string count)
        {
            return  await ssDAO.SS_Update(ss,salary,item_id,count);
          //  return await ssdDAO.SSD_Add(ssd);
        }

        public async Task<ActionResult> salarystandard_query(string sid)
        {
            IEnumerable<salary_standard_details> ssd = await ssDAO.SSD_Find(sid);
            salary_standard ss = await ssDAO.SS_FindByid(sid);
            ViewBag.s = ssd;
            ViewBag.y = ss;
            return View();
        }


        public ActionResult salarystandard_query_locate()
        {
            return View();
        }
        public ActionResult salarystandard_query_list(string str)
        {
            //FenYe<salary_standard> fenYe = new FenYe<salary_standard> {
            //    CurrentPage = 1,
            //    PageSize = 3
            //};
            // fenYe = await ssDAO.SS_Find(fenYe, str);
            ViewBag.s = str;
            return View();
        }
        public ActionResult salarystandard_change_locate()
        {
            return View();
        }
        public ActionResult salarystandard_change_list(string str)
        {
            ViewBag.s = str;
            return View();
        }

        public async Task<ActionResult> salarystandard_change(string sid)
        {
            IEnumerable<salary_standard_details> ssd = await ssDAO.SSD_Find(sid);
            salary_standard ss = await ssDAO.SS_FindByid(sid);
            IEnumerable<Users_XJ> list = await userDAO.Users_Find();
            Users_XJ users = (Users_XJ)Session["users"];
            ViewBag.us = users;
            ViewBag.s = ssd;
            ViewBag.y = ss;
            ViewBag.u = list ;
            return View();
        }
        [HttpPost]
        public async Task<int> Change_Update(salary_standard ss, string salary, string item_id, string count)
        {
            return await ssDAO.Change_Update(ss, salary, item_id, count);
            //  return await ssdDAO.SSD_Add(ssd);
        }

        





    }
}
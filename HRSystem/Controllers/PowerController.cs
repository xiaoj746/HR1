using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HRSystem.Controllers
{
    public class PowerController : Controller
    {
        Users_XJ_DAO userDAO = new Users_XJ_DAO();
        RolesDAO rDAO = new RolesDAO();
        // GET: Power
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> User_Find()
        {
            IEnumerable<Users_XJ> list = await userDAO.Users_Find();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult user_list()
        {
            return View();
        }
        public ActionResult user_add()
        {
            return View();
        }
        public ActionResult right_list()
        {
            return View();
        }
        public ActionResult right_add()
        {
            return View();
        }
        public ActionResult right_list_information(int rid)
        {
            ViewBag.s= rid; 
            return View();
        }
        

        public async Task<ActionResult> user_edit(int uid)
        {
            UserRolesFenye user = await userDAO.UserRolesByid(uid);
            IEnumerable<RolesXJ> list = await userDAO.RolesFind();
            ViewBag.list = list;
            ViewBag.s = user;
            return View();
        }

        public async Task<ActionResult> RolesFind()
        {
            IEnumerable<RolesXJ> list = await userDAO.RolesFind();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<int> userAdd(UserRolesFenye user)
        {
            return await userDAO.userAdd(user); ;
        }
        public async Task<int> userUpdate(UserRolesFenye user)
        {
            return await userDAO.userUpdate(user); ;
        }

        public async Task<int> userDelete(int uid)
        {
            return await userDAO.userDelete(uid); ;
        }

        public async Task<ActionResult> UserFenye_Find(FenYe<UserRolesFenye> fenYe, string str)
        {
            fenYe = await userDAO.UserFenye_Find(fenYe,str);
          return Json(fenYe, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Roles_Find(FenYe<RolesXJ> fenYe, string str)
        {
            fenYe = await rDAO.Roles_Find(fenYe, str);
            return Json(fenYe, JsonRequestBehavior.AllowGet);
        }

        public async Task<int> roles_add(RolesXJ  rolesXJ)
        {
            return await rDAO.roles_add(rolesXJ); ;
        }
        public async Task<int> rolesDelete(int rid)
        {
            return await rDAO.rolesDelete(rid); ;
        }
        public async Task<ActionResult> Jurisdiction_Find()
        {
             List<Jurisdiction_s> list =await rDAO.Jurisdiction_Find();
            return Json(list, JsonRequestBehavior.AllowGet); ;
        }
        public async Task<ActionResult> getJurisByid(int rid)
        {
            int[]  ri= await rDAO.getJurisByid(rid);
            return Json(ri, JsonRequestBehavior.AllowGet); ;
        }
        public async Task<ActionResult> getJuris(int rid)
        {
         RolesXJ roles = await rDAO.getJuris(rid);
            return Json(roles, JsonRequestBehavior.AllowGet); ;
        }

        public async Task<int> roles_update(RolesXJ rolesXJ,string JuriID)
        {
            return await rDAO.roles_update(rolesXJ,JuriID); ;
        }
        
    }
}
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
    public class InterviewController : Controller
    {
        Engage_resumeDAO erd=new Engage_resumeDAO();
        Engage_interviewDAO eid=new Engage_interviewDAO();
        // GET: Interview
        public ActionResult Interview_list()
        {

            return View();
        }
        public async Task<ActionResult> yxjl(int pageSize,int currentPage)
        {
            FenYe<Engage_resume> fen =await erd.yxjl(pageSize,currentPage);
            return Json(fen, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Interview_register(int id)
        {
            Engage_resume engr = await erd.msdj(id);
            ViewBag.tp = engr.Human_picture;
            ViewData.Model = engr;        
            IEnumerable<Users_XJ> name = await erd.cxname();
            ViewBag.List_Key1 = new SelectList(name.ToList(), "u_true_name", "u_true_name").AsEnumerable();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Interview_register(Engage_resume e)
        {
            await erd.xgzt(e);
            int result= await eid.msinsert(e);
            if (result>0)
            {
                return Content("<script>alert('登记成功！');location.href='/Interview/Interview_list'</script>");
            }
            else
            {
                return Content("<script>alert('登记失败！');location.href='/Interview/Interview_register?id="+e.Res_id+"'</script>");
            }
        }
        public ActionResult sift_list()
        {
            return View();
        }
        public async Task<ActionResult> mssx(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fen = await erd.mssx(pageSize, currentPage);
            return Json(fen, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> interview_sift(int id)
        {
            Engage_resume e = await erd.mssx(id);
            ViewBag.tp = e.Human_picture;
            ViewData.Model = e;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> interview_sift(Engage_resume e)
        {
            if (e.Check_comment == "删除简历")
            {
                int result=await eid.msdelete(e);
                if (result>0)
                {
                    return Content("<script>alert('操作成功！');location.href='/Interview/sift_list'</script>");
                }
                else
                {
                    return Content("<script>alert('失败！');location.href='/Interview/sift_list'</script>");
                }
            }
            else
            {
                await erd.xglysq(e);
                int result=await eid.msupdate(e);
                if (result > 0)
                {
                    return Content("<script>alert('操作成功！');location.href='/Interview/sift_list'</script>");
                }
                else
                {
                    return Content("<script>alert('失败！');location.href='/Interview/sift_list'</script>");
                }
            }
            
        }
    }
}
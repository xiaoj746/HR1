using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;
using System.Web.UI;
using System.Xml.Linq;

namespace HRSystem.Controllers
{
    public class PositionController : Controller
    {
        Engage_major_releaseDAO emrd= new Engage_major_releaseDAO();
        Config_major_kindDAO cmk=new Config_major_kindDAO();
        Config_majorDAO cmd=new Config_majorDAO();
        Engage_resumeDAO erd=new Engage_resumeDAO();
        //I查询
        public async Task GetDropList()
        {
            IEnumerable<config_file_first_kind> I = await emrd.Iselect();
            SelectList selectListItems = new SelectList(I, "[first_kind_name]", "[first_kind_name]");
            ViewBag.s = selectListItems;
        }
         //實現二級連動
        public async Task<JsonResult> IISelect(string name)
        {
            IEnumerable<config_file_second_kind> IIcx = await emrd.IIselect(name);
            return Json(IIcx, JsonRequestBehavior.AllowGet);
        }
        
        public async Task<JsonResult> major(string name)
        {
            IEnumerable<Config_major> IIcx = await cmd.Config_majorSelectIDAsync(name);
            return Json(IIcx, JsonRequestBehavior.AllowGet);
        }
        //實現三級聯動
        public async Task<JsonResult> IIISelect(string name,string name2)
        {
            IEnumerable<config_file_third_kind> IIIcx = await emrd.IIIselect(name,name2);
            return Json(IIIcx, JsonRequestBehavior.AllowGet);
        }
        
        public async Task<ActionResult> position_register()
        {
            IEnumerable<config_file_first_kind> I = await emrd.Iselect();
            ViewBag.List_Key1 = new SelectList(I.ToList(), "first_kind_name", "first_kind_name").AsEnumerable(); 
            //Key2、Key3 赋空值
            List<config_file_third_kind> selectlist = new List<config_file_third_kind>();
            ViewBag.List_Key2 = new SelectList(selectlist, "second_kind_name", "second_kind_name").AsEnumerable();
            ViewBag.List_Key3 = new SelectList(selectlist, "third_kind_name", "third_kind_name").AsEnumerable();
             IEnumerable<Config_major_kind> major = await cmk.Config_major_kindSelectAsync();
             ViewBag.s = new SelectList(major.ToList(), "major_kind_name", "major_kind_name").AsEnumerable();
            List<Config_major_kind> selectlist1 = new List<Config_major_kind>();
            ViewBag.majorkey = new SelectList(selectlist1, "major_name", "major_name").AsEnumerable();
            //姓名
            IEnumerable<Users_XJ> name = await erd.cxname();
            ViewBag.List_Key6 = new SelectList(name.ToList(), "u_true_name", "u_true_name").AsEnumerable();
            return View();
        }
        //职位发布
        [HttpPost]
        public async Task<ActionResult> position_register(Engage_major_release e)
        {
            int result= await emrd.Engage_major_releaseInsertAsync(e);
            if (result>0)
            {
                return Content("<script>alert('添加成功！');location.href='/Position/position_change_update'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！');location.href='/Position/position_register'</script>");
            }
        }
        //职位发布变更查询
        public   ActionResult position_change_update()
        {
          
            return  View();
        }
        public async Task<ActionResult> position_change_updatecx(FenYe<Engage_major_release> fenYe)
        {

            fenYe = await emrd.Engage_major_releaseSelectAsync(fenYe);
            return Json(fenYe, JsonRequestBehavior.AllowGet);
        }
        //职位发布变更  删除
        public async Task<int> Engage_major_release_Delete(int id)
        {

            return await emrd.Engage_major_releaseDeleteAsync(id);

        }
        //总条数
        public async Task<ActionResult> position_change_updatezts()
        {

            IEnumerable<Engage_major_release> cx = await emrd.Engage_major_releaseSelectztsAsync();
            return Json(cx, JsonRequestBehavior.AllowGet);
        }
        //职位发布变更 修改
        [HttpGet]
        public async Task<ActionResult>position_change_updatexg(int id,string name1,string name2,string name3)
        {
   
            ViewData.Model=await emrd.Engage_major_releaseSelectidAsync(id);
            //姓名
            IEnumerable<Users_XJ> name = await erd.cxname();
            ViewBag.List_Key6 = new SelectList(name.ToList(), "u_true_name", "u_true_name").AsEnumerable();
            IEnumerable<config_file_first_kind> I = await emrd.Iselect();
            ViewBag.List_Key1 = new SelectList(I.ToList(), "first_kind_name", "first_kind_name").AsEnumerable();
            IEnumerable<config_file_second_kind> II = await emrd.IIselect(name1);
            ViewBag.List_Key2 = new SelectList(II.ToList(), "second_kind_name", "second_kind_name").AsEnumerable();
            IEnumerable<config_file_third_kind> III = await emrd.IIIselect(name1,name2);
            ViewBag.List_Key3 = new SelectList(III.ToList(), "third_kind_name", "third_kind_name").AsEnumerable();
            IEnumerable<Config_major_kind> major_kind = await cmk.Config_major_kindSelectAsync();
            ViewBag.List_Key4 = new SelectList(major_kind.ToList(), "major_kind_name", "major_kind_name").AsEnumerable();
            IEnumerable<Config_major> major = await cmd.Config_majorSelectIDAsync(name3);
            ViewBag.List_Key5 = new SelectList(major.ToList(), "major_name", "major_name").AsEnumerable();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> position_change_updatexg(Engage_major_release e)
        {
            int result = await emrd.Engage_major_releaseUpdateAsync(e);
            if (result > 0)
            {
                return Content("<script>alert('提交成功！');location.href='/Position/position_change_update'</script>");

            }
            else
            {
                return Content("<script>alert('提交失败！');location.href='/Position/position_change_updatexg?id=" + e.Mre_id + "'</script>");
                //return RedirectToAction("TreaUpdate",new { id = trea.id});
            }

        }
        //职位发布查询 页面
        public  ActionResult position_release_searchAsync()
        {
            return View();
        }
        public async Task<ActionResult> position_release_searchsqAsync(int id, string name1, string name2, string name3)
        {
            ViewData.Model = await emrd.Engage_major_releaseSelectidAsync(id);
            IEnumerable<config_file_first_kind> I = await emrd.Iselect();
            ViewBag.List_Key1 = new SelectList(I.ToList(), "first_kind_name", "first_kind_name").AsEnumerable();
            IEnumerable<config_file_second_kind> II = await emrd.IIselect(name1);
            ViewBag.List_Key2 = new SelectList(II.ToList(), "second_kind_name", "second_kind_name").AsEnumerable();
            IEnumerable<config_file_third_kind> III = await emrd.IIIselect(name1, name2);
            ViewBag.List_Key3 = new SelectList(III.ToList(), "third_kind_name", "third_kind_name").AsEnumerable();
            IEnumerable<Config_major_kind> major_kind = await cmk.Config_major_kindSelectAsync();
            ViewBag.List_Key4 = new SelectList(major_kind.ToList(), "major_kind_name", "major_kind_name").AsEnumerable();
            IEnumerable<Config_major> major = await cmd.Config_majorSelectIDAsync(name3);
            ViewBag.List_Key5 = new SelectList(major.ToList(), "major_name", "major_name").AsEnumerable();
            return View();
        }
        public async Task<ActionResult> position_release_searchsqxgAsync(Engage_major_release e)
        {
            int result = await emrd.Engage_major_releaseUpdateAsync(e);
            if (result > 0)
            {
                return Content("<script>alert('提交成功！');location.href='/Position/position_release_searchAsync'</script>");

            }
            else
            {
                return Content("<script>alert('提交失败！');location.href=''/Position/position_release_searchsqAsync?id=" + e.Mre_id + "&&name1='"+e.First_kind_name+"&&name2="+e.Second_kind_name+"&&name3="+e.Third_kind_name+"</script>");
                //return RedirectToAction("TreaUpdate",new { id = trea.id});
            }
        }
    }
}
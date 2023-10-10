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
    public class HumanResourcesController : Controller
    {
        Config_major_kindDAO cmkd = new Config_major_kindDAO();
        Config_majorDAO cmd = new Config_majorDAO();
        Engage_major_releaseDAO emrd=new Engage_major_releaseDAO();
        Config_public_charDAO cpcd = new Config_public_charDAO();
        Config_public_charDAO cpc = new Config_public_charDAO();
        Human_fileDAO hfd=new Human_fileDAO();
        Engage_resumeDAO erd=new Engage_resumeDAO();

        // GET: HumanResources

        public ActionResult Human_list()
        {
            return View();
        }

        public async Task<ActionResult> Human_register(int id,string majorkind)
        {
            Engage_resume e = await erd.msdj(id);
            Users_XJ users = (Users_XJ)Session["users"];
            e.Register2 = users.u_name;
            ViewData.Model =e;
            ViewBag.tp = e.Human_picture; 
            IEnumerable<config_file_first_kind> I = await emrd.Iselect();
            ViewBag.List_Key13 = new SelectList(I.ToList(), "first_kind_name", "first_kind_name").AsEnumerable();
            //Key2、Key3 赋空值
            List<config_file_third_kind> selectlist1 = new List<config_file_third_kind>();
            ViewBag.List_Key14 = new SelectList(selectlist1, "second_kind_name", "second_kind_name").AsEnumerable();
            ViewBag.List_Key15 = new SelectList(selectlist1, "third_kind_name", "third_kind_name").AsEnumerable();
            //职位分类
            IEnumerable<Config_major_kind> major_Kinds = await cmkd.Config_major_kindSelectAsync();
            ViewBag.List_Key1 = new SelectList(major_Kinds.ToList(), "Major_kind_name", "Major_kind_name").AsEnumerable();
            //职位名称
            IEnumerable<Config_major> selectlist =await erd.Config_majorSelectAsync(majorkind);
            ViewBag.List_Key2 = new SelectList(selectlist, "major_name", "major_name").AsEnumerable();
            //国籍
            IEnumerable<Config_public_char> guoji = await cpc.Config_public_charSelectAsync("国籍");
            ViewBag.List_Key3 = new SelectList(guoji.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //民族
            IEnumerable<Config_public_char> 民族 = await cpc.Config_public_charSelectAsync("民族");
            ViewBag.List_Key4 = new SelectList(民族.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //宗教信仰
            IEnumerable<Config_public_char> 宗教信仰 = await cpc.Config_public_charSelectAsync("宗教信仰");
            ViewBag.List_Key5 = new SelectList(宗教信仰.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //政治面貌
            IEnumerable<Config_public_char> 政治面貌 = await cpc.Config_public_charSelectAsync("政治面貌");
            ViewBag.List_Key6 = new SelectList(政治面貌.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //教育年限
            IEnumerable<Config_public_char> 教育年限 = await cpc.Config_public_charSelectAsync("教育年限");
            ViewBag.List_Key7 = new SelectList(教育年限.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //学历
            IEnumerable<Config_public_char> 学历 = await cpc.Config_public_charSelectAsync("学历");
            ViewBag.List_Key8 = new SelectList(学历.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //专业
            IEnumerable<Config_public_char> 专业 = await cpc.Config_public_charSelectAsync("专业");
            ViewBag.List_Key9 = new SelectList(专业.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //爱好
            IEnumerable<Config_public_char> 爱好 = await cpc.Config_public_charSelectAsync("爱好");
            ViewBag.List_Key10 = new SelectList(爱好.ToList(), "attribute_name", "attribute_name").AsEnumerable();

            //特长
            IEnumerable<Config_public_char> 特长 = await cpc.Config_public_charSelectAsync("特长");
            ViewBag.List_Key11 = new SelectList(特长.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //职称
            IEnumerable<Config_public_char> 职称 = await cpc.Config_public_charSelectAsync("职称");
            ViewBag.List_Key12 = new SelectList(职称.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //薪资
            IEnumerable<salary_standard> xz = await hfd.cxxz();
            ViewBag.List_Key16 = new SelectList(xz.ToList(), "standard_name", "standard_name").AsEnumerable();

       
            return View();
            
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
        public async Task<JsonResult> IIISelect(string name, string name2)
        {
            IEnumerable<config_file_third_kind> IIIcx = await emrd.IIIselect(name, name2);
            return Json(IIIcx, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> Human_register(Engage_resume e)
        {
            int result = await hfd.Human_registerInsert(e);
            
            if (result > 0)
            {
                return Content("<script>alert('添加成功！');location.href='/HumanResources/check_list'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！')</script>");
            }
        }
        public ActionResult check_list()
        {
            return View();
        }
        public async Task<ActionResult> lysqAsync(int pageSize, int currentPage)
        {
            FenYe<Human_file> fen = await hfd.rlfh(pageSize, currentPage);
            return Json(fen, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> human_check(int id)
        {
            //国籍
            IEnumerable<Config_public_char> guoji = await cpc.Config_public_charSelectAsync("国籍");
            ViewBag.List_Key3 = new SelectList(guoji.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //民族
            IEnumerable<Config_public_char> 民族 = await cpc.Config_public_charSelectAsync("民族");
            ViewBag.List_Key4 = new SelectList(民族.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //宗教信仰
            IEnumerable<Config_public_char> 宗教信仰 = await cpc.Config_public_charSelectAsync("宗教信仰");
            ViewBag.List_Key5 = new SelectList(宗教信仰.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //政治面貌
            IEnumerable<Config_public_char> 政治面貌 = await cpc.Config_public_charSelectAsync("政治面貌");
            ViewBag.List_Key6 = new SelectList(政治面貌.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //教育年限
            IEnumerable<Config_public_char> 教育年限 = await cpc.Config_public_charSelectAsync("教育年限");
            ViewBag.List_Key7 = new SelectList(教育年限.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //学历
            IEnumerable<Config_public_char> 学历 = await cpc.Config_public_charSelectAsync("学历");
            ViewBag.List_Key8 = new SelectList(学历.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //专业
            IEnumerable<Config_public_char> 专业 = await cpc.Config_public_charSelectAsync("专业");
            ViewBag.List_Key9 = new SelectList(专业.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //爱好
            IEnumerable<Config_public_char> 爱好 = await cpc.Config_public_charSelectAsync("爱好");
            ViewBag.List_Key10 = new SelectList(爱好.ToList(), "attribute_name", "attribute_name").AsEnumerable();

            //特长
            IEnumerable<Config_public_char> 特长 = await cpc.Config_public_charSelectAsync("特长");
            ViewBag.List_Key11 = new SelectList(特长.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //职称
            IEnumerable<Config_public_char> 职称 = await cpc.Config_public_charSelectAsync("职称");
            ViewBag.List_Key12 = new SelectList(职称.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //薪资
            IEnumerable<salary_standard> xz = await hfd.cxxz();
            ViewBag.List_Key16 = new SelectList(xz.ToList(), "standard_name", "standard_name").AsEnumerable();
            Human_file e = await hfd.fhcx(id);
            ViewBag.tp = e.Human_picture;
            ViewData.Model=e;
            Users_XJ users = (Users_XJ)Session["users"];
            e.Checker = users.u_name;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> human_check(Engage_resume e)
        {
            int result=await hfd.humanupdate(e);
            if (result>0)
            {
                return Content("<script>alert('复核成功！');location.href='/HumanResources/check_list'</script>");
            }
            else
            {
                return Content("<script>alert('复核失败！');location.href='/HumanResources/human_check?id=" + e.Huf_id + "'</script>");
            }
        }
    }
}
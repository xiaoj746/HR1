using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace HRSystem.Controllers
{
    public class ResumeController : Controller
    {
        Config_major_kindDAO cmkd = new Config_major_kindDAO();
        Config_majorDAO cmd = new Config_majorDAO();
        Engage_resumeDAO erd = new Engage_resumeDAO();
        Config_public_charDAO cpcd = new Config_public_charDAO();
        Config_public_charDAO cpc=new Config_public_charDAO();
        Engage_major_releaseDAO emrd=new Engage_major_releaseDAO();

        // GET: Resume
        //简历新增
        [HttpGet]
        public async Task<ActionResult> register(int id,string name)
        {
            ViewData.Model = await emrd.Engage_major_releaseSelectkindidAsync(id);
            //职位分类
            IEnumerable<Config_major_kind> major_Kinds = await cmkd.Config_major_kindSelectAsync();
            ViewBag.List_Key1 = new SelectList(major_Kinds.ToList(), "Major_kind_name", "Major_kind_name").AsEnumerable();
            //职位名称
            IEnumerable<Config_major> selectlist = await cmd.Config_majorSelectAsync(name);
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
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> register(Engage_resume e)
        {
            int result = await erd.Engage_resumeinsert(e);
            if (result > 0)
            {
                return Content("<script>alert('添加成功！');location.href='/Position/position_change_update'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！');location.href='/Position/position_register'</script>");
            }
        }
        public async Task<ActionResult> majorSelect(string name)
        {
            IEnumerable<Config_major>  cx=await erd.Config_majorSelectAsync(name);
            return Json(cx,JsonRequestBehavior.AllowGet);
        }
        
        //查询简历
        public async Task<ActionResult> cxjl(FenYe<Engage_resume> fenYe)
        {
           fenYe = await erd.select(fenYe);
            return Json(fenYe, JsonRequestBehavior.AllowGet);
        }
        //简历筛选编辑
        public async Task<ActionResult> resume_details(int id)
        {

            Engage_resume e=await erd.selectid(id);
            ViewBag.tp = e.Human_picture;
            ViewData.Model = e;
            //国籍
            IEnumerable<Config_public_char> guoji = await cpc.Config_public_charSelectAsync("国籍");
            ViewBag.List_Key1 = new SelectList(guoji.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //民族
            IEnumerable<Config_public_char> 民族 = await cpc.Config_public_charSelectAsync("民族");
            ViewBag.List_Key2 = new SelectList(民族.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //宗教信仰
            IEnumerable<Config_public_char> 宗教信仰 = await cpc.Config_public_charSelectAsync("宗教信仰");
            ViewBag.List_Key3 = new SelectList(宗教信仰.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //政治面貌
            IEnumerable<Config_public_char> 政治面貌 = await cpc.Config_public_charSelectAsync("政治面貌");
            ViewBag.List_Key4 = new SelectList(政治面貌.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //教育年限
            IEnumerable<Config_public_char> 教育年限 = await cpc.Config_public_charSelectAsync("教育年限");
            ViewBag.List_Key6 = new SelectList(教育年限.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //学历
            IEnumerable<Config_public_char> 学历 = await cpc.Config_public_charSelectAsync("学历");
            ViewBag.List_Key5 = new SelectList(学历.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //专业
            IEnumerable<Config_public_char> 专业 = await cpc.Config_public_charSelectAsync("专业");
            ViewBag.List_Key7 = new SelectList(专业.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //爱好
            IEnumerable<Config_public_char> 爱好 = await cpc.Config_public_charSelectAsync("爱好");
            ViewBag.List_Key9 = new SelectList(爱好.ToList(), "attribute_name", "attribute_name").AsEnumerable();

            //特长
            IEnumerable<Config_public_char> 特长 = await cpc.Config_public_charSelectAsync("特长");
            ViewBag.List_Key8 = new SelectList(特长.ToList(), "attribute_name", "attribute_name").AsEnumerable();
            //姓名
            IEnumerable<Users_XJ> name = await erd.cxname();
            ViewBag.List_Key10 = new SelectList(name.ToList(), "u_true_name", "u_true_name").AsEnumerable();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> resume_details(Engage_resume e)
        {
            int result = await  erd.update(e);
            if (result == 1)
            {
                return Content("<script>alert('复核成功！');location.href='/Resume/resume_list'</script>");
            }
            else
            {
                return Content("<script>alert('复核失败！');location.href='/Resume/resume_details?id='"+e.Res_id+"</script>");
            }
        }
        public async Task<ActionResult> resume_choose()
        {
            IEnumerable<Config_major_kind> major_kind = await cmkd.Config_major_kindSelectAsync();
            ViewBag.List_Key1 = new SelectList(major_kind.ToList(), "major_kind_name", "major_kind_name").AsEnumerable();
            List<Config_major> selectlist = new List<Config_major>();
            ViewBag.List_Key2 = new SelectList(selectlist, "major_name", "major_name").AsEnumerable();
            return View();
        }

        public async  Task<ActionResult> resume_choose1(int PageSize,int CurrentPage,string major_kind_name,string major_name,string remark,string begintime,string endtime,int a)
        {
            FenYe<Engage_resume> fy = await erd.tjselect(PageSize,CurrentPage,major_kind_name,major_name,remark,begintime,endtime,a);
            //return Content("<script>location.href='/Resume/resume_list?pageSize=2&&currentPage=1&&major_kind_name=" + e.Major_kind_name + "&&major_name="+e.Major_name + "&&remark="+e.Remark + "&&begintime="+e.begintime+"&&endtime="+e.endtime+"'</script>");
            return Json(fy, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> resume_list(Engage_resume e)
        {
            // ViewData.Model= await erd.tjselect(pageSize, currentPage, major_kind_name, major_name, remark, begintime, endtime);
            ViewData.Model = e;
            return View();
        }
        public async Task<ActionResult> resume_choose2()
        {
            IEnumerable<Config_major_kind> major_kind = await cmkd.Config_major_kindSelectAsync();
            ViewBag.List_Key1 = new SelectList(major_kind.ToList(), "major_kind_name", "major_kind_name").AsEnumerable();
            List<Config_major> selectlist = new List<Config_major>();
            ViewBag.List_Key2 = new SelectList(selectlist, "major_name", "major_name").AsEnumerable();
            return View();
        }
        public ActionResult resume_list2(Engage_resume e)
        {
            ViewData.Model = e;
            return View();
        }
        public ActionResult WSC()
        {
            var file = HttpContext.Request.Files["file"];//获取上传文件对象
                                                         //生成文件名
            string name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //获取后缀名
            string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            string mz = name + ext;
            //相对路径
            string path = $"~/Uploaders/" + mz;
            //绝对路径
            string jpath = Server.MapPath(path);
            //创建上传文件对应的文件夹
            (new FileInfo(jpath)).Directory.Create();
            file.SaveAs(jpath);
            ViewBag.tp = path;
            return Content(mz);

        }

    }
}                                   
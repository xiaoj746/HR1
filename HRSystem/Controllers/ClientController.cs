using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HRSystem.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        //YRX
        Config_major_kindDAO cmkdao = new Config_major_kindDAO();
        Config_majorDAO cmdao = new Config_majorDAO();
        Config_public_charDAO cpcdao = new Config_public_charDAO();
        //XJ
        config_file_first_kind_DAO cffkDAO = new config_file_first_kind_DAO();
        config_file_second_kind_DAO cfskDAO = new config_file_second_kind_DAO();
        config_file_third_kind_DAO cftDAO = new config_file_third_kind_DAO();
        salary_management_settings_DAO smsDAO = new salary_management_settings_DAO();
        config_public_char_DAOXJ cpcDAO = new config_public_char_DAOXJ();
        public ActionResult first_kind()
        {
          
            return View();
        }
        /// <summary>
        /// I级机构显示数据
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> first_kind_Show()
        {
            IEnumerable<config_file_first_kind> list = await cffkDAO.config_file_first_kind_Find();

            SelectList selectListItems = new SelectList(list, "first_kind_id", "first_kind_name");
            ViewBag.s = selectListItems;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// I级机构删除
        /// </summary>
        /// <returns></returns>
        public async Task<int> FKR_Delete(string CFFFKid)
        {
            return await cffkDAO.CFFK_Delete(CFFFKid);
        }


        public ActionResult first_kind_register()
        {
            return View();
        }

        /// <summary>
        /// I级机构添加
        /// </summary>
        /// <returns></returns>
        public async Task<int> FKR_Add(config_file_first_kind cffk)
        {
            return await cffkDAO.CFFK_FindMaxID(cffk);
        }

        public ActionResult first_kind_change()
        {
            return View();
        }

        /// <summary>
        /// 查I级机构单个数据
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> FKR_Update_show(int CFFFKid)
        {
            config_file_first_kind cffk = await cffkDAO.CFFK_Update_show(CFFFKid);
            return Json(cffk, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查I级机构修改
        /// </summary>
        /// <returns></returns>
        public async Task<int> FKR_Update(config_file_first_kind cffk)
        {
            return await cffkDAO.CFFK_Update(cffk);
        }

        public ActionResult second_kind()
        {
            return View();
        }

        /// <summary>
        /// II级机构显示数据
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> second_kind_Show()
        {
            IEnumerable<config_file_second_kind> list = await cfskDAO.config_file_second_kind_Find();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> second_kind_register()
        {
            //查询config_file_first_kind数据显示到下拉框上
            IEnumerable<config_file_first_kind> list = await cffkDAO.config_file_first_kind_Find();
            SelectList selectListItems = new SelectList(list, "first_kind_id", "first_kind_name");
            ViewBag.s = selectListItems;
            return View();
        }

        [HttpPost]
        /// <summary>
        /// II级机构添加
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>
        public async Task<ActionResult> FSK_Add(config_file_second_kind cfsk)
        {
            if (ModelState.IsValid) //后台验证
            {
                int result = await cfskDAO.CFSK_Add(cfsk);
                if (result > 0)
                {
                    return Content("<script>alert('添加成功！');location.href='/Client/second_kind'</script>");

                }
                else
                {
                    return Content("<script>alert('添加失败！');location.href='/Client/second_kind_register'</script>");
                    //return RedirectToAction("TreaUpdate",new { id = trea.id});
                }
            }
            else
            {
                return Content("<script>alert('不能为空！');location.href='/Client/second_kind_register'</script>");

            }
        }



        public async Task<ActionResult> second_kind_change(int cfskID)
        {
            //查II级机构单个数据
            ViewData.Model = await cfskDAO.CFSK_Update_ByidShow(cfskID);
            return View();
        }

        [HttpPost]
        /// <summary>
        /// II级机构修改
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>
        public async Task<ActionResult> FSK_Update(config_file_second_kind cfsk)
        {
            if (ModelState.IsValid) //后台验证
            {
                int result = await cfskDAO.CFSK_Update(cfsk);
                if (result > 0)
                {
                    return Content("<script>alert('修改成功！');location.href='/Client/second_kind'</script>");

                }
                else
                {
                    return Content("<script>alert('修改失败！');location.href='/Client/second_kind_change?cfskID=" + cfsk.fsk_id + "'</script>");
                    //return RedirectToAction("TreaUpdate",new { id = trea.id});
                }
            }
            else
            {
                return Content("<script>alert('不能为空！');location.href='/Client/second_kind_change?cfskID=" + cfsk.fsk_id + "'</script>");

            }
        }

        /// <summary>
        /// II级机构删除
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>
        public async Task<int> FSK_Delete(string fskID)
        {

         return await cfskDAO.CFSK_Delete(fskID);
 
        }


        public ActionResult third_kind()
        {
            return View();
        }

        /// <summary>
        /// III级机构显示数据
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TK_Show()
        {
            IEnumerable<config_file_third_kind> list = await cftDAO.CFTK_Find();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult third_kind_register() { 
           return View();
        }

        public async Task<ActionResult> third_kind_register_Find()
        {
            //查询config_file_first_kind数据显示到下拉框上
            IEnumerable<config_file_first_kind_cader> list = await cftDAO.CFTK_FindChidern();
            return Json(list, JsonRequestBehavior.AllowGet);
            //SelectList selectListItems = new SelectList(list, "first_kind_id", "first_kind_name");
            ////查询config_file_first_kind数据显示到下拉框上
            //IEnumerable<config_file_second_kind> list2 = await cfskDAO.config_file_second_kind_Find();
            //SelectList selectListItems2 = new SelectList(list2, "second_kind_id", "second_kind_name");
            //ViewBag.s = selectListItems;
            //ViewBag.y = selectListItems2;
            // return View();
        }

        [HttpPost]
        /// <summary>
        /// III级机构添加
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>
        public async Task<int> FTK_Add(config_file_third_kind cftk)
        {
           
                return await cftDAO.CFTK_Add(cftk);
              
           
        }

        public async Task<ActionResult> third_kind_change(int tkID)
        {
            //查III级机构单个数据
            ViewData.Model = await cftDAO.CFTK_Update_ByidShow(tkID);
            return View();
        }

        [HttpPost]
        /// <summary>
        /// III级机构修改
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>
        public async Task<ActionResult> FTK_Update(config_file_third_kind cftk)
        {

                int result = await cftDAO.CFTK_Update(cftk);
                if (result > 0)
                {
                    return Content("<script>alert('修改成功！');location.href='/Client/third_kind'</script>");

                }
                else
                {
                    return Content("<script>alert('修改失败！');location.href='/Client/third_kind_change?tkID=" + cftk.ftk_id + "'</script>");
                    //return RedirectToAction("TreaUpdate",new { id = trea.id});
                }
            

        }
        /// <summary>
        /// III级机构删除
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>
        public async Task<int> FTK_Delete(int ftkID)
        {

            return await cftDAO.CFTK_Delete(ftkID);

        }

        public ActionResult profession_design()
        {
            return View();
        }

        public ActionResult major_kind()
        {
            return View();
        }
        public ActionResult major()
        {
            return View();
        }
        public ActionResult public_char()
        {
            return View();
        }
        public ActionResult salary_item()
        {
            return View();
        }

        /// <summary>
        /// 薪酬项目设置显示数据
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SMS_Show()
        {
            IEnumerable<config_public_charXJ> list = await cpcDAO.SMS_Find();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 薪酬项目设置删除
        /// </summary>
        /// <returns></returns>
        public async Task<int> SMS_Delete(int pbcID)
        {
            return await cpcDAO.SMS_Delete(pbcID);
        }

        public ActionResult salary_grant_mode()
        {
            return View();
        }

        public ActionResult primary_key()
        {
            return View();
        }




        //YRX

        //职称设置查询
        public async Task<ActionResult> rolescx()
        {
            IEnumerable<Config_public_char> cx = await cpcdao.Config_public_charZCSelectAsync();
            return Json(cx, JsonRequestBehavior.AllowGet);

        }
        //职称设置删除
        public async Task<int> ZCDelete(int id)
        {
            return await cpcdao.Config_public_charDeleteAsync(id);
        }


        //职位分类设置查询
        public async Task<ActionResult> major_kindcx()
        {
            IEnumerable<Config_major_kind> cx = await cmkdao.Config_major_kindSelectAsync();
            return Json(cx, JsonRequestBehavior.AllowGet);

        }

        //职位分类设置添加
        [HttpGet]
        public ActionResult Major_kindAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Major_kindAdd(Config_major_kind c)
        {
            int result = await cmkdao.Config_major_kindInsertAsync(c);
            if (result > 0)
            {
                return RedirectToAction("/major_kind");
            }
            else
            {
                return View(c);
            }
        }
        //职位分类设置删除
        public async Task<int> Major_kindDelete(int id)
        {
            return await cmkdao.Config_major_kindDeleteAsync(id);
        }

        //职位设置查询
        public async Task<ActionResult> majorcx()
        {
            IEnumerable<Config_major> cx = await cmdao.Config_majorSelectAsync();
            return Json(cx, JsonRequestBehavior.AllowGet);

        }
        //职位设置添加
        [HttpGet]
        public async Task<ActionResult> MajorAdd()
        {
            await GetDropList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> MajorAdd(Config_major c)
        {
            int result = await cmdao.Config_majorInsertAsync(c);
            if (result > 0)
            {
                return RedirectToAction("/major");
            }
            else
            {
                return View(c);
            }
        }
        //职位设置中下拉框id查询
        public async Task GetDropList()
        {

            IEnumerable<Config_major_kind> id = await cmkdao.Config_major_kindSelectAsync();
            SelectList selectListItems = new SelectList(id, "major_kind_name", "major_kind_id");
            ViewBag.s = selectListItems;
        }
        //职位设置删除
        public async Task<int> MajorDelete(int id)
        {
            return await cmdao.Config_majorDeleteAsync(id);
        }

        //公共属性设置查询
        public async Task<ActionResult> charcx(FenYe<Config_public_char> fenYe)
        {
            fenYe = await cpcdao.Config_public_charSelectAsync(fenYe);
            return Json(fenYe, JsonRequestBehavior.AllowGet);

        }
        //公告属性添加
        [HttpGet]
        public ActionResult CharAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CharAdd(Config_public_char c)
        {
            int result = await cpcdao.Config_public_charInsertAsync(c);
            if (result > 0)
            {
                return RedirectToAction("public_char");
            }
            else
            {
                return View(c);
            }
        }
        //公告属性删除
        public async Task<int> CharDelete(int id)
        {
            return await cpcdao.Config_public_charDeleteAsync(id);
        }






    }
}
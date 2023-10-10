using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Web.Helpers;
using System.Xml.Linq;
using System.Net;

namespace HRSystem.Controllers
{
    public class EmployController : Controller
    {
        Engage_resumeDAO erd = new Engage_resumeDAO();
        Engage_interviewDAO eid = new Engage_interviewDAO();
        // GET: Employ
        public ActionResult register_list()
        {
            return View();
        }
        public async Task<ActionResult> lysqAsync(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fen = await erd.lysq(pageSize, currentPage);
            return Json(fen, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> register(int id)
        {
            Engage_resume e = await erd.mssx(id);
            ViewBag.tp = e.Human_picture;
            ViewData.Model = e;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> register(Engage_resume e)
        {

            if (e.Pass_checkcomment == "不通过")
            {
                int result = await eid.sqly(e);
                if (result > 0)
                {
                    return Content("<script>alert('登记成功！');location.href='/Employ/register_list'</script>");
                }
                else
                {
                    return Content("<script>alert('登记失败！');location.href='/Employ/register_list'</script>");
                }
            }
            else
            {
                int result = await eid.sqly(e);
                if (result > 0)
                {
                    return Content("<script>alert('登记成功！');location.href='/Employ/register_list'</script>");
                }
                else
                {
                    return Content("<script>alert('登记失败！');location.href='/Employ/register_list'</script>");
                }
            }
        }
        public ActionResult check_list()
        {
            return View();
        }
        public async Task<ActionResult> lysp(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fen = await erd.lysp(pageSize, currentPage);
            return Json(fen, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> check(int id)
        {
            Engage_resume e = await erd.mssx(id);
            ViewData.Model = e;
            ViewBag.tp = e.Human_picture;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> check(Engage_resume e)
        {
            int result = await eid.sply(e);
            if (result > 0)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.To.Add("3123244639@qq.com");

                message.CC.Add("2577187662@qq.com");

                message.From = new MailAddress("2577187662@qq.com", "长沙实力人力资源", System.Text.Encoding.UTF8);
                message.Subject = "录用通知";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = "恭喜你被我公司录取";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = false;
                message.Priority = System.Net.Mail.MailPriority.Normal;

                SmtpClient client = new SmtpClient();

                client.Credentials = new NetworkCredential("2577187662@qq.com", "zlxmeildfevrdiai");


                client.Port = 25;

                client.Host = "Smtp.qq.com";

                try
                {

                    client.Send(message);
                    Console.WriteLine("发送成功");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.Message, "发送邮件出错");
                }

                return Content("<script>alert('登记成功！');location.href='/Employ/check_list'</script>");
            }
            else
            {
                return Content("<script>alert('登记失败！');location.href='/Employ/check_list'</script>");
            }
        }
        public ActionResult list()
        {
            return View();
        }
        public async Task<ActionResult> cxlyAsync(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fen = await erd.cxly(pageSize, currentPage);
            return Json(fen, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> details(int id)
        {
            Engage_resume e = await erd.mssx(id);
            ViewData.Model = e;
            ViewBag.tp = e.Human_picture;
            return View();
        }
    }
}
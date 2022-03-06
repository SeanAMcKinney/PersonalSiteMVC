using PersonalSiteMVC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PersonalSiteMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
            string body = $"You have received an email form <strong>{cvm.Name}</strong>. the email address given was <strong>{cvm.Email}</strong>.<br/><strong>The following message was sent:</strong> {cvm.Message}";

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("administrator@mckinneywebdev.com");
            mm.To.Add(new MailAddress("seanamckinney8@gmail.com"));
            mm.Subject = cvm.Subject;
            mm.Body = body;

            mm.IsBodyHtml = true;
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient smtp = new SmtpClient("mail.mckinneywebdev.com");
            NetworkCredential cred = new NetworkCredential("administrator@mckinneywebdev.com", "eight8ate*");

            smtp.Credentials = cred;
            smtp.Send(mm);
            return Json(cvm);
        }

        public PartialViewResult ContactConfirmation(string name, string email)
        {
            ViewBag.Name = name;
            ViewBag.Email = email;
            return PartialView("ContactConfirmation");
        }

    }//end class
}//end namespace
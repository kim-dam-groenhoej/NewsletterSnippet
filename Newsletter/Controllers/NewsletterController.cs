using Newsletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Newsletter.Controllers
{
    public class AjaxResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }

    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult Index()
        {
            return View();
        }

        private void SendEmail(string toEmail, string title, string content)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            message.From = new System.Net.Mail.MailAddress("testemail@kimdamgroenhoej.dk");
            message.To.Add(new System.Net.Mail.MailAddress(toEmail));

            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = title;
            message.Body = content;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("mail.kimdamgroenhoej.dk", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("testemail@kimdamgroenhoej.dk", "43DDnG!");

            // Allow self signed cerificate
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            client.Send(message);
        }

        public ActionResult SendNewsLetter()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendNewsLetter(NewsletterPost model)
        {
            if (ModelState.IsValid)
            {
                var db = new NewsletterContext();
                
                foreach (var user in db.NewsletterUsers)
                {
                    this.SendEmail(user.Email, model.Title, model.Content);
                }

                TempData["success"] = true;
            }

            return RedirectToAction("SendNewsLetter");
        }

        [HttpPost]
        public ActionResult Signup(NewsletterUser model)
        {
            var response = new AjaxResponse();
            response.Success = true;
            response.Message = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    var db = new NewsletterContext();

                    var result = db.NewsletterUsers.FirstOrDefault(u => u.Email == model.Email);

                    //var result = (from user in db.NewsletterUsers
                    //                where user.Email == model.Email
                    //                select user).FirstOrDefault();

                    if (result != null)
                    {
                        throw new Exception("Email '" + model.Email + "' findes allerede.");
                    }

                    db.NewsletterUsers.Add(model);
                    db.SaveChanges();

                    response.Message = "E-mail er tilføjet";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            } else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        response.Message = error.ErrorMessage + ".<br />";
                    }
                }
            }

            return Json(response);
        }
    }
}
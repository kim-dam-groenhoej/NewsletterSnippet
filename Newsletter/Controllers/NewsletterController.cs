using Newsletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public ActionResult SendNewsLetter(NewsletterPost model)
        {

            TempData["success"] = true;

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
                    using (var db = new NewsletterContext())
                    {
                        var result = (from user in db.NewsletterUsers
                                      where user.Email == model.Email
                                      select user).FirstOrDefault();

                        if (result != null)
                        {
                            throw new Exception("Email '" + model.Email + "' findes allerede.");
                        }

                        db.NewsletterUsers.Add(model);
                        db.SaveChanges();

                        response.Message = "E-mail er tilføjet";
                    }
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
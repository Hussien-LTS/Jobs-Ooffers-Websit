using Jobs_Ooffers_Websit.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Jobs_Ooffers_Websit.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View(db.Categories.ToList());
        }
        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Apply(string Msg)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var job = new ApplyForJob();

            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {

                job.UserId = UserId;
                job.JobId = JobId;
                job.Msg = Msg;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.result = "تمت الاضافة بنجاح";
            }
            else
            {
                ViewBag.result = "لقد تم التقدم لهذه الوظيفة";

            }

            return View();
        }

        [Authorize]
        public ActionResult GetJobByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);

        }
        [Authorize]

        public ActionResult GetJobByPublisher()
        {
            var UserID = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;

            var groubed = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitel = gr.Key,
                              Items = gr
                          };

            return View(groubed.ToList());
        }

        // GET: Roles/Edit/5
        [Authorize]

        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {

            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                job.ApplyDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("GetJobByUser");
            }

            return View(job);


        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {

            // TODO: Add delete logic here
            var jobToDelete = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(jobToDelete);
            db.SaveChanges();
            return RedirectToAction("GetJobByUser");


        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        // GET:
        public ActionResult Contact()
        {


            return View();
        }
        // POST:
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("hussain662536@gmail.com", "H216709303a$");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("hussain662536@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل " + contact.Name + "<br>" +
                             "بريد المرسل" + contact.Email + "<br>" +
                              "عنوان المرسل" + contact.Subject + "<br>" +
                               "نص الرسالة" + contact.Msg;
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }
        // GET:
        public ActionResult Search()
        {
            return View();
        }
        // POST:
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDesc.Contains(searchName));
            return View(result);
        }
    }
}
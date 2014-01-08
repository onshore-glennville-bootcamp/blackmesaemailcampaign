using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using BLL;

namespace BlackMesaEmailCampaign.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/
        public ActionResult Index(SubscribersVM subVM)
        {
            MailService mail = new MailService();
            MailFM fm = mail.GetFM(subVM);
            if (fm.To != null) return View(fm);
            else
            {
                ViewBag.ErrorMessage = "Select some subscribers to send e-mail"; 
                return View(fm); }

        }
        [HttpPost]
        public ActionResult Send(MailFM mailFM)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                if (mailFM.To != null)
                {
                    bool multiplemail = true;
                    List<string> email = new List<string>();
                    while (multiplemail)
                    {
                        string mailadd = "";
                        if (mailFM.To.Contains(','))
                        {
                            int mailindex = mailFM.To.IndexOf(',');
                            mailadd = mailFM.To.Substring(0, mailindex);
                            mailFM.To = mailFM.To.Substring(mailindex + 1);
                            email.Add(mailadd);
                        }
                        else
                        {
                            email.Add(mailFM.To);
                            multiplemail = false;
                        }

                    }
                    foreach (string mails in email)
                    {
                        mail.To.Add(mails);

                        mail.From = new MailAddress("blackmesaemailcampaign@gmail.com");
                        mail.Subject = mailFM.Subject;
                        string Body = mailFM.Body;
                        mail.Body = Body;
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential
                        ("blackmesaemailcampaign", "bootcamp123");// Username and password for accont used for sending emails
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        mail.To.Clear();
                    } return RedirectToAction("ViewSubscribers", "Subscribers");
                }
                else
                {
                    ViewBag.ErrorMessage = "You need to add some subscribers in order to send an email.";
                   // return RedirectToAction("ViewSubscribers", "Subscribers");
                    return View("Index");
                }
            }
            else
            {
                return View("ViewSubscribers");
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL;

namespace BlackMesaEmailCampaign.Controllers
{
    public class SubscribersController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(SubscribersFM subscriber)
        {
            if (subscriber.Email != null)
            {
                UserServices log = new UserServices();
                if (!log.IsExistingSubscriber(subscriber.Email))
                {
                    log.CreateSubscribers(subscriber);
                    ViewBag.ErrorMessage = "Subscriber created";
                }
                else
                {
                    ViewBag.ErrorMessage = "Subscriber email already exists.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Subscriber email required";
            }
            return View();
        }
    }
}
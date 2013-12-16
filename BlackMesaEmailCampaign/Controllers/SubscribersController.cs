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
            UserServices log = new UserServices();
            if (!log.IsExistingSubscriber(subscriber.Email))
            {
                log.CreateSubscribers (subscriber);
                ViewBag.ErrorMessage = "Subscriber Created";
                return View();
            }
            ViewBag.ErrorMessage = "Subscriber email already exist.";
            return View();
        }
    }
}
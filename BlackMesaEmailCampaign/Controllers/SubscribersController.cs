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
            return ViewSubscribers();
        }
        [HttpPost]
        public ActionResult Add(SubscribersFM subscriber)
        {
            UserServices log = new UserServices();
            if (subscriber.Email != null && log.ValidEmail(subscriber.Email))
            {
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
                ViewBag.ErrorMessage = "Subscriber email not valid.";
            }
            return ViewSubscribers();
        }
        [HttpGet]
        public ActionResult ViewSubscribers()
        {
            UserServices userS = new UserServices();
            SubscribersVM subscribersVM = userS.GetAllSubscribers();
            subscribersVM.Subscribers = userS.SortByEmail(subscribersVM.Subscribers);
            return View(subscribersVM);
        }
        [HttpPost]
        public ActionResult ViewSubscribers(SubscribersVM subscribersVM)
        {
            return View(subscribersVM);
        }
    }
}
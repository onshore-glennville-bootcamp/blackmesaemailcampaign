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
            return View();
        }
        public ActionResult ViewSubscribers()
        {
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByEmail(userS.GetAllSubscribers());
            ViewBag.Sort = "Email";
            return View(subscriber);
        }
        public ActionResult ViewSubscribersByEmail(string sort)
        {
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByEmail(userS.GetAllSubscribers());
            if (sort == "Email")
            {
                ViewBag.Sort = "";
                subscriber.Subscribers.Reverse();
                return View("ViewSubscribers", subscriber);
            }
            ViewBag.Sort = "Email";            
            return View("ViewSubscribers", subscriber);
        }
        public ActionResult ViewSubscribersByLastName(string sort)
        {
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByLastName(userS.GetAllSubscribers());
            if (sort == "LastName")
            {
                ViewBag.Sort = "";
                subscriber.Subscribers.Reverse();
                return View("ViewSubscribers", subscriber);
            }
            ViewBag.Sort = "LastName";
            return View("ViewSubscribers", subscriber);
        }
        public ActionResult ViewSubscribersByFirstName(string sort)
        {
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByFirstName(userS.GetAllSubscribers());
            if (sort == "FirstName")
            {
                ViewBag.Sort = "";
                subscriber.Subscribers.Reverse();
                return View("ViewSubscribers", subscriber);
            }
            ViewBag.Sort = "FirstName";
            return View("ViewSubscribers", subscriber);
        }
    }
}
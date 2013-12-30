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
        //Gets Form for Adding Subscribers
        [HttpGet]
        public ActionResult Add()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        //Validates Form for Adding Subscribers, then adds if validated
        [HttpPost]
        public ActionResult Add(SubscribersFM subscriber)
        {
            UserServices log = new UserServices();
            if (subscriber.Email != null && log.ValidEmail(subscriber.Email))
            {
                if (!log.IsExistingSubscriber(subscriber.Email))
                {
                    if (!log.TooLong(subscriber.FirstName) && !log.TooLong(subscriber.LastName))
                    {
                        log.CreateSubscribers(subscriber);
                        ViewBag.ErrorMessage = "Subscriber created";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "First/Last Name cannot be longer than 100 characters.";
                    }
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
        //List Subscribers
        public ActionResult ViewSubscribers()
        {
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.SortByLastName(userS.GetAllSubscribers());
            ViewBag.Sort = "LastName";
            return View(subscriber);
        }
        //Sort list of subscribers by last name, reverse if already sorted by last name
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
        //Sort list of subscribers by first name, reverse if already sorted by first name
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
        //Search Subscribers
        [HttpGet]
        public ActionResult SearchSubscribers()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult SearchSubscribers(string searchString)
        {
            UserServices userS = new UserServices();
            SubscribersVM subscriber = new SubscribersVM();
            subscriber.Subscribers = userS.Search(searchString);
            //and then we finish jumping through hoops...
            return View("ViewSubscribers", subscriber);
        }

        //Gets list of checked Subscribers.  Needs code for emailing them.
        [HttpPost]
        public ActionResult Email(SubscribersVM selectedSubscribers)
        {
            UserServices log = new UserServices();
            SubscribersVM test = log.Checked(selectedSubscribers);
            return View("ViewSubscribers", log.Checked(selectedSubscribers));
        }
    }
}